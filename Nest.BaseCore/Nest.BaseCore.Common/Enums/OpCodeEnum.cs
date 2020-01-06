using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 操作码
    /// </summary>
    public enum OpCodeEnum
    {
        /// <summary>
        /// 无操作
        /// </summary>
        [Description("无操作")]
        Null = 0,
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Add = 1,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Mod = 2,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Del = 3,
        /// <summary>
        /// 分配宿舍
        /// </summary>
        [Description("分配宿舍")]
        DORMITORY_ALLOC = 100,
        /// <summary>
        /// 退宿
        /// </summary>
        [Description("退宿")]
        DORMITORY_DEALLOC = 101,
    }

}
