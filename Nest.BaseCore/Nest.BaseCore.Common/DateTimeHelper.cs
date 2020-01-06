using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 时间处理 辅助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 时间格式化，默认"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="time"></param>
        /// <param name="format">格式</param>
        /// <returns></returns>
        public static string ToFormatStr(this DateTime time, string format = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                return time.ToString(format);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 取两个时间相差天数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float GetDiffDays(DateTime start, DateTime end)
        {
            try
            {
                TimeSpan timeSpan = end.Subtract(start);
                float differ = timeSpan.Days + (float)timeSpan.Hours / 24 + (float)timeSpan.Minutes / 60 / 24;

                return differ;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 获取当前时间是0上学1下学
        /// </summary>
        /// <returns></returns>
        public static int GetCarPathDirection(DateTime dateTime)
        {
            if (dateTime.Hour>12)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

}
