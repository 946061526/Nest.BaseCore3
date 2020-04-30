using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nest.BaseCore.Domain;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UnitTestProject1
{
    public class AutofacConfig
    {
        protected static IContainer Container
        {
            get;
            set;
        }

        public static IServiceProvider Register(IServiceCollection services)
        {
            var connection = "server=127.0.0.1;port=3306;database=db1;uid=root;pwd=123456;CharSet=utf8;TreatTinyAsBoolean=true";
            services.AddDbContext<MainContext>(options => options.UseMySql(connection));

            #region AutoFac 注入仓储、业务逻辑服务

            //批量匹配注入，使用AutoFac提供的容器接管当前项目默认容器
            var builder = new ContainerBuilder();


            Assembly[] assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Select(Assembly.LoadFrom).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository")) && !type.IsAbstract)
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();
            builder.Populate(services);
            Container = builder.Build();
            //ConfigureServices方法由void改为返回IServiceProvider
            return new AutofacServiceProvider(Container);

            #endregion
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

    }
}
