using StackExchange.Redis;

namespace Nest.BaseCore.Cache
{
    /// <summary>
    /// Redis数据库
    /// </summary>
    public class RedisDatabase
    {
        /// <summary>
        /// 默认库，db0
        /// </summary>
        public static IDatabase DB_Default
        {
            get
            {
                return RedisClient.Instance.GetDatabase(0);
            }
        }
        /// <summary>
        /// 用户库, db1
        /// </summary>
        public static IDatabase DB_UserService
        {
            get
            {
                return RedisClient.Instance.GetDatabase(1);
            }
        }
        /// <summary>
        /// 权限服务库, db2
        /// </summary>
        public static IDatabase DB_AuthorityService
        {
            get
            {
                return RedisClient.Instance.GetDatabase(2);
            }
        }
        /// <summary>
        /// 基础服务库, db3
        /// </summary>
        public static IDatabase DB_BasicService
        {
            get
            {
                return RedisClient.Instance.GetDatabase(3);
            }
        }
        /// <summary>
        /// 设备服务库, db4
        /// </summary>
        public static IDatabase DB_DeviceService
        {
            get
            {
                return RedisClient.Instance.GetDatabase(4);
            }
        }        
        /// <summary>
        /// 请假服务库, db7
        /// </summary>
        public static IDatabase DB_LeaveService
        {
            get
            {
                return RedisClient.Instance.GetDatabase(7);
            }
        }
        /// <summary>
        /// 记录服务库, db8
        /// </summary>
        public static IDatabase DB_RecordService
        {
            get
            {
                return RedisClient.Instance.GetDatabase(8);
            }
        }
        /// <summary>
        /// 微信服务库, db9
        /// </summary>
        public static IDatabase DB_WechatService
        {
            get
            {
                return RedisClient.Instance.GetDatabase(9);
            }
        }
    }
}
