using LeaRun.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Repository
{
    /// <summary>
    /// 操作数据库工厂
    /// </summary>
    public class DataFactory
    {
        private static readonly Object locker = new Object();
        private static Database db = null;
        /// <summary>
        /// 获取指定的数据库连接
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static IDatabase Database(string connString)
        {
            //在并发时，使用单一对象
            if (db == null)
            {
                return db = new Database(connString);
            }
            else
            {
                lock (locker)
                {
                    return db;
                }
            }
        }
        /// <summary>
        /// 获取指定的数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDatabase Database()
        {
            return Database("LeaRunFramework_SqlServer");
        }
    }
}
