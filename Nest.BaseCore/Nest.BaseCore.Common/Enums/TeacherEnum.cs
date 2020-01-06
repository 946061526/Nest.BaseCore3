using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 教师
    /// </summary>
    public enum TeacherStatusEnum
    {
        /// <summary>
        /// 在职
        /// </summary>
        [Description("在职")]
        Work = 1,
        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        NotWork = 2,
    }

    /// <summary>
    /// 微信用户注册类型
    /// </summary>
    public enum WeChatTypeEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 0,
        /// <summary>
        /// 教师
        /// </summary>
        [Description("教师")]
        Teacher = 1,
        /// <summary>
        /// 家长
        /// </summary>
        [Description("家长")]
        Parent = 2,
    }

    /// <summary>
    /// 是否绑定微信
    /// </summary>
    public enum BindEnum
    {
        /// <summary>
        /// 已绑定
        /// </summary>
        [Description("已绑定")]
        Bind = 1,
        /// <summary>
        /// 未绑定
        /// </summary>
        [Description("未绑定")]
        NotBind = 2,
    }

    /// <summary>
    /// 老师任职关系:1班主任/2授课或生活
    /// </summary>
    public enum EntryFootEnum
    {
        /// <summary>
        /// 班主任
        /// </summary>
        [Description("班主任")]
        HeadTeacher = 1,
        /// <summary>
        /// 授课或生活
        /// </summary>
        [Description("授课或生活")]
        Other = 2,
    }
}
