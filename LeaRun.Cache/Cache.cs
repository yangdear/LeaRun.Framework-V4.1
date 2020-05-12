using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Cache
{
    public class Cache : ICache
    {
        private static Dictionary<object, CacheBody> Dic = null;
        private object _object = new object();

        static Cache()
        {
            if (Dic == null)
            {
                Dic = new Dictionary<object, CacheBody>();
            }
        }

        /// <summary>
        /// 获得缓存容器的长度
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            if (Dic != null)
            {
                return Dic.Count();
            }
            return 0;
        }

        /// <summary>
        /// 添加缓存值 当容器中不存在key值时添加
        /// 过期时间无限延长，不依赖任何文件
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <returns></returns>
        public int Add(object argKey, object argValue)
        {
            lock (_object)
            {
                if (Dic != null && !Dic.ContainsKey(argKey))
                {
                    CacheBody body = new CacheBody() { Body=argValue,Expiration=DateTime.MaxValue,DependencyFile=string.Empty };
                    Dic.Add(argKey,body);
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
        public int Add(object argKey, object argValue, DateTime expiration)
        {
            lock (_object)
            {
                if (Dic != null && !Dic.ContainsKey(argKey))
                {
                    CacheBody body = new CacheBody() { Body = argValue, Expiration = expiration, DependencyFile = string.Empty };
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
        public int Insert(object argKey, object argValue)
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
                        CacheBody body = new CacheBody() { Body = argValue, Expiration = DateTime.MaxValue, DependencyFile = string.Empty };
                        Dic.Add(argKey, body);
                    }
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
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        public int Insert(object argKey, object argValue, DateTime expiration)
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
                        CacheBody body = new CacheBody() { Body = argValue, Expiration = expiration, DependencyFile = string.Empty };
                        Dic.Add(argKey, body);
                    }
                    return 1;
                }
                return 0;
            }
        }

        

        /// <summary>
        /// 移除缓存容器中的值
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <returns></returns>
        public int Remove(object argKey)
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
        public int Replace(object argKey, object argValue)
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
        public int Replace(object argKey, object argValue, DateTime expiration)
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
        public object Get(object argKey)
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
            return null;
        }

        /// <summary>
        /// 清空所有缓存数据
        /// </summary>
        public void Clear()
        {
            lock (_object)
            {
                if (Dic != null)
                {
                    Dic.Clear();
                }
            }
        }

        /// <summary>
        /// 获得所有的key值数组
        /// </summary>
        /// <returns></returns>
        public object[] GetKeys()
        {
            if (Dic != null)
            {
                return Dic.Keys.ToArray();
            }
            return null;
        }

        /// <summary>
        /// 获得所有缓存value值数组
        /// </summary>
        /// <returns></returns>
        public object[] GetValues()
        {
            if (Dic != null)
            {
                return Dic.Values.Where(item=>DateTime.Now<=item.Expiration).Select(item => item.Body).ToArray();
            }
            return null;
        }

        /// <summary>
        /// Key值数组属性
        /// </summary>
        public object[] Keys
        {
            get
            {
                return GetKeys();
            }
        }

        /// <summary>
        /// Value值数组属性
        /// </summary>
        public object[] Values
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
