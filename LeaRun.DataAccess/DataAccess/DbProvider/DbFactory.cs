using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace LeaRun.DataAccess
{
    /// <summary>
    /// 数据库服务工厂。
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        public static string CreateDbParmCharacter()
        {
            string character = string.Empty;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    character = "@";
                    break;
                case DatabaseType.Oracle:
                    character = ":";
                    break;
                case DatabaseType.MySql:
                    character = "?";
                    break;
                case DatabaseType.Access:
                    character = "@";
                    break;
                case DatabaseType.SQLite:
                    character = "@";
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return character;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型和传入的
        /// 数据库链接字符串来创建相应数据库连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DbConnection CreateDbConnection(string connectionString)
        {
            DbConnection conn = null;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    conn = new SqlConnection(connectionString);
                    break;
                case DatabaseType.Oracle:
                    conn = new OracleConnection(connectionString);
                    break;
                case DatabaseType.MySql:
                    conn = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.Access:
                    conn = new OleDbConnection(connectionString);
                    break;
                case DatabaseType.SQLite:
                    conn = new SQLiteConnection(connectionString);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return conn;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库命令对象
        /// </summary>
        /// <returns></returns>
        public static DbCommand CreateDbCommand()
        {
            DbCommand cmd = null;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    cmd = new SqlCommand();
                    break;
                case DatabaseType.Oracle:
                    cmd = new OracleCommand();
                    break;
                case DatabaseType.MySql:
                    cmd = new MySqlCommand();
                    break;
                case DatabaseType.Access:
                    cmd = new OleDbCommand();
                    break;
                case DatabaseType.SQLite:
                    cmd = new SQLiteCommand();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return cmd;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库适配器对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataAdapter CreateDataAdapter()
        {
            IDbDataAdapter adapter = null;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    adapter = new SqlDataAdapter();
                    break;
                case DatabaseType.Oracle:
                    adapter = new OracleDataAdapter();
                    break;
                case DatabaseType.MySql:
                    adapter = new MySqlDataAdapter();
                    break;
                case DatabaseType.Access:
                    adapter = new OleDbDataAdapter();
                    break;
                case DatabaseType.SQLite:
                    adapter = new SQLiteDataAdapter();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return adapter;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 和传入的命令对象来创建相应数据库适配器对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataAdapter CreateDataAdapter(DbCommand cmd)
        {
            IDbDataAdapter adapter = null;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    adapter = new SqlDataAdapter((SqlCommand)cmd);
                    break;
                case DatabaseType.Oracle:
                    adapter = new OracleDataAdapter((OracleCommand)cmd);
                    break;
                case DatabaseType.MySql:
                    adapter = new MySqlDataAdapter((MySqlCommand)cmd);
                    break;
                case DatabaseType.Access:
                    adapter = new OleDbDataAdapter((OleDbCommand)cmd);
                    break;
                case DatabaseType.SQLite:
                    adapter = new SQLiteDataAdapter((SQLiteCommand)cmd);
                    break;
                default: throw new Exception("数据库类型目前不支持！");
            }
            return adapter;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter()
        {
            DbParameter param = null;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    param = new SqlParameter();
                    break;
                case DatabaseType.Oracle:
                    param = new OracleParameter();
                    break;
                case DatabaseType.MySql:
                    param = new MySqlParameter();
                    break;
                case DatabaseType.Access:
                    param = new OleDbParameter();
                    break;
                case DatabaseType.SQLite:
                    param = new SQLiteParameter();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value)
        {
            DbParameter param = DbFactory.CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value, DbType dbType)
        {
            DbParameter param = DbFactory.CreateDbParameter();
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value, DbType dbType, int size)
        {
            DbParameter param = DbFactory.CreateDbParameter();
            param.DbType = dbType;
            param.ParameterName = paramName;
            param.Value = value;
            param.Size = size;
            return param;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbParameter(string paramName, object value, int size)
        {
            DbParameter param = DbFactory.CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;
            param.Size = size;
            return param;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter CreateDbOutParameter(string paramName, int size)
        {
            DbParameter param = DbFactory.CreateDbParameter();
            param.Direction = ParameterDirection.Output;
            param.ParameterName = paramName;
            param.Size = size;
            return param;
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 和传入的参数来创建相应数据库的sql语句对应参数对象
        /// </summary>
        /// <returns></returns>
        public static DbParameter[] CreateDbParameters(int size)
        {
            int i = 0;
            DbParameter[] param = null;
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    param = new SqlParameter[size];
                    while (i < size) { param[i] = new SqlParameter(); i++; }
                    break;
                case DatabaseType.Oracle:
                    param = new OracleParameter[size];
                    while (i < size) { param[i] = new OracleParameter(); i++; }
                    break;
                case DatabaseType.MySql:
                    param = new MySqlParameter[size];
                    while (i < size) { param[i] = new MySqlParameter(); i++; }
                    break;
                case DatabaseType.Access:
                    param = new OleDbParameter[size];
                    while (i < size) { param[i] = new OleDbParameter(); i++; }
                    break;
                case DatabaseType.SQLite:
                    param = new SQLiteParameter[size];
                    while (i < size) { param[i] = new SQLiteParameter(); i++; }
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return param;
        }
    }
}
