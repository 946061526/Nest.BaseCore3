using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 性别 0男/1女
    /// </summary>
    public enum SexTypeEnum
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 0,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 1,
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKonw = 2
    }
}
