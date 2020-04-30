using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nest.BaseCore.Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatewayController : ControllerBase
    {
        [HttpGet]
        [Route("get")]
        public string Get()
        {
            return @"我是api网关！我是微服务架构中的唯一入口，
                    我提供一个单独且统一的API入口用于访问内部一个或多个API。";
        }
    }
}
