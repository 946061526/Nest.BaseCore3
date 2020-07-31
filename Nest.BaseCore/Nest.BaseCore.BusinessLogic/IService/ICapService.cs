using Nest.BaseCore.Domain.MqModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Nest.BaseCore.BusinessLogic.IService
{
    /// <summary>
    /// Cap消息服务
    /// </summary>
    public interface ICapService
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="MqName">消息名称（相当于MQ的消息管道）</param>
        /// <param name="MqModel">消息对象</param>
        /// <returns></returns>
        Task Send(string MqName, IMqModel MqModel);
    }
}
