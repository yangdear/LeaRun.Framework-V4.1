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
    /// 数据范围权限表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.22 15:28</date>
    /// </author>
    /// </summary>
    public class Base_DataScopePermissionBll : RepositoryFactory<Base_DataScopePermission>
    {
        private static Base_DataScopePermissionBll item;
        /// <summary>
        /// 静态化
        /// </summary>
        public static Base_DataScopePermissionBll Instance
        {
            get
            {
                if (item == null)
                {
                    item = new Base_DataScopePermissionBll();
                }
                return item;
            }
        }
        /// <summary>
        /// 新建的项目数据，默认把数据权限设置了这样就别必要要去数据权限管理里面去打钩
        /// </summary>
        /// <param name="ModuleId">模块主键</param>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="ResourceId">对什么资源</param>
        /// <param name="isOpenTrans">事务</param>
        public void AddScopeDefault(string ModuleId, string ObjectId, string ResourceId, DbTransaction isOpenTrans = null)
        {
            Base_DataScopePermission entity = new Base_DataScopePermission();
            entity.Create();
            entity.ModuleId = ModuleId;
            entity.ObjectId = ObjectId;
            entity.Category = "5";
            entity.ResourceId = ResourceId;
            if (isOpenTrans != null)
            {
                Repository().Insert(entity, isOpenTrans);
            }
            else
            {
                Repository().Insert(entity);
            }
        }

        #region 公司管理
        /// <summary>
        /// 加载公司列表
        /// <param name="ModuleId">模块主键</param>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="Category">对象分类:1-部门2-角色3-岗位4-群组</param>
        /// </summary>
        /// <returns></returns>
        public DataTable GetScopeCompanyList(string ModuleId, string ObjectId, string Category)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  c.CompanyId ,				--公司ID
                                    c.ParentId ,				--公司节点
                                    c.Code ,					--编码
                                    c.FullName ,				--名称
                                    c.SortCode ,				--排序吗
                                    dsp.ObjectId				--是否存在
                            FROM    Base_Company c
                                    LEFT JOIN Base_DataScopePermission dsp ON c.CompanyId = dsp.ResourceId
												                                AND dsp.ObjectId = @ObjectId
                                                                                AND dsp.Category = @Category
                                                                                AND dsp.ModuleId = @ModuleId");
            strSql.Append(" WHERE   1 = 1");
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( CompanyId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" order by c.SortCode ASC");
            parameter.Add(DbFactory.CreateDbParameter("@ObjectId", ObjectId));
            parameter.Add(DbFactory.CreateDbParameter("@Category", Category));
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        #endregion

        #region 部门管理
        /// <summary>
        /// 加载部门列表
        /// <param name="ModuleId">模块主键</param>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="Category">对象分类:1-部门2-角色3-岗位4-群组</param>
        /// </summary>
        /// <returns></returns>
        public DataTable GetScopeDepartmentList(string ModuleId, string ObjectId, string Category)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  T.* ,
                                    dsp.ObjectId
                            FROM    ( SELECT    CompanyId ,				--公司ID
                                                CompanyId AS DepartmentId ,--部门ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                ParentId ,				--节点ID
                                                SortCode ,				--排序编码
                                                'Company' AS Sort		--分类
                                      FROM      Base_Company			--公司表
                                      UNION
                                      SELECT    CompanyId ,				--公司ID
                                                DepartmentId ,			--部门ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                CompanyId AS ParentId ,	--节点ID
                                                SortCode ,				--排序编码
                                                'Department' AS Sort	--分类
                                      FROM      Base_Department			--部门表
          
                                    ) T
                                    LEFT JOIN Base_DataScopePermission dsp ON T.DepartmentId = dsp.ResourceId
                                                                              AND dsp.ObjectId = @ObjectId
                                                                              AND dsp.Category = @Category
                                                                              AND dsp.ModuleId = @ModuleId");
            strSql.Append(" WHERE 1 = 1");
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( DepartmentId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" order by T.SortCode ASC");
            parameter.Add(DbFactory.CreateDbParameter("@ObjectId", ObjectId));
            parameter.Add(DbFactory.CreateDbParameter("@Category", Category));
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        #endregion

        #region 角色管理
        /// <summary>
        /// 加载角色列表
        /// <param name="ModuleId">模块主键</param>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="Category">对象分类:1-部门2-角色3-岗位4-群组</param>
        /// </summary>
        /// <returns></returns>
        public DataTable GetScopeRoleList(string ModuleId, string ObjectId, string Category)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  T.* ,
                                    dsp.ObjectId
                            FROM    ( SELECT    CompanyId ,				--公司ID
                                                CompanyId AS RoleId ,	--角色ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                ParentId ,				--节点ID
                                                SortCode ,				--排序编码
                                                'Company' AS Sort		--分类
                                      FROM      Base_Company			--公司表
                                      UNION
                                      SELECT    CompanyId ,				--公司ID
                                                RoleId ,				--角色ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                CompanyId AS ParentId ,	--节点ID
                                                SortCode ,				--排序编码
                                                'Roles' AS Sort			--分类
                                      FROM      Base_Roles
                                    ) T
                                    LEFT JOIN Base_DataScopePermission dsp ON T.RoleId = dsp.ResourceId
                                                                              AND dsp.ObjectId = @ObjectId
                                                                              AND dsp.Category = @Category
                                                                              AND dsp.ModuleId = @ModuleId");
            strSql.Append(" WHERE 1 = 1");
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( RoleId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" order by T.SortCode ASC");
            parameter.Add(DbFactory.CreateDbParameter("@ObjectId", ObjectId));
            parameter.Add(DbFactory.CreateDbParameter("@Category", Category));
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        #endregion

        #region 岗位管理
        /// <summary>
        /// 加载岗位列表
        /// <param name="ModuleId">模块主键</param>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="Category">对象分类:1-部门2-角色3-岗位4-群组</param>
        /// </summary>
        /// <returns></returns>
        public DataTable GetScopePostList(string ModuleId, string ObjectId, string Category)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  T.* ,
                                    dsp.ObjectId
                            FROM    ( SELECT    CompanyId AS Id ,		--公司ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                ParentId ,				--节点ID
                                                SortCode ,				--排序编码
                                                'Company' AS Sort		--分类
                                      FROM      Base_Company			--公司表
                                      UNION
                                      SELECT    DepartmentId AS Id,		--部门ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                CompanyId AS ParentId ,	--节点ID
                                                SortCode ,				--排序编码
                                                'Department' AS Sort	--分类
                                      FROM      Base_Department			--部门表
                                      UNION
                                      SELECT    PostId AS Id,			--岗位ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                DepartmentId AS ParentId ,--节点ID
                                                SortCode ,				--排序编码
                                                'Post' AS Sort			--分类
                                      FROM      Base_Post				--岗位表
          
                                    ) T
                                    LEFT JOIN Base_DataScopePermission dsp ON T.Id = dsp.ResourceId
                                                                              AND dsp.ObjectId = @ObjectId
                                                                              AND dsp.Category = @Category
                                                                              AND dsp.ModuleId = @ModuleId");
            strSql.Append(" WHERE 1 = 1");
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( Id IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" order by T.SortCode ASC");
            parameter.Add(DbFactory.CreateDbParameter("@ObjectId", ObjectId));
            parameter.Add(DbFactory.CreateDbParameter("@Category", Category));
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        #endregion

        #region 用户组管理
        /// <summary>
        /// 加载用户组列表
        /// <param name="ModuleId">模块主键</param>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="Category">对象分类:1-部门2-角色3-岗位4-群组</param>
        /// </summary>
        /// <returns></returns>
        public DataTable GetScopeUserGroupList(string ModuleId, string ObjectId, string Category)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  T.* ,
                                    dsp.ObjectId
                            FROM    ( SELECT    CompanyId AS Id ,		--公司ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                ParentId ,				--节点ID
                                                SortCode ,				--排序编码
                                                'Company' AS Sort		--分类
                                      FROM      Base_Company			--公司表
                                      UNION
                                      SELECT    DepartmentId AS Id,		--部门ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                CompanyId AS ParentId ,	--节点ID
                                                SortCode ,				--排序编码
                                                'Department' AS Sort	--分类
                                      FROM      Base_Department			--部门表
                                      UNION
                                      SELECT    GroupUserId AS Id,		--用户组ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                DepartmentId AS ParentId ,--节点ID
                                                SortCode ,				--排序编码
                                                'UserGroup' AS Sort		--分类
                                      FROM      Base_GroupUser			--用户组表
          
                                    ) T
                                    LEFT JOIN Base_DataScopePermission dsp ON T.Id = dsp.ResourceId
                                                                              AND dsp.ObjectId = @ObjectId
                                                                              AND dsp.Category = @Category
                                                                              AND dsp.ModuleId = @ModuleId");
            strSql.Append(" WHERE 1 = 1");
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( Id IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" order by T.SortCode ASC");
            parameter.Add(DbFactory.CreateDbParameter("@ObjectId", ObjectId));
            parameter.Add(DbFactory.CreateDbParameter("@Category", Category));
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        #endregion

        #region 用户管理
        /// <summary>
        /// 加载用户列表
        /// <param name="ModuleId">模块主键</param>
        /// <param name="ObjectId">对象主键</param>
        /// <param name="Category">对象分类:1-部门2-角色3-岗位4-群组</param>
        /// </summary>
        /// <returns></returns>
        public DataTable GetScopeUserList(string ModuleId, string ObjectId, string Category)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  T.* ,
                                    dsp.ObjectId
                            FROM    ( SELECT    CompanyId AS Id ,		--公司ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                ParentId ,				--节点ID
                                                '' AS Gender,			--性别
                                                SortCode ,				--排序编码
                                                'Company' AS Sort		--分类
                                      FROM      Base_Company			--公司表
                                      UNION
                                      SELECT    DepartmentId AS Id,		--部门ID
                                                Code ,					--编码
                                                FullName ,				--名称
                                                CompanyId AS ParentId ,	--节点ID
                                                '' AS Gender,			--性别
                                                SortCode ,				--排序编码
                                                'Department' AS Sort	--分类
                                      FROM      Base_Department			--部门表
                                      UNION
                                      SELECT    UserId AS Id,			--用户ID
                                                Code ,					--编码
                                                RealName ,				--名称
                                                DepartmentId AS ParentId ,--节点ID
                                                Gender,					--性别
                                                SortCode ,				--排序编码
                                                'User' AS Sort			--分类
                                      FROM      Base_User			    --用户表
          
                                    ) T
                                    LEFT JOIN Base_DataScopePermission dsp ON T.Id = dsp.ResourceId
                                                                              AND dsp.ObjectId = @ObjectId
                                                                              AND dsp.Category = @Category
                                                                              AND dsp.ModuleId = @ModuleId");
            strSql.Append(" WHERE 1 = 1");
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( Id IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" order by T.SortCode ASC");
            parameter.Add(DbFactory.CreateDbParameter("@ObjectId", ObjectId));
            parameter.Add(DbFactory.CreateDbParameter("@Category", Category));
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        #endregion
    }
}