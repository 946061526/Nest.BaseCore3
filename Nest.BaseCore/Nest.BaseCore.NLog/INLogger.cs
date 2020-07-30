using System;

namespace Nest.BaseCore.NLogger
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface INLogger
    {
        void WriteLog(LogWriteTargetEnum logWriteTarget, LogLevelEnum logLevel, Exception ex,
         string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);
    }
}
