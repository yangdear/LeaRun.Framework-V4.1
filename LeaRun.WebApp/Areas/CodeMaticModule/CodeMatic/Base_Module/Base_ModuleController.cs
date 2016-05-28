using LeaRun.Utilities;
using LeaRun.Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace LeaRun.Web.Areas.CodeMaticModule.Controllers
{
    /// <summary>
    /// 模块设置表控制器
    /// </summary>
    public class Base_ModuleController : Controller
    {
        /// <summary>
        /// 模块设置表业务处理类
        /// </summary>
        Base_ModuleBll base_modulebll = new Base_ModuleBll();
        /// <summary>
        /// 模块设置表实体类类
        /// </summary>
        Base_Module base_module = new Base_Module();
        /// <summary>
        /// 模块设置表 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Base_ModuleIndex()
        {
            return View();
        }
        /// <summary>
        /// 模块设置表 绑定列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Base_ModuleGrid()
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "No-Cache");
            string orderField = Request["pqGrid_OrderField"];                            //排序字段  
            string orderType = Request["pqGrid_OrderType"];                             //排序类型
            string pqGrid_Sort = Request["pqGrid_Sort"];                                //要显示字段
            Hashtable where = new Hashtable();
            return Content(JsonHelper.PqGridJson<Base_Module>(base_modulebllGetList(where), pqGrid_Sort));
        }
        /// <summary>
        /// 模块设置表 删除信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Base_ModuleDelete()
        {
            string key = Request["key"];                                                //主键
            string[] array = key.Split(',');
            return Content(base_modulebll.DeleteEntity(array).ToString());
        }
        /// <summary>
        /// 模块设置表 表单页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Base_ModuleForm()
        {
            return View();
        }
        /// <summary>
        /// 模块设置表提交表单信息
        /// </summary>
        /// <param name="base_module">模块设置表实体</param>
        /// <returns></returns>
        public ActionResult SubmitBase_ModuleForm(Base_Module base_module)
        {
            string key = Request["key"];                                                //主键
            bool IsOk = false;
            if (!string.IsNullOrEmpty(key))//判断是否编辑
            {
                base_module.ModuleId = key;
                base_module.ModifyDate = DateTime.Now;
                base_module.ModifyUserId = SessionHelper.GetSessionUser().UserId;
                base_module.ModifyUserName = SessionHelper.GetSessionUser().UserName;
                IsOk = base_modulebll.UpdateEntity(base_module);
            }
            else
            {
                base_module.ModuleId = CommonHelper.GetGuid;
                base_module.CreateUserId = SessionHelper.GetSessionUser().UserId;
                base_module.CreateUserName = SessionHelper.GetSessionUser().UserName;
                base_module.SortCode = CommonHelper.GetInt(base_modulebll.GetMaxCode());
                IsOk = base_modulebll.AddEntity(base_module);
            }
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 模块设置表获取对象返回JSON
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public ActionResult GetBase_ModuleForm(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                base_module = base_modulebll.GetEntity(key);
                string strJson = JsonHelper.ObjectToJson(base_module);
                return Content(strJson);
            }
            return null;
        }
    }
}