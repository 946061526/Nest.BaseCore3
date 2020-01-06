using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// Excel导入枚举类型
    /// </summary>
    public enum ExcelImPortEnum
    {
        /// <summary>
        /// 班级
        /// </summary>
        [Description("班级导入模板")]
        Class = 1,
        /// <summary>
        /// 老师
        /// </summary>
        [Description("教师导入模板")]
        Teacher = 2,
        /// <summary>
        /// 学生和家长
        /// </summary>
        [Description("学生导入模板")]
        StudentAndParent = 3,
        /// <summary>
        /// 宿舍分配导入模板
        /// </summary>
        [Description("宿舍分配导入模板")]
        RoomImportModule = 4,
        /// <summary>
        /// 宿舍分配导入失败数据
        /// </summary>
        [Description("宿舍分配导入失败数据")]
        RoomImportFail = 5,

        /// <summary>
        /// 导出老师信息
        /// </summary>
        [Description("导出老师信息")]
        ExportTeacher = 6,
        /// <summary>
        /// 导出家长信息
        /// </summary>
        [Description("导出家长信息")]
        ExportParent = 7,

    }
}
