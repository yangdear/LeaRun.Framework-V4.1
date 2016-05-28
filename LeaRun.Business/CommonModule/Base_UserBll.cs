//=====================================================================================
// All Rights Reserved , Copyright @ Learun 2014
// Software Developers @ Learun 2014
//=====================================================================================

using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 用户管理
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.11 15:45</date>
    /// </author>
    /// </summary>
    public class Base_UserBll : RepositoryFactory<Base_User>
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="keyword">模块查询</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="DepartmentId">部门ID</param>
        /// <param name="jqgridparam">分页条件</param>
        /// <returns></returns>
        public DataTable GetPageList(string keyword, string CompanyId, string DepartmentId, ref JqGridParam jqgridparam)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    u.UserId ,					--用户ID
                                                u.Code ,					--用户编码
                                                u.Account ,					--登录账户
                                                u.RealName ,				--姓名
                                                u.Spell,                    --拼音
                                                u.Gender ,					--性别
                                                u.Mobile ,					--手机
                                                u.Telephone ,				--电话
                                                u.Email ,					--电子邮箱
                                                u.CompanyId ,			    --所在公司ID
                                                c.FullName AS CompanyName ,	--所在公司
                                                u.DepartmentId,				--所在部门ID
                                                d.FullName AS DepartmentName,--所在部门
                                                e.Duty,                     --职务
                                                u.Enabled ,					--是否有效
                                                u.LogOnCount ,				--登录次数
                                                u.LastVisit ,				--最后登录时间
                                                u.SortCode,                 --排序吗
                                                u.CreateUserId,				--创建时间
                                                u.Remark					--备注
                                      FROM      Base_User u
                                                LEFT JOIN Base_Company c ON c.CompanyId = u.CompanyId
                                                LEFT JOIN Base_Department d ON d.DepartmentId = u.DepartmentId
                                                LEFT JOIN Base_Employee e ON e.UserId = u.UserId
                                    ) T WHERE 1=1");
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(@" AND (RealName LIKE @keyword
                                    OR Account LIKE @keyword
                                    OR Code LIKE @keyword
                                    OR Spell LIKE @keyword)");
                parameter.Add(DbFactory.CreateDbParameter("@keyword", '%' + keyword + '%'));
            }
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
                strSql.Append(" AND ( UserId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            return Repository().FindTablePageBySql(strSql.ToString(), parameter.ToArray(), ref jqgridparam);
        }
        /// <summary>
        /// 判断是否连接服务器
        /// </summary>
        /// <returns></returns>
        public bool IsLinkServer()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  GETDATE()");
            DataTable dt = Repository().FindTableBySql(strSql.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 登陆验证信息
        /// </summary>
        /// <param name="Account">账户</param>
        /// <param name="Password">密码</param>
        /// <param name="result">返回结果</param>
        /// <returns></returns>
        public Base_User UserLogin(string Account, string Password, out string result)
        {
            if (!this.IsLinkServer())
            {
                throw new Exception("服务器连接不上，" + DbResultMsg.ReturnMsg);
            }
            Base_User entity = Repository().FindEntity("Account", Account);
            if (entity != null && entity.UserId != null)
            {
                if (entity.Enabled == 1)
                {
                    string dbPassword = Md5Helper.MD5(DESEncrypt.Encrypt(Password.ToLower(), entity.Secretkey).ToLower(), 32).ToLower();
                    if (dbPassword == entity.Password)
                    {
                        DateTime PreviousVisit = CommonHelper.GetDateTime(entity.LastVisit);
                        DateTime LastVisit = DateTime.Now;
                        int LogOnCount = CommonHelper.GetInt(entity.LogOnCount) + 1;
                        entity.PreviousVisit = PreviousVisit;
                        entity.LastVisit = LastVisit;
                        entity.LogOnCount = LogOnCount;
                        entity.Online = 1;
                        Repository().Update(entity);
                        result = "succeed";
                    }
                    else
                    {
                        result = "error";
                    }
                }
                else
                {
                    result = "lock";
                }
                return entity;
            }
            result = "-1";
            return null;
        }
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public DataTable UserRoleList(string CompanyId, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT  r.RoleId ,				--角色ID
                                    r.Code ,				--编码
                                    r.FullName ,			--名称
                                    r.SortCode ,			--排序码
                                    ou.ObjectId				--是否存在
                            FROM    Base_Roles r
                                    LEFT JOIN Base_ObjectUserRelation ou ON ou.ObjectId = r.RoleId
                                                                            AND ou.UserId = @UserId
                                                                            AND ou.Category = 2
                            WHERE 1 = 1");
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( RoleId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            strSql.Append(" AND r.CompanyId = @CompanyId");
            parameter.Add(DbFactory.CreateDbParameter("@UserId", UserId));
            parameter.Add(DbFactory.CreateDbParameter("@CompanyId", CompanyId));
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 选择用户列表
        /// </summary>
        /// <param name="keyword">模块查询</param>
        /// <returns></returns>
        public DataTable OptionUserList(string keyword)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(@"SELECT TOP 50 * FROM ( SELECT    
                                        u.UserId ,
                                        u.Account ,
                                        u.code,
                                        u.RealName ,
                                        u.DepartmentId ,
                                        d.FullName AS DepartmentName,
                                        u.Gender
                                FROM    Base_User u
                                LEFT JOIN Base_Department d ON d.DepartmentId = u.DepartmentId
                                WHERE   u.RealName LIKE @keyword
                                        OR u.Account LIKE @keyword
                                        OR u.Code LIKE @keyword
                                        OR u.Spell LIKE @keyword
                                        OR u.UserId IN (
                                        SELECT  u.UserId
                                        FROM    Base_User u
                                                INNER JOIN Base_ObjectUserRelation oc ON u.UserId = oc.UserId
                                                INNER JOIN dbo.Base_Company c ON c.CompanyId = oc.ObjectId
                                        WHERE   c.FullName LIKE @keyword
                                        UNION
                                        SELECT  u.UserId
                                        FROM    Base_User u
                                                INNER JOIN Base_ObjectUserRelation od ON u.UserId = od.UserId
                                                INNER JOIN Base_Department d ON d.DepartmentId = od.ObjectId
                                        WHERE   d.FullName LIKE @keyword
                                        UNION
                                        SELECT  u.UserId
                                        FROM    Base_User u
                                                INNER JOIN Base_ObjectUserRelation oro ON u.UserId = oro.UserId
                                                INNER JOIN Base_Roles r ON r.RoleId = oro.ObjectId
                                        WHERE   r.FullName LIKE @keyword
                                        UNION
                                        SELECT  u.UserId
                                        FROM    Base_User u
                                                INNER JOIN Base_ObjectUserRelation op ON u.UserId = op.UserId
                                                INNER JOIN Base_Post p ON p.PostId = op.ObjectId
                                        WHERE   p.FullName LIKE @keyword
                                        UNION
                                        SELECT  u.UserId
                                        FROM    Base_User u
                                                INNER JOIN Base_ObjectUserRelation og ON u.UserId = og.UserId
                                                INNER JOIN Base_GroupUser g ON g.GroupUserId = og.ObjectId
                                        WHERE   g.FullName LIKE @keyword )
                            ) a WHERE 1 = 1");
                parameter.Add(DbFactory.CreateDbParameter("@keyword", '%' + keyword + '%'));
            }
            else
            {
                strSql.Append(@"SELECT TOP 50
                                        u.UserId ,
                                        u.Account ,
                                        u.code ,
                                        u.RealName ,
                                        u.DepartmentId ,
                                        d.FullName AS DepartmentName ,
                                        u.Gender
                                FROM    Base_User u
                                        LEFT JOIN Base_Department d ON d.DepartmentId = u.DepartmentId
                                WHERE   1 = 1");
            }
            if (!ManageProvider.Provider.Current().IsSystem)
            {
                strSql.Append(" AND ( UserId IN ( SELECT ResourceId FROM Base_DataScopePermission WHERE");
                strSql.Append(" ObjectId IN ('" + ManageProvider.Provider.Current().ObjectId.Replace(",", "','") + "') ");
                strSql.Append(" ) )");
            }
            return Repository().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
    }
}