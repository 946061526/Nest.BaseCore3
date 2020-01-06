using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 国标性别
    /// </summary>
    public class SexType
    {
        /// <summary>
        /// 获取性别列表
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, string> GetSexs()
        {
            IDictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(new KeyValuePair<int, string>(0, "男"));
            dic.Add(new KeyValuePair<int, string>(1, "女"));
            return dic;
        }

    }
}