using System;
using System.Linq;

using System.Runtime.Caching;

namespace EcConfig.Core.Cache
{
    public static class CacheManager
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;

        /// <summary>
        /// Add value inside cache associated to a defined key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Add<T>(string key, T value)
        {
            Cache.Add(key, value, DateTime.Now.AddDays(1));
        }

        /// <summary>
        /// Get cache value according to a defined key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return (T) Cache.Get(key);
        }

        /// <summary>
        /// Define if cache contains data for defined key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exist(string key)
        {
            return Cache.Any(x => x.Key == key);
        }

        /// <summary>
        /// Clear cache for a defined key
        /// </summary>
        /// <param name="key"></param>
        public static void Clear(string key)
        {
            Cache.Remove(key);
        }
    }
}
