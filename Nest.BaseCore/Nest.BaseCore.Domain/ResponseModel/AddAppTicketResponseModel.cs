using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Domain.ResponseModel
{
    /// <summary>
    /// 票据信息
    /// </summary>
    public class AddAppTicketResponseModel
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 票据
        /// </summary>
        public string Ticket { get; set; }
    }
}
