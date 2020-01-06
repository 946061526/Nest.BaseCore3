using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nest.BaseCore.Common;
using Nest.BaseCore.Log;
using System;
using System.Net;

namespace Nest.BaseCore.Aop
{
    /// <summary>
    /// 统一异常处理
    /// </summary>
    public class GlobalExceptionAttribute : IExceptionFilter
    {
        private readonly IExceptionlessLogger _exceptionlessLogger;
        public GlobalExceptionAttribute(IExceptionlessLogger exceptionlessLogger)
        {
            _exceptionlessLogger = exceptionlessLogger;
        }

        public void OnException(ExceptionContext context)
        {
            ApiResultModel<string> apiResult = null;
            var ex = context.Exception;
            if (ex != null)
            {
                apiResult = new ApiResultModel<string>() { Code = ApiResultCode.Exception, Message = ex.Message };
                context.Result = new JsonResult(apiResult);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;

                //日志
                System.Threading.Tasks.Task.Run(() =>
                {
                    Net4Logger.Error(context.HttpContext.Request.Path, ex.Message, ex);
                    //_exceptionlessLogger.Error(context.HttpContext.Request.Path, ex.Message, "");
                });
            }
        }
    }
}
