using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    public sealed class Singleton
    {
        private static Singleton singleton = null;// 定义一个静态变量来保存类的实例
        private static readonly object obj = new object();// 定义一个标识确保线程同步

        /// <summary>
        /// 私有防止外部实例化
        /// </summary>
        private Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
                // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
                // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
                if (singleton == null)
                {
                    lock (obj)
                    {
                        if (singleton == null)
                        {
                            singleton = new Singleton();
                        }
                    }
                }
                return singleton;
            }
        }

    }
}
