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
    /// 角色管理
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.07 15:34</date>
    /// </author>
    /// </summary>
    public class Base_RolesBll : RepositoryFactory<Base_Roles>
    {
        /// <summary>
        /// 根据公司id获取角色 列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="jqgridparam">分页条件</param>
        /// <returns></returns>
        public DataTable GetPageList(string CompanyId, ref JqGridParam jqgridparam)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    r.RoleId ,					--主键
                                                r.CompanyId ,				--所属公司Id
                                                c.FullName AS CompanyName ,	--所属公司
                                                r.Code ,					--编码
                                                r.FullName ,				--名称
                                                isnull(U.Qty,0) AS MemberCount,--成员人数
                                                r.Category ,			    --分类
                                                r.Enabled ,					--有效
                                                r.SortCode ,				--排序码
                                                r.Remark					--说明
                                      FROM      Base_Roles r
                                                LEFT JOIN Base_Company c ON c.CompanyId = r.CompanyId
                                                LEFT JOIN ( SELECT  COUNT(1) AS Qty ,
                                                                    ObjectId
                                                            FROM    Base_ObjectUserRelation
                                                            WHERE   Category = '2'
                                                            GROUP BY ObjectId
                                                          ) U ON U.ObjectId = R.RoleId
                                    ) T WHERE 1=1 ");
            if (!string.IsNullOrEmpty(CompanyId))
            {
                strSql.Append(" AND CompanyId = @CompanyId");
                parameter.Add(DbFactory.CreateDbParameter("@CompanyId", CompanyId));
            }
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( RoleId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            return Repository().FindTablePageBySql(strSql.ToString(), parameter.ToArray(), ref jqgridparam);
        }
    }
}