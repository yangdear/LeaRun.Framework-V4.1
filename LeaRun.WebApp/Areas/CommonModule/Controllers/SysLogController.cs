using LeaRun.Business;
using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.CommonModule.Controllers
{
    /// <summary>
    /// 系统日志控制器
    /// </summary>
    public class SysLogController : PublicController<Base_SysLog>
    {
        private Base_SysLogBll base_syslogbll = new Base_SysLogBll();
        /// <summary>
        /// 【系统日志】返回系统日志列表JSON
        /// </summary>
        /// <param name="ModuleId">模块ID</param>
        /// <param name="ParameterJson">搜索条件</param>
        /// <param name="jqgridparam">表格参数</param>
        /// <returns></returns>
        public ActionResult GridPageListJson(string ModuleId, string ParameterJson, JqGridParam jqgridparam)
        {
            try
            {
                Stopwatch watch = CommonHelper.TimerStart();
                //if (!string.IsNullOrEmpty(ParameterJson) && ParameterJson.Length > 2)
                //{
                //    List<DbParameter> parameter = new List<DbParameter>();
                //    IList conditions = ParameterJson.JonsToList<Condition>();
                //    string WhereSql = ConditionBuilder.GetWhereSql(conditions, out parameter);
                //}
                List<Base_SysLog> ListData = base_syslogbll.GetPageList(ModuleId, ParameterJson, ref jqgridparam);
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
        /// 【系统日志明细】返回表格JSON
        /// </summary>
        /// <param name="SysLogId">主键值</param>
        /// <returns></returns>
        public JsonResult GetSysLogDetailJson(string SysLogId)
        {
            List<Base_SysLogDetail> list = base_syslogbll.GetSysLogDetailList(SysLogId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 【清空日志】
        /// </summary>
        /// <returns></returns>
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult RemoveLog()
        {
            return View();
        }
        /// <summary>
        /// 【清空日志】 表单提交事件
        /// </summary>
        /// <param name="KeepTime">日志保留时间</param>
        public ActionResult SubmitRemoveLog(string KeepTime)
        {
            string Remark = "";
            if (KeepTime == "7")//保留近一周
            {
                Remark = "保留近一周";
            }
            else if (KeepTime == "1")//保留近一个月
            {
                Remark = "保留近一个月";
            }
            else if (KeepTime == "3")//保留近三个月
            {
                Remark = "保留近三个月";
            }
            if (KeepTime == "0")//
            {
                Remark = "不保留，全部删除";
            }
            try
            {
                var Message = "清空失败。";
                int IsOk = Base_SysLogBll.Instance.RemoveLog(KeepTime);
                if (IsOk >= 0)
                {
                    IsOk = 1;
                    Message = "删除成功。";
                }
                Base_SysLogBll.Instance.WriteLog("", OperationType.Other, IsOk.ToString(), Message + Remark);
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                Base_SysLogBll.Instance.WriteLog("", OperationType.Other, "-1", Remark + "," + "错误：" + ex.Message);
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败，错误：" + ex.Message }.ToString());
            }
        }
        #region 日志文件
        /// <summary>
        /// 日志文件视图
        /// </summary>
        /// <returns></returns>
        public ActionResult FileIndex()
        {
            return View();
        }
        /// <summary>
        /// 日志文件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult FileList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/log"));
            FileInfo[] files = dir.GetFiles();
            FileDateSorter.QuickSort(files, 0, files.Length - 1);//按时间排序
            foreach (FileInfo fsi in files)
            {
                sb.Append("{");
                sb.Append("\"id\":\"" + fsi.Name + "\",");
                sb.Append("\"text\":\"" + fsi.Name + "\",");
                sb.Append("\"value\":\"" + fsi.Name + "\",");
                sb.Append("\"img\":\"/Content/Images/Icon16/page_white_error.png\",");
                sb.Append("\"isexpand\":true,");
                sb.Append("\"hasChildren\":false");
                sb.Append("},");
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return Content(sb.ToString());
        }
        /// <summary>
        /// 读日志
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public ActionResult ReadTxtLog(string FileName)
        {
            string filepath = Server.MapPath("~/log/" + FileName);
            FileStream fs = new System.IO.FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));//取得这txt文件的编码
            string txtvalue = sr.ReadToEnd().ToString();
            sr.Close();
            return Content(txtvalue);
        }
        #endregion

        #region 登陆访问统计
        /// <summary>
        /// 登陆访问统计
        /// </summary>
        /// <returns></returns>
        public ActionResult LogChartIndex()
        {
            return View();
        }
        /// <summary>
        /// 登陆访问列表
        /// </summary>
        /// <param name="day">天</param>
        /// <returns></returns>
        public ActionResult LoginList(string day)
        {
            string datatime = DateTime.Now.ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(day))
            {
                datatime = CommonHelper.GetDateTime(DateTime.Now.ToString("yyyy-MM-") + day).ToString("yyyy-MM-dd");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  convert(varchar(100),CreateDate,120) AS CreateDate ,
                                    IPAddress ,
                                    IPAddressName
                            FROM    [Login].dbo.BPMS_SysLoginLog
                            WHERE   convert(varchar(10),CreateDate,120) = '" + datatime + "' ORDER BY CreateDate DESC ,IPAddress DESC");
            DataTable dt = DataFactory.Database().FindTableBySql(strSql.ToString());
            return Content(dt.ToJson());
        }
        /// <summary>
        /// 登陆访问统计（返回JSON）
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginChart()
        {
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            List<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter("@y", year));
            parameter.Add(DbFactory.CreateDbParameter("@m", month));
            DataTable dt = DataFactory.Database().FindTableByProc("[Login].dbo.[PROC_Get_LoginCnt]", parameter.ToArray());
            //StringBuilder sb_cnt = new StringBuilder();
            //StringBuilder sb_ip = new StringBuilder();
            //for (int i = 1; i < 31; i++)
            //{
            //    sb_cnt.Append(dt.Rows[0][i].ToString() + ",");
            //    sb_ip.Append(dt.Rows[1][i].ToString() + ",");
            //}
            //object[] cnt = sb_cnt.ToString().Split(',');
            //object[] ip = sb_ip.ToString().Split(',');
            //var JsonData = new
            //{
            //    cntData = cnt,
            //    ipData = ip,
            //};
            return Content(dt.ToJson());
        }
        #endregion
    }
}