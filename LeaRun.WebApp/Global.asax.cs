using LeaRun.DataAccess;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace LeaRun.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 当前应用程序启动这件事会发生
        /// </summary>
        protected void Application_Start()
        {
            //设置当前数据库类型
            DbHelper.DbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), ConfigHelper.AppSettings("ComponentDbType"), true);
            Application["OnLineCount"] = 50;//在应用程序第一次启动时初始化在线人数为0
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        /// <summary>
        /// 离开应用程序启动这件事会发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["OnLineCount"] = (int)Application["OnLineCount"] - 1;
            Application.UnLock();
        }
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = this.Context.Server.GetLastError();
            if (ex != null)
            {
                //登录是否过期
                if (!ManageProvider.Provider.IsOverdue())
                {
                    HttpContext.Current.Response.Redirect("~/Login/Default");
                }
                Dictionary<string, string> modulesError = new Dictionary<string, string>();
                modulesError.Add("发生时间", DateTime.Now.ToString());
                modulesError.Add("错误描述", ex.Message.Replace("\r\n", ""));
                modulesError.Add("错误对象", ex.Source);
                modulesError.Add("错误页面", "" + HttpContext.Current.Request.Url + "");
                modulesError.Add("浏览器IE", HttpContext.Current.Request.UserAgent);
                modulesError.Add("服务器IP", NetHelper.GetIPAddress());
                Application["error"] = modulesError;
                HttpContext.Current.Response.Redirect("~/Error/Index");
            }
        }
    }
}