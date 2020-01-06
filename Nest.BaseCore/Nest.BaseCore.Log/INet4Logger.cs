using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Log
{
    /// <summary>
    /// log4net
    /// </summary>
    public interface INet4Logger
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="ex">异常</param>
        void Debug(string source, string message, Exception ex = null);

        /// <summary>
        /// 错误、异常信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="ex">异常</param>
        void Error(string source, string message, Exception ex = null);
        /// <summary>
        /// 正常信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="ex">异常</param>
        void Info(string source, string message, Exception ex = null);
    }
}
