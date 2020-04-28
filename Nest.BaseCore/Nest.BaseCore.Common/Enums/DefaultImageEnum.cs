using System.ComponentModel;

namespace Nest.BaseCore.Common.Enums
{
    /// <summary>
    /// 默认图片枚举类型
    /// </summary>
    public enum DefaultImageEnum
    {
        /// <summary>
        /// 不使用默认图片
        /// </summary>
        [Description("")]
        None = -1,
        /// <summary>
        /// 使用默认图片（地址：default.png）
        /// </summary>
        [Description("default.png")]
        DefaultImage = 0,
    }
}
