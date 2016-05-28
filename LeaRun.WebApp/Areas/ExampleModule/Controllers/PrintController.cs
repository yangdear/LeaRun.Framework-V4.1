using LeaRun.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.ExampleModule.Controllers
{
    /// <summary>
    /// 打印功能
    /// </summary>
    public class PrintController : Controller
    {
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 打印测试数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPrintList()
        {
            string filepath = Server.MapPath("~/Areas/ExampleModule/Views/Print/PrintTextJson.txt");
            FileStream fs = new System.IO.FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));//取得这txt文件的编码
            return Content(sr.ReadToEnd().ToString());
        }
    }
}
