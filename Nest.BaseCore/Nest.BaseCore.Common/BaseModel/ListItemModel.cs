using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 树形或下拉 数据模型
    /// </summary>
    public class ListItemModel
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; } = "";

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// 上级值 默认-1
        /// </summary>
        public string ParentValue { get; set; } = "";

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; } = false;
    }

    /// <summary>
    /// 树形节点 模型
    /// </summary>
    public class TreeNodesModel
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// 值
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        /// 上级值 默认0
        /// </summary>
        public string Pid { get; set; } = "0";

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// 是否展开子节点
        /// </summary>
        public bool Spread = true;

        /// <summary>
        /// 是否选中
        /// </summary>
        public int CheckedState { get; set; } = 0;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get { return CheckedState == 1; } }

        /// <summary>
        /// 子级
        /// </summary>
        public List<TreeNodesModel> Children { get; set; } = new List<TreeNodesModel>();
    }
}
