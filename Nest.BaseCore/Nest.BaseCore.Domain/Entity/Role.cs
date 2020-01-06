using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nest.BaseCore.Domain
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table("Role")]
    public class Role
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
    }
}