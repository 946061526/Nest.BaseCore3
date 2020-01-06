using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 班级
    /// </summary>
    public enum ClassStatusEnum
    {
        /// <summary>
        /// 已毕业
        /// </summary>
        [Description("已毕业")]
        Graduation = 1,
        /// <summary>
        /// 未毕业
        /// </summary>
        [Description("未毕业")]
        NotGraduation = 2,
    }


    /// <summary>
    /// 班级教师关联
    /// </summary>
    public enum ClassTeacherEnum
    {
        /// <summary>
        /// 班主任
        /// </summary>
        [Description("班主任")]
        ClassTeacher = 1,
        /// <summary>
        /// 授课
        /// </summary>
        [Description("授课")]
        Teaching = 2,
    }

}
