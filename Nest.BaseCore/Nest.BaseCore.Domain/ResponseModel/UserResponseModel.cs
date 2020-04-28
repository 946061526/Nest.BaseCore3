using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Domain.ResponseModel
{
    class UserResponseModel
    {
    }

    public class QueryUserResponseModel { }

    /// <summary>
    /// 登录返回
    /// </summary>
    public class LoginResponseModel : CurrrentUserInfoModel
    {
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { set; get; }
    }
    /// <summary>
    /// 当前登录者信息
    /// </summary>
    public class CurrrentUserInfoModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        ///// <summary>
        ///// 角色Id
        ///// </summary>
        //public string RoleId { set; get; }
        ///// <summary>
        ///// 角色Id
        ///// </summary>
        //public string RoleName { set; get; }
    }
}
