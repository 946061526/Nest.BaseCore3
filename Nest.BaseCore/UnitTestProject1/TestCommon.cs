using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest.BaseCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class TestCommon
    {
        [TestMethod]
        public void GetTimeSpanDays()
        {
            var days = Utils.GetTimeSpanDays(DateTime.Now, DateTime.Now.AddDays(2));
        }
    }
}
