using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Log
{
    /// <summary>
    /// Exceptionless
    /// </summary>
    public interface IExceptionlessLogger
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="tags">标签</param>
        void Debug(string source, string message, params string[] tags);

        /// <summary>
        /// 错误、异常信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="tags">标签</param>
        void Error(string source, string message, params string[] tags);
        
        /// <summary>
        /// 正常信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="tags">标签</param>
        void Info(string source, string message, params string[] tags);
    }
}
