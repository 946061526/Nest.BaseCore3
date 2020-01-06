using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nest.BaseCore.Common
{
    /// <summary>
    /// AES加密 辅助类
    /// </summary>
    public class AESHelper
    {
        private static readonly string _defaultKey = "5a4009a2847940029dabb536b68838ac";

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns></returns>
        public static string AESEncrypt(string str)
        {
            return AESEncrypt(str, _defaultKey);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string str, string key)
        {
            var encryptKey = Encoding.UTF8.GetBytes(key);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(encryptKey, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor,
                            CryptoStreamMode.Write))

                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(str);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result,
                            iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <returns></returns>
        public static string AESDecrypt(string str)
        {
            return AESDecrypt(str, _defaultKey);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string AESDecrypt(string str, string key)
        {
            var fullCipher = Convert.FromBase64String(str);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var decryptKey = Encoding.UTF8.GetBytes(key);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(decryptKey, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt,
                            decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                    return result;
                }
            }
        }
    }
}
