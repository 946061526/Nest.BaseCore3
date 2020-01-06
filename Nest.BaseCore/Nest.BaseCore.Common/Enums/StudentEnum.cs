using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 学生状态 1在读/2休学/3退学/4毕业
    /// </summary>
    public enum StudentStatusEnum
    {
        /// <summary>
        /// 在读
        /// </summary>
        [Description("在读")]
        Reading = 1,
        /// <summary>
        /// 休学
        /// </summary>
        [Description("休学")]
        Suspension = 2,
        /// <summary>
        /// 退学
        /// </summary>
        [Description("退学")]
        Dropout = 3,
        /// <summary>
        /// 毕业
        /// </summary>
        [Description("毕业")]
        Graduation = 4
    }

    /// <summary>
    /// 学生变更
    /// </summary>
    public enum StudentChangeEnum
    {
        /// <summary>
        /// 留级
        /// </summary>
        [Description("留级")]
        Retention = 1,
        /// <summary>
        /// 升级
        /// </summary>
        [Description("升级")]
        Upgrade = 2,
        /// <summary>
        /// 换班
        /// </summary>
        [Description("换班")]
        Shift = 3,
        /// <summary>
        /// 休学
        /// </summary>
        [Description("休学")]
        Suspension = 4,
        /// <summary>
        /// 退学
        /// </summary>
        [Description("退学")]
        Dropout = 5,
        /// <summary>
        /// 毕业
        /// </summary>
        [Description("毕业")]
        Graduation = 6,
        /// <summary>
        /// 在读
        /// </summary>
        [Description("在读")]
        School = 7
    }

    /// <summary>
    /// 学生类型 1走读/2住校
    /// </summary>
    public enum StudentTypeEnum
    {
        /// <summary>
        /// 走读
        /// </summary>
        [Description("走读")]
        Day = 1,
        /// <summary>
        /// 住校
        /// </summary>
        [Description("住校")]
        Board = 2,
    }


    /// <summary>
    /// 学生实时状态
    /// </summary>
    public enum StudentLocationStatusEnum
    {
        /// <summary>
        /// 校内
        /// </summary>
        [Description("校内")]
        OnCampus = 1,
        /// <summary>
        /// 宿舍
        /// </summary>
        [Description("宿舍")]
        DormRoom = 2,
        /// <summary>
        /// 校外
        /// </summary>
        [Description("校外")]
        OffCampus = 3,
    }
}
