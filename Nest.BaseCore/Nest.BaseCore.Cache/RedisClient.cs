using Nest.BaseCore.Common;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Nest.BaseCore.Cache
{
    /// <summary>
    /// Redis操作类
    /// </summary>
    public class RedisClient
    {

        #region 链接配置

        ///// <summary>
        ///// Redis服务器ip
        ///// </summary>
        //public static string RedisHost
        //{
        //    get
        //    {
        //        try
        //        {
        //            return AppSettingsHelper.Configuration["RedisConfig:Host"];
        //        }
        //        catch
        //        {
        //            return "127.0.0.1";
        //        }
        //    }
        //}

        ///// <summary>
        ///// Redis服务器端口
        ///// </summary>
        //public static int RedisPort
        //{
        //    get
        //    {
        //        try
        //        {
        //            return int.Parse(AppSettingsHelper.Configuration["RedisConfig:Port"]);
        //        }
        //        catch
        //        {
        //            return 6379;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Redis密码
        ///// </summary>
        //public static string RedisPassword
        //{
        //    get
        //    {
        //        try
        //        {
        //            return AppSettingsHelper.Configuration["RedisConfig:Pass"];

        //        }
        //        catch
        //        {
        //            return string.Empty;
        //        }
        //    }
        //}
        #endregion

        private static RedisConfig _redisConfig;
        private static ConnectionMultiplexer _instance;
        private static readonly object _redisLock = new object();
        /// <summary>
        /// Redis实例
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                try
                {
                    if (_redisConfig == null)
                    {
                        _redisConfig = AppSettingsHelper.GetAppSettings<RedisConfig>("RedisConfig");//读取配置文件
                    }

                    if (_instance == null || !_instance.IsConnected)
                    {
                        lock (_redisLock)
                        {
                            var configurationOptions = new ConfigurationOptions
                            {
                                //AbortOnConnectFail = false,
                                //Password = RedisPassword,
                                Password = _redisConfig.Pass
                            };
                            //configurationOptions.EndPoints.Add(new DnsEndPoint(RedisHost, RedisPort));
                            configurationOptions.EndPoints.Add(new DnsEndPoint(_redisConfig.Host, _redisConfig.Port));
                            _instance = ConnectionMultiplexer.Connect(configurationOptions);

                            //注册如下事件
                            _instance.ConnectionFailed += MuxerConnectionFailed;
                            _instance.ConnectionRestored += MuxerConnectionRestored;
                            _instance.ErrorMessage += MuxerErrorMessage;
                            _instance.ConfigurationChanged += MuxerConfigurationChanged;
                            _instance.InternalError += MuxerInternalError;
                        }
                    }
                    return _instance;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


        private static Dictionary<string, ConnectionMultiplexer> _instanceList = new Dictionary<string, ConnectionMultiplexer>();
        /// <summary>
        /// 获取实例
        /// </summary>
        public static ConnectionMultiplexer GetInstance(string serverHost, int serverPort = 6379, string serverPassword = "")
        {
            try
            {
                lock (_redisLock)
                {
                    ConnectionMultiplexer instance = null;
                    var key = string.Format("{0}_{1}_{2}", serverHost, serverPort, serverPassword);
                    if (_instanceList.Keys.Contains(key))
                        instance = _instanceList[key];
                    if (instance != null && instance.GetDatabase().IsConnected("testKey"))
                        return instance;

                    var configurationOptions = new ConfigurationOptions
                    {
                        //AbortOnConnectFail = false,
                        Password = serverPassword,
                    };
                    configurationOptions.EndPoints.Add(new DnsEndPoint(serverHost, serverPort));
                    instance = ConnectionMultiplexer.Connect(configurationOptions);
                    _instanceList.Add(key, instance);
                    return instance;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 触发事件

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            //LogHelper.SafeLogMessage("Configuration changed: " + e.EndPoint);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            //LogHelper.SafeLogMessage("ErrorMessage: " + e.Message);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            //LogHelper.SafeLogMessage("ConnectionRestored: " + e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            //LogHelper.SafeLogMessage("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType +(e.Exception == null ? "" : (", " + e.Exception.Message)));
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            //LogHelper.SafeLogMessage("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            //LogHelper.SafeLogMessage("InternalError:Message" + e.Exception.Message);
        }

        //场景不一样，选择的模式便会不一样，大家可以按照自己系统架构情况合理选择长连接还是Lazy。
        //建立连接后，通过调用ConnectionMultiplexer.GetDatabase 方法返回对 Redis Cache 数据库的引用。从 GetDatabase 方法返回的对象是一个轻量级直通对象，不需要进行存储。

        /// <summary>
        /// 使用的是Lazy，在真正需要连接时创建连接。
        /// 延迟加载技术
        /// 微软azure中的配置 连接模板
        /// </summary>
        //private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        //{
        //    //var options = ConfigurationOptions.Parse(constr);
        //    ////options.ClientName = GetAppName(); // only known at runtime
        //    //options.AllowAdmin = true;
        //    //return ConnectionMultiplexer.Connect(options);
        //    ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect(Coonstr);
        //    muxer.ConnectionFailed += MuxerConnectionFailed;
        //    muxer.ConnectionRestored += MuxerConnectionRestored;
        //    muxer.ErrorMessage += MuxerErrorMessage;
        //    muxer.ConfigurationChanged += MuxerConfigurationChanged;
        //    muxer.HashSlotMoved += MuxerHashSlotMoved;
        //    muxer.InternalError += MuxerInternalError;
        //    return muxer;
        //});

        #endregion


        #region 数据库操作

        /// <summary>
        /// 这里的 MergeKey 用来拼接 Key 的前缀，具体不同的业务模块使用不同的前缀。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //private static string MergeKey(string key)
        //{
        //    return key;
        //    //return BaseSystemInfo.SystemCode + key;
        //}

        ///// <summary>
        ///// 根据key保存缓存对象
        ///// </summary>
        public static void Set<T>(IDatabase db, string key, T data, int expireMinutes = 0)
        {
            string str = JsonConvert.SerializeObject(data);
            if (expireMinutes > 0)
                db.StringSet(key, str, TimeSpan.FromMinutes(expireMinutes));
            else
                db.StringSet(key, str);
        }

        ///// <summary>
        ///// 根据key保存缓存对象
        ///// </summary>
        public static void Set(IDatabase db, string key, object data, int expireMinutes = 0)
        {
            string str = JsonConvert.SerializeObject(data);
            if (expireMinutes > 0)
                db.StringSet(key, str, TimeSpan.FromMinutes(expireMinutes));
            else
                db.StringSet(key, str);
        }

        ///// <summary>
        ///// 根据key获取缓存对象
        ///// </summary>
        public static string Get(IDatabase db, string key)
        {
            return db.StringGet(key);
        }

        ///// <summary>
        ///// 根据key获取缓存对象
        ///// </summary>
        public static T Get<T>(IDatabase db, string key)
        {
            var val = db.StringGet(key);
            if (!val.HasValue)
                return default(T);
            return JsonConvert.DeserializeObject<T>(val);
        }

        /// <summary>
        /// 异步设置缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static async Task SetAsync<T>(IDatabase db, string key, T value)
        {
            //key = MergeKey(key);
            var data = JsonConvert.SerializeObject(value);
            await db.StringSetAsync(key, data);
        }

        /// <summary>
        /// 异步获取缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<object> GetAsync(IDatabase db, string key)
        {
            //key = MergeKey(key);
            object value = await db.StringGetAsync(key);
            return value;
        }

        /// <summary>
        /// 递增
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long Increment(IDatabase db, string key, int value = 1)
        {
            //key = MergeKey(key);
            //三种命令模式
            //Sync,同步模式会直接阻塞调用者，但是显然不会阻塞其他线程。
            //Async,异步模式直接走的是Task模型。
            //Fire - and - Forget,就是发送命令，然后完全不关心最终什么时候完成命令操作。

            //即发即弃：
            //通过配置 CommandFlags 来实现即发即弃功能，在该实例中该方法会立即返回，如果是string则返回null 如果是int则返回0.
            //这个操作将会继续在后台运行，一个典型的用法页面计数器的实现：
            return db.StringIncrement(key, value, flags: CommandFlags.FireAndForget);
        }

        /// <summary>
        /// 递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long Decrement(IDatabase db, string key, int value = 1)
        {
            //key = MergeKey(key);
            return db.StringDecrement(key, value, flags: CommandFlags.FireAndForget);
        }

        /// <summary>
        /// 判断在缓存中是否存在该key的缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(IDatabase db, string key)
        {
            //key = MergeKey(key);
            return db.KeyExists(key); //可直接调用
        }

        /// <summary>
        /// 移除指定key的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(IDatabase db, string key)
        {
            //key = MergeKey(key);
            return db.KeyDelete(key);
        }

        #endregion

        #region Redis Hash散列数据类型操作
        /// <summary>
        /// Redis散列数据类型  批量新增
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="flags"></param>
        /// <param name="hashEntrys"></param>
        /// <param name="key"></param>
        public static void HashSet(IDatabase db, string key, List<HashEntry> hashEntrys, CommandFlags flags = CommandFlags.None)
        {
            db.HashSet(key, hashEntrys.ToArray(), flags);
        }
        /// <summary>
        /// Redis散列数据类型  新增一个
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="val"></param>
        public static void HashSet<T>(IDatabase db, string key, string field, T val, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            db.HashSet(key, field, JsonHelper.SerializeObject(val), when, flags);
        }
        /// <summary>
        ///  Redis散列数据类型 获取指定key的指定field
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static T HashGet<T>(IDatabase db, string key, string field) where T : class
        {
            var data = db.HashGet(key, field);
            if (!data.HasValue)
            {
                return null;
            }
            return JsonHelper.DeserializeObject<T>(db.HashGet(key, field));
        }
        /// <summary>
        ///  Redis散列数据类型 获取所有field所有值,以 HashEntry[]形式返回
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static HashEntry[] HashGetAll(IDatabase db, string key, CommandFlags flags = CommandFlags.None)
        {
            return db.HashGetAll(key, flags);
        }
        /// <summary>
        /// Redis散列数据类型 获取key中所有field的值。
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static List<T> HashGetAllValues<T>(IDatabase db, string key, CommandFlags flags = CommandFlags.None) where T : class
        {
            List<T> list = new List<T>();
            var hashVals = db.HashValues(key, flags).ToStringArray();
            foreach (var item in hashVals)
            {
                list.Add(JsonHelper.DeserializeObject<T>(item));
            }
            return list;
        }

        /// <summary>
        /// Redis散列数据类型 获取所有Key名称
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static string[] HashGetAllKeys(IDatabase db, string key, CommandFlags flags = CommandFlags.None)
        {
            return db.HashKeys(key, flags).ToStringArray();
        }
        /// <summary>
        ///  Redis散列数据类型  单个删除field
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HashDelete(IDatabase db, string key, string hashField, CommandFlags flags = CommandFlags.None)
        {
            return db.HashDelete(key, hashField, flags);
        }
        /// <summary>
        ///  Redis散列数据类型  批量删除field
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static long HashDelete(IDatabase db, string key, string[] hashFields, CommandFlags flags = CommandFlags.None)
        {
            List<RedisValue> list = new List<RedisValue>();
            for (int i = 0; i < hashFields.Length; i++)
            {
                list.Add(hashFields[i]);
            }
            return db.HashDelete(key, list.ToArray(), flags);
        }
        /// <summary>
        ///  Redis散列数据类型 判断指定键中是否存在此field
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HashExists(IDatabase db, string key, string field, CommandFlags flags = CommandFlags.None)
        {
            return db.HashExists(key, field, flags);
        }
        /// <summary>
        /// Redis散列数据类型  获取指定key中field数量
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static long HashLength(IDatabase db, string key, CommandFlags flags = CommandFlags.None)
        {
            return db.HashLength(key, flags);
        }
        /// <summary>
        /// Redis散列数据类型  为key中指定field增加incrVal值
        /// </summary>
        /// <param name="db">redis database index</param>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="incrVal"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static double HashIncrement(IDatabase db, string key, string field, double incrVal, CommandFlags flags = CommandFlags.None)
        {
            return db.HashIncrement(key, field, incrVal, flags);
        }
        #endregion

        #region Redis List数据类型操作
        /// <summary>
        /// 移除并返回key所对应列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static string ListLeftPop(IDatabase db, string redisKey)
        {
            return db.ListLeftPop(redisKey);
        }
        /// <summary>
        /// 移除并返回key所对应列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static string ListRightPop(IDatabase db, string redisKey)
        {
            return db.ListRightPop(redisKey);
        }
        /// <summary>
        /// 移除指定key及key所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListRemove(IDatabase db, string redisKey, string redisValue)
        {
            return db.ListRemove(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表尾部插入值，如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListRightPush(IDatabase db, string redisKey, string redisValue)
        {
            return db.ListRightPush(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表头部插入值，如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListLeftPush(IDatabase db, string redisKey, string redisValue)
        {
            return db.ListLeftPush(redisKey, redisValue);
        }
        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static long ListLength(IDatabase db, string redisKey)
        {
            return db.ListLength(redisKey);
        }
        /// <summary>
        /// 截断List
        /// </summary>
        /// <param name="db"></param>
        /// <param name="redisKey"></param>
        /// <param name="start">起始元素位置</param>
        /// <param name="end">结束元素位置（包含在内)</param>
        public static void ListTrim(IDatabase db, string redisKey, long start, long end)
        {
            db.ListTrim(redisKey, start, end);
        }
        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static IEnumerable<RedisValue> ListRange(IDatabase db, string redisKey)
        {
            try
            {
                return db.ListRange(redisKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static T ListLeftPop<T>(IDatabase db, string redisKey) where T : class
        {
            var redisValue = db.ListLeftPop(redisKey);
            if (redisValue.IsNullOrEmpty)
                return null;
            return JsonHelper.DeserializeObject<T>(redisValue);
        }
        /// <summary>
        /// 移除并返回该列表上的最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static T ListRightPop<T>(IDatabase db, string redisKey) where T : class
        {
            var redisValue = db.ListRightPop(redisKey);
            if (redisValue.IsNullOrEmpty)
                return null;
            return JsonHelper.DeserializeObject<T>(redisValue);
        }
        /// <summary>
        /// 在列表尾部插入值，如果键不存在，先创建再插入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListRightPush<T>(IDatabase db, string redisKey, T redisValue)
        {
            return db.ListRightPush(redisKey, JsonHelper.SerializeObject(redisValue));
        }
        /// <summary>
        /// 在列表头部插入值，如果键不存在，创建后插入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static long ListLeftPush<T>(IDatabase db, string redisKey, T redisValue)
        {
            return db.ListLeftPush(redisKey, JsonHelper.SerializeObject(redisValue));
        }
        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<string> ListLeftPopAsync(IDatabase db, string redisKey)
        {
            return await db.ListLeftPopAsync(redisKey);
        }
        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<string> ListRightPopAsync(IDatabase db, string redisKey)
        {
            return await db.ListRightPopAsync(redisKey);
        }
        /// <summary>
        /// 移除列表指定键上与值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<long> ListRemoveAsync(IDatabase db, string redisKey, string redisValue)
        {
            return await db.ListRemoveAsync(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表尾部差入值，如果键不存在，先创建后插入
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static async Task<long> ListRightPushAsync(IDatabase db, string redisKey, string redisValue)
        {
            return await db.ListRightPushAsync(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表头部插入值，如果键不存在，先创建后插入
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static async Task<long> ListLeftPushAsync(IDatabase db, string redisKey, string redisValue)
        {
            return await db.ListLeftPushAsync(redisKey, redisValue);
        }
        /// <summary>
        /// 返回列表上的长度，如果不存在，返回0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<long> ListLengthAsync(IDatabase db, string redisKey)
        {
            return await db.ListLengthAsync(redisKey);
        }
        /// <summary>
        /// 返回在列表上键对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<RedisValue>> ListRangeAsync(IDatabase db, string redisKey)
        {
            return await db.ListRangeAsync(redisKey);
        }
        /// <summary>
        /// 移除并返回存储在key对应列表的第一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<T> ListLeftPopAsync<T>(IDatabase db, string redisKey) where T : class
        {
            var redisValue = await db.ListLeftPopAsync(redisKey);
            if (redisValue.IsNullOrEmpty)
                return null;
            return JsonHelper.DeserializeObject<T>(redisValue);
        }
        /// <summary>
        /// 移除并返回存储在key 对应列表的最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public static async Task<T> ListRightPopAsync<T>(IDatabase db, string redisKey) where T : class
        {
            var redisValue = await db.ListRightPopAsync(redisKey);
            if (redisValue.IsNullOrEmpty)
                return null;
            return JsonHelper.DeserializeObject<T>(redisValue);
        }
        /// <summary>
        /// 在列表尾部插入值，如果值不存在，先创建后写入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static async Task<long> ListRightPushAsync<T>(IDatabase db, string redisKey, T redisValue)
        {
            return await db.ListRightPushAsync(redisKey, JsonHelper.SerializeObject(redisValue));
        }
        /// <summary>
        /// 在列表头部插入值，如果值不存在，先创建后写入值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public static async Task<long> ListLeftPushAsync<T>(IDatabase db, string redisKey, T redisValue)
        {
            return await db.ListLeftPushAsync(redisKey, JsonHelper.SerializeObject(redisValue));
        }
        #endregion
    }

    /// <summary>
    /// Redis配置类
    /// </summary>
    public class RedisConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Pass { get; set; }
    }
}
