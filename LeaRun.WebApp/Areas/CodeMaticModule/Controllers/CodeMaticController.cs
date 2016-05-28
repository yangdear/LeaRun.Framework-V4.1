using LeaRun.Business;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace LeaRun.WebApp.Areas.CodeMaticModule.Controllers
{
    /// <summary>
    /// 代码生成器
    /// </summary>
    public class CodeMaticController : Controller
    {
        CodeMaticBll codematicbll = new CodeMaticBll();
        Base_DataBaseBll base_databasebll = new Base_DataBaseBll();
        /// <summary>
        /// 代码生成 初始化页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CodeMaticIndex()
        {
            return View();
        }
        /// <summary>
        /// 代码生成设置页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CodeMaticConten()
        {
            return View();
        }
        /// <summary>
        /// 加载数据库表 返回JSon
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTableNameTreeJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (ConfigHelper.AppSettings("CodeMaticMode") == "PowerDesigner")
            {
                XmlNodeList myXmlNodeList = codematicbll.GetTableName();
                foreach (XmlNode myXmlNode in myXmlNodeList)
                {
                    sb.Append("{");
                    sb.Append("\"id\":\"" + myXmlNode.ChildNodes[2].InnerText + "\",");
                    sb.Append("\"text\":\"" + myXmlNode.ChildNodes[1].InnerText + "\",");
                    sb.Append("\"value\":\"" + myXmlNode.ChildNodes[2].InnerText + "\",");
                    sb.Append("\"title\":\"" + myXmlNode.ChildNodes[2].InnerText + "\",");
                    sb.Append("\"img\":\"/Content/Images/Icon16/database_table.png\",");
                    sb.Append("\"isexpand\":true,");
                    sb.Append("\"hasChildren\":false,");
                    sb.Append("\"ChildNodes\":[]");
                    sb.Append("},");
                }
            }
            else if (ConfigHelper.AppSettings("CodeMaticMode") == "DataBase")
            {
                DataTable dt = base_databasebll.GetList();
                if (!DataHelper.IsExistRows(dt))
                {
                    foreach (DataRow itemRow in dt.Rows)
                    {
                        sb.Append("{");
                        sb.Append("\"id\":\"" + itemRow["name"] + "\",");
                        sb.Append("\"text\":\"" + itemRow["tdescription"] + "\",");
                        sb.Append("\"value\":\"" + itemRow["name"] + "\",");
                        sb.Append("\"title\":\"" + itemRow["name"] + "\",");
                        sb.Append("\"img\":\"/Content/Images/Icon16/database_table.png\",");
                        sb.Append("\"isexpand\":true,");
                        sb.Append("\"hasChildren\":false,");
                        sb.Append("\"ChildNodes\":[]");
                        sb.Append("},");
                    }
                }
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return Content(sb.ToString());
        }
        /// <summary>
        /// 加载数据库表字段 返回JSon
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFieldTreeJson(string tableCode)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            if (ConfigHelper.AppSettings("CodeMaticMode") == "PowerDesigner")
            {
                dt = codematicbll.GetColumns(tableCode);
            }
            else if (ConfigHelper.AppSettings("CodeMaticMode") == "DataBase")
            {
                dt = base_databasebll.GetColumnList(tableCode);
                dt.Columns["column"].ColumnName = "column_Name";
                dt.Columns["datatype"].ColumnName = "data_type";
                dt.Columns["length"].ColumnName = "char_col_decl_length";
                dt.Columns["remark"].ColumnName = "comments";
            }
            return Content(dt.ToJson());
        }

        #region 查看生成代码
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="ProjectName">项目名称</param>
        /// <param name="ClassName">类名备注</param>
        /// <param name="EntityName">实体类名称</param>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public ActionResult GetCodeBuilderEntity(string ProjectName, string ClassName, string EntityName, string table)
        {
            codematicbll.Company = "Learun";
            codematicbll.Author = "she";
            codematicbll.CreateYear = DateTime.Now.ToString("yyyy");
            codematicbll.CreateDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            codematicbll.ClassName = ClassName;
            codematicbll.ProjectName = ProjectName;
            codematicbll.EntityName = EntityName;

            DataTable dt = new DataTable();
            string PrimaryKeyColumns = "";
            if (ConfigHelper.AppSettings("CodeMaticMode") == "PowerDesigner")
            {
                dt = codematicbll.GetColumns(table);
                PrimaryKeyColumns = codematicbll.GetPrimaryKey(table);
            }
            else if (ConfigHelper.AppSettings("CodeMaticMode") == "DataBase")
            {
                dt = base_databasebll.GetColumnList(table);
                dt.Columns["column"].ColumnName = "column_Name";
                dt.Columns["datatype"].ColumnName = "data_type";
                dt.Columns["length"].ColumnName = "char_col_decl_length";
                dt.Columns["remark"].ColumnName = "comments";
                PrimaryKeyColumns = base_databasebll.GetPrimaryKey(table);
            }
            return Content(codematicbll.CodeBuilder_Entity(table, PrimaryKeyColumns, dt));
        }
        /// <summary>
        /// 生成业务层
        /// </summary>
        /// <param name="ProjectName">项目名称</param>
        /// <param name="ClassName">类名备注</param>
        /// <param name="EntityName">实体类名称</param>
        /// <param name="ServiceName">实体类名称</param>
        /// <param name="BusinessName">业务类名称</param>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public ActionResult GetCodeBuilderBusiness(string ProjectName, string ClassName, string ServiceName, string EntityName, string BusinessName, string table)
        {
            codematicbll.Company = "Learun";
            codematicbll.Author = "she";
            codematicbll.CreateYear = DateTime.Now.ToString("yyyy");
            codematicbll.CreateDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            codematicbll.ClassName = ClassName;
            codematicbll.ProjectName = ProjectName;
            codematicbll.EntityName = EntityName;
            codematicbll.BusinessName = BusinessName;
            codematicbll.ServiceName = ServiceName;

            return Content(codematicbll.GetCodeBuilderBusiness(table).ToString());
        }
        /// <summary>
        /// 生成表单页面
        /// </summary>
        /// <param name="Areas">业务区域</param>
        /// <param name="ControllerName">控制器名称</param>
        /// <param name="ClassName">类名备注</param>
        /// <param name="PageFormName">表单文件名称</param>
        /// <param name="taboe">表名</param>
        /// <param name="FromJson">表单参数</param>
        /// <param name="FormCss">表单Css</param>
        /// <param name="ColumnCount">显示列数</param>
        /// <returns></returns>
        public ActionResult GetCodeBuilderFrom(string Areas, string ControllerName, string ClassName, string PageFormName, string table, string FromJson, string FormCss, int ColumnCount)
        {
            codematicbll.Company = "Learun";
            codematicbll.Author = "she";
            codematicbll.CreateYear = DateTime.Now.ToString("yyyy");
            codematicbll.CreateDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            codematicbll.ClassName = ClassName;
            codematicbll.PageFormName = PageFormName;
            codematicbll.ControllerName = ControllerName;
            codematicbll.AreasName = Areas;

            return Content(codematicbll.GetCodeBuilderFrom(table, FromJson, ColumnCount, FormCss).ToString());
        }
        /// <summary>
        /// 生成明细页面
        /// </summary>
        /// <param name="Areas">业务区域</param>
        /// <param name="ControllerName">控制器名称</param>
        /// <param name="ClassName">类名备注</param>
        /// <param name="PageFormDetailName">明细文件名称</param>
        /// <param name="taboe">表名</param>
        /// <param name="FromJson">表单参数</param>
        /// <param name="FormCss">表单Css</param>
        /// <param name="ColumnCount">显示列数</param>
        /// <returns></returns>
        public ActionResult GetCodeBuilderFromDetail(string Areas, string ControllerName, string ClassName, string PageFormDetailName, string table, string FromDetailJson, string FormDetailCss, int ColumnCount)
        {
            codematicbll.Company = "Learun";
            codematicbll.Author = "she";
            codematicbll.CreateYear = DateTime.Now.ToString("yyyy");
            codematicbll.CreateDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            codematicbll.ClassName = ClassName;
            codematicbll.PageFormDetailName = PageFormDetailName;
            codematicbll.ControllerName = ControllerName;
            codematicbll.AreasName = Areas;

            return Content(codematicbll.GetCodeBuilderFromDetail(table, FromDetailJson, ColumnCount, FormDetailCss).ToString());
        }
        /// <summary>
        /// 生成列表页面
        /// </summary>
        /// <param name="Areas">业务区域</param>
        /// <param name="ControllerName">控制器名称</param>
        /// <param name="ClassName">类名备注</param>
        /// <param name="PageListName">列表文件名称</param>
        /// <param name="table">表名</param>
        /// <param name="showFieldJson">显示字段</param>
        /// <param name="AllowOrder">是否排序</param>
        /// <param name="OrderType">排序类型</param>
        /// <param name="OrderField">排序字段</param>
        /// <param name="AllowPageing">是否分页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="PageLayout">页面布局</param>
        /// <param name="method">操作</param>
        /// <returns></returns>
        public ActionResult GetCodeBuilderList(string Areas, string ControllerName, string ClassName, string PageListName, string table,
            string showFieldJson, string AllowOrder, string OrderType, string OrderField, string AllowPageing, string pageSize, string PageLayout, string method)
        {
            codematicbll.Company = "Learun";
            codematicbll.Author = "she";
            codematicbll.CreateYear = DateTime.Now.ToString("yyyy");
            codematicbll.CreateDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            codematicbll.ClassName = ClassName;
            codematicbll.PageListName = PageListName;
            codematicbll.ControllerName = ControllerName;
            codematicbll.AreasName = Areas;

            Hashtable htmethod = HashtableHelper.ParameterToHashtable(method);
            return Content(codematicbll.GetCodeBuilderList(table, showFieldJson, AllowOrder, OrderType, OrderField, AllowPageing, pageSize, PageLayout, htmethod).ToString());
        }
        /// <summary>
        /// 生成控制器
        /// </summary>
        /// <param name="Areas">业务区域</param>
        /// <param name="ClassName">类名备注</param>
        /// <param name="ControllerName">控制器名称</param>
        /// <param name="BusinessName">业务层名称</param>
        /// <param name="EntityName">实体层名称</param>
        /// <param name="table">名表</param>
        /// <returns></returns>
        public ActionResult GetCodeBuilderController(string Areas, string ClassName, string ControllerName, string BusinessName, string EntityName, string table)
        {
            codematicbll.Company = "Learun";
            codematicbll.Author = "she";
            codematicbll.CreateYear = DateTime.Now.ToString("yyyy");
            codematicbll.CreateDate = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
            codematicbll.ClassName = ClassName;
            codematicbll.ControllerName = ControllerName;
            codematicbll.BusinessName = BusinessName;
            codematicbll.EntityName = EntityName;
            codematicbll.AreasName = Areas;
            return Content(codematicbll.GetCodeBuilderController(table).ToString());
        }
        #endregion

        #region 下载生成代码
        /// <summary>
        /// 下载生成代码
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public ActionResult DownloadCodeBuilder(string table)
        {
            var path = Server.MapPath("~/Areas/CodeMaticModule/DataModel/CodeMatic/" + table + ".zip");
            return File(path, "application/zip-x-compressed", table + ".zip");
        }
        #endregion
    }
}
