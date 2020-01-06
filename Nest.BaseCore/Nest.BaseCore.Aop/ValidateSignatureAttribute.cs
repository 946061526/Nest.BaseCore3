using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Nest.BaseCore.Cache;
using Nest.BaseCore.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nest.BaseCore.Aop
{
    /// <summary>
    /// 验证签名
    /// </summary>
    public class ValidateSignatureAttribute : ActionFilterAttribute
    {
        const string TicketKey = "ticket";
        const string SignKey = "sign";
        const string TimestampKey = "timestamp";

        /// <summary>
        ///     在调用操作方法之前发生。
        /// </summary>
        /// <param name="actionContext">操作上下文。</param>
        public override async System.Threading.Tasks.Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 判断是否忽略验证
            if (context.ActionDescriptor is ControllerActionDescriptor cad)
            {
                var controleIgnor = cad.ControllerTypeInfo.GetCustomAttributes(inherit: true).Any(x => x is IgnorValidateSignatureAttribute || x is InnerServiceAttribute || x is AllowAnonymousAttribute);
                if (controleIgnor)
                    return;
                var actionIgnor = cad.MethodInfo.GetCustomAttributes(inherit: true).Any(x => x is IgnorValidateSignatureAttribute || x is InnerServiceAttribute || x is AllowAnonymousAttribute);
                if (actionIgnor)
                    return;
            }
            ApiResultModel<string> apiResult = null;
            HttpRequest request = context.HttpContext.Request;

            #region 票据

            string ticket = "";
            var secret = "";
            if (context.RouteData.Values["Action"].ToString() != "GetAppTicket")
            {
                if (request.Headers.ContainsKey(TicketKey))
                {
                    ticket = request.Headers[TicketKey].ToString();
                    var redisKey = RedisCommon.GetTicketKey(ticket);
                    var redisData = RedisClient.Get<AppTicketModel>(RedisDatabase.DB_AuthorityService, redisKey);
                    if (redisData == null)
                    {
                        apiResult = new ApiResultModel<string>() { Code = ApiResultCode.TicketInvalid };
                        context.Result = new JsonResult(apiResult);
                        return;
                    }
                    secret = redisData.AppSecret;
                }
                else
                {
                    apiResult = new ApiResultModel<string>() { Code = ApiResultCode.NoTicket };
                    context.Result = new JsonResult(apiResult);
                    return;
                }
            }
            else
            {
                secret = AppSettingsHelper.Configuration["ApiConfig:SignDefaultKey"];//生成票据时，签名用默认key
            }

            #endregion

            #region  签名

            Dictionary<string, object> dictionary = null;

            if (request.Method == "POST")
            {
                if (request.ContentLength > 0)
                {
                    request.Body.Position = 0;
                    Stream stream = request.Body;
                    byte[] buffer = new byte[request.ContentLength.Value];
                    stream.Read(buffer, 0, buffer.Length);

                    var bodyStr = Encoding.UTF8.GetString(buffer);
                    dictionary = JsonHelper.DeserializeObject<Dictionary<string, object>>(bodyStr);
                }
            }
            else
            {
                dictionary = new Dictionary<string, object>(context.ActionArguments);
            }

            if (!dictionary.ContainsKey(SignKey))//参数不包含签名
            {
                apiResult = new ApiResultModel<string>() { Code = ApiResultCode.NoSign };
                context.Result = new JsonResult(apiResult);
                return;
            }
            else if (!dictionary.ContainsKey(TimestampKey))//参数不包含时间戳
            {
                apiResult = new ApiResultModel<string>() { Code = ApiResultCode.NoTimestamp };
                context.Result = new JsonResult(apiResult);
                return;
            }

            var keys = dictionary.Keys.ToList();
            foreach (var key in keys)
            {
                //参数为集合类型
                var value = dictionary[key];
                if (value != null && value.GetType().Namespace == "Newtonsoft.Json.Linq")
                {
                    dictionary[key] = JsonHelper.SerializeObject(value);
                }
            }

            //验证签名
            apiResult = ValidateSignature(dictionary, secret);
            if (apiResult.Code != ApiResultCode.Success)
            {
                context.Result = new JsonResult(apiResult);
                return;
            }

            #endregion

            await base.OnActionExecutionAsync(context, next);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        private ApiResultModel<string> ValidateSignature(Dictionary<string, object> dictionary, string secret)
        {
            var result = new ApiResultModel<string>() { Code = ApiResultCode.SignError };

            var sign = dictionary[SignKey].ToString();
            dictionary.Remove(SignKey);
            var paramSign = AuthenticationHelper.GetSign(dictionary, secret);
            if (paramSign == sign)
            {
                //验证签名时效
                var code = AuthenticationHelper.CheckTimeStamp(dictionary[TimestampKey].ToString());
                result.Code = code;
            }
            return result;
        }
    }

    /// <summary>
    /// 忽略签名
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class IgnorValidateSignatureAttribute : Attribute
    {

    }
}
