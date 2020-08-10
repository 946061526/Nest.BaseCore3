using Elasticsearch.Net;
using Nest.BaseCore.Common;
using System;
using System.Linq;

namespace Nest.BaseCore.ElasticSearch
{
    /// <summary>
    /// ElasticClient提供者
    /// </summary>
    public class EsClientProvider : IEsClientProvider
    {
        /// <summary>
        /// 获取elastic client
        /// </summary>
        /// <returns></returns>
        public ElasticClient GetClient()
        {
            var url = AppSettingsHelper.Configuration["EsConfig:ConnectionStrings"].ToString();
            if (string.IsNullOrEmpty(url))
                throw new Exception("ES连接配置不能为空");

            return GetClient(url, "");
        }
        /// <summary>
        /// 指定index获取ElasticClient
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public ElasticClient GetClient(string indexName)
        {
            var url = AppSettingsHelper.Configuration["EsConfig:ConnectionStrings"].ToString();
            if (string.IsNullOrEmpty(url))
                throw new Exception("ES连接配置不能为空");

            return GetClient(url, indexName);
        }


        /// <summary>
        /// 根据url获取ElasticClient
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaultIndex"></param>
        /// <returns></returns>
        private ElasticClient GetClient(string url, string defaultIndex = "")
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception("ES连接配置不能为空");
            }
            var uri = new Uri(url);
            var connectionSetting = new ConnectionSettings(uri);
            if (!string.IsNullOrWhiteSpace(url))
            {
                connectionSetting.DefaultIndex(defaultIndex);
            }
            return new ElasticClient(connectionSetting);
        }
        /// <summary>
        /// 根据urls获取ElasticClient
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="defaultIndex"></param>
        /// <returns></returns>
        private ElasticClient GetClient(string[] urls, string defaultIndex = "")
        {
            if (urls == null || urls.Length < 1)
            {
                throw new Exception("ES连接配置不能为空");
            }
            var uris = urls.Select(p => new Uri(p)).ToArray();
            var connectionPool = new SniffingConnectionPool(uris);
            var connectionSetting = new ConnectionSettings(connectionPool);
            if (!string.IsNullOrWhiteSpace(defaultIndex))
            {
                connectionSetting.DefaultIndex(defaultIndex);
            }
            return new ElasticClient(connectionSetting);
        }
    }
}
