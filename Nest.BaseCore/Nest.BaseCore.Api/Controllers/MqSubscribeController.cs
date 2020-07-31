using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest.BaseCore.Domain.MqModel;

namespace Nest.BaseCore.Api.Controllers
{
    /// <summary>
    /// MQ订阅控制器，用于接收mq消息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MqSubscribeController : ControllerBase
    {
        /// <summary>
        /// 消息服务
        /// </summary>
        private readonly ICapPublisher _publisher;

        public MqSubscribeController(ICapPublisher publisher)
        {
            _publisher = publisher;
        }

        /// <summary>
        /// 接收对象消息
        /// </summary>
        [NonAction]
        [CapSubscribe("mqKey.test.log")]
        public Task ReceiveLogMsg(TestMqModel model)
        {
            var res = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            Console.WriteLine($"{ DateTime.Now } 收到消息: {res}");
            return Task.CompletedTask;
        }
    }
}

