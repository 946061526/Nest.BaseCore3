using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.BusinessLogic.IService
{
    /// <summary>
    /// 日志逻辑
    /// </summary>
    public interface INLogService
    {
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
        void DebugToDB(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);
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
        void DebugToFile(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);
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
        void DebugToDbAndFile(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);


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
        void InfoToDB(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);
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
        void InfoToFile(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);
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
        void InfoToDbAndFile(string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);


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
        void ErrorToDB(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);
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
        void ErrorToFile(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);
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
        void ErrorToDbAndFile(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);


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
        void FatalToDB(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);
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
        void FatalToFile(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);
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
        void FatalToDbAndFile(Exception ex, string LogTitle, string LogMessage, string SourceType = "", string ServiceName = "", string Module = "", string FunctionName = "", string UserAD = "", string InParam = "", string ShortDescription = "", DateTime? ExecuteTime = null);

    }
}
