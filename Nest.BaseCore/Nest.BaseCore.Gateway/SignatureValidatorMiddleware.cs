
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Ocelot.Logging;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest.BaseCore.Gateway
{
    /// <summary>
    /// 签名验证中间件
    /// </summary>
    public class SignatureValidatorMiddleware: OcelotMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly OcelotRequestDelegate _next;

        public SignatureValidatorMiddleware(OcelotRequestDelegate next, IConfiguration configuration, IMemoryCache memoryCache, IOcelotLoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger<SignatureValidatorMiddleware>())
        {
            _next = next;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        public async Task Invoke(DownstreamContext context)
        {
            //var permissions = await _memoryCache.GetOrCreateAsync("ApiPermissions", async entry =>
            //{
            //    using (var conn = new SqlConnection(_configuration.GetConnectionString("ApiPermissions")))
            //    {
            //        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            //        return (await conn.QueryAsync<ApiPermission>("SELECT * FROM dbo.ApiPermissions")).ToArray();
            //    }
            //});
            //var result = await context.HttpContext.AuthenticateAsync(context.DownstreamReRoute.AuthenticationOptions.AuthenticationProviderKey);
            //context.HttpContext.User = result.Principal;
            //var user = context.HttpContext.User;

            //var request = context.HttpContext.Request;
            //var permission = permissions.FirstOrDefault(p =>
            //    request.Path.Value.Equals(p.PathPattern, StringComparison.OrdinalIgnoreCase) && p.Method.ToUpper() == request.Method.ToUpper());
            //if (permission == null)// 完全匹配不到，再根据正则匹配
            //{
            //    permission =
            //        permissions.FirstOrDefault(p =>
            //            Regex.IsMatch(request.Path.Value, p.PathPattern, RegexOptions.IgnoreCase) && p.Method.ToUpper() == request.Method.ToUpper());
            //}
            //if (!user.Identity.IsAuthenticated)
            //{
            //    if (permission != null && string.IsNullOrWhiteSpace(permission.AllowedRoles)) //默认需要登录才能访问
            //    {
            //        //context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "Anonymous") }, context.DownstreamReRoute.AuthenticationOptions.AuthenticationProviderKey));
            //    }
            //    else
            //    {
            //        SetPipelineError(context, new UnauthenticatedError("unauthorized, need login"));
            //        return;
            //    }
            //}
            //else
            //{
            //    if (!string.IsNullOrWhiteSpace(permission?.AllowedRoles) &&
            //        !permission.AllowedRoles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Any(r => user.IsInRole(r)))
            //    {
            //        SetPipelineError(context, new UnauthorisedError("forbidden, have no permission"));
            //        return;
            //    }
            //}

            var request = context.HttpContext.Request;

            await _next.Invoke(context);
        }
    }
}
