using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nest.BaseCore.Domain.Entity
{
    /// <summary>
    /// 日志实体
    /// </summary>
    public class Log_Info
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 日志等级
        /// </summary>
        public int Level { get; set; } = 0;
        /// <summary>
        /// 来源类型
        /// </summary>
        public string SourceType { get; set; } = "";
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServerName { get; set; } = "";
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Module { get; set; } = "";
        /// <summary>
        /// 函数
        /// </summary>
        public string FunctionName { get; set; } = "";
        /// <summary>
        /// 用户标识
        /// </summary>
        public string User_AD { get; set; } = "";
        /// <summary>
        /// 输入参数
        /// </summary>
        public string InParam { get; set; } = "";
        /// <summary>
        /// 内容描述
        /// </summary>
        public string Short_Description { get; set; } = "";
        /// <summary>
        /// 执行耗时
        /// </summary>
        public decimal Execute_Time { get; set; } = 0;
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime Log_CreateTime { get; set; } = DateTime.Now;
    }
}
