using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 记录帮助类
    /// </summary>
    public class OperateHelper
    {
        
    }

    public enum OpCode
    {
        /// <summary>
        /// 无操作
        /// </summary>
        Null = 0,
        /// <summary>
        /// 添加
        /// </summary>
        Add = 1,
        /// <summary>
        /// 修改
        /// </summary>
        Mod = 2,
        /// <summary>
        /// 删除
        /// </summary>
        Del = 3
    }
    public enum OpStatus
    {
        /// <summary>
        /// 未完成
        /// </summary>
        undone = 1,
        /// <summary>
        /// 已完成
        /// </summary>
        completed = 2,
    }
    public enum RDataType
    {
        /// <summary>
        /// 白名单
        /// </summary>
        White = 1,
        /// <summary>
        /// 访客
        /// </summary>
        Visitor = 2,
    }

}