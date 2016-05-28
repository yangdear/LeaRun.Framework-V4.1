using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 模块按钮
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.07.09 12:41</date>
    /// </author>
    /// </summary>
    public class Base_ButtonBll : RepositoryFactory<Base_Button>
    {
        /// <summary>
        /// 获取按钮列表
        /// </summary>
        /// <param name="ModuleId">模块ID</param>
        /// <param name="Category">分类：1-工具栏，2：右击栏</param>
        /// <returns></returns>
        public List<Base_Button> GetList(string ModuleId, string Category)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Base_Button WHERE 1=1");
            strSql.Append(" AND ModuleId = @ModuleId ");
            strSql.Append(" AND Category = @Category ");
            strSql.Append(" ORDER BY SortCode ASC");
            List<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            parameter.Add(DbFactory.CreateDbParameter("@Category", Category));
            return Repository().FindListBySql(strSql.ToString(), parameter.ToArray());
        }
    }
}
