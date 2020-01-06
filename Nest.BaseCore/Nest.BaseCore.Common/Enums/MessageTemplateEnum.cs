using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 消息推送模板 1上车/2下车/3请假/4请假审核结果
    /// </summary>
    public enum MessageTemplateEnum
    {
        /// <summary>
        /// 上车
        /// </summary>
        [Description("上车")]
        GetOnTemplate = 1,
        /// <summary>
        /// 下车
        /// </summary>
        [Description("下车")]
        GetOffTemplate = 2,
        /// <summary>
        /// 请假
        /// </summary>
        [Description("请假")]
        LeaveTemplate = 3,
        /// <summary>
        /// 请假审核结果
        /// </summary>
        [Description("请假审核结果")]
        LeaveExamineTemplate = 4,
    }
}
