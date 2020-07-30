using System.ComponentModel;

namespace Nest.BaseCore.NLogger
{
    class LogEnum
    {
    }

    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevelEnum
    {
        /// <summary>
        /// 一般调试信息
        /// </summary>
        [Description("调试")]
        Debug,
        /// <summary>
        /// 一般信息
        /// </summary>
        [Description("一般信息")]
        Info,
        /// <summary>
        /// 错误或异常
        /// </summary>
        [Description("错误或异常")]
        Error,
        /// <summary>
        /// 严重，可能导致程序崩溃
        /// </summary>
        [Description("严重")]
        Fatal
    }

    /// <summary>
    /// 日志写入目标位置
    /// </summary>
    public enum LogWriteTargetEnum
    {
        /// <summary>
        /// 写入文件
        /// </summary>
        [Description("文件")]
        File,
        /// <summary>
        /// 写入数据库
        /// </summary>
        [Description("数据库")]
        Database,
        /// <summary>
        /// 同时写入数据库和文件
        /// </summary>
        [Description("数据库和文件")]
        DatabaseAndFile
    }
}
