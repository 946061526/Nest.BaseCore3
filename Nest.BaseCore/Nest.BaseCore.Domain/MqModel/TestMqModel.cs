using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Domain.MqModel
{
    /// <summary>
    /// 测试消息对象
    /// </summary>
    public class TestMqModel : IMqModel
    {
        /// <summary>
        /// id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
