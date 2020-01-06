using System.ComponentModel;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 关于人员的类型枚举
    /// </summary>
    public class UserTypeEnum
    {
    }

    /// <summary>
    /// 后台用户类型
    /// </summary>
    public enum AdminUserType
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        SuperAdmin = 2,
        /// <summary>
        /// 老师
        /// </summary>
        [Description("老师")]
        Teacher = 4,
        /// <summary>
        /// 宿管
        /// </summary>
        [Description("宿管")]
        DormitoryAdmin = 6,
    }

    /// <summary>
    /// 人员类型
    /// </summary>
    public enum PeopleTypeEnum
    {
        /// <summary>
        /// 学生
        /// </summary>
        [Description("学生")]
        Student = 1,
        /// <summary>
        /// 教师
        /// </summary>
        [Description("教师")]
        Teacher = 2,
        /// <summary>
        /// 家长
        /// </summary>
        [Description("家长")]
        Parent = 3,
    }

    /// <summary>
    /// 微信用户类型 1教师/2家长
    /// </summary>
    public enum WechatUserTypeEnum
    {
        /// <summary>
        /// 教师
        /// </summary>
        [Description("教师")]
        Teacher = 1,
        /// <summary>
        /// 家长
        /// </summary>
        [Description("家长")]
        Parents = 2,
    }
}
