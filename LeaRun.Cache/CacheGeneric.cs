using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Cache
{
    public class Cache<T, V> : ICache<T, V>
    {
        private static Dictionary<T, CacheBody<V>> Dic = null;
        private object _object = new object();

        static Cache()
        {
            Dic = new Dictionary<T, CacheBody<V>>();
        }

        /// <summary>
        /// 获得缓存容器的长度
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            if (Dic != null)
            {
                return Dic.Count;
            }
            return 0;
        }

        /// <summary>
        /// 添加缓存值 当容器中不存在key值时添加
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <returns></returns>
        public int Add(T argKey, V argValue)
        {
            lock (_object)
            {
                if (Dic != null && !Dic.ContainsKey(argKey))
                {
                    CacheBody<V> body = new CacheBody<V>() { Body=argValue,DependencyFile=string.Empty, Expiration=DateTime.MaxValue };
                    Dic.Add(argKey, body);
                    return 1;
                }
                return 0;
            }
        }

        /// <summary>
        /// 添加缓存值 当容器中不存在key值时添加
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        public int Add(T argKey, V argValue, DateTime expiration)
        {
            lock (_object)
            {
                if (Dic != null && !Dic.ContainsKey(argKey))
                {
                    CacheBody<V> body = new CacheBody<V>() { Body = argValue, DependencyFile = string.Empty, Expiration = expiration };
                    Dic.Add(argKey, body);
                    return 1;
                }
                return 0;
            }
        }

        /// <summary>
        /// 添加缓存值 当容器中不存在key值时添加
        /// 当容器中存在key值修改
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <returns></returns>
        public int Insert(T argKey, V argValue)
        {
            lock (_object)
            {
                if (Dic != null)
                {
                    if (Dic.ContainsKey(argKey))
                    {
                        Dic[argKey].Body = argValue;
                    }
                    else
                    {
                        CacheBody<V> body = new CacheBody<V>() { Body = argValue, DependencyFile = string.Empty, Expiration = DateTime.MaxValue };
                        Dic.Add(argKey, body);
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 添加缓存值 当容器中不存在key值时添加
        /// 当容器中存在key值修改
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        public int Insert(T argKey, V argValue, DateTime expiration)
        {
            lock (_object)
            {
                if (Dic != null)
                {
                    if (Dic.ContainsKey(argKey))
                    {
                        Dic[argKey].Body = argValue;
                    }
                    else
                    {
                        CacheBody<V> body = new CacheBody<V>() { Body = argValue, DependencyFile = string.Empty, Expiration = expiration };
                        Dic.Add(argKey, body);
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 移除缓存容器中的值
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <returns></returns>
        public int Remove(T argKey)
        {
            lock (_object)
            {
                if (Dic != null && Dic.ContainsKey(argKey))
                {
                    Dic.Remove(argKey);
                    return 1;
                }
                return 0;
            }
        }

        /// <summary>
        /// 替换缓存容器中的值
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <returns></returns>
        public int Replace(T argKey, V argValue)
        {
            lock (_object)
            {
                if (Dic != null && Dic.ContainsKey(argKey))
                {
                    Dic[argKey].Body = argValue;
                    return 1;
                }
                return 0;
            }
        }

        /// <summary>
        /// 替换缓存容器中的值
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        public int Replace(T argKey, V argValue, DateTime expiration)
        {
            lock (_object)
            {
                if (Dic != null && Dic.ContainsKey(argKey))
                {
                    Dic[argKey].Body = argValue;
                    Dic[argKey].Expiration = expiration;
                    return 1;
                }
                return 0;
            }
        }

        /// <summary>
        /// 获得缓存值
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <returns></returns>
        public V Get(T argKey)
        {
            if (Dic != null && Dic.ContainsKey(argKey))
            {
                if (DateTime.Now <= Dic[argKey].Expiration)
                {
                    return Dic[argKey].Body;
                }
                else
                {
                    Remove(argKey);
                }
            }
            return default(V);
        }

        /// <summary>
        /// 清空所有缓存数据
        /// </summary>
        public void Clear()
        {
            if (Dic != null)
            {
                Dic.Clear();
            }
        }

        /// <summary>
        /// 获得所有的key值数组
        /// </summary>
        /// <returns></returns>
        public T[] GetKeys()
        {
            if (Dic != null)
            {
                return Dic.Keys.ToArray<T>();
            }
            return default(T[]);
        }

        /// <summary>
        /// 获得所有缓存value值数组
        /// </summary>
        /// <returns></returns>
        public V[] GetValues()
        {
            if (Dic != null)
            {
                return Dic.Values.Where(item => item.Expiration >= DateTime.Now).Select(item => item.Body).ToArray<V>();
            }
            return default(V[]);
        }

        /// <summary>
        /// Key值数组属性
        /// </summary>
        public T[] Keys
        {
            get
            {
                return GetKeys();
            }
        }

        /// <summary>
        /// Value值数组属性
        /// </summary>
        public V[] Values
        {
            get
            {
                return GetValues();
            }
        }

        /// <summary>
        /// 容器长度属性
        /// </summary>
        public int Count
        {
            get
            {
                return Length();
            }
        }
    }
}
