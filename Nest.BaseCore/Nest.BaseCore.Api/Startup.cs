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

        //    //ע��Swagger������������һ���Ͷ��Swagger �ĵ�
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new Info
        //        {
        //            Version = "v1",
        //            Title = "Nest.BaseCore.Api",
        //            Description = "Nest",
        //        });
        //        c.CustomSchemaIds(type => type.FullName); // �����ͬ�����ᱨ�������
        //        //swagger�п��������ʱ���Ƿ���Ҫ��url������accesstoken
        //        c.OperationFilter<AddAuthTokenHeaderParameter>();

        //        // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
        //        var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼
        //        var xmlPath = Path.Combine(basePath, "SwaggerDoc.xml");
        //        c.IncludeXmlComments(xmlPath);
        //    });

        //    #endregion

        //    var connection = Configuration.GetConnectionString("MySQL");
        //    services.AddDbContext<MainContext>(options => options.UseMySQL(connection));

        //    //����CAP
        //    services.AddCap(cap =>
        //    {
        //        cap.UseEntityFramework<MainContext>();

        //        ////���ò������
        //        //cap.UseDashboard();

        //        //ʹ��RabbitMQ
        //        cap.UseRabbitMQ(rb =>
        //        {
        //            //rabbitmq����������
        //            rb.HostName = "192.168.1.221";
        //            rb.UserName = "guest";
        //            rb.Password = "guest";
        //        });

        //        //���ô���ɹ������������ݿ��б����ʱ�䣨�룩��Ϊ��֤ϵͳ���ܣ����ݻᶨ������
        //        cap.SucceedMessageExpiredAfter = 24 * 3600;

        //        //����ʧ�����Դ���
        //        cap.FailedRetryCount = 5;
        //    });


        //    services.AddMvc(options =>
        //    {
        //        options.Filters.Add<GlobalExceptionAttribute>();//ͳһ�쳣����
        //    }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        //    //ע��Logger����
        //    services.AddSingleton<IExceptionlessLogger, ExceptionlessLogger>();

        //    //��ʼ��Net4Log
        //    ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
        //    XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//��log4net.config�ļ��ж�ȡ������Ϣ

        //    #region AutoFac ע��ִ���ҵ���߼�����

        //    //����ƥ��ע�룬ʹ��AutoFac�ṩ�������ӹܵ�ǰ��ĿĬ������
        //    var builder = new ContainerBuilder();

        //    Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

        //    builder.RegisterAssemblyTypes(assemblies)
        //           .Where(type => (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsAbstract)
        //           .AsSelf().AsImplementedInterfaces()
        //           .PropertiesAutowired().InstancePerLifetimeScope();
        //    builder.Populate(services);
        //    var container = builder.Build();
        //    //ConfigureServices������void��Ϊ����IServiceProvider
        //    return new AutofacServiceProvider(container);

        //    #endregion

        //}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Swagger

            //ע��Swagger������������һ���Ͷ��Swagger �ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Nest.BaseCore.Api",
                    Description = "Nest",
                });
                c.CustomSchemaIds(type => type.FullName); // �����ͬ�����ᱨ�������
                //swagger�п��������ʱ���Ƿ���Ҫ��url������accesstoken
                c.OperationFilter<AddAuthTokenHeaderParameter>();

                // Ϊ Swagger JSON and UI����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼
                var xmlPath = Path.Combine(basePath, "SwaggerDoc.xml");
                c.IncludeXmlComments(xmlPath);
            });

            #endregion

            var connection = Configuration.GetConnectionString("MySQL");
            services.AddDbContext<MainContext>(options => options.UseMySQL(connection));

            //����CAP
            services.AddCap(cap =>
            {
                cap.UseEntityFramework<MainContext>();

                ////���ò������
                //cap.UseDashboard();

                //ʹ��RabbitMQ
                cap.UseRabbitMQ(rb =>
                {
                    //rabbitmq����������
                    rb.HostName = "192.168.1.221";
                    rb.UserName = "guest";
                    rb.Password = "guest";
                });

                //���ô���ɹ������������ݿ��б����ʱ�䣨�룩��Ϊ��֤ϵͳ���ܣ����ݻᶨ������
                cap.SucceedMessageExpiredAfter = 24 * 3600;

                //����ʧ�����Դ���
                cap.FailedRetryCount = 5;
            });


            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionAttribute>();//ͳһ�쳣����
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //ע��Logger����
            services.AddSingleton<IExceptionlessLogger, ExceptionlessLogger>();

            //��ʼ��Net4Log
            ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//��log4net.config�ļ��ж�ȡ������Ϣ
            
        }

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterAssemblyTypes(typeof(Program).Assembly).
        //        Where(x => x.Name.EndsWith("service", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();
        //    builder.RegisterDynamicProxy();
        //}
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //ҵ���߼������ڳ��������ռ�
            Assembly service = Assembly.Load("Nest.BaseCore.Service");
            //�ӿڲ����ڳ��������ռ�
            Assembly repository = Assembly.Load("Nest.BaseCore.Repository");
            //�Զ�ע��
            builder.RegisterAssemblyTypes(service, repository)
                .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
                .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();
            ////ע��ִ�������IRepository�ӿڵ�Repository��ӳ��
            //builder.RegisterGeneric(typeof(Repository<>))
            //    //InstancePerDependency��Ĭ��ģʽ��ÿ�ε��ã���������ʵ��������ÿ�����󶼴���һ���µĶ���
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


            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������swagger-ui��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nest.BaseCore.Api");
                c.RoutePrefix = string.Empty;
            });

            ////����cap�м��
            //app.UseCap();

            // exceptionless
            app.UseExceptionless(Configuration["Exceptionless:ApiKey"]);
        }

        /// <summary>
        /// ����ͷ��Token����
        /// </summary>
        public class AddAuthTokenHeaderParameter : IOperationFilter
        {
            public void Apply(Operation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null) operation.Parameters = new List<IParameter>();
                var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;

                //���ж��Ƿ�����������,
                var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor != null)
                {
                    var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                    bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                    //�������ķ���,���������tokenֵ
                    if (!isAnonymous)
                    {
                        operation.Parameters.Add(new NonBodyParameter()
                        {
                            Name = "Ticket",
                            In = "header",//query header body path formData
                            Type = "string",
                            Description = "Ʊ�ݣ����ڰ�ȫ��֤",
                            Default = "Ticket",
                            Required = true //�Ƿ��ѡ
                        });
                        operation.Parameters.Add(new NonBodyParameter()
                        {
                            Name = "Token",
                            In = "header",//query header body path formData
                            Type = "string",
                            Description = "��¼�����֤���ơ�û�пɲ�������̨������Ҫ����֤",
                            Default = "Token",
                            Required = true //�Ƿ��ѡ
                        });
                    }
                }
            }
        }
    }
}