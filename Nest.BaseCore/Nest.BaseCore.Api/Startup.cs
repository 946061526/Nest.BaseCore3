using Autofac;
using Exceptionless;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nest.BaseCore.Aop;
using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.BusinessLogic.Service;
using Nest.BaseCore.Common;
using Nest.BaseCore.Domain;
using Nest.BaseCore.ElasticSearch;
using Nest.BaseCore.Log;
using Nest.BaseCore.NLogger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http;
using IOperationFilter = Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter;

namespace Nest.BaseCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //数据库
            //var mySqlConn = Configuration.GetConnectionString("MySQL");
            //services.AddDbContext<MainContext>(options => options.UseMySql(mySqlConn));

            //配置CAP
            services.AddCap(cap =>
            {
                //cap.UseEntityFramework<MainContext>();
                cap.UseSqlServer(Configuration.GetConnectionString("Default"));

                //cap.UseMySql(mySqlConn);
                //cap.UseRabbitMQ("localhost");

                //使用RabbitMQ
                cap.UseRabbitMQ(rb =>
                {
                    //rabbitmq服务器配置                   
                    rb.HostName = Configuration["MQConfig:Host"];
                    rb.Port = int.Parse(Configuration["MQConfig:Port"]);
                    rb.UserName = Configuration["MQConfig:UserName"];
                    rb.Password = Configuration["MQConfig:Password"];
                });

                //设置处理成功的数据在数据库中保存的时间（秒），为保证系统新能，数据会定期清理。
                cap.SucceedMessageExpiredAfter = 24 * 3600;

                //设置失败重试次数
                cap.FailedRetryCount = 5;

                //cap.FailedThresholdCallback = FailCallBack;
            });

            #region 日志服务
            //初始化Net4Log
            ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//从log4net.config文件中读取配置信息

            //注入Logger服务
            services.AddSingleton<IExceptionlessLogger, ExceptionlessLogger>();

            #endregion

            #region Swagger

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Nest.BaseCore.Api",
                    Description = "A simple example ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Karroy",
                        Email = "94606152@qq.com",
                    },
                });
                c.CustomSchemaIds(type => type.FullName); // 解决相同类名会报错的问题
                c.OperationFilter<AddAuthTokenHeaderParameter>();//请求头加参数

                // 为Swagger设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录
                var xmlPath = Path.Combine(basePath, "SwaggerDoc.xml");
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

            //注入HttpContex
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();


            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionAttribute>();//统一异常处理
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            ////注入逻辑层服务
            //services.AddScoped<IUserService, UserService>()
            //    .AddScoped<IRoleService, RoleService>();

            services.AddSingleton<Nest.BaseCore.NLogger.INLogger, Nest.BaseCore.NLogger.NLogger>()
                .AddScoped<INLogService, NLogService>();

            //Dapper相关注入
            #region Dapper相关注入

            //services.Configure<DapperDbOption>(Configuration.GetSection("DapperDbOpion"));//数据库连接配置
            //services.AddScoped<IUnitOfWork, UnitOfWork>();//工作单元注入
            //services.AddScoped<ILogInfoRepository, TCT.Net.DDD.RepositoryDapper.Repository.LogInfoRepository>();//对应仓储注入
            #endregion

            #region ElasticSearch

            services.AddSingleton<IEsClientProvider, EsClientProvider>();

            var typesEs = Assembly.Load("Nest.BaseCore.RepositoryEs").GetTypes()
                .Where(p => !p.IsAbstract && (p.GetInterfaces().Any(i => i == typeof(IBaseEsContext))))
                .ToList();
            typesEs.ForEach(p => services.AddTransient(p));

            services.AddSingleton<Nest.BaseCore.BusinessLogic.Es.IDocService, Nest.BaseCore.BusinessLogic.Es.DocService>();
            #endregion

            services.AddControllers();

        }

        ////失败时的回调通知函数
        //public void FailCallBack(DotNetCore.CAP..MessageType messageType, string messageName, string messageContent)
        //{
        //    Console.WriteLine($"失败回调:messageType:{messageType};messageName:{messageName};
        //        messageContent:{ messageContent}
        //    ");
        // }

        /// <summary>
        /// autofac注入
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsAbstract)
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Nlog记日志

            NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
            NLog.LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("Default");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  //避免日志中的中文输出乱码
            #endregion


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "接口文档");
                c.RoutePrefix = string.Empty;
            });

            // exceptionless
            app.UseExceptionless(Configuration["Exceptionless:ApiKey"]);


            //注入HttpContex
            //Nest.BaseCore.Common.HttpContext.Configure(app.ApplicationServices.GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


        /// <summary>
        /// 请求头加参数
        /// </summary>
        protected class AddAuthTokenHeaderParameter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
                var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;

                //先判断是否是匿名访问,
                var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor != null)
                {
                    var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                    bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                    //非匿名的方法,链接中添加token值
                    if (!isAnonymous)
                    {
                        operation.Parameters.Add(new OpenApiParameter()
                        {
                            In = ParameterLocation.Header,
                            Name = "ticket",
                            Description = "票据，用于安全认证",
                            Required = true, //是否必选
                            AllowEmptyValue = false,
                        });
                        operation.Parameters.Add(new OpenApiParameter()
                        {
                            In = ParameterLocation.Header,
                            Name = "token",
                            Description = "登录身份验证令牌。没有可不传，后台根据需要做验证",
                            Required = true, //是否必选
                            AllowEmptyValue = true,
                        });
                    }
                }
            }
        }
    }
}
