using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Domain.MqModel
{
    /// <summary>
    /// Mq消息对象
    /// </summary>
    [Serializable]
    public class IMqModel
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MsgId { get; set; } = Guid.NewGuid().ToString();
    }
}
