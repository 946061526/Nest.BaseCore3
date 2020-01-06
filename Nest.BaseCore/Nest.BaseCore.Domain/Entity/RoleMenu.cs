using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nest.BaseCore.Domain
{
    /// <summary>
    /// 角色权限关联表
    /// </summary>
    [Table("RoleMenu")]
    public class RoleMenu
    {
        [Key]
        public string id { get; set; }
        public string roleId { get; set; }
        public string menuId { get; set; }
        [Column(TypeName = "bit")]
        public bool readed { get; set; }
        [Column(TypeName = "bit")]
        public bool writed { get; set; }
    }
}

