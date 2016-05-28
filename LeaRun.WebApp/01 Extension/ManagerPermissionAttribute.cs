using LeaRun.Business;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp
{
    /// <summary>
    /// 权限验证,加强安全验证防止未授权匿名不合法的请求
    /// <author>
    ///		<name>shecixiong</name>
    ///		<date>2014.06.11</date>
    /// </author>
    /// </summary>
    public class ManagerPermissionAttribute : AuthorizeAttribute
    {
        private PermissionMode _CustomMode;
        /// <summary>默认构造</summary>
        /// <param name="Mode">权限认证模式</param>
        public ManagerPermissionAttribute(PermissionMode Mode)
        {
            _CustomMode = Mode;
        }
        /// <summary>权限认证</summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //登录权限认证
            if (!ManageProvider.Provider.IsOverdue())
            {
                filterContext.Result = new RedirectResult("~/Login/Default");
            }
            //防止被搜索引擎爬虫、网页采集器
            if (!this.PreventCreeper())
            {
                filterContext.Result = new RedirectResult("~/Login/Default");
            }
            //权限拦截是否忽略
            if (_CustomMode == PermissionMode.Ignore)
            {
                return;
            }
            //执行权限认证
            if (!this.ActionAuthorize(filterContext))
            {
                ContentResult Content = new ContentResult();
                Content.Content = "<script type='text/javascript'>top.Loading(false);alert('很抱歉！您的权限不足，访问被拒绝！');</script>";
                filterContext.Result = Content;
            }
        }
        /// <summary>
        /// 执行权限认证
        /// </summary>
        /// <returns></returns>
        private bool ActionAuthorize(AuthorizationContext filterContext)
        {
            if (ManageProvider.Provider.Current().IsSystem)
                return true;
            var areaName = filterContext.RouteData.DataTokens["area"] + "/";            //获取当前区域
            var controllerName = filterContext.RouteData.Values["controller"] + "/";    //获取控制器
            var action = filterContext.RouteData.Values["Action"];                      //获取当前Action
            string requestPath = "/" + areaName + controllerName + action;              //拼接构造完整url
            string ModuleId = DESEncrypt.Decrypt(CookieHelper.GetCookie("ModuleId"));
            bool Result = Base_ModulePermissionBll.Instance.ActionAuthorize(requestPath.ToLower(), ManageProvider.Provider.Current().ObjectId, ModuleId, ManageProvider.Provider.Current().UserId);
            return Result;
        }
        /// <summary>
        /// 防止被搜索引擎爬虫、网页采集器
        /// </summary>
        /// <returns></returns>
        private bool PreventCreeper()
        {
            return true;
        }
    }
}