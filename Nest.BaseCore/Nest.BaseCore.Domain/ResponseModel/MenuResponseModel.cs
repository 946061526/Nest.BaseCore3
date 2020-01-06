using System;
using System.Collections.Generic;
using System.Text;
using Nest.BaseCore.Common;

namespace Nest.BaseCore.Domain.ResponseModel
{

    /// <summary>
    /// 菜单权限模型
    /// </summary>
    public class MenuResponseModel
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单父级Id:0表示一级菜单
        /// </summary>
        public string ParentId { set; get; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Path { set; get; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { set; get; }

        /// <summary>
        /// 是否显示：true隐藏/false显示
        /// </summary>
        public bool Hidden { set; get; } = true;

        /// <summary>
        /// 菜单类型 1模块/2功能/3操作
        /// </summary>
        public MenuTypeEnum Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            //get
            //{
            //    var s = 0;
            //    if (int.TryParse(MenuId, out s))
            //        return s;
            //    return 0;
            //}
            get; set;
        }
    }
}
