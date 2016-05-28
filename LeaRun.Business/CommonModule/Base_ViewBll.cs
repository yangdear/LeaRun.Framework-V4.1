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
    /// 视图设置表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.04 10:09</date>
    /// </author>
    /// </summary>
    public class Base_ViewBll : RepositoryFactory<Base_View>
    {
        /// <summary>
        /// 根据模块Id获取视图列表
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public List<Base_View> GetViewList(string ModuleId)
        {
            StringBuilder WhereSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            WhereSql.Append(" AND ModuleId = @ModuleId order by sortcode asc");
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return Repository().FindList(WhereSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 视图提交（新增、编辑、删除）
        /// </summary>
        /// <param name="KeyValue">判断新增、修改</param>
        /// <param name="ModuleId">模块Id</param>
        /// <param name="ViewJson">视图Json</param>
        /// <param name="ViewWhereJson">视图条件Json</param>
        /// <returns></returns>
        public int SubmitForm(string KeyValue, string ModuleId, string ViewJson, string ViewWhereJson)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                List<Base_View> ViewList = ViewJson.JonsToList<Base_View>();
                List<Base_ViewWhere> ViewWhereList = ViewWhereJson.JonsToList<Base_ViewWhere>();
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    database.Delete<Base_View>("ModuleId", ModuleId, isOpenTrans);
                    database.Delete<Base_ViewWhere>("ModuleId", ModuleId, isOpenTrans);
                }
                foreach (Base_View base_view in ViewList)
                {
                    if (string.IsNullOrEmpty(base_view.ViewId))
                        base_view.ViewId = CommonHelper.GetGuid;
                    base_view.ModuleId = ModuleId;
                    base_view.ParentId = "0";
                    database.Insert(base_view, isOpenTrans);
                }
                foreach (Base_ViewWhere base_viewwhere in ViewWhereList)
                {
                    if (string.IsNullOrEmpty(base_viewwhere.ViewWhereId))
                        base_viewwhere.ViewWhereId = CommonHelper.GetGuid;
                    base_viewwhere.ModuleId = ModuleId;
                    database.Insert(base_viewwhere, isOpenTrans);
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