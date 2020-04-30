using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest.BaseCore.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class TestLog
    {
        private IExceptionlessLogger _exceptionlessLogger;
        public TestLog(IExceptionlessLogger exceptionlessLogger)
        {
            _exceptionlessLogger = exceptionlessLogger;
        }


        [TestMethod]
        public void TestNetLog()
        {
            Net4Logger.Debug("debug", "阿萨德法师法方为人阿萨德法师法方为人阿萨德法师法方为人", new Exception("debug"));
            Net4Logger.Error("error", "asfsafdsfasfdsf阿萨德法师法方为人阿萨德法师法方为人阿萨德法师法方为人", new Exception("error"));
            Net4Logger.Info("info", "1q324154354325654阿萨德法师法方为人阿萨德法师法方为人阿萨德法师法方为人", new Exception("info"));
        }

        [TestMethod]
        public void TestExceptionlessLog()
        {
            _exceptionlessLogger.Debug("debug", "昂首已公司都该哦IQ日均额我就是国家法定124safs", "tag1");
            _exceptionlessLogger.Error("error", "昂首已公司都该哦IQ日均额我就是国家法定124safs", "tag1", "tag2");
            _exceptionlessLogger.Info("info  ", "昂首已公司都该哦IQ日均额我就是国家法定124safs", "tag1", "tag2", "tag3");
        }
    }
}
