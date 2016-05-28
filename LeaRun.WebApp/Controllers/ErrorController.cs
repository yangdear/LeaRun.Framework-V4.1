using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string message)
        {
            Dictionary<string, string> modulesError = (Dictionary<string, string>)HttpContext.Application["error"];
            ViewData["Message"] = modulesError;
            return View();
        }
        /// <summary>
        /// 错误页面404
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404()
        {
            return View();
        }
        /// <summary>
        /// 建议升级浏览器软件
        /// </summary>
        /// <returns></returns>
        public ActionResult Browser()
        {
            return View();
        }
    }
}
