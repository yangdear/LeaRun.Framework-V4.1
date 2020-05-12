using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Cache
{
    public interface ICache<T, V>
    {
        /// <summary>
        /// 获得缓存容器的长度
        /// </summary>
        /// <returns></returns>
        int Length();

        /// <summary>
        /// 添加缓存值 当容器中不存在key值时添加
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <returns></returns>
        int Add(T argKey, V argValue);

        /// <summary>
        /// 添加缓存值 当容器中不存在key值时添加
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        int Add(T argKey, V argValue, DateTime expiration);

        /// <summary>
        /// 添加缓存值 当容器中不存在key值时添加
        /// 当容器中存在key值修改
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <returns></returns>
        int Insert(T argKey, V argValue);

        /// <summary>
        /// 添加缓存值 当容器中不存在key值时添加
        /// 当容器中存在key值修改
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        int Insert(T argKey, V argValue, DateTime expiration);

        /// <summary>
        /// 移除缓存容器中的值
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <returns></returns>
        int Remove(T argKey);

        /// <summary>
        /// 替换缓存容器中的值
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <returns></returns>
        int Replace(T argKey, V argValue);

        /// <summary>
        /// 替换缓存容器中的值
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <param name="argValue">value值</param>
        /// <param name="expiration">过期时间</param>
        /// <returns></returns>
        int Replace(T argKey, V argValue, DateTime expiration);

        /// <summary>
        /// 获得缓存值
        /// </summary>
        /// <param name="argKey">key值</param>
        /// <returns></returns>
        V Get(T argKey);

        /// <summary>
        /// 清空所有缓存数据
        /// </summary>
        void Clear();

        /// <summary>
        /// 获得所有的key值数组
        /// </summary>
        /// <returns></returns>
        T[] GetKeys();

        /// <summary>
        /// 获得所有缓存value值数组
        /// </summary>
        /// <returns></returns>
        V[] GetValues();

        /// <summary>
        /// Key值数组属性
        /// </summary>
        T[] Keys { get; }

        /// <summary>
        /// Value值数组属性
        /// </summary>
        V[] Values { get; }

        /// <summary>
        /// 容器长度属性
        /// </summary>
        int Count { get; }
    }
}
