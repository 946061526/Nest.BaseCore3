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
    }
}
