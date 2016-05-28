using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.ExampleModule.Controllers
{
    public class EchartsController : Controller
    {
        public ActionResult pie()
        {
            return View();
        }
        public ActionResult line()
        {
            return View();
        }
    }
}
