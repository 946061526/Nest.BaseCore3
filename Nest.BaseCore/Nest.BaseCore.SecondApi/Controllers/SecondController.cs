using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nest.BaseCore.SecondApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecondController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SecondController> _logger;

        public SecondController(ILogger<SecondController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("get")]
        public string Get()
        {
            return "i'm SecondApi";
        }
    }
}
