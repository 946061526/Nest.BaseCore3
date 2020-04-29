using Autofac;
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
using Nest.BaseCore.Domain;
using Nest.BaseCore.Log;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using IOperationFilter = Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Builder;
using Exceptionless;

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


            var mySqlConn = Configuration.GetConnectionString("MySQL");
            services.AddDbContext<MainContext>(options => options.UseMySQL(mySqlConn));

            //配置CAP
            services.AddCap(cap =>
            {
                //cap.UseEntityFramework<MainContext>();

                cap.UseMySql(mySqlConn);

                //使用RabbitMQ
                cap.UseRabbitMQ(rb =>
                {
                    //rabbitmq服务器配置
                    rb.HostName = "192.168.1.221";
                    rb.UserName = "guest";
                    rb.Password = "guest";
                });

                //设置处理成功的数据在数据库中保存的时间（秒），为保证系统新能，数据会定期清理。
                cap.SucceedMessageExpiredAfter = 24 * 3600;

                //设置失败重试次数
                cap.FailedRetryCount = 5;
            });


            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionAttribute>();//统一异常处理
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            #region 日志服务
            //初始化Net4Log
            ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//从log4net.config文件中读取配置信息

            //注入Logger服务
            services.AddSingleton<IExceptionlessLogger, ExceptionlessLogger>();

            #endregion

            ////注入逻辑层服务
            //services.AddScoped<IUserService, UserService>()
            //    .AddScoped<IRoleService, RoleService>();

            services.AddControllers();

        }

        /// <summary>
        /// 依赖注入
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "接口文档");
                c.RoutePrefix = string.Empty;
            });

            //启用cap中间件
            //app.UseCap();

            // exceptionless
            app.UseExceptionless(Configuration["Exceptionless:ApiKey"]);

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
