
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 返回状态码
    /// </summary>
    public enum ApiResultCode
    {
        /// <summary>
        /// 业务处理成功
        /// </summary>
        [Description("业务处理成功")]
        Success = 200,
        /// <summary>
        /// 业务处理失败（默认）
        /// </summary>
        [Description("业务处理失败")]
        Fail = 414,
        /// <summary>
        /// 未找到相关记录
        /// </summary>
        [Description("未找到相关记录")]
        NoRecord = 416,
        /// <summary>
        /// 业务处理异常
        /// </summary>
        [Description("业务处理异常")]
        Exception = 500,


        #region 授权服务，45开头，如4501,4502...

        /// <summary>
        /// 非法请求
        /// </summary>
        [Description("非法请求")]
        NoToken = 4501,
        /// <summary>
        /// 用户登录失效
        /// </summary>
        [Description("用户登录失效，请重新登录")]
        UserInvalid = 4502,
        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("用户不存在")]
        UserNotExists = 4503,
        /// <summary>
        /// 登录密码错误
        /// </summary>
        [Description("登录密码错误")]
        LoginPassError = 4504,

        /// <summary>
        /// 请求无票据
        /// </summary>
        [Description("请求无票据")]
        NoTicket = 4511,
        /// <summary>
        /// 票据失效
        /// </summary>
        [Description("票据失效")]
        TicketInvalid = 4512,
        /// <summary>
        /// 请求无签名
        /// </summary>
        [Description("请求无签名")]
        NoSign = 4513,
        /// <summary>
        /// 签名错误
        /// </summary>
        [Description("签名错误")]
        SignError = 4514,
        /// <summary>
        /// 签名超时
        /// </summary>
        [Description("签名超时")]
        SignTimeout = 4515,
        /// <summary>
        /// 签名时间戳格式错误
        /// </summary>
        [Description("签名时间戳格式错误")]
        TimestampFromatError = 4516,
        /// <summary>
        /// 签名缺少Timestamp参数
        /// </summary>
        [Description("签名缺少Timestamp参数")]
        NoTimestamp = 4517,

        #endregion

    }

    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T">数据</typeparam>
    public class ApiResultModel<T>
    {
        public ApiResultModel()
        {
            Code = ApiResultCode.Fail;
            Data = default(T);
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public ApiResultCode Code { set; get; }

        private string _msg = string.Empty;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(_msg))
                {
                    return Code.GetEnumDescription();
                }
                return _msg;
            }
            set
            {
                _msg = value;
            }
        }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { set; get; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; } = 0;
    }
}
