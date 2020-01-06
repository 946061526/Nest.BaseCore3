using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nest.BaseCore.Domain
{
    /// <summary>
    /// 权限表
    /// </summary>
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public string parentId { get; set; }
        public string path { get; set; }
        public string icon { get; set; }
        public int type { get; set; } = 1;
        public int sort { get; set; } = 0;
        [Column(TypeName = "bit")]
        public bool hidden { get; set; } = false;
    }
}

