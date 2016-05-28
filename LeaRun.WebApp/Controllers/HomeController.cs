using LeaRun.Business;
using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public Base_ModuleBll base_modulebll = new Base_ModuleBll();
        Base_ModulePermissionBll base_modulepermissionbll = new Base_ModulePermissionBll();
        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Login");
        }
        /// <summary>
        /// 访问模块，写入系统菜单Id
        /// </summary>
        /// <param name="ModuleId">模块id</param>
        /// <param name="ModuleName">模块名称</param>
        /// <returns></returns>
        public ActionResult SetModuleId(string ModuleId, string ModuleName)
        {
            string _ModuleId = DESEncrypt.Encrypt(ModuleId);
            CookieHelper.WriteCookie("ModuleId", _ModuleId);
            if (!string.IsNullOrEmpty(ModuleName))
            {
                Base_SysLogBll.Instance.WriteLog(ModuleId, OperationType.Visit, "1", ModuleName);
            }
            return Content(_ModuleId);
        }
        /// <summary>
        /// 离开tab事件
        /// </summary>
        /// <param name="ModuleId">模块id</param>
        /// <param name="ModuleName">模块名称</param>
        /// <returns></returns>
        public ActionResult SetLeave(string ModuleId, string ModuleName)
        {
            Base_SysLogBll.Instance.WriteLog(ModuleId, OperationType.Leave, "1", ModuleName);
            return Content(ModuleId);
        }

        #region 后台首页-开始菜单
        /// <summary>
        /// 开始菜单UI
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult StartIndex()
        {
            ViewBag.Account = ManageProvider.Provider.Current().Account + "（" + ManageProvider.Provider.Current().UserName + "）";
            return View();
        }
        /// <summary>
        /// 开始-欢迎首页
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult StartPanel()
        {
            return View();
        }
        /// <summary>
        /// 加载开始菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadStartMenu()
        {
            string ObjectId = ManageProvider.Provider.Current().ObjectId;
            List<Base_Module> list = base_modulepermissionbll.GetModuleList(ObjectId).FindAll(t => t.Enabled == 1);
            return Content(list.ToJson().Replace("&nbsp;", ""));
        }
        #endregion

        #region 后台首页-手风琴菜单
        /// <summary>
        /// 手风琴UI
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult AccordionIndex()
        {
            ViewBag.Account = ManageProvider.Provider.Current().Account + "（" + ManageProvider.Provider.Current().UserName + "）";
            return View();
        }
        /// <summary>
        /// 手风琴-欢迎首页
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult AccordionPage()
        {
            return View();
        }
        /// <summary>
        /// 加载手风琴菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadAccordionMenu()
        {
            string ObjectId = ManageProvider.Provider.Current().ObjectId;
            List<Base_Module> list = base_modulepermissionbll.GetModuleList(ObjectId).FindAll(t => t.Enabled == 1);
            return Content(list.ToJson().Replace("&nbsp;", ""));
        }
        #endregion

        #region 后台首页-无限树菜单
        /// <summary>
        /// 无限树UI
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult TreeIndex()
        {
            ViewBag.Account = ManageProvider.Provider.Current().Account + "（" + ManageProvider.Provider.Current().UserName + "）";
            return View();
        }
        /// <summary>
        /// 无限树-欢迎首页
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult TreePage()
        {
            return View();
        }
        /// <summary>
        /// 加载无限树菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadTreeMenu(string ModuleId)
        {
            string ObjectId = ManageProvider.Provider.Current().ObjectId;
            List<Base_Module> list = base_modulepermissionbll.GetModuleList(ObjectId).FindAll(t => t.Enabled == 1);
            List<TreeJsonEntity> TreeList = new List<TreeJsonEntity>();
            foreach (Base_Module item in list)
            {
                TreeJsonEntity tree = new TreeJsonEntity();
                bool hasChildren = false;
                List<Base_Module> childnode = list.FindAll(t => t.ParentId == item.ModuleId);
                if (childnode.Count > 0)
                {
                    hasChildren = true;
                }
                if (item.Category == "页面")
                {
                    tree.Attribute = "Location";
                    tree.AttributeValue = item.Location;
                }
                tree.id = item.ModuleId;
                tree.text = item.FullName;
                tree.value = item.ModuleId;
                tree.isexpand = false;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                tree.img = item.Icon != null ? "/Content/Images/Icon16/" + item.Icon : item.Icon;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson(ModuleId));
        }
        #endregion

        #region 快捷方式设置
        /// <summary>
        /// 快捷方式设置
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult Shortcuts()
        {
            return View();
        }
        /// <summary>
        /// 快捷方式 返回菜单模块树JSON
        /// </summary>
        /// <returns></returns>
        public ActionResult ShortcutsModuleTreeJson()
        {
            Base_ShortcutsBll base_shortcutsbll = new Base_ShortcutsBll();
            string UserId = ManageProvider.Provider.Current().UserId;
            List<Base_Module> ShortcutList = base_shortcutsbll.GetShortcutList(UserId);
            string ObjectId = ManageProvider.Provider.Current().ObjectId;
            List<Base_Module> list = base_modulepermissionbll.GetModuleList(ObjectId).FindAll(t => t.Enabled == 1);
            List<TreeJsonEntity> TreeList = new List<TreeJsonEntity>();
            foreach (Base_Module item in list)
            {
                TreeJsonEntity tree = new TreeJsonEntity();
                tree.id = item.ModuleId;
                tree.text = item.FullName;
                tree.value = item.ModuleId;
                if (item.Category == "页面")
                {
                    tree.checkstate = ShortcutList.FindAll(t => t.ModuleId == item.ModuleId).Count == 0 ? 0 : 1;
                    //tree.checkstate = item["objectid"].ToString() != "" ? 1 : 0;
                    tree.showcheck = true;
                }
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = list.FindAll(t => t.ParentId == item.ModuleId).Count > 0 ? true : false;
                tree.parentId = item.ParentId;
                tree.img = item.Icon != null ? "/Content/Images/Icon16/" + item.Icon : item.Icon;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }
        /// <summary>
        /// 快捷方式列表返回JSON
        /// </summary>
        /// <returns></returns>
        public ActionResult ShortcutsListJson()
        {
            Base_ShortcutsBll base_shortcutsbll = new Base_ShortcutsBll();
            string UserId = ManageProvider.Provider.Current().UserId;
            List<Base_Module> ShortcutList = base_shortcutsbll.GetShortcutList(UserId);
            return Content(ShortcutList.ToJson());
        }
        /// <summary>
        /// 快捷方式设置 提交保存
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public ActionResult SubmitShortcuts(string ModuleId)
        {
            try
            {
                Base_ShortcutsBll base_shortcutsbll = new Base_ShortcutsBll();
                string UserId = ManageProvider.Provider.Current().UserId;
                int IsOk = base_shortcutsbll.SubmitForm(ModuleId, UserId);
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "设置成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        #endregion

        #region 技术支持
        /// <summary>
        /// 技术支持
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult SupportPage()
        {
            return View();
        }
        #endregion

        #region 关于我们
        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult AboutPage()
        {
            return View();
        }
        #endregion

        #region 个性化皮肤设置
        /// <summary>
        /// 个性化皮肤设置
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult SkinIndex()
        {
            return View();
        }
        /// <summary>
        /// 切换主题
        /// </summary>
        /// <param name="UItheme"></param>
        /// <returns></returns>
        public ActionResult SwitchTheme(string UItheme)
        {
            CookieHelper.WriteCookie("UItheme", UItheme, 43200);
            return Content("1");
        }
        #endregion
    }
}
