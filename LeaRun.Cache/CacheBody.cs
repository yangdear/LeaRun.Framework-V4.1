using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Cache
{
    public class CacheBody
    {
        public CacheBody()
        { 
        }

        /// <summary>
        /// 存储数据
        /// </summary>
        public object Body { get; set; }

        /// <summary>
        /// 数据有效期
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// 依赖文件
        /// </summary>
        public string DependencyFile { get; set; }
    }
}
