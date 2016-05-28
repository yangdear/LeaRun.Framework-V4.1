using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.ExampleModule.Controllers
{
    public class HadoopController : Controller
    {
        #region 百万级别数据量测试
        public ActionResult TestTableMegaIndex()
        {
            return View();
        }
        public ActionResult TestTableMegaListJson(JqGridParam jqgridparam)
        {
            Stopwatch watch = CommonHelper.TimerStart();
            string UserId = ManageProvider.Provider.Current().UserId;
            DataTable ListData = this.FindTablePageBySql("SELECT TestId, Code, FullName, CreateDate, CreateUserName, Remark FROM TestTable", ref jqgridparam);
            var JsonData = new
            {
                total = jqgridparam.total,
                page = jqgridparam.page,
                records = jqgridparam.records,
                costtime = CommonHelper.TimerEnd(watch),
                rows = ListData,
            };
            return Content(JsonData.ToJson());
        }
        #endregion

        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public DataTable FindTablePageBySql(string strSql, ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            DataTable dt = DataFactory.Database().FindTablePageBySql(strSql, orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return dt;
        }
    }
}
