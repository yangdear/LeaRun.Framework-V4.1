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
    /// 部门管理
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.07 12:34</date>
    /// </author>
    /// </summary>
    public class Base_DepartmentBll : RepositoryFactory<Base_Department>
    {
        /// <summary>
        /// 获取 公司、部门 列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTree()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    CompanyId,				--公司ID
												CompanyId AS DepartmentId ,--部门ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                ParentId ,				--节点ID
                                                SortCode,				--排序编码
                                                'Company' AS Sort		--分类
                                      FROM      Base_Company			--公司表
                                      UNION
                                      SELECT    CompanyId,				--公司ID
												DepartmentId,			--部门ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                CompanyId AS ParentId ,	--节点ID
                                                SortCode,				--排序编码
                                                'Department' AS Sort	--分类
                                      FROM      Base_Department			--部门表ParentId=0
                                    ) T WHERE 1=1 ");
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( DepartmentId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" ORDER BY SortCode ASC");
            return Repository().FindTableBySql(strSql.ToString());
        }
        /// <summary>
        /// 根据公司id获取部门 列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        public DataTable GetList(string CompanyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    d.DepartmentId ,			--主键
                                                c.FullName AS CompanyName ,	--所属公司
                                                d.CompanyId ,				--所属公司Id
                                                d.Code ,					--编码
                                                d.FullName ,				--部门名称
                                                d.ShortName ,				--部门简称
                                                d.Nature ,					--部门性质
                                                d.Manager ,					--负责人
                                                d.Phone ,					--电话
                                                d.Fax ,						--传真
                                                d.Enabled ,					--有效
                                                d.SortCode,                 --排序吗
                                                d.Remark					--说明
                                      FROM      Base_Department d
                                                LEFT JOIN Base_Company c ON c.CompanyId = d.CompanyId
                                    ) T WHERE 1=1 ");
            List<DbParameter> parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strSql.Append(" AND CompanyId = @CompanyId");
                parameter.Add(DbFactory.CreateDbParameter("@CompanyId", CompanyId));
            }
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( DepartmentId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" ORDER BY CompanyId ASC,SortCode ASC");
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
    }
}