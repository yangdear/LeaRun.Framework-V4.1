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
    /// 岗位管理
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.07 15:34</date>
    /// </author>
    /// </summary>
    public class Base_PostBll : RepositoryFactory<Base_Post>
    {
        /// <summary>
        /// 获取岗位列表
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
                            FROM    ( SELECT    post.PostId ,                   --岗位ID
                                                post.Code ,                     --岗位编码
                                                post.FullName ,                 --岗位名称
                                                post.DepartmentId ,             --所在部门Id
                                                dep.FullName AS DepartmentName ,--所在部门
                                                post.CompanyId ,                --所在公司Id
                                                cpy.FullName AS CompanyName ,   --所在公司
                                                post.Enabled ,                  --是否有效
                                                post.Remark ,                   --岗位描述
                                                post.SortCode                   --排序码
                                      FROM      Base_Post post
                                                LEFT JOIN Base_Department dep ON dep.DepartmentId = post.DepartmentId
                                                LEFT JOIN Base_Company cpy ON cpy.CompanyId = post.CompanyId
                                    ) T
                            WHERE   1 = 1 ");
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
                strSql.Append(" AND ( PostId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            return Repository().FindTablePageBySql(strSql.ToString(), parameter.ToArray(), ref jqgridparam);
        }
    }
}