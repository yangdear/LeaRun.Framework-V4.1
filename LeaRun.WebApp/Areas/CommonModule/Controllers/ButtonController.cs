using LeaRun.Business;
using LeaRun.Entity;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.CommonModule.Controllers
{
    /// <summary>
    /// 按钮管理控制器
    /// </summary>
    public class ButtonController : PublicController<Base_Button>
    {
        public Base_ModuleBll base_modulebll = new Base_ModuleBll();
        Base_ButtonBll base_buttonbll = new Base_ButtonBll();
        /// <summary>
        /// 加载模块目录
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
                    if (item.AllowButton != 1)
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
        /// 加载按钮 （返回树Json）
        /// </summary>
        /// <param name="ModuleId">模块ID</param>
        /// <param name="Category">分类：1-工具栏，2：右击栏</param>
        /// <returns></returns>
        public ActionResult ButtonTreeJson(string ModuleId, string Category)
        {
            List<Base_Button> list = base_buttonbll.GetList(ModuleId, Category);
            List<TreeJsonEntity> TreeList = new List<TreeJsonEntity>();
            foreach (Base_Button item in list)
            {
                string ButtonId = item.ButtonId;
                bool hasChildren = false;
                List<Base_Button> childnode = list.FindAll(t => t.ParentId == ButtonId);
                if (childnode.Count > 0)
                {
                    hasChildren = true;
                }
                TreeJsonEntity tree = new TreeJsonEntity();
                tree.id = ButtonId;
                tree.text = item.FullName;
                tree.value = item.Code;
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
        /// 【模块按钮管理】返回公司列表JONS
        /// </summary>
        /// <param name="ModuleId">模块ID</param>
        /// <param name="Category">分类：1-工具栏，2：右击栏</param>
        /// <returns></returns>
        public ActionResult TreeGridListJson(string ModuleId, string Category)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(ModuleId))
            {
                List<Base_Button> ListData = base_buttonbll.GetList(ModuleId, Category);
                sb.Append("{ \"rows\": [");
                sb.Append(TreeGridJson(ListData, -1));
                sb.Append("]}");
            }
            return Content(sb.ToString());
        }
        int lft = 1, rgt = 1000000;
        public string TreeGridJson(List<Base_Button> ListData, int index, string ParentId = "0")
        {
            StringBuilder sb = new StringBuilder();
            List<Base_Button> ChildNodeList = ListData.FindAll(t => t.ParentId == ParentId);
            if (ChildNodeList.Count > 0) { index++; }
            foreach (Base_Button entity in ChildNodeList)
            {
                string strJson = entity.ToJson();
                strJson = strJson.Insert(1, "\"ButtonName\":\"" + entity.FullName + "\",");
                strJson = strJson.Insert(1, "\"level\":" + index + ",");
                strJson = strJson.Insert(1, "\"isLeaf\":" + (ListData.Count<Base_Button>(t => t.ParentId == entity.ButtonId) == 0 ? true : false).ToString().ToLower() + ",");
                strJson = strJson.Insert(1, "\"expanded\":true,");
                strJson = strJson.Insert(1, "\"lft\":" + lft++ + ",");
                strJson = strJson.Insert(1, "\"rgt\":" + rgt-- + ",");
                sb.Append(strJson);
                sb.Append(TreeGridJson(ListData, index, entity.ButtonId));
            }
            return sb.ToString().Replace("}{", "},{");
        }
        /// <summary>
        /// 【模块按钮管理】返回对象JSON
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetFormControl(string KeyValue)
        {
            Base_Button entity = repositoryfactory.Repository().FindEntity(KeyValue);
            string JsonData = entity.ToJson();
            JsonData = JsonData.Insert(1, "\"ParentName\":\"" + repositoryfactory.Repository().FindEntity(entity.ParentId).FullName + "\",");
            return Content(JsonData);
        }
    }
}
