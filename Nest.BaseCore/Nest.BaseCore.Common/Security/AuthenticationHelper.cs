using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 安全验证（包括生成秘钥、Token验证、签名验证等）
    /// </summary>
    public class AuthenticationHelper
    {
        private const string PrivateSecretKey = "6424cb5a2d99e2128d234f7cf3527c7d";

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="deviceNo"></param>
        /// <param name="ticket"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static string GetToken(string userName, string deviceNo, string ticket, string nonce)
        {
            var str = $"{userName}{deviceNo}{ticket}{nonce}";
            return AESHelper.AESEncrypt(str);
        }

        /// <summary>
        /// 生成票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="clientType"></param>
        /// <param name="deviceNo"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static string GetTicket(string appId, string clientType, string deviceNo, string nonce)
        {
            var str = $"{appId}{clientType}{deviceNo}{nonce}{PrivateSecretKey}";
            return MD5Helper.GetMd5(str);
        }

        /// <summary>
        /// 生成秘钥
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="clientType"></param>
        /// <param name="deviceNo"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static string GetAppSecret(string appId, string clientType, string deviceNo, string nonce)
        {
            var str = $"{appId}{clientType}{deviceNo}{nonce}";
            return AESHelper.AESEncrypt(str, PrivateSecretKey);
        }


        #region 签名及验签

        /// <summary>
        /// 计算签名
        /// </summary>
        /// <param name="dictionary">参数字典</param>
        /// <param name="key">私玥</param>
        /// <returns></returns>
        public static string GetSign(Dictionary<string, object> dictionary, string key)
        {
            SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>(dictionary);
            StringBuilder sb = new StringBuilder();
            foreach (var keyvalue in sortedDictionary)
            {
                if (keyvalue.Value != null && keyvalue.Value.GetType().FullName != "System.String" && (keyvalue.Value.GetType().IsClass || keyvalue.Value.GetType().IsInterface))
                {
                    sb.Append($"{keyvalue.Key}={JsonHelper.SerializeObject(keyvalue.Value)}&");
                }
                else
                {
                    sb.Append($"{keyvalue.Key}={keyvalue.Value}&");
                }
            }
            var str = sb.ToString().TrimEnd('&');
            return GetSign(str, key);
        }

        /// <summary>
        /// 计算签名
        /// </summary>
        /// <param name="dictionary">参数字典</param>
        /// <param name="key">私玥</param>
        /// <param name="parms">输出参数字符串</param>
        /// <returns></returns>
        public static string GetSign(Dictionary<string, object> dictionary, string key, out string parms)
        {
            SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>(dictionary);
            StringBuilder sb = new StringBuilder();
            foreach (var keyvalue in sortedDictionary)
            {
                if (keyvalue.Value != null && keyvalue.Value.GetType().FullName != "System.String" && (keyvalue.Value.GetType().IsClass || keyvalue.Value.GetType().IsInterface))
                {
                    sb.Append($"{keyvalue.Key}={JsonHelper.SerializeObject(keyvalue.Value)}&");
                }
                else
                {
                    sb.Append($"{keyvalue.Key}={keyvalue.Value}&");
                }
            }
            var str = sb.ToString().TrimEnd('&');
            parms = $"{str}&key={key}";
            return GetSign(str, key);
        }

        /// <summary>
        /// 计算签名
        /// </summary>
        /// <param name="parms">参数字符串</param>
        /// <param name="key">私玥</param>
        /// <returns></returns>
        public static string GetSign(string parms, string key)
        {
            string str = $"{parms}&key={key}";
            return MD5Helper.GetMd5(str);
        }

        #endregion

        /// <summary>
        /// 检查时间戳是否有效（2分钟内有效）
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static ApiResultCode CheckTimeStamp(string ts)
        {
            if (string.IsNullOrEmpty(ts))
            {
                return ApiResultCode.NoTimestamp;
            }
            else if (ts.ToInt(0) == 0)
            {
                return ApiResultCode.TimestampFromatError;
            }
            long timestamp = Math.Abs(DateTime.Now.ToTimeSpan() - ts.ToInt());
            if (timestamp > 120 || timestamp < 0)
            {
                return ApiResultCode.SignTimeout;
            }
            return ApiResultCode.Success;
        }
    }
}
