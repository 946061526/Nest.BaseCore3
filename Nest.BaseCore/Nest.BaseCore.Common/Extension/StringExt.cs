using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExt
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 字符串转数组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string[] ToSplitArray(this string value, char split)
        {
            return value.Split(split);
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBase64(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            var buff = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// base64 解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FormBase64(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            var buff = Convert.FromBase64String(input);

            return Encoding.UTF8.GetString(buff);
        }

        /// <summary>
        /// 字符串转换成utf-8编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUtf8(string str)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(str);
            string decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }

        /// <summary>
        /// 将服务器图片相对路径转化为HTTP路径
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultImageEnum">默认图片类型，默认使用默认图</param>
        /// <returns></returns>
        public static string ToImageUrl(this string input, DefaultImageEnum defaultImageEnum = DefaultImageEnum.DefaultImage)
        {
            var imgSaveService = AppSettingsHelper.Configuration["ImageUploadConfig:UploadService"];
            if (string.IsNullOrEmpty(input))//输入url为空
            {
                if (defaultImageEnum == DefaultImageEnum.None)
                {
                    return string.Empty;
                }
                else//使用默认图
                {
                    //描述中应存放默认图片的地址
                    return $"{imgSaveService}/{defaultImageEnum.GetEnumDescription()}";
                }
            }
            if (input.StartsWith("http://") || input.StartsWith("https://"))
            {
                return input;
            }
            return $"{imgSaveService}/{input.Replace('\\', '/')}";
        }


        #region 判断否是base64字符串

        /// <summary>
        /// 是否base64字符串
        /// </summary>
        /// <param name="base64Str">要判断的字符串</param>
        /// <returns></returns>
        public static bool IsBase64(this string base64Str)
        {
            byte[] bytes = null;
            return IsBase64(base64Str, out bytes);
        }

        /// <summary>
        /// 是否base64字符串
        /// </summary>
        /// <param name="base64Str">要判断的字符串</param>
        /// <param name="bytes">字符串转换成的字节数组</param>
        /// <returns></returns>
        private static bool IsBase64(string base64Str, out byte[] bytes)
        {
            bytes = null;
            if (string.IsNullOrEmpty(base64Str))
                return false;
            else
            {
                if (base64Str.Contains(","))
                    base64Str = base64Str.Split(',')[1];
                if (base64Str.Length % 4 != 0)
                    return false;
                if (base64Str.Any(c => !base64CodeArray.Contains(c)))
                    return false;
            }
            try
            {
                bytes = Convert.FromBase64String(base64Str);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static char[] base64CodeArray = new char[]
        {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        '0', '1', '2', '3', '4',  '5', '6', '7', '8', '9', '+', '/', '='
        };

        #endregion
    }
}
