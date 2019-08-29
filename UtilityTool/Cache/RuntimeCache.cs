using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using UtilityTool.Cache.Interface;
using UtilityTool.Enum;

namespace UtilityTool.Cache
{
    public class RuntimeCache : ICache
    {
        private readonly MemoryCache _Cache;
        public DateTimeOffset _DateTimeOffset;
        /// <summary>
        /// default cache time 5 minutes
        /// </summary>
        public RuntimeCache() : this(MemoryCache.Default, DateTimeOffset.Now.AddMinutes(5))
        {

        }
        public RuntimeCache(MemoryCache memoryCache, DateTimeOffset dateTimeOffset)
        {
            _Cache = memoryCache;
            _DateTimeOffset = dateTimeOffset;
        }

        /// <summary>
        /// 取得快取內容
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TResult Get<TResult>(string key)
        {
            TResult result = default(TResult);
            if (Exists(key))
            {
                if (_Cache.Get(key) is TResult)
                {
                    result = (TResult)_Cache.Get(key);
                }
                else
                {
                    throw new NotSupportedException("Target type is not exist");
                }
            }
            return result;
        }

        /// <summary>
        /// 清除key的快取
        /// </summary>
        /// <param name="key"></param>
        public void Clear(string key)
        {
            if (Exists(key))
            {
                _Cache.Remove(key);
            }
        }

        /// <summary>
        /// 清除所有快取
        /// </summary>
        /// <param name="key"></param>
        public void Clear()
        {
            _Cache.Dispose();
        }

        public void Save(string key, object data, CacheTypeEnum type, int seconds)
        {
            if (!Exists(key))
            {
                CacheItemPolicy policy = null;
                switch (type)
                {
                    case CacheTypeEnum.AbsoluteExpiration:
                        policy = GetAbsoluteExpirationPolicy(seconds);
                        break;
                    case CacheTypeEnum.SlidingExpiration:
                        policy = GetSlidingExpirationPolicy(seconds);
                        break;
                }
                _Cache.Set(key, data, policy);
            }
        }


        #region private method

        /// <summary>
        /// 檢查快取是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool Exists(string key)
        {
            return _Cache.Contains(key);
        }

        /// <summary>
        /// 取得SlidingExpirationPolicy
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private CacheItemPolicy GetSlidingExpirationPolicy(int seconds)
        {
            var policy = new CacheItemPolicy()
            {
                SlidingExpiration = TimeSpan.FromSeconds(seconds)
            };
            return policy;
        }

        /// <summary>
        /// 取得AbsoluteExpirationPolicy
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private CacheItemPolicy GetAbsoluteExpirationPolicy(int seconds)
        {
            var policy = new CacheItemPolicy()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(seconds)
            };
            return policy;
        }
        #endregion
    }
}
