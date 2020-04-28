using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nest.BaseCore.Domain.RequestModel
{
    class UserRequestModel
    {
    }

    public class AddUserRequestModel { }

    public class EditUserRequestModel { }

    public class QueryUserRequestModel { }

    /// <summary>
    /// 登录请求参数
    /// </summary>
    public class LoginRequestModel
    {
        public LoginRequestModel()
        {
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { set; get; }

        /// <summary>
        /// 登录密码(MD5加密过)
        /// </summary>
        [Display(Name = "登录密码")]
        public string Password { set; get; }
    }
}
