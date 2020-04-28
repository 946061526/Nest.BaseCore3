using Microsoft.AspNetCore.Http;
using Nest.BaseCore.Common.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 随机字符串
        /// </summary>
        public static string GetNonce()
        {
            return MD5Helper.GetMd5(new Random().Next(1000).ToString()).ToLower().Replace("s", "S");
        }

        /// <summary>
        /// 时间截，自1970年以来的秒数
        /// </summary>
        public static string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 生成随机数字编码串（可用于短信码等）
        /// </summary>
        /// <returns></returns>
        public static string GetRamdonCode(int length = 6)
        {
            if (length > 21)
            {
                length = 21;
            }
            StringBuilder maxNumber = new StringBuilder();
            StringBuilder minNumber = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                maxNumber.Append("9");
                if (i == 0)
                {
                    minNumber.Append("1");
                }
                else
                {
                    minNumber.Append("0");
                }
            }
            Random random = new Random(DateTime.Now.Second);
            return random.Next(minNumber.ToString().ToInt(), maxNumber.ToString().ToInt()).ToString();
        }

        /// <summary>
        /// 获取时间差天数
        /// </summary>
        /// <param name="sTime">开始时间</param>
        /// <param name="eTime">结束时间</param>
        /// <returns></returns>
        public static int GetTimeSpanDays(DateTime sTime, DateTime eTime)
        {
            TimeSpan ts = eTime.Subtract(sTime);
            return (int)ts.Days;
        }

        /// <summary>
        /// 获取两个经纬度之间的距离（单位：米）
        /// </summary>
        /// <param name="longitude1">经度1</param>
        /// <param name="latitude1">纬度1</param>
        /// <param name="longitude2">经度2</param>
        /// <param name="latitude2">纬度2</param>
        /// <returns>两经纬度之间距离（单位：米）</returns>
        public static double GetDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            double dd = HaversineHelper.Distance(longitude1, latitude1, longitude2, latitude2);
            dd = Math.Round(dd, 2);
            return dd;
        }
        /// <summary>
        /// 获取两个经纬度之间的距离（单位：米）
        /// </summary>
        /// <param name="longitude1">经度1</param>
        /// <param name="latitude1">纬度1</param>
        /// <param name="longitude2">经度2</param>
        /// <param name="latitude2">纬度2</param>
        /// <returns>两经纬度之间距离（单位：米）</returns>
        public static double GetDistance(string longitude1, string latitude1, string longitude2, string latitude2)
        {
            return GetDistance(longitude1.ToDouble(), latitude1.ToDouble(), longitude2.ToDouble(), latitude2.ToDouble());
        }

        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return System.Web.HttpUtility.UrlEncode(str); //HttpUtility.UrlEncode("text", System.Text.Encoding.GetEncoding("gb2312"))
        }

        /// <summary>
        /// URL字符解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return System.Web.HttpUtility.UrlEncode(str);
        }
    }
}
