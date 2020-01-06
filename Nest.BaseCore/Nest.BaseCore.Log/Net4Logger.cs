using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;
using log4net.Config;

namespace Nest.BaseCore.Log
{
    /// <summary>
    /// log4net
    /// </summary>
    public class Net4Logger
    {
        private static readonly string _repositoryName = "Net4LoggerRepository";

        //private static log4net.Repository.ILoggerRepository _repository;
        private static ILog _logErr;
        private static ILog _logDebug;
        private static ILog _logInfo;

        static Net4Logger()
        {
            //if (_repository == null)
            //{
            //    _repository = LogManager.CreateRepository(_repositoryName);

            //    //log4net从log4net.config文件中读取配置信息
            //    XmlConfigurator.Configure(_repository, new FileInfo("log4net.config"));
            //}
            if (_logDebug == null)
            {
                //_logDebug = LogManager.GetLogger(_repository.Name, "DebugLogger"); 
                _logDebug = LogManager.GetLogger(_repositoryName, "DebugLogger");
            }
            if (_logErr == null)
            {
                _logErr = LogManager.GetLogger(_repositoryName, "ErrorLogger");
            }
            if (_logInfo == null)
            {
                _logInfo = LogManager.GetLogger(_repositoryName, "InfoLogger");
            }
        }


        /// <summary>
        /// 错误、异常信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="ex">异常</param>
        public static void Error(string source, string message, Exception ex = null)
        {
            if (ex == null)
            {
                _logErr.Error($"{source}。   {message}");
            }
            else
            {
                _logErr.Error($"{source}。   {message}", ex);
            }
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="ex">异常</param>
        public static void Debug(string source, string message, Exception ex = null)
        {
            if (ex == null)
            {
                _logDebug.Error($"{source}。   {message}");
            }
            else
            {
                _logDebug.Error($"{source}。   {message}", ex);
            }
        }

        /// <summary>
        /// 正常信息
        /// </summary>
        /// <param name="source">来源</param>
        /// <param name="message">内容</param>
        /// <param name="ex">异常</param>
        public static void Info(string source, string message, Exception ex = null)
        {
            if (ex == null)
            {
                _logInfo.Error($"{source}。   {message}");
            }
            else
            {
                _logInfo.Error($"{source}。   {message}", ex);
            }
        }
    }
}
