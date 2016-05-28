using LeaRun.Business;
using LeaRun.Entity;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.CommonModule.Controllers
{
    /// <summary>
    /// 角色管理控制器
    /// </summary>
    public class RolesController : PublicController<Base_Roles>
    {
        Base_RolesBll base_rolesbll = new Base_RolesBll();

        /// <summary>
        /// 【角色管理】返回列表JONS
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="jqgridparam">JqGrid表格参数</param>
        /// <returns></returns>
        public ActionResult GridPageListJson(string CompanyId, JqGridParam jqgridparam)
        {
            try
            {
                Stopwatch watch = CommonHelper.TimerStart();
                DataTable ListData = base_rolesbll.GetPageList(CompanyId, ref jqgridparam);
                var JsonData = new
                {
                    total = jqgridparam.total,
                    page = jqgridparam.page,
                    records = jqgridparam.records,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = ListData,
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                Base_SysLogBll.Instance.WriteLog("", OperationType.Query, "-1", "异常错误：" + ex.Message);
                return null;
            }
        }
    }
}