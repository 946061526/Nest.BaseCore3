using Nest.BaseCore.Common.BaseModel;
using Nest.BaseCore.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Nest.BaseCore.Domain.RequestModel
{
    public class AppTicketRequestModel
    {
    }

    public class AddAppTicketRequestModel : BaseRequestModel
    {
        /// <summary>
        /// AppID
        /// </summary>
        [Required]
        public string AppId { get; set; }
        /// <summary>
        /// 客户端类型（ios、android、小程序等）
        /// </summary>
        [Required]
        public ClientTypeEnum ClientType { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        [Required]
        public string DeviceNo { get; set; }
    }
}
