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
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.CommonModule.Controllers
{
    /// <summary>
    /// 数据库管理控制器
    /// </summary>
    public class DataBaseController : Controller
    {
        Base_DataBaseBll base_databasebll = new Base_DataBaseBll();

        #region 列表
        [ManagerPermission(PermissionMode.Enforce)]
        /// <summary>
        /// 数据库管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 【数据库管理】返回列表JONS
        /// </summary>
        /// <returns></returns>
        public ActionResult GridListJson(string tableName)
        {
            try
            {
                Stopwatch watch = CommonHelper.TimerStart();
                DataTable ListData = base_databasebll.GetList(tableName);
                var JsonData = new
                {
                    records = ListData.Rows.Count,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = ListData,
                };
                string str = JsonData.ToJson();
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                Base_SysLogBll.Instance.WriteLog("", OperationType.Query, "-1", "异常错误：" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 【数据库管理】返回列表JONS
        /// </summary>
        /// <returns></returns>
        public ActionResult GridColumnListJson(string tableName)
        {
            try
            {
                DataTable ListData = base_databasebll.GetColumnList(tableName);
                var JsonData = new
                {
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
        /// 删除表
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult DeleteTable(string KeyValue)
        {
            try
            {
                base_databasebll.DeleteTable(KeyValue);
                return Content(new JsonMessage { Success = true, Code = "1", Message = "删除成功" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        #endregion

        #region 表单
        /// <summary>
        /// 表单
        /// </summary>
        /// <returns></returns>
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="TableDescription">表说明</param>
        /// <param name="TableFieldJson">表字段</param>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubmitForm(string TableName, string TableDescription, string TableFieldJson, string KeyValue)
        {
            try
            {
                string Message = KeyValue == "" ? "新建成功。" : "编辑成功。";
                string strSql = JsonToSql(TableName, TableDescription, TableFieldJson);
                base_databasebll.CreateTable(new StringBuilder(strSql), KeyValue);
                return Content(new JsonMessage { Success = true, Code = "1", Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 查看Sql语句
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="TableDescription">表说明</param>
        /// <param name="TableFieldJson">表字段</param>
        public ActionResult LookSql(string TableName, string TableDescription, string TableFieldJson)
        {
            return Content(JsonToSql(TableName, TableDescription, TableFieldJson));
        }
        /// <summary>
        /// Json转换SQL语句
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="TableDescription">表说明</param>
        /// <param name="TableFieldJson">表字段</param>
        /// <returns></returns>
        public string JsonToSql(string TableName, string TableDescription, string TableFieldJson)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("/*==============================================================*/\r\n");
            strSql.Append("--Table: " + TableName + "                                             \r\n");
            strSql.Append("--Description: " + TableDescription + "                                \r\n");
            strSql.Append("/*==============================================================*/\r\n");
            strSql.Append("CREATE TABLE " + TableName + "(\r\n");
            DataTable TableField = TableFieldJson.JsonToDataTable();
            if (!DataHelper.IsExistRows(TableField))
            {
                foreach (DataRow item in TableField.Rows)
                {
                    string Column = item["Column"].ToString();                      //列名
                    if (!string.IsNullOrEmpty(Column))
                    {
                        string DataBaseType = item["DataBaseType"].ToString();      //数据类型
                        string AllowNull = item["AllowNull"].ToString();            //允许空
                        string Identify = item["Identify"].ToString();              //标识
                        string PrimaryKey = item["PrimaryKey"].ToString();          //主键
                        string DefaultValue = item["DefaultValue"].ToString();      //默认值
                        string Description = item["Description"].ToString();        //说明
                        strSql.Append("\t" + Column + "\t\t\t\t");
                        strSql.Append(DataBaseType + " ");
                        strSql.Append(AllowNull == "1" ? " NULL" : " NOT NULL " + " ");
                        strSql.Append(PrimaryKey == "0" ? "" : " PRIMARY KEY" + " ");
                        strSql.Append(Identify == "0" ? "" : " identity(1,1)" + " ");
                        if (!string.IsNullOrEmpty(DefaultValue))
                        {
                            strSql.Append(" default(" + DefaultValue + ")" + " ");
                        }
                        strSql.Append(",\r\n");
                    }
                }
            }
            strSql.Append(")\r\n");
            //strSql.Append("go\r\n\r\n");
            //为表添加描述信息
            strSql.Append("EXECUTE sp_addextendedproperty 'MS_Description', \r\n\t'" + TableDescription + "',\r\n\t'user', 'dbo', 'table', '" + TableName + "'\r\n");
            //strSql.Append("go\r\n\r\n");
            if (!DataHelper.IsExistRows(TableField))
            {
                foreach (DataRow item in TableField.Rows)
                {
                    string Column = item["Column"].ToString();                      //列名
                    if (!string.IsNullOrEmpty(Column))
                    {
                        string Description = item["Description"].ToString();        //说明
                        //为字段添加描述信息
                        strSql.Append("EXECUTE sp_addextendedproperty 'MS_Description', \r\n\t'" + Description + "',\r\n\t'user', 'dbo', 'table', '" + TableName + "', 'column', '" + Column + "'\r\n");
                        //strSql.Append("go\r\n\r\n");
                    }
                }
            }
            return strSql.ToString();
        }
        #endregion

        #region 明细
        /// <summary>
        /// 明细
        /// </summary>
        /// <returns></returns>
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// 查询数据库表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ParameterJson">查询条件</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public ActionResult GridDataTableListJson(string tableName, string ParameterJson, JqGridParam jqgridparam)
        {
            try
            {
                Stopwatch watch = CommonHelper.TimerStart();
                DataTable ListData = base_databasebll.GetDataTableList(tableName, ParameterJson, ref jqgridparam);
                var JsonData = new
                {
                    total = jqgridparam.total,
                    page = jqgridparam.page,
                    records = jqgridparam.records,
                    costtime = CommonHelper.TimerEnd(watch),
                    rows = ListData,
                };
                string str = JsonData.ToJson();
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                Base_SysLogBll.Instance.WriteLog("", OperationType.Query, "-1", "异常错误：" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 编辑表格行数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pk">主键字段</param>
        /// <param name="entityJons">实体参数</param>
        /// <returns></returns>
        public ActionResult EditDataTableRow(string tableName, string pk, string entityJons)
        {
            try
            {
                Hashtable ht = new Hashtable();
                DataTable dt = DataHelper.GetNewDataTable(base_databasebll.GetColumnList(tableName), "datatype = 'datetime'");
                foreach (DataRow item in dt.Rows)
                {
                    ht[item["column"].ToString().ToLower()] = item["column"].ToString().ToLower();
                }
                Hashtable TableField = HashtableHelper.JsonToHashtable(entityJons);
                foreach (string item in ht.Keys)
                {
                    TableField.Remove(item);
                }
                int IsOk = DataFactory.Database().Update(tableName, TableField, pk);
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "操作成功" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 删除表格行数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pk">主键字段</param>
        /// <returns></returns>
        public ActionResult DeleteDataTableRow(string tableName, string pk, string entityJons)
        {
            try
            {
                Hashtable TableField = HashtableHelper.JsonToHashtable(entityJons);
                int IsOk = DataFactory.Database().Delete(tableName, pk.ToLower(), TableField[pk.ToLower()].ToString());
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "删除成功" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        #endregion

        #region SQL命令
        /// <summary>
        /// SQL命令视图
        /// </summary>
        /// <returns></returns>
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult SqlCommand()
        {
            return View();
        }
        #endregion

        #region 数据库备份计划
        /// <summary>
        /// 数据库备份视图
        /// </summary>
        /// <returns></returns>
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult DbBackup()
        {
            return View();
        }
        /// <summary>
        ///【备份计划】返回列表JONS
        /// </summary>
        /// <returns></returns>
        public ActionResult DbBackupList()
        {
            try
            {
                List<Base_BackupJob> ListData = DataFactory.Database().FindList<Base_BackupJob>("ORDER BY CreateDate DESC");
                var JsonData = new
                {
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
        /// 删除备份计划
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="JobName">计划名称</param>
        /// <returns></returns>
        public ActionResult DeleteDbBackup(string KeyValue, string JobName)
        {
            try
            {
                base_databasebll.DeleteDbBackup(KeyValue, JobName);
                return Content(new JsonMessage { Success = true, Code = "1", Message = "删除成功" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 新增备份计划
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="_Mode"></param>
        /// <param name="_StartTime"></param>
        /// <returns></returns>
        public ActionResult AddDbBackup(Base_BackupJob entity, string _Mode, string _StartTime)
        {
            try
            {
                entity.FilePath = entity.FilePath;
                entity.Create();
                base_databasebll.CreateDbBackup(entity, _Mode, _StartTime);
                return Content(new JsonMessage { Success = true, Code = "1", Message = "操作成功" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        #endregion
    }
}
