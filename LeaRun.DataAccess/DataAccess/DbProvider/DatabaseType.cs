using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.DataAccess
{
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// 数据库类型：Oracle
        /// </summary>
        Oracle,
        /// <summary>
        /// 数据库类型：SqlServer
        /// </summary>
        SqlServer,
        /// <summary>
        /// 数据库类型：Access
        /// </summary>
        Access,
        /// <summary>
        /// 数据库类型：MySql
        /// </summary>
        MySql,
        /// <summary>
        /// 数据库类型：SQLite
        /// </summary>
        SQLite
    }
}
