using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 用数据对象
    /// </summary>
    public class UserDataModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 令牌
        /// </summary>
        public string UserToken { get; set; }
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        public string ClientType { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string AppSecret { get; set; }
    }
}
