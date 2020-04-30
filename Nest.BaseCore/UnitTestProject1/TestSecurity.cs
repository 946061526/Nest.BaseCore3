using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest.BaseCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class TestSecurity
    {
        [TestMethod]
        public void TestMd5()
        {
            var str = MD5Helper.GetMd5("123456@xs");
        }

        [TestMethod]
        public void TestAES()
        {
            var str = "123456@xs";
            str = AESHelper.AESEncrypt(str);
            str = AESHelper.AESDecrypt(str);

            var key = GuidTool.GetGuid();
            str = AESHelper.AESEncrypt(str, key);
            str = AESHelper.AESDecrypt(str, key);
        }
    }
}
