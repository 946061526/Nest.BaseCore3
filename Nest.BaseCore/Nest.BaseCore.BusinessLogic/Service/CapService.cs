using DotNetCore.CAP;
using Nest.BaseCore.BusinessLogic.IService;
using Nest.BaseCore.Domain.MqModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nest.BaseCore.BusinessLogic.Service
{
    /// <summary>
    /// Cap消息服务
    /// </summary>
    public class CapService : ICapService
    {
        /// <summary>
        /// 消息服务
        /// </summary>
        private readonly ICapPublisher _publisher;

        public CapService(ICapPublisher publisher)
        {
            _publisher = publisher;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="MqName"></param>
        /// <param name="MqModel"></param>
        public void Send(string MqName, IMqModel MqModel)
        {
            _publisher.Publish(MqName, MqModel);
        }

        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="MqName"></param>
        /// <param name="MqModel"></param>
        /// <returns></returns>
        public async Task SendAsync(string MqName, IMqModel MqModel)
        {
            await _publisher.PublishAsync(MqName, MqModel);
        }
    }
}