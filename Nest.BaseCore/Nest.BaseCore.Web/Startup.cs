using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Exceptionless;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest.BaseCore.Aop;
using Nest.BaseCore.Domain;
using Nest.BaseCore.Log;

namespace Nest.BaseCore.Web
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
            var mySqlConn = Configuration.GetConnectionString("MySQL");
            services.AddDbContext<MainContext>(options => options.UseMySql(mySqlConn));

            #region 日志服务
            //初始化Net4Log
            ILoggerRepository repository = LogManager.CreateRepository("Net4LoggerRepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));//从log4net.config文件中读取配置信息

            //注入Logger服务
            services.AddSingleton<IExceptionlessLogger, ExceptionlessLogger>();

            #endregion

            //注入HttpContex
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();


            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionAttribute>();//统一异常处理
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews();
        }

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
            // exceptionless
            app.UseExceptionless(Configuration["Exceptionless:ApiKey"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
