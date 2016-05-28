using LeaRun.Business;
using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.ExampleModule.Controllers
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class OrderController : PublicController<POOrder>
    {
        POOrderBll poorderbll = new POOrderBll();
        Base_CodeRuleBll base_coderulebll = new Base_CodeRuleBll();
        /// <summary>
        /// 获取自动单据编码
        /// </summary>
        /// <returns></returns>
        private string BillCode()
        {
            string UserId = ManageProvider.Provider.Current().UserId;
            //string ModuleId = DESEncrypt.Decrypt(CookieHelper.GetCookie("ModuleId"));
            string ModuleId = "336fbb33-083e-49ae-a31f-a55797c26f74";
            return base_coderulebll.GetBillCode(UserId, ModuleId);
        }
        /// <summary>
        /// 采购订单表单
        /// </summary>
        /// <returns></returns>
        public override ActionResult Form()
        {
            string KeyValue = Request["KeyValue"];
            if (string.IsNullOrEmpty(KeyValue))
            {
                ViewBag.BillNo = this.BillCode();
                ViewBag.CreateUserName = ManageProvider.Provider.Current().UserName;
            }
            return View();
        }
        /// <summary>
        /// 订单列表（返回Json）
        /// </summary>
        /// <param name="BillNo">制单编号</param>
        /// <param name="StartTime">制单开始时间</param>
        /// <param name="EndTime">制单结束时间</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public ActionResult GetOrderList(string BillNo, string StartTime, string EndTime, JqGridParam jqgridparam)
        {
            try
            {
                Stopwatch watch = CommonHelper.TimerStart();
                List<POOrder> ListData = poorderbll.GetOrderList(BillNo, StartTime, EndTime, jqgridparam);
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
            catch (Exception ex)
            {
                Base_SysLogBll.Instance.WriteLog("", OperationType.Query, "-1", "异常错误：" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 订单明细列表（返回Json）
        /// </summary>
        /// <param name="POOrderId">订单主键</param>
        /// <returns></returns>
        public ActionResult GetOrderEntryList(string POOrderId)
        {
            try
            {
                var JsonData = new
                {
                    rows = poorderbll.GetOrderEntryList(POOrderId),
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                Base_SysLogBll.Instance.WriteLog("", OperationType.Query, "-1", "异常错误：" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 提交订单表单（新增、编辑）
        /// </summary>
        /// <param name="KeyValue">订单主键</param>
        /// <param name="entity">订单实体</param>
        /// <param name="POOrderEntryJson">订单明细</param>
        /// <returns></returns>
        public ActionResult SubmitOrderForm(string KeyValue, POOrder entity, string POOrderEntryJson)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "变更成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    database.Delete<POOrderEntry>("POOrderId", KeyValue, isOpenTrans);
                    entity.Modify(KeyValue); database.Update(entity, isOpenTrans);
                }
                else
                {
                    entity.Create();
                    entity.POOrderType = 0;
                    database.Insert(entity, isOpenTrans);
                    base_coderulebll.OccupyBillCode(ManageProvider.Provider.Current().UserId, "336fbb33-083e-49ae-a31f-a55797c26f74", isOpenTrans);
                }
                List<POOrderEntry> POOrderEntryList = POOrderEntryJson.JonsToList<POOrderEntry>();
                int index = 1;
                foreach (POOrderEntry poorderentry in POOrderEntryList)
                {
                    if (!string.IsNullOrEmpty(poorderentry.ItemCode))
                    {
                        poorderentry.SortCode = index;
                        poorderentry.Create();
                        poorderentry.POOrderId = entity.POOrderId;
                        database.Insert(poorderentry, isOpenTrans);
                        index++;
                    }
                }
                database.Commit();
                return Content(new JsonMessage { Success = true, Code = "1", Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                database.Rollback();
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }

        #region 物料信息
        /// <summary>
        /// 物料对象实体
        /// </summary>
        public class ItemEntity
        {
            public string ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string ItemSpell { get; set; }
            public string ItemModel { get; set; }
            public string UnitId { get; set; }
            public string ItemProduct { get; set; }
            public string SortCode { get; set; }
            public string Remark { get; set; }
        }
        /// <summary>
        /// 物料列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ItemList()
        {
            return View();
        }
        /// <summary>
        /// 物料列表（返回列表JSON）
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public ActionResult GetItemListJson(string keywords)
        {
            string filepath = Server.MapPath("~/Areas/ExampleModule/Views/Order/ItemJson.txt");
            FileStream fs = new System.IO.FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));//取得这txt文件的编码
            int PageSize = 20;
            List<ItemEntity> ListData = new List<ItemEntity>();
            if (!string.IsNullOrEmpty(keywords))
            {
                //Linq模糊查询
                ListData = (from itementity in JsonHelper.JonsToList<ItemEntity>(sr.ReadToEnd().ToString())
                            where itementity.ItemCode.Contains(keywords)
                            || itementity.ItemName.Contains(keywords)
                            || itementity.ItemSpell.Contains(keywords.ToUpper())
                            select itementity).Take(PageSize).ToList<ItemEntity>();
            }
            else
            {
                ListData = JsonHelper.JonsToList<ItemEntity>(sr.ReadToEnd().ToString()).Take(PageSize).ToList<ItemEntity>();
            }
            var JsonData = new
            {
                rows = ListData,
            };
            return Content(JsonData.ToJson());
        }
        #endregion

        #region 供应商信息
        /// <summary>
        /// 供应商对象实体
        /// </summary>
        public class SupplierEntity
        {
            public string SupplierId { get; set; }
            public string SupplierName { get; set; }
            public string SupplierSpell { get; set; }
        }
        /// <summary>
        /// 供应商列表（返回列表JSON）
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public ActionResult GetSupplierListJson(string keywords)
        {
            string filepath = Server.MapPath("~/Areas/ExampleModule/Views/Order/SupplierJson.txt");
            FileStream fs = new System.IO.FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));//取得这txt文件的编码
            int PageSize = 20;
            List<SupplierEntity> ListData = new List<SupplierEntity>();
            if (!string.IsNullOrEmpty(keywords))
            {
                //Linq模糊查询
                ListData = (from supplierentity in JsonHelper.JonsToList<SupplierEntity>(sr.ReadToEnd().ToString())
                            where supplierentity.SupplierName.Contains(keywords)
                            || supplierentity.SupplierSpell.Contains(keywords)
                            select supplierentity).Take(PageSize).ToList<SupplierEntity>();
            }
            else
            {
                ListData = JsonHelper.JonsToList<SupplierEntity>(sr.ReadToEnd().ToString()).Take(PageSize).ToList<SupplierEntity>();
            }
            return Content(ListData.ToJson());
        }
        #endregion
    }
}
