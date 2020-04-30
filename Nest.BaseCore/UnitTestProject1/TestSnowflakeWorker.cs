using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest.BaseCore.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class TestSnowflakeWorker
    {
        [TestMethod]
        public void MainTest()
        {
            Trace.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff"));
            SnowflakeIdWorker idWorker = new SnowflakeIdWorker(0, 0);
            for (int i = 0; i < 1000; i++)
            {
                Trace.WriteLine(string.Format("{0}         {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff"), idWorker.NextId()));
            }

            Trace.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff"));

            //SequentialGuidGenerator，效率低于SnowflakeIdWorker
            for (int i = 0; i < 1000; i++)
            {
                var id = SequentialGuidGenerator.NewSequentialGuid(SequentialGuidType.SequentialAsString);
                Trace.WriteLine(string.Format("{0}          {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff"), id));
            }

            Trace.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff"));
        }

    }
}
