using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Cache;
using Nest.BaseCore.Common.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest.BaseCore.Gateway
{
    public class SignatureGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => "Signature";

        const string TicketKey = "ticket";
        const string SignKey = "sign";
        const string TimestampKey = "timestamp";

        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {

            ApiResultModel<string> apiResult = null;
            var request = context.Request;

            #region 票据

            string ticket = "";
            var secret = "";
            //if (request.Raw.a.RouteData.Values["Action"].ToString().ToLower() != "getappticket")
            //{
            //    if (request.Headers.ContainsKey(TicketKey))
            //    {
            //        ticket = request.Headers[TicketKey].ToString();
            //        var redisKey = RedisCommon.GetTicketKey(ticket);
            //        var redisData = RedisClient.Get<AppTicketModel>(RedisDatabase.DB_AuthorityService, redisKey);
            //        if (redisData == null)
            //        {
            //            apiResult = new ApiResultModel<string>() { Code = ApiResultCode.TicketInvalid };
            //            context.Result = new GrantValidationResult(apiResult);
            //            return;
            //        }
            //        secret = redisData.AppSecret;
            //    }
            //    else
            //    {
            //        apiResult = new ApiResultModel<string>() { Code = ApiResultCode.NoTicket };
            //        context.Result = new JsonResult(apiResult);
            //        return;
            //    }
            //}
            //else
            //{
            //    secret = AppSettingsHelper.Configuration["ApiConfig:SignDefaultKey"];//生成票据时，签名用默认key
            //}

            #endregion

            throw new NotImplementedException();
        }
    }
}
