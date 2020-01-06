using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nest.BaseCore.Domain
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string RealName { get; set; }
        public int Status { get; set; }
        public DateTime createTime { get; set; }
    }
}
