using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// http上下文
    /// </summary>
    public class HttpContext
    {
        private static IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// 当前上下文
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Current => _contextAccessor.HttpContext;


        public static void Configure(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }
}
