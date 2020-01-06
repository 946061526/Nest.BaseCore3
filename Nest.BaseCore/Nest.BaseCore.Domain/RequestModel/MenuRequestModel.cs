using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Nest.BaseCore.Common;

namespace Nest.BaseCore.Domain.RequestModel
{
    public class MenuRequestModel
    {
    }

    /// <summary>
    /// 新增菜单 参数
    /// </summary>
    public class AddMenuRequestModel
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required]
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单父级Id:0表示一级菜单
        /// </summary>
        [Required]
        public string ParentId { set; get; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        [Required]
        public string Path { set; get; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { set; get; }

        /// <summary>
        /// 菜单类型 1模块/2功能/3操作
        /// </summary>
        [Required]
        public MenuTypeEnum Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; } = 0;
    }

    /// <summary>
    /// 查询菜单 参数
    /// </summary>
    public class QueryMenuRequestModel
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单父级Id:0表示一级菜单
        /// </summary>
        public string ParentId { set; get; }

        /// <summary>
        /// 菜单类型 -1全部/1模块/2功能/3操作
        /// </summary>
        public MenuTypeEnum? Type { get; set; }
    }
}
