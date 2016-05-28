using LeaRun.Business;
using LeaRun.Entity;
using LeaRun.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.CommonModule.Controllers
{
    /// <summary>
    /// 表单附加属性 控制器
    /// </summary>
    public class FormLayoutController : PublicController<Base_FormAttribute>
    {
        private Base_ModuleBll base_modulebll = new Base_ModuleBll();
        Base_FormAttributeBll base_formattributebll = new Base_FormAttributeBll();
        /// <summary>
        /// 【系统表单】模块目录
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
                    if (item.AllowForm != 1)
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
        /// 表单设计器
        /// </summary>
        /// <returns></returns>
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult Layout()
        {
            string ModuleId = Request["ModuleId"];
            string strhtml = base_formattributebll.CreateBuildFormTable(2, ModuleId);
            ViewBag.BuildFormTable = strhtml;
            return View();
        }
    }
}
