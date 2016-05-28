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
using System.Data;
using System.Data.Common;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 用户组管理
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.07 15:36</date>
    /// </author>
    /// </summary>
    public class Base_GroupUserBll : RepositoryFactory<Base_GroupUser>
    {
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="DepartmentId">部门ID</param>
        /// <param name="jqgridparam">分页条件</param>
        /// <returns></returns>
        public DataTable GetPageList(string CompanyId, string DepartmentId, ref JqGridParam jqgridparam)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    gu.GroupUserId ,              --用户组ID
                                                gu.Code ,                     --用户组编码
                                                gu.FullName ,                 --用户组名称
                                                gu.DepartmentId ,             --所在部门Id
                                                dep.FullName AS DepartmentName ,--所在部门
                                                gu.CompanyId ,                --所在公司Id
                                                cpy.FullName AS CompanyName , --所在公司
                                                gu.Enabled ,                  --是否有效
                                                gu.Remark ,                   --岗位描述
                                                gu.SortCode                   --排序码
                                      FROM      Base_GroupUser gu
                                                LEFT JOIN Base_Department dep ON dep.DepartmentId = gu.DepartmentId
                                                LEFT JOIN Base_Company cpy ON cpy.CompanyId = gu.CompanyId
                                    ) T WHERE   1 = 1 ");
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strSql.Append(" AND CompanyId = @CompanyId");
                parameter.Add(DbFactory.CreateDbParameter("@CompanyId", CompanyId));
            }
            if (!string.IsNullOrEmpty(DepartmentId))
            {
                strSql.Append(" AND DepartmentId = @DepartmentId");
                parameter.Add(DbFactory.CreateDbParameter("@DepartmentId", DepartmentId));
            }
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( GroupUserId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            return Repository().FindTablePageBySql(strSql.ToString(), parameter.ToArray(), ref jqgridparam);
        }
    }
}