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
    /// 首页快捷方式
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.22 21:02</date>
    /// </author>
    /// </summary>
    public class Base_ShortcutsBll : RepositoryFactory<Base_Shortcuts>
    {
        /// <summary>
        /// 获取首页快捷方式列表
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public List<Base_Module> GetShortcutList(string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    Base_Module M
                                    RIGHT JOIN Base_Shortcuts S ON s.ModuleId = M.ModuleId
                            WHERE   S.CreateUserId = @CreateUserId
                            ORDER BY M.SortCode");
            List<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter("@CreateUserId", UserId));
            return DataFactory.Database().FindListBySql<Base_Module>(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 快捷方式（新增、编辑、删除）
        /// </summary>
        /// <param name="ModuleId">模块Id</param>
        /// <returns></returns>
        public int SubmitForm(string ModuleId, string UserId)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                string[] array = ModuleId.Split(',');
                database.Delete<Base_Shortcuts>("CreateUserId", UserId, isOpenTrans);
                foreach (string item in array)
                {
                    if (item.Length>0)
                    {
                        Base_Shortcuts entity = new Base_Shortcuts();
                        entity.Create();
                        entity.ModuleId = item;
                        entity.CreateUserId = UserId;
                        database.Insert(entity);
                    }
                }
                database.Commit();
                return 1;
            }
            catch
            {
                database.Rollback();
                return -1;
            }
        }
    }
}