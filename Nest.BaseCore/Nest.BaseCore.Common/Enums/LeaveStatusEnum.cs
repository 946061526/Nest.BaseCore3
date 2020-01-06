using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    public enum LeaveStatusEnum
    {
        /// <summary>
        /// 全部，用于查询筛选
        /// </summary>
        [Description("全部")]
        All = -1,

        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        NoExamine = 0,
        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        Pass = 1,
        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("审核不通过")]
        NoPass = 2,

    }
}
