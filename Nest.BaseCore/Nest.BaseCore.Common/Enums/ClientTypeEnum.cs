using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 客户端类型
    /// </summary>
    public enum ClientTypeEnum
    {
        /// <summary>
        /// 安卓
        /// </summary>
        [Description("android")]
        Android = 1,
        /// <summary>
        /// 苹果
        /// </summary>
        [Description("ios")]
        Ios = 2,
        /// <summary>
        /// 微信小程序
        /// </summary>
        [Description("MiniApp")]
        MiniApp = 3,
    }
}
