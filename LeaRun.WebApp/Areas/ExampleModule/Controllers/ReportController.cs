using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace LeaRun.WebApp.Areas.ExampleModule.Controllers
{
    public class ReportController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RdlcReportDemo()
        {
            string aspx = "~/Content/Report/Quotation/Quotation.aspx";
            using (var sw = new StringWriter())
            {
                System.Web.HttpContext.Current.Server.Execute(aspx,sw,true);
                return Content(sw.ToString());
            }
        }
        public ActionResult RdlcBankAnalyzeDemo()
        {
            string aspx = "~/Content/Report/BankAnalyze/BankAnalyze.aspx";
            using (var sw = new StringWriter())
            {
                System.Web.HttpContext.Current.Server.Execute(aspx, sw, true);
                return Content(sw.ToString());
            }
        }

        public ActionResult RdlcMoneyDemo()
        {
            string aspx = "~/Content/Report/Money/Money.aspx";
            using (var sw = new StringWriter())
            {
                System.Web.HttpContext.Current.Server.Execute(aspx, sw, true);
                return Content(sw.ToString());
            }
        }
    }
}
