using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// Guid工具类
    /// </summary>
    public class GuidTool
    {
        /// <summary>
        /// 生成Guid： Guid.NewGuid().ToString("N")
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
