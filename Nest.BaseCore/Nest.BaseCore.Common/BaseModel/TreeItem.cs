using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 树形关系项
    /// </summary>
    /// <typeparam name="T">树形节点类型</typeparam>
    public class TreeItem<T>
    {
        /// <summary>
        /// 项对象
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// 子项集合
        /// </summary>
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }
}
