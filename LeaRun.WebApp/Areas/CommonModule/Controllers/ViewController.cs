using LeaRun.Utilities;
using LeaRun.Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using LeaRun.WebApp;
using LeaRun.Entity;
using System.Data.Common;
using LeaRun.DataAccess;
using System.Data;

namespace LeaRun.WebApp.Areas.CommonModule.Controllers
{
    /// <summary>
    /// 视图设置控制器
    /// </summary>
    public class ViewController : PublicController<Base_View>
    {
        private Base_ModuleBll base_modulebll = new Base_ModuleBll();
        private Base_ViewBll base_viewbll = new Base_ViewBll();
        private Base_ViewWhereBll base_viewwherebll = new Base_ViewWhereBll();
        /// <summary>
        /// 【视图设置】模块目录
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeJson()
        {
            List<Base_Module> list = base_modulebll.GetList();
            List<TreeJsonEntity> TreeList = new List<TreeJsonEntity>();
            foreach (Base_Module item in list)
            {
                string ModuleId = item.ModuleId;
                bool hasChildren = false;
                List<Base_Module> childnode = list.FindAll(t => t.ParentId == ModuleId);
                if (childnode.Count > 0)
                {
                    hasChildren = true;
                }
                else
                {
                    if (item.Category == "目录")
                    {
                        continue;
                    }
                }
                if (item.Category == "页面")
                    if (item.AllowView != 1)
                        continue;
                TreeJsonEntity tree = new TreeJsonEntity();
                tree.id = ModuleId;
                tree.text = item.FullName;
                tree.value = ModuleId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                tree.img = item.Icon != null ? "/Content/Images/Icon16/" + item.Icon : item.Icon;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }
        /// <summary>
        /// 【视图设置】返回表格JONS
        /// </summary>
        /// <param name="CompanyId">模块主键</param>
        /// <returns></returns>
        public ActionResult GridListJson(string ModuleId)
        {
            if (!string.IsNullOrEmpty(ModuleId))
            {
                List<Base_View> ListData = base_viewbll.GetViewList(ModuleId);
                var JsonData = new
                {
                    rows = ListData,
                };
                return Content(JsonData.ToJson());
            }
            return null;
        }
        /// <summary>
        /// 【视图设置》显示标题字段】返回列表JSON
        /// </summary>
        /// <param name="ModuleId">模块主键</param>
        /// <returns></returns>
        public JsonResult GetViewJson(string ModuleId)
        {
            List<Base_View> list = base_viewbll.GetViewList(ModuleId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 【视图设置》查询条件字段】返回列表JSON
        /// </summary>
        /// <param name="ModuleId">模块主键</param>
        /// <returns></returns>
        public JsonResult GetViewWhereJson(string ModuleId)
        {
            List<Base_ViewWhere> list = base_viewwherebll.GetViewWhereList(ModuleId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 【视图设置】表单提交事件
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="ModuleId">模块Id</param>
        /// <param name="ViewJson">视图Json</param>
        /// <param name="ViewWhereJson">视图条件Json</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ViewSubmitForm(string KeyValue, string ModuleId, string ViewJson, string ViewWhereJson)
        {
            try
            {
                int IsOk = 0;
                IsOk = base_viewbll.SubmitForm(KeyValue, ModuleId, ViewJson, ViewWhereJson);
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "操作成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败，错误：" + ex.Message }.ToString());
            }
        }
    }
}