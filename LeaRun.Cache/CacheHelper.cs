using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace LeaRun.Cache
{
    public class CacheHelper
    {
        private static System.Web.Caching.Cache cache = HttpRuntime.Cache;

        /// <summary>
        /// 插入缓存，如果存在则替换
        /// </summary>
        /// <param name="argKey"></param>
        /// <param name="argValue"></param>
        /// <returns></returns>
        public static int Insert(string argKey, object argValue)
        {
            cache.Insert(argKey,argValue);
            return 1;
        }
        /// <summary>
        /// 插入缓存，如果存在则替换
        /// </summary>
        /// <param name="argKey"></param>
        /// <param name="argValue"></param>
        /// <param name="argDependency"></param>
        /// <returns></returns>
        public static int Insert(string argKey, object argValue, CacheDependency argDependency)
        {
            cache.Insert(argKey, argValue,argDependency);
            return 1;
        }
        /// <summary>
        /// 插入缓存，如果存在则替换
        /// </summary>
        /// <param name="argKey"></param>
        /// <param name="argValue"></param>
        /// <param name="argDependency"></param>
        /// <param name="argExpiration"></param>
        /// <returns></returns>
        public static int Insert(string argKey, object argValue, CacheDependency argDependency, DateTime argExpiration)
        {
            cache.Insert(argKey, argValue, argDependency, argExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
            return 1;
        }
        /// <summary>
        /// 添加缓存，如果存在则抛出异常
        /// </summary>
        /// <param name="argKey"></param>
        /// <param name="argValue"></param>
        /// <returns></returns>
        public static int Add(string argKey, object argValue)
        {
            cache.Add(argKey, argValue, null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration,CacheItemPriority.Default,null);
            return 1;
        }
        /// <summary>
        /// 添加缓存，如果存在则抛出异常
        /// </summary>
        /// <param name="argKey"></param>
        /// <param name="argValue"></param>
        /// <param name="argDependency"></param>
        /// <returns></returns>
        public static int Add(string argKey, object argValue, CacheDependency argDependency)
        {
            cache.Add(argKey, argValue, argDependency, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            return 1;
        }
        /// <summary>
        /// 添加缓存，如果存在则抛出异常
        /// </summary>
        /// <param name="argKey"></param>
        /// <param name="argValue"></param>
        /// <param name="argDependency"></param>
        /// <param name="argExpiration"></param>
        /// <returns></returns>
        public static int Add(string argKey, object argValue, CacheDependency argDependency, DateTime argExpiration)
        {
            cache.Add(argKey,argValue,argDependency,argExpiration,System.Web.Caching.Cache.NoSlidingExpiration,CacheItemPriority.Default,null);
            return 0;
        }
        /// <summary>
        /// 返回缓存中的所有数据行
        /// </summary>
        /// <returns></returns>
        public static int Count()
        {
            return cache.Count;
        }

        /// <summary>
        /// 根据键值获得缓存值
        /// </summary>
        /// <param name="argKey"></param>
        /// <returns></returns>
        public static object Get(string argKey)
        {
            return cache[argKey];
        }

        /// <summary>
        /// 根据键值获得特定类型的缓存值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argKey"></param>
        /// <returns></returns>
        public static T Get<T>(string argKey)
        {
            if (cache[argKey] != null)
            {
                return (T)cache[argKey];
            }
            return default(T);
        }
        /// <summary>
        /// 移除特定的键值
        /// </summary>
        /// <param name="argKey"></param>
        /// <returns></returns>
        public static int Remove(string argKey)
        {
            cache.Remove(argKey);
            return 1;
        }
    }
}
