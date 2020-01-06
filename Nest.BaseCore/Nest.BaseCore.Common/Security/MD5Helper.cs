using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// md5加密 辅助类
    /// </summary>
    public class MD5Helper
    {
        /// <summary>
        /// md5加密（返回大写，utf8编码方式）
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns></returns>
        public static string GetMd5(string str)
        {
            using (var md5 = MD5.Create())
            {
                var value = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                var result = BitConverter.ToString(value);
                return result.Replace("-", "");
            }
        }

        /// <summary>
        /// md5加密（返回大写）
        /// </summary>
        /// <param name="stream">要加密的流</param>
        /// <returns></returns>
        public static string GetMd5(Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                var value = md5.ComputeHash(stream);
                var result = BitConverter.ToString(value);
                return result.Replace("-", "");
            }
        }

        /// <summary>
        /// 获取文件MD5摘要
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileAbstract(string fileName)
        {
            using (FileStream file = new FileStream(fileName, FileMode.Open))
            {
                return GetFileAbstract(file);
            }
        }

        /// <summary>
        /// 根据stream获取文件MD5摘要
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetFileAbstract(Stream stream)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(stream);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

    }
}
