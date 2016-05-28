using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.DataAccess.DbProvider
{
    /// <summary>
    /// 有关数据库操作的定义。
    /// </summary>
    public enum DbOperation
    {
        /// <summary>
        /// 查询
        /// </summary>
        Select,
        /// <summary>
        /// 插入
        /// </summary>
        Insert,
        /// <summary>
        /// 更新
        /// </summary>
        Update,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 截取
        /// </summary>
        Truncate
    }
}
