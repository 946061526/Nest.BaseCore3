using Microsoft.AspNetCore.Http;
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

    }
}
