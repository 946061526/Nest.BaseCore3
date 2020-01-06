using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Aop
{
    /// <summary>
    /// 内部服务API接口，仅提供给在白名单中的IP服务器使用
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class InnerServiceAttribute : Attribute
    {
    }
}
