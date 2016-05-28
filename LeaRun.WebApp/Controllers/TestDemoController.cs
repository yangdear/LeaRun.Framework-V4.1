using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Controllers
{
    public class TestDemoController : Controller
    {
        /// <summary>
        /// 消息提示
        /// </summary>
        /// <returns></returns>
        public ActionResult jBox_master()
        {
            return View();
        }
        /// <summary>
        /// 布局
        /// </summary>
        /// <returns></returns>
        public ActionResult layout()
        {
            return View();
        }
    }
}
