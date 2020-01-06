using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace Nest.BaseCore.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// Post 默认方式提供数据
        /// </summary>
        /// <param name="url">WebApi请求地址</param>
        /// <param name="data">请求数据 如id=5&parent=0</param>
        public static string Post(HttpClient client, string url, string data)
        {
            string result;
            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = client.PostAsync(url, content).Result;
            result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        /// <summary>  
        /// Post方式  
        /// </summary>  
        /// <param name="jsonData">要处理的JSON数据</param>  
        /// <param name="url">要提交的URL</param>  
        /// <returns>返回的JSON处理字符串</returns>  
        public static string Post(HttpClient client, string jsonData, string url, string token = "")
        {
            string strResult = "";

            var content = new StringContent(jsonData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (!string.IsNullOrEmpty(token))//请求头加token
            {
                content.Headers.Add("token", token);
            }
            var response = client.PostAsync(url, content).Result;
            strResult = response.Content.ReadAsStringAsync().Result;
            return strResult;
        }

        /// <summary>  
        /// Post方式  
        /// </summary>  
        /// <param name="data">要处理的对象</param>  
        /// <param name="url">要提交的URL</param>
        /// <returns>返回的对象。当HTTP调用失败时，返回类型T默认构造的对象</returns>  
        public static T Post<T>(HttpClient client, object data, string url) where T : class
        {
            string strResult = "";
            string JSONData = JsonHelper.SerializeObject(data);

            var content = new StringContent(JSONData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(url, content).Result;
            strResult = response.Content.ReadAsStringAsync().Result;
            return JsonHelper.DeserializeObject<T>(strResult);
        }

        /// <summary>  
        /// Get方式
        /// </summary>  
        /// <param name="jsonData">要处理的JSON数据</param>  
        /// <param name="url">要提交的URL</param>  
        /// <returns>返回的JSON处理字符串</returns>  
        public static string Get(HttpClient client, string jsonData, string url)
        {
            string strResult = "";
            if (!string.IsNullOrEmpty(jsonData))
            {
                jsonData = jsonData.IndexOf('?') > -1 ? (jsonData) : ("?" + jsonData);
            }

            client.DefaultRequestHeaders.Connection.Add("Keep-Alive");
            var response = client.GetAsync(url + jsonData).Result;
            if (response.Content.Headers.ContentType.MediaType == MediaTypeNames.Application.Octet
                || response.Content.Headers.ContentType.MediaType.StartsWith("image/"))
            {
                byte[] result = response.Content.ReadAsByteArrayAsync().Result;
                if (result == null)
                {
                    return "";
                }
                strResult = Convert.ToBase64String(result);
            }
            else
            {
                strResult = response.Content.ReadAsStringAsync().Result;
            }
            return strResult;
        }
    }
}