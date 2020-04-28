using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nest.BaseCore.Domain.Entity
{
    /// <summary>
    /// App票据信息
    /// </summary>
    [Table("AppTicket")]
    public class AppTicket
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// AppID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 客户端类型（ios、android）
        /// </summary>
        public string ClientType { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceNo { get; set; }
        /// <summary>
        /// 随机串
        /// </summary>
        public string Noncestr { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 票据
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}
