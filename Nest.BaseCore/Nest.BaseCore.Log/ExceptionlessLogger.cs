using Exceptionless;
using Exceptionless.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Log
{
    /// <summary>
    /// Exceptionless
    /// </summary>
    public class ExceptionlessLogger : IExceptionlessLogger
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="args">参数</param>
        public void Debug(string source, string message, params string[] args)
        {
            if (args.Length > 0)
                ExceptionlessClient.Default.CreateLog(message, LogLevel.Debug).AddTags(args).Submit();
            else
                ExceptionlessClient.Default.SubmitLog(message, LogLevel.Debug);
        }

        /// <summary>
        /// 错误、异常信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="args">参数</param>
        public void Error(string source, string message, params string[] args)
        {
            if (args.Length > 0)
                ExceptionlessClient.Default.CreateLog(message, LogLevel.Error).AddTags(args).Submit();
            else
                ExceptionlessClient.Default.SubmitLog(message, LogLevel.Error);
        }

        /// <summary>
        /// 正常信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="args">参数</param>
        public void Info(string source, string message, params string[] args)
        {
            if (args.Length > 0)
                ExceptionlessClient.Default.CreateLog(message, LogLevel.Info).AddTags(args).Submit();
            else
                ExceptionlessClient.Default.SubmitLog(message, LogLevel.Info);
        }
    }
}
