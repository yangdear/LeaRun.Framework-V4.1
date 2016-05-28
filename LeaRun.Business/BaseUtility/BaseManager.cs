using LeaRun.DataAccess;
using LeaRun.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 定义通用的功能
    /// <author>
    ///		<name>shecixiong</name>
    ///		<date>2014.02.28</date>
    /// </author>
    /// </summary>
    public class BaseManager : IBaseManager
    {
        private IDatabase db = DataFactory.Database();

        #region 获取最大编号+1
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FieldName">字段名称</param>
        /// <returns></returns>
        public object GetSortCode<T>(string FieldName) where T : new()
        {
            object obj = db.FindMax<T>(FieldName);
            if (!string.IsNullOrEmpty(obj.ToString()))
            {
                return Convert.ToInt32(obj) + 1;
            }
            return 10000001;
        }
        #endregion

        #region 验证对象值不能重复
        /// <summary>
        /// 验证对象值不能重复
        /// </summary>
        /// <param name="tablename">实体类</param>
        /// <param name="fieldname">属性字段</param>
        /// <param name="fieldvalue">属性字段值</param>
        /// <param name="keyfield">主键字段</param>
        /// <param name="keyvalue">主键字段值</param>
        /// <returns></returns>
        public bool FieldExist(string tablename, string fieldname, string fieldvalue, string keyfield, string keyvalue)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append("Select count(1) From " + tablename + " where 1=1");
            strSql.Append(" AND " + fieldname + " = @fieldname");
            parameter.Add(DbFactory.CreateDbParameter("@fieldname", fieldvalue));
            if (!string.IsNullOrEmpty(keyfield))
            {
                strSql.Append(" AND " + keyvalue + " != @keyfield");
                parameter.Add(DbFactory.CreateDbParameter("@keyfield", keyfield));
            }
            int Msg = DataFactory.Database().FindCountBySql(strSql.ToString(), parameter.ToArray());
            if (Msg == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
