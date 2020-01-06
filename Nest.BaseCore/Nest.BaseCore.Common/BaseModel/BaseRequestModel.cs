using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 接口请求参数基类
    /// </summary>
    public class BaseRequestModel
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        [Required]
        public string Timestamp { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        [Required]
        public string Nonce { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [Required]
        public string Sign { get; set; }
    }

    /// <summary>
    /// 分页基本请求参数模型
    /// </summary>
    public class BasePageRequestModel : BaseRequestModel
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        [Required]
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页码大小
        /// </summary>
        [Required]
        public int PageSize { get; set; } = 10;
    }

    /// <summary>
    /// 分页关键字基本参数模型
    /// </summary>
    public class BasePageKeywordModel : BasePageRequestModel
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string KeyWord { get; set; } = "";
    }

    /// <summary>
    /// 字符串Id基本参数模型
    /// </summary>
    public class BaseIdModel : BaseRequestModel
    {
        /// <summary>
        /// 字符串Id
        /// </summary>
        public string Id { get; set; } = "";
    }

    /// <summary>
    /// 微信手机号模型
    /// </summary>
    public class WechatModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 类型:1教师/2家长
        /// </summary>
        public int Type { get; set; }
    }

    /// <summary>
    /// 微信OpenId模型
    /// </summary>
    public class WechatOpenIdModel
    {
        /// <summary>
        /// 微信用户Id
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 类型:1教师/2家长
        /// </summary>
        public int Type { get; set; }
    }

    /// <summary>
    /// 微信绑定模型
    /// </summary>
    public class WechatPwdModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 绑定密码
        /// </summary>
        public string Pwd { get; set; }
    }

}
