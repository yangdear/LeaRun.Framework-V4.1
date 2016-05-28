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
    /// 视图设置权限表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.14 13:17</date>
    /// </author>
    /// </summary>
    public class Base_ViewPermissionBll : RepositoryFactory<Base_ViewPermission>
    {
        /// <summary>
        /// 视图权限列表
        /// </summary>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="Category">对象分类:1-部门2-角色3-岗位4-群组</param>
        /// <returns></returns>
        public DataTable GetList(string ObjectId, string Category)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(@"SELECT  v.*,vp.ModuleId AS ObjectId FROM  Base_View v
                                INNER JOIN Base_ViewPermission P ON v.ModuleId = P.ModuleId AND v.ViewId = P.ViewId AND p.ObjectId IN ( '" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "' )");
                strSql.Append(" LEFT JOIN ( SELECT DISTINCT *  FROM    Base_ViewPermission");
                strSql.Append(" WHERE ObjectId = @ObjectId) vp ON v.ModuleId = vP.ModuleId  AND v.ViewId = vP.ViewId");
            }
            else
            {
                strSql.Append(@"SELECT  v.ViewId ,					--ID
                                        v.ModuleId ,				--模块ID
                                        v.ShowName ,				--名称
                                        v.SortCode ,				--排序吗
                                        vp.ObjectId					--是否存在
                                FROM    Base_View v
                                        LEFT JOIN Base_ViewPermission vp ON vp.ViewId = v.ViewId
                                                                            AND vp.ObjectId = @ObjectId");
            }
            strSql.Append(" order by v.SortCode ASC");
            parameter.Add(DbFactory.CreateDbParameter("@ObjectId", ObjectId));
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 加载表格视图权限
        /// </summary>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="ModuleId">模块主键</param>
        /// <returns></returns>
        public List<Base_View> GetViewList(string ObjectId, string ModuleId)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            if (!ManageProvider.Provider.Current().IsSystem)
            {

                strSql.Append(@"SELECT  FieldName,Enabled
                                FROM    Base_View v
                                WHERE   v.ModuleId = @ModuleId AND FieldName NOT IN (
                                SELECT  FieldName FROM    Base_View v INNER JOIN Base_ViewPermission P ON v.ModuleId = P.ModuleId AND v.ViewId = P.ViewId");
                strSql.Append(" WHERE   p.ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') AND v.ModuleId = @ModuleId)");
            }
            else
            {
                strSql.Append(@"SELECT FieldName,Enabled FROM Base_View v WHERE 1=0 ");
                strSql.Append(" AND v.ModuleId = @ModuleId");
            }
            strSql.Append(" ORDER BY v.SortCode ASC ");
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return DataFactory.Database().FindListBySql<Base_View>(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 根据对象Id获取模块视图权限列表
        /// </summary>
        /// <param name="ObjectId">对象ID</param>
        /// <returns></returns>
        public DataTable GetViewPermission(string ObjectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    m.ModuleId AS ID ,
                                                m.ParentId ,
                                                m.FullName ,
                                                m.Icon ,
                                                m.SortCode
                                      FROM      Base_Module m
                                                LEFT JOIN Base_ModulePermission mp ON mp.ModuleId = m.ModuleId");
            strSql.Append(@" WHERE     mp.ObjectId IN ('" + ObjectId.Replace(",", "','") + "')");
            strSql.Append(@" UNION
                                      SELECT    v.ViewId AS ID ,
                                                v.ModuleId AS ParentId ,
                                                v.FullName ,
                                                '' AS Icon ,
                                                v.SortCode
                                      FROM      Base_View v
                                                LEFT JOIN Base_ViewPermission vp ON vp.ViewId = v.ViewId
                                      WHERE     vp.ObjectId IN ('" + ObjectId.Replace(",", "','") + "') ) A");
            strSql.Append(" ORDER BY SortCode ASC ");
            return Repository().FindTableBySql(strSql.ToString());
        }
    }
}