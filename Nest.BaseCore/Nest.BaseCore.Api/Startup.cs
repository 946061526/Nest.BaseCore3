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
using Nest.BaseCore.Common;
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
            //���ݿ�
            var mySqlConn = Configuration.GetConnectionString("MySQL");
            services.AddDbContext<MainContext>(options => options.UseMySql(mySqlConn));

            //����CAP
            services.AddCap(cap =>
            {
                cap.UseEntityFramework<MainContext>();

                //cap.UseMySql(mySqlConn);
                //cap.UseRabbitMQ("localhost");

                //ʹ��RabbitMQ
                cap.UseRabbitMQ(rb =>
                {
                    //rabbitmq����������                   
                    rb.HostName = AppSettingsHelper.Configuration["MQConfig:Host"];
                    rb.Port = int.Parse(AppSettingsHelper.Configuration["MQConfig:Port"]);
                    rb.UserName = AppSettingsHelper.Configuration["MQConfig:UserName"];
                    rb.Password = AppSettingsHelper.Configuration["MQConfig:Password"];
                });

                //���ô���ɹ������������ݿ��б����ʱ�䣨�룩��Ϊ��֤ϵͳ���ܣ����ݻᶨ������
                cap.SucceedMessageExpiredAfter = 24 * 3600;

                //����ʧ�����Դ���
                cap.FailedRetryCount = 5;
            });

            #region ��־����
            //��ʼ��Net4Log
            ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//��log4net.config�ļ��ж�ȡ������Ϣ

            //ע��Logger����
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
                c.CustomSchemaIds(type => type.FullName); // �����ͬ�����ᱨ�������
                c.OperationFilter<AddAuthTokenHeaderParameter>();//����ͷ�Ӳ���

                // ΪSwagger����xml�ĵ�ע��·��
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼
                var xmlPath = Path.Combine(basePath, "SwaggerDoc.xml");
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

            //ע��HttpContex
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();


            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionAttribute>();//ͳһ�쳣����
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            ////ע���߼������
            //services.AddScoped<IUserService, UserService>()
            //    .AddScoped<IRoleService, RoleService>();

            services.AddControllers();

        }

        /// <summary>
        /// autofacע��
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "�ӿ��ĵ�");
                c.RoutePrefix = string.Empty;
            });

            //����cap�м��
            //app.UseCap();

            // exceptionless
            app.UseExceptionless(Configuration["Exceptionless:ApiKey"]);


            //ע��HttpContex
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
        /// ����ͷ�Ӳ���
        /// </summary>
        protected class AddAuthTokenHeaderParameter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
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
                        operation.Parameters.Add(new OpenApiParameter()
                        {
                            In = ParameterLocation.Header,
                            Name = "ticket",
                            Description = "Ʊ�ݣ����ڰ�ȫ��֤",
                            Required = true, //�Ƿ��ѡ
                            AllowEmptyValue = false,
                        });
                        operation.Parameters.Add(new OpenApiParameter()
                        {
                            In = ParameterLocation.Header,
                            Name = "token",
                            Description = "��¼�����֤���ơ�û�пɲ�������̨������Ҫ����֤",
                            Required = true, //�Ƿ��ѡ
                            AllowEmptyValue = true,
                        });
                    }
                }
            }
        }
    }
}
