using LeaRun.Business;
using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.CommonModule.Controllers
{
    /// <summary>
    /// 接口管理控制器
    /// </summary>
    public class InterfaceManageController : PublicController<Base_InterfaceManage>
    {
        Base_InterfaceManageBll base_interfacemanagebll = new Base_InterfaceManageBll();
        /// <summary>
        /// 【接口管理】返回接口列表JSON
        /// </summary>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public ActionResult GridPageListJson(JqGridParam jqgridparam)
        {
            Stopwatch watch = CommonHelper.TimerStart();
            List<Base_InterfaceManage> ListData = base_interfacemanagebll.GetPageList(ref jqgridparam);
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
        /// <summary>
        /// 【接口管理】返回接口参数列表JSON
        /// </summary>
        /// <param name="InterfaceId"></param>
        /// <returns></returns>
        public ActionResult GridInterfaceParameterListJson(string InterfaceId)
        {
            List<Base_InterfaceManageParameter> ListData = base_interfacemanagebll.GetInterfaceParameterList(InterfaceId);
            var JsonData = new
            {
                rows = ListData,
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 提交接口表单（新增、编辑）
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="entity">接口对象</param>
        /// <param name="ParameterJson">接口参数</param>
        /// <returns></returns>
        public ActionResult SubmitInterfaceForm(string KeyValue, Base_InterfaceManage entity, string ParameterJson)
        {
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                int IsOk = base_interfacemanagebll.SubmitInterfaceForm(KeyValue, entity, ParameterJson);
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
    }
}