using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.NLogger
{
    /// <summary>
    /// 数据库日志实体
    /// </summary>
    public class LogInfoModel
    {
        //Id, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime, LogLevel, LogTitle, LogMessage, LogCreateTime
        public string SourceType { get; set; } = "";
        public string ServiceName { get; set; } = "";
        public string Module { get; set; } = "";
        public string FunctionName { get; set; } = "";
        public string UserAD { get; set; } = "";
        public string InParam { get; set; } = "";
        public string ShortDescription { get; set; } = "";
        public DateTime ExecuteTime { get; set; } = DateTime.Now;
        public string LogLevel { get; set; } = "";
        public string LogTitle { get; set; } = "";
        public string LogMessage { get; set; } = "";
    }
}
