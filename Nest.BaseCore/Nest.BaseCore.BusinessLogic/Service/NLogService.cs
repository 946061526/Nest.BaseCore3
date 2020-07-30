using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.NLogger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.BusinessLogic.Service
{
    /// <summary>
    /// 日志逻辑 实现
    /// </summary>
    public class NLogService : INLogService
    {
        private readonly INLogger _nLogger;
        public NLogService(INLogger nLogger)
        {
            _nLogger = nLogger;
        }

        #region Debug级别

        /// <summary>
        /// Debug信息写入数据库
        /// </summary>
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
        public void DebugToDB(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.Database, LogLevelEnum.Debug, null,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        /// <summary>
        /// Debug信息写入数据库 和文件
        /// </summary>
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
        public void DebugToDbAndFile(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.DatabaseAndFile, LogLevelEnum.Debug, null,
              LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        /// <summary>
        /// Debug信息写入文件
        /// </summary>
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
        public void DebugToFile(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.File, LogLevelEnum.Debug, null,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        #endregion

        #region Info级别

        /// <summary>
        /// Info信息写入数据库
        /// </summary>
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
        public void InfoToDB(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.Database, LogLevelEnum.Info, null,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        /// <summary>
        /// Info信息写入数据库 和文件
        /// </summary>
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
        public void InfoToDbAndFile(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.DatabaseAndFile, LogLevelEnum.Info, null,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        /// <summary>
        /// Info信息写入文件
        /// </summary>
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
        public void InfoToFile(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.File, LogLevelEnum.Info, null,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        #endregion

        #region Error级别

        /// <summary>
        /// Error信息写入数据库
        /// </summary>
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
        public void ErrorToDB(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.Database, LogLevelEnum.Error, ex,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        /// <summary>
        /// Error信息写入数据库 和文件
        /// </summary>
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
        public void ErrorToDbAndFile(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.DatabaseAndFile, LogLevelEnum.Error, ex,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        /// <summary>
        /// Error信息写入文件
        /// </summary>
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
        public void ErrorToFile(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.File, LogLevelEnum.Error, ex,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        #endregion

        #region Fatal级别

        /// <summary>
        /// Fatal信息写入数据库
        /// </summary>
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
        public void FatalToDB(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.Database, LogLevelEnum.Fatal, ex,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        /// <summary>
        /// Error信息写入数据库 和文件
        /// </summary>
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
        public void FatalToDbAndFile(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.DatabaseAndFile, LogLevelEnum.Fatal, ex,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        /// <summary>
        /// Error信息写入文件
        /// </summary>
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
        public void FatalToFile(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null)
        {
            _nLogger.WriteLog(LogWriteTargetEnum.File, LogLevelEnum.Fatal, ex,
                LogTitle, LogMessage, SourceType, ServiceName, Module, FunctionName, UserAD, InParam, ShortDescription, ExecuteTime);
        }
        #endregion

    }
}
