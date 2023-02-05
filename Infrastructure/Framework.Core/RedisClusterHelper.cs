using CSRedis;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Common.IOCOptions;

namespace Framework.Core
{
    /// <summary>
    /// Redis集群工具类
    /// </summary>
    public class RedisClusterHelper
    {
        private readonly RedisClusterOptions _redisOptions;
        private readonly CSRedisClient _cSRedisClient;

        public RedisClusterHelper(IOptionsMonitor<RedisClusterOptions> options)
        {
            _redisOptions = options.CurrentValue;
            // ["127.0.0.1:6379",.....]
            _cSRedisClient = new CSRedisClient(string.Join(",", _redisOptions.Hosts.ToArray()));
        }
        #region Hash操作
        /// <summary>
        /// 设置hash
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async void HashSet(string key, string field, object value)
        {
            await _cSRedisClient.HSetAsync(key, field, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// 根据key和field获取对应的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string field)
        {
            var objValue = _cSRedisClient.HGetAsync(key, field).Result;
            return JsonConvert.DeserializeObject<T>(objValue);
        }

        /// <summary>
        /// 判断是否存在某个字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public async Task<bool> ContainsKey(string key, string field)
        {
            return await _cSRedisClient.HExistsAsync(key, field);
        }

        /// <summary>
        /// 根据key获取所有字段集合
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> HashKeys(string key)
        {
            var keys = _cSRedisClient.HKeysAsync(key).Result;
            return keys.ToList();
        }

        /// <summary>
        /// 获取所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> HashValues(string key)
        {
            var keys = _cSRedisClient.HValsAsync(key).Result;
            return keys.ToList();
        }

        /// <summary>
        /// 根据字段删除信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        public void HashRemoveForFields(string key, params string[] field)
        {
            _cSRedisClient.HDelAsync(key, field).Wait();
        }
        #endregion

        #region String操作

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public void Set(string key, string value, TimeSpan time)
        {
            _cSRedisClient.Set(key, value, time);
        }

        /// <summary>
        /// 根据字符串Key获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            return _cSRedisClient.Get(key);
        }

        /// <summary>
        /// 根据Key删除Value
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _cSRedisClient.Del(key);
        }

        /// <summary>
        /// 判断字符串的Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return _cSRedisClient.Exists(key);
        }
        #endregion

    }
}
