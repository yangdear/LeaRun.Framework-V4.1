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
using System.Linq;
using LeaRun.Cache;

namespace LeaRun.Business
{
    /// <summary>
    /// 模块权限表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.18 16:01</date>
    /// </author>
    /// </summary>
    public class Base_ModulePermissionBll : RepositoryFactory<Base_ModulePermission>
    {
        private static Base_ModulePermissionBll item;
        public static Base_ModulePermissionBll Instance
        {
            get
            {
                if (item == null)
                {
                    item = new Base_ModulePermissionBll();
                }
                return item;
            }
        }
        /// <summary>
        /// 模块权限列表
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
                strSql.Append(@"SELECT  m.ModuleId ,				--模块ID
                                        m.ParentId ,				--模块节点
                                        m.Code ,					--编码
                                        m.FullName ,				--名称
                                        m.Icon ,					--图标
                                        m.SortCode ,				--排序码
                                        cp.ModuleId AS ObjectId     --是否存在
                                FROM    Base_Module M INNER JOIN ( SELECT DISTINCT ModuleId  FROM   Base_ModulePermission");
                strSql.Append(" WHERE  ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "')) MP ON M.ModuleId = mp.ModuleId");
                strSql.Append(" LEFT JOIN ( SELECT DISTINCT ModuleId  FROM  Base_ModulePermission");
                strSql.Append(" WHERE  ObjectId = @ObjectId ) CP ON cp.ModuleId = M.ModuleId");
            }
            else
            {
                strSql.Append(@"SELECT  m.ModuleId ,				--模块ID
                                        m.ParentId ,				--模块节点
                                        m.Code ,					--编码
                                        m.FullName ,				--名称
                                        m.Icon ,					--图标
                                        m.SortCode ,				--排序吗
                                        mp.ObjectId					--是否存在
                                FROM    Base_Module m
                                        LEFT JOIN Base_ModulePermission mp ON mp.ModuleId = m.ModuleId
                                                                          AND mp.ObjectId = @ObjectId");
            }
            parameter.Add(DbFactory.CreateDbParameter("@ObjectId", ObjectId));
            strSql.Append(" ORDER BY  m.SortCode ASC ");
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 加载权限模块
        /// </summary>
        /// <param name="ObjectId">对象主键</param>
        /// <returns></returns>
        public List<Base_Module> GetModuleList(string ObjectId)
        {
            StringBuilder strSql = new StringBuilder();
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(@"SELECT DISTINCT  M.* FROM Base_Module M");
                strSql.Append(" INNER JOIN Base_ModulePermission MP ON M.ModuleId = MP.ModuleId WHERE   ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "')");
            }
            else
            {
                strSql.Append(@"SELECT * FROM Base_Module M");
            }
            strSql.Append(" ORDER BY  M.SortCode ASC ");
            return DataFactory.Database().FindListBySql<Base_Module>(strSql.ToString());
        }
        /// <summary>
        /// 根据对象Id获取模块权限列表
        /// </summary>
        /// <param name="ObjectId">对象ID</param>
        /// <returns></returns>
        public DataTable GetModulePermission(string ObjectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  m.ModuleId ,
                                    m.ParentId ,
                                    m.FullName ,
                                    m.Icon
                            FROM    Base_Module m
                                    LEFT JOIN Base_ModulePermission mp ON mp.ModuleId = m.ModuleId");
            strSql.Append(" WHERE   mp.ObjectId IN ('" + ObjectId.Replace(",", "','") + "')");
            strSql.Append(" ORDER BY  m.SortCode ASC ");
            return Repository().FindTableBySql(strSql.ToString());
        }

        /// <summary>
        /// Action执行权限认证
        /// </summary>
        /// <param name="Action">视图Action</param>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="ModuleId">模块Id</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public bool ActionAuthorize(string Action, string ObjectId, string ModuleId, string UserId)
        {
            List<Base_Module> ListData = new List<Base_Module>();
            object ActionAuthorize_List = DataCache.Get("ActionAuthorizeList_" + UserId);
            if (ActionAuthorize_List == null)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT  b.ModuleId,b.FullName ,b.ActionEvent AS Location FROM    Base_Button b INNER JOIN Base_ButtonPermission bp ON bp.ModuleButtonId = b.ButtonId AND bp.ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "')");
                strSql.Append(" UNION ");
                strSql.Append(@"SELECT  m.ModuleId,m.FullName , m.Location FROM    Base_Module m INNER JOIN Base_ModulePermission mp ON mp.ModuleId = m.ModuleId  AND mp.ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "')");
                ListData = DataFactory.Database().FindListBySql<Base_Module>(strSql.ToString());
                DataCache.Insert("ActionAuthorizeList_" + UserId, ListData);
            }
            else
            {
                ListData = (List<Base_Module>)ActionAuthorize_List;
            }
            ListData = (from entity in ListData
                        where (entity.Location.ToLower() == Action && entity.ModuleId == ModuleId)
                        select entity).ToList();
            int Count = ListData.Count;
            if (Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}