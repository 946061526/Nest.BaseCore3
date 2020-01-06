
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Nest.BaseCore.Common
{
    ///// <summary>
    ///// 配置文件操作类
    ///// </summary>
    //public static class AppSettingsHelper
    //{
    //    private static IConfigurationSection _appSection = null;

    //    /// <summary>
    //    /// 读取配置节点
    //    /// </summary>
    //    /// <param name="key"></param>
    //    /// <returns></returns>
    //    //public static string AppSetting(string key)
    //    //{
    //    //    string str = string.Empty;
    //    //    if (_appSection.GetSection(key) != null)
    //    //    {
    //    //        str = _appSection.GetSection(key).Value;
    //    //    }
    //    //    return str;
    //    //}

    //    /// <summary>
    //    /// 绑定要设置的配置内容
    //    /// </summary>
    //    /// <param name="section"></param>
    //    public static void SetAppSetting(IConfigurationSection section)
    //    {
    //        _appSection = section;
    //    }

    //    /// <summary>
    //    /// 读取节点的值
    //    /// </summary>
    //    /// <param name="key">节点名称</param>
    //    /// <returns></returns>
    //    public static string GetValue(string key)
    //    {
    //        //return AppSetting(nodeName);
    //        string str = string.Empty;
    //        if (_appSection.GetSection(key) != null)
    //        {
    //            str = _appSection.GetSection(key).Value;
    //        }
    //        return str;
    //    }
    //}

    /// <summary>
    /// 读取配置文件帮助类
    /// </summary>
    public class AppSettingsHelper
    {
        public static IConfiguration Configuration { get; set; }
        static AppSettingsHelper()
        {
            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }

        /// <summary>
        /// 根据键获取配置对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetAppSettings<T>(string key) where T : class, new()
        {
            var appconfig = new ServiceCollection()
             .AddOptions()
             .Configure<T>(Configuration.GetSection(key))
             .BuildServiceProvider()
             .GetService<IOptions<T>>()
             .Value;
            return appconfig;
        }
        public static string GetKeyValue(string key)
        {
            return Configuration.GetSection(key).Value;
        }
    }
}
