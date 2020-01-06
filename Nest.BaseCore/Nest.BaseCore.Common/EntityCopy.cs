using System;
using System.Linq;
using System.Reflection;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 将实体赋值为另一个实体
    /// </summary>
    public class EntityCopy
    {
        /// <summary>
        /// 将源实体公有属性值复制到目标实体同名同类型属性(不区分大小写)
        /// </summary>
        /// <param name="src">源实体</param>
        /// <param name="des">目标实体</param>
        public static void Copy(object src, object des)
        {

            Type typeSrc = src.GetType();
            Type typeDes = des.GetType();

            PropertyInfo[] srcInfos = typeSrc.GetProperties();
            PropertyInfo[] desInfos = typeDes.GetProperties();

            foreach (PropertyInfo desInfo in desInfos)
            {
                //此处不区分大小写
                PropertyInfo srcInfo = srcInfos.FirstOrDefault(x => x.Name.ToLower() == desInfo.Name.ToLower() && x.PropertyType == desInfo.PropertyType);
                if (srcInfo != null)
                {
                    if (desInfo.CanWrite)//允许set
                        desInfo.SetValue(des, srcInfo.GetValue(src));
                }
            }
        }
    }
}
