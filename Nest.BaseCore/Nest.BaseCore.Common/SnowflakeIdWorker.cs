using System;
using System.Collections.Generic;
using System.Text;

namespace Nest.BaseCore.Common
{
    /*
    snowflake生成的ID整体上按照时间自增排序，并且整个分布式系统内不会产生ID碰撞，在同一毫秒内最多可以生成 1024 X 4096 = 4194304个全局唯一ID。
　　优点：不依赖数据库，完全内存操作速度快
    缺点：不同服务器需要保证系统时钟一致
    */

    /// <summary>
    /// 生成序列ID（Snowflake算法）
    /// </summary>
    /// <remarks></remarks>
    public class SnowflakeIdWorker
    {
        /// <summary>
        /// 开始时间截
        /// 1288834974657 是(Thu, 04 Nov 2010 01:42:54 GMT) 这一时刻到1970-01-01 00:00:00时刻所经过的毫秒数。
        /// 当前时刻减去1288834974657 的值刚好在2^41 里，因此占41位。
        /// 所以这个数是为了让时间戳占41位才特地算出来的。
        /// </summary>
        public const long Twepoch = 1288834974657L;

        /// <summary>
        /// 工作节点Id占用5位
        /// </summary>
        const int WorkerIdBits = 5;

        /// <summary>
        /// 数据中心Id占用5位
        /// </summary>
        const int DatacenterIdBits = 5;

        /// <summary>
        /// 序列号占用12位
        /// </summary>
        const int SequenceBits = 12;

        /// <summary>
        /// 支持的最大机器Id，结果是31 (这个移位算法可以很快的计算出几位二进制数所能表示的最大十进制数)
        /// </summary>
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);

        /// <summary>
        /// 支持的最大数据中心Id，结果是31
        /// </summary>
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        /// <summary>
        /// 机器ID向左移12位
        /// </summary>
        private const int WorkerIdShift = SequenceBits;

        /// <summary>
        /// 数据标识id向左移17位(12+5)
        /// </summary>
        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;

        /// <summary>
        /// 时间截向左移22位(5+5+12)
        /// </summary>
        public const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        /// <summary>
        /// 生成序列的掩码，这里为4095 (0b111111111111=0xfff=4095)
        /// </summary>
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        /// <summary>
        /// 毫秒内序列(0~4095)
        /// </summary>
        private long _sequence = 0L;

        /// <summary>
        /// 上次生成Id的时间截
        /// </summary>
        private long _lastTimestamp = -1L;

        /// <summary>
        /// 工作节点Id
        /// </summary>
        public long WorkerId { get; protected set; }

        /// <summary>
        /// 数据中心Id
        /// </summary>
        public long DatacenterId { get; protected set; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="workerId">工作ID (0~31)</param>
        /// <param name="datacenterId">数据中心ID (0~31)</param>
        public SnowflakeIdWorker(long workerId, long datacenterId)
        {
            WorkerId = workerId;
            DatacenterId = datacenterId;

            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(String.Format("worker Id can't be greater than {0} or less than 0", MaxWorkerId));
            }
            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(String.Format("datacenter Id can't be greater than {0} or less than 0", MaxDatacenterId));
            }
        }

        private static readonly object _lockObj = new Object();

        /// <summary>
        /// 获得下一个ID (该方法是线程安全的)
        /// </summary>
        /// <returns></returns>
        public virtual long NextId()
        {
            lock (_lockObj)
            {
                //获取当前时间戳
                var timestamp = TimeGen();

                //如果当前时间小于上一次ID生成的时间戳，说明系统时钟回退过这个时候应当抛出异常
                if (timestamp < _lastTimestamp)
                {
                    throw new InvalidOperationException(String.Format(
                        "Clock moved backwards.  Refusing to generate id for {0} milliseconds", _lastTimestamp - timestamp));
                }

                //如果是同一时间生成的，则进行毫秒内序列
                if (_lastTimestamp == timestamp)
                {
                    _sequence = (_sequence + 1) & SequenceMask;
                    //毫秒内序列溢出
                    if (_sequence == 0)
                    {
                        //阻塞到下一个毫秒,获得新的时间戳
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }

                //时间戳改变，毫秒内序列重置
                else
                {
                    _sequence = 0;
                }

                //上次生成ID的时间截
                _lastTimestamp = timestamp;

                //移位并通过或运算拼到一起组成64位的ID
                return ((timestamp - Twepoch) << TimestampLeftShift) |
                         (DatacenterId << DatacenterIdShift) |
                         (WorkerId << WorkerIdShift) | _sequence;
            }
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns>毫秒</returns>
        private static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns>毫秒</returns>
        protected virtual long TimeGen()
        {
            return GetTimestamp();
        }

        /// <summary>
        /// 阻塞到下一个毫秒，直到获得新的时间戳
        /// </summary>
        /// <param name="lastTimestamp">上次生成Id的时间截</param>
        /// <returns></returns>
        protected virtual long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }
    }
}
