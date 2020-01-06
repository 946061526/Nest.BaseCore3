using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 读取配置文件规则
    /// </summary>
    public class TemplateConfigModel
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 消息模板标尾
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 点击消息跳转url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public string TemplateId { get; set; }
    }
}
