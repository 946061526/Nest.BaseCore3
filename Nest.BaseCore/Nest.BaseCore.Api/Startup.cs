using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Exceptionless;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest.BaseCore.Aop;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Log;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nest.BaseCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllers();

        //    #region Swagger

        //    //注册Swagger生成器，定义一个和多个Swagger 文档
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new Info
        //        {
        //            Version = "v1",
        //            Title = "Nest.BaseCore.Api",
        //            Description = "Nest",
        //        });
        //        c.CustomSchemaIds(type => type.FullName); // 解决相同类名会报错的问题
        //        //swagger中控制请求的时候发是否需要在url中增加accesstoken
        //        c.OperationFilter<AddAuthTokenHeaderParameter>();

        //        // 为 Swagger JSON and UI设置xml文档注释路径
        //        var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录
        //        var xmlPath = Path.Combine(basePath, "SwaggerDoc.xml");
        //        c.IncludeXmlComments(xmlPath);
        //    });

        //    #endregion

        //    var connection = Configuration.GetConnectionString("MySQL");
        //    services.AddDbContext<MainContext>(options => options.UseMySQL(connection));

        //    //配置CAP
        //    services.AddCap(cap =>
        //    {
        //        cap.UseEntityFramework<MainContext>();

        //        ////启用操作面板
        //        //cap.UseDashboard();

        //        //使用RabbitMQ
        //        cap.UseRabbitMQ(rb =>
        //        {
        //            //rabbitmq服务器配置
        //            rb.HostName = "192.168.1.221";
        //            rb.UserName = "guest";
        //            rb.Password = "guest";
        //        });

        //        //设置处理成功的数据在数据库中保存的时间（秒），为保证系统新能，数据会定期清理。
        //        cap.SucceedMessageExpiredAfter = 24 * 3600;

        //        //设置失败重试次数
        //        cap.FailedRetryCount = 5;
        //    });


        //    services.AddMvc(options =>
        //    {
        //        options.Filters.Add<GlobalExceptionAttribute>();//统一异常处理
        //    }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        //    //注入Logger服务
        //    services.AddSingleton<IExceptionlessLogger, ExceptionlessLogger>();

        //    //初始化Net4Log
        //    ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
        //    XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//从log4net.config文件中读取配置信息

        //    #region AutoFac 注入仓储、业务逻辑服务

        //    //批量匹配注入，使用AutoFac提供的容器接管当前项目默认容器
        //    var builder = new ContainerBuilder();

        //    Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

        //    builder.RegisterAssemblyTypes(assemblies)
        //           .Where(type => (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsAbstract)
        //           .AsSelf().AsImplementedInterfaces()
        //           .PropertiesAutowired().InstancePerLifetimeScope();
        //    builder.Populate(services);
        //    var container = builder.Build();
        //    //ConfigureServices方法由void改为返回IServiceProvider
        //    return new AutofacServiceProvider(container);

        //    #endregion

        //}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Swagger

            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Nest.BaseCore.Api",
                    Description = "Nest",
                });
                c.CustomSchemaIds(type => type.FullName); // 解决相同类名会报错的问题
                //swagger中控制请求的时候发是否需要在url中增加accesstoken
                c.OperationFilter<AddAuthTokenHeaderParameter>();

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录
                var xmlPath = Path.Combine(basePath, "SwaggerDoc.xml");
                c.IncludeXmlComments(xmlPath);
            });

            #endregion

            var connection = Configuration.GetConnectionString("MySQL");
            services.AddDbContext<MainContext>(options => options.UseMySQL(connection));

            //配置CAP
            services.AddCap(cap =>
            {
                cap.UseEntityFramework<MainContext>();

                ////启用操作面板
                //cap.UseDashboard();

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

            //注入Logger服务
            services.AddSingleton<IExceptionlessLogger, ExceptionlessLogger>();

            //初始化Net4Log
            ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//从log4net.config文件中读取配置信息
            
        }

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterAssemblyTypes(typeof(Program).Assembly).
        //        Where(x => x.Name.EndsWith("service", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();
        //    builder.RegisterDynamicProxy();
        //}
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //业务逻辑层所在程序集命名空间
            Assembly service = Assembly.Load("Nest.BaseCore.Service");
            //接口层所在程序集命名空间
            Assembly repository = Assembly.Load("Nest.BaseCore.Repository");
            //自动注入
            builder.RegisterAssemblyTypes(service, repository)
                .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
                .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();
            ////注册仓储，所有IRepository接口到Repository的映射
            //builder.RegisterGeneric(typeof(Repository<>))
            //    //InstancePerDependency：默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象；
            //    .As(typeof(IRepository<>)).InstancePerDependency();

            builder.RegisterDynamicProxy();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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


            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nest.BaseCore.Api");
                c.RoutePrefix = string.Empty;
            });

            ////启用cap中间件
            //app.UseCap();

            // exceptionless
            app.UseExceptionless(Configuration["Exceptionless:ApiKey"]);
        }

        /// <summary>
        /// 请求头加Token参数
        /// </summary>
        public class AddAuthTokenHeaderParameter : IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null) operation.Parameters = new List<IParameter>();
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
                        operation.Parameters.Add(new NonBodyParameter()
                        {
                            Name = "Ticket",
                            In = "header",//query header body path formData
                            Type = "string",
                            Description = "票据，用于安全认证",
                            Default = "Ticket",
                            Required = true //是否必选
                        });
                        operation.Parameters.Add(new NonBodyParameter()
                        {
                            Name = "Token",
                            In = "header",//query header body path formData
                            Type = "string",
                            Description = "登录身份验证令牌。没有可不传，后台根据需要做验证",
                            Default = "Token",
                            Required = true //是否必选
                        });
                    }
                }
            }
        }
    }
}