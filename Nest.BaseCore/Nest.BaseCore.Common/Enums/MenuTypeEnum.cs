using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 菜单类型枚举 1模块/2功能/3操作
    /// </summary>
    public enum MenuTypeEnum
    {
        /// <summary>
        /// 全部 用于查询
        /// </summary>
        [Description("全部")]
        All = -1,

        /// <summary>
        /// 模块
        /// </summary>
        [Description("模块")]
        Module = 1,
        /// <summary>
        /// 功能
        /// </summary>
        [Description("功能")]
        Function = 2,
        /// <summary>
        /// 操作
        /// </summary>
        [Description("操作")]
        Operation = 3,
    }
}
