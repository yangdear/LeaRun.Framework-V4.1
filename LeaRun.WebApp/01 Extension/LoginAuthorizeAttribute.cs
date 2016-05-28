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
    /// 登录权限认证
    /// <author>
    ///		<name>shecixiong</name>
    ///		<date>2014.06.11</date>
    /// </author>
    /// </summary>
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 响应前执行验证,查看当前用户是否有效 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var areaName = filterContext.RouteData.DataTokens["area"];
            var controllerName = filterContext.RouteData.Values["controller"];
            var action = filterContext.RouteData.Values["Action"];
            //登录是否过期
            if (!ManageProvider.Provider.IsOverdue())
            {
                filterContext.Result = new RedirectResult("~/Login/Default");
            }
        }
    }
}