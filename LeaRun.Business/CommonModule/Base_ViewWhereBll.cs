//=====================================================================================
// All Rights Reserved , Copyright @ Learun 2014
// Software Developers @ Learun 2014
//=====================================================================================

using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 视图查询条件表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.04 10:10</date>
    /// </author>
    /// </summary>
    public class Base_ViewWhereBll : RepositoryFactory<Base_ViewWhere>
    {
        /// <summary>
        /// 根据模块Id获取视图查询条件列表
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public List<Base_ViewWhere> GetViewWhereList(string ModuleId)
        {
            StringBuilder WhereSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            WhereSql.Append(" AND ModuleId = @ModuleId");
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return Repository().FindList(WhereSql.ToString(), parameter.ToArray());
        }
    }
}