using Ocelot.Middleware.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest.BaseCore.Gateway
{
    public static class OceletServiceExtension
    {
        /// <summary>
        /// 注册签名验证中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IOcelotPipelineBuilder UseSignatureValidatorMiddleware(this IOcelotPipelineBuilder builder)
        {
            return builder.UseMiddleware<SignatureValidatorMiddleware>();
        }
    }
}
