using NLog;
using System;

namespace Nest.BaseCore.NLogger
{
    /// <summary>
    /// 日志实现类
    /// </summary>
    public class NLogger : INLogger
    {
        public static Logger LoggerDB = null;
        public static Logger LoggerFile = null;
        public static Logger LoggerDbAndFile = null;

        public NLogger()
        {
            LoggerDB = LogManager.GetLogger("log_db");//写数据库
            LoggerFile = LogManager.GetLogger("log_file");//写文件
            LoggerDbAndFile = LogManager.GetLogger("log_db_file");//写数据库和文件
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logWriteTarget">写入目标位置（文件、数据库）</param>
        /// <param name="logLevel">日志级别</param>
        /// <param name="ex">异常</param>
        /// <param name="LogTitle">日志标题</param>
        /// <param name="LogMessage">日志内容</param>
        /// <param name="SourceType">来源类型</param>
        /// <param name="ServiceName">服务名称</param>
        /// <param name="Module">模块</param>
        /// <param name="FunctionName">函数</param>
        /// <param name="UserAD">用户标识</param>
        /// <param name="InParam">输入参数</param>
        /// <param name="ShortDescription">描述</param>
        /// <param name="ExecuteTime">执行时间</param>
        public void WriteLog(LogWriteTargetEnum logWriteTarget, LogLevelEnum logLevel, Exception ex,
           string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            LogLevel level = LogLevel.Info;
            if (logLevel == LogLevelEnum.Debug)
            {
                level = LogLevel.Debug;
            }
            else if (logLevel == LogLevelEnum.Error)
            {
                level = LogLevel.Error;
            }
            else if (logLevel == LogLevelEnum.Fatal)
            {
                level = LogLevel.Fatal;
            }
            try
            {
                LogEventInfo logEventInfo = new LogEventInfo(level, LogTitle, LogMessage);
                if (logWriteTarget == LogWriteTargetEnum.Database)
                {
                    logEventInfo.Properties["SourceType"] = SourceType;
                    logEventInfo.Properties["ServiceName"] = ServiceName;
                    logEventInfo.Properties["Module"] = Module;
                    logEventInfo.Properties["FunctionName"] = FunctionName;
                    logEventInfo.Properties["UserAD"] = UserAD;
                    logEventInfo.Properties["InParam"] = InParam;
                    logEventInfo.Properties["ShortDescription"] = ShortDescription;
                    logEventInfo.Properties["ExecuteTime"] = ExecuteTime == null ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : ExecuteTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    logEventInfo.Properties["LogLevel"] = level.ToString();
                    logEventInfo.Properties["LogTitle"] = LogTitle;
                    logEventInfo.Properties["LogMessage"] = LogMessage;
                    logEventInfo.Properties["LogCreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    if (ex != null)
                        logEventInfo.Exception = ex;

                    LoggerDB.Log(logEventInfo);
                }
                else if (logWriteTarget == LogWriteTargetEnum.File)
                {
                    if (ex != null)
                        logEventInfo.Exception = ex;

                    LoggerFile.Log(logEventInfo);
                }
                else if (logWriteTarget == LogWriteTargetEnum.DatabaseAndFile)
                {
                    logEventInfo.Properties["SourceType"] = SourceType;
                    logEventInfo.Properties["ServiceName"] = ServiceName;
                    logEventInfo.Properties["Module"] = Module;
                    logEventInfo.Properties["FunctionName"] = FunctionName;
                    logEventInfo.Properties["UserAD"] = UserAD;
                    logEventInfo.Properties["InParam"] = InParam;
                    logEventInfo.Properties["ShortDescription"] = ShortDescription;
                    logEventInfo.Properties["ExecuteTime"] = ExecuteTime == null ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : ExecuteTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    logEventInfo.Properties["LogLevel"] = level.ToString();
                    logEventInfo.Properties["LogTitle"] = LogTitle;
                    logEventInfo.Properties["LogMessage"] = LogMessage;
                    logEventInfo.Properties["LogCreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    if (ex != null)
                        logEventInfo.Exception = ex;

                    LoggerDbAndFile.Log(logEventInfo);
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }


        }

    }
}
