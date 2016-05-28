using LeaRun.Entity;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace LeaRun.Business
{
    /// <summary>
    /// 代码生成器
    /// </summary>
    public class CodeMaticBll
    {
        Base_DataBaseBll base_databasebll = new Base_DataBaseBll();
        /// <summary>
        /// 加载所有数据表
        /// </summary>
        /// <returns></returns>
        public XmlNodeList GetTableName()
        {
            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(DirFileHelper.MapPath("~/Areas/CodeMaticModule/DataModel/LeaRun.Framework.pdm"));
            // 获取表格
            string selectPath = "/Model/*[local-name()='RootObject' and namespace-uri()='object'][1]/*[local-name()='Children' and namespace-uri()='collection'][1]/*[local-name()='Model' and namespace-uri()='object'][1]/*[local-name()='Tables' and namespace-uri()='collection'][1]";
            XmlNodeList myXmlNodeList = myXmlDocument.SelectSingleNode(selectPath).ChildNodes;
            return myXmlNodeList;
        }
        /// <summary>
        /// 获取某一个表的所有字段
        /// </summary>
        /// <param name="tableCode">查询指定表</param>
        /// <returns></returns>
        public DataTable GetColumns(string tableCode)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(tableCode))
            {
                DataTable dt = new DataTable();
                DataColumn dc1 = new DataColumn("column_Name", Type.GetType("System.String"));
                DataColumn dc2 = new DataColumn("comments", Type.GetType("System.String"));
                DataColumn dc3 = new DataColumn("char_col_decl_length", Type.GetType("System.String"));
                DataColumn dc4 = new DataColumn("data_type", Type.GetType("System.String"));
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);

                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(DirFileHelper.MapPath("~/Areas/CodeMaticModule/DataModel/LeaRun.Framework.pdm"));
                // 获取表格
                string selectPath = "/Model/*[local-name()='RootObject' and namespace-uri()='object'][1]/*[local-name()='Children' and namespace-uri()='collection'][1]/*[local-name()='Model' and namespace-uri()='object'][1]/*[local-name()='Tables' and namespace-uri()='collection'][1]";
                XmlNodeList myXmlNodeList = myXmlDocument.SelectSingleNode(selectPath).ChildNodes;
                foreach (XmlNode myXmlNode in myXmlNodeList)
                {
                    if (myXmlNode.ChildNodes[2].InnerText.Equals(tableCode))
                    {
                        XmlNodeList myXmlNodeList_field = myXmlNode.ChildNodes[10].ChildNodes;
                        foreach (XmlNode myXmlNode_field in myXmlNodeList_field)
                        {
                            int count = myXmlNode_field.ChildNodes.Count;
                            DataRow dr = dt.NewRow();
                            dr["column_Name"] = myXmlNode_field.ChildNodes[2].InnerText;
                            dr["comments"] = myXmlNode_field.ChildNodes[1].InnerText;
                            dr["data_type"] = myXmlNode_field.ChildNodes[9].InnerText;
                            if (count > 9)
                            {
                                try
                                {
                                    dr["char_col_decl_length"] = myXmlNode_field.ChildNodes[10].InnerText;
                                }
                                catch (Exception)
                                {
                                }
                                finally
                                {

                                }
                            }
                            dt.Rows.Add(dr);
                        }
                        break;
                    }
                }
                return dt;
            }
            return null;
        }
        /// <summary>
        /// 获取某一个表的主键字段
        /// </summary>
        /// <param name="tableCode">查询指定表</param>
        /// <returns></returns>
        public string GetPrimaryKey(string tableCode)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(tableCode))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(DirFileHelper.MapPath("~/Areas/CodeMaticModule/DataModel/LeaRun.Framework.pdm"));
                // 获取表格
                string selectPath = "/Model/*[local-name()='RootObject' and namespace-uri()='object'][1]/*[local-name()='Children' and namespace-uri()='collection'][1]/*[local-name()='Model' and namespace-uri()='object'][1]/*[local-name()='Tables' and namespace-uri()='collection'][1]";
                XmlNodeList myXmlNodeList = myXmlDocument.SelectSingleNode(selectPath).ChildNodes;
                foreach (XmlNode myXmlNode in myXmlNodeList)
                {
                    if (myXmlNode.ChildNodes[2].InnerText.Equals(tableCode))
                    {
                        XmlNodeList myXmlNodeList_field = myXmlNode.ChildNodes[11].ChildNodes;
                        foreach (XmlNode myXmlNode_field in myXmlNodeList_field)
                        {
                            return myXmlNode_field.ChildNodes[2].InnerText;
                        }
                    }
                }
            }
            return "";
        }

        #region 代码拼接组件

        #region C#实体数据类型
        /// <summary>
        /// C#实体数据类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string FindModelsType(string name)
        {
            name = name.ToLower();
            if (name == "int" || name == "number" || name == "integer" || name == "smallint")
            {
                return "int?";
            }
            else if (name == "tinyint")
            {
                return "byte?";
            }
            else if (name == "numeric" || name == "real" || name == "float")
            {
                return "Single?";
            }
            else if (name == "float")
            {
                return "float?";
            }
            else if (name == "decimal" || name == "number(8,2)")
            {
                return "decimal?";
            }
            else if (name == "char" || name == "varchar" || name == "nvarchar2" || name == "text" || name == "nchar" || name == "nvarchar" || name == "ntext")
            {
                return "string";
            }
            else if (name == "bit")
            {
                return "bool?";
            }
            else if (name == "datetime" || name == "date" || name == "smalldatetime")
            {
                return "DateTime?";
            }
            else if (name == "money" || name == "smallmoney")
            {
                return "double?";
            }
            else
            {
                return "string";
            }
        }
        #endregion

        #region 公共变量
        /// <summary>
        /// 类名备注
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 实体类名称
        /// </summary>
        public string EntityName { get; set; }
        /// <summary>
        /// 数据类名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 业务类名称
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 表单页名称
        /// </summary>
        public string PageFormName { get; set; }
        /// <summary>
        /// 表单页名称
        /// </summary>
        public string PageFormDetailName { get; set; }
        /// <summary>
        /// 列表页名称
        /// </summary>
        public string PageListName { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 业务区域
        /// </summary>
        public string AreasName { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 创建年份
        /// </summary>
        public string CreateYear { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate { get; set; }
        #endregion

        #region 生成实体类
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="Key">表主键</param>
        /// <param name="Entity_dt">字段</param>
        /// <returns></returns>
        public string CodeBuilder_Entity(string table, string Key, DataTable Entity_dt)
        {
            StringBuilder sb_Entity = new StringBuilder();
            sb_Entity.Append("//=====================================================================================\r\n");
            sb_Entity.Append("// All Rights Reserved , Copyright @ " + Company + " " + CreateYear + "\r\n");
            sb_Entity.Append("// Software Developers @ " + Company + " " + CreateYear + "\r\n");
            sb_Entity.Append("//=====================================================================================\r\n\r\n");
            sb_Entity.Append("using LeaRun.DataAccess.Attributes;\r\n");
            sb_Entity.Append("using LeaRun.Utilities;\r\n");
            sb_Entity.Append("using System;\r\n");
            sb_Entity.Append("using System.ComponentModel;\r\n");
            sb_Entity.Append("using System.ComponentModel.DataAnnotations;\r\n");
            sb_Entity.Append("using System.Text;\r\n\r\n");

            sb_Entity.Append("namespace " + ProjectName + "\r\n");
            sb_Entity.Append("{\r\n");
            sb_Entity.Append("    /// <summary>\r\n");
            sb_Entity.Append("    /// " + ClassName + "\r\n");
            sb_Entity.Append("    /// <author>\r\n");
            sb_Entity.Append("    ///		<name>" + Author + "</name>\r\n");
            sb_Entity.Append("    ///		<date>" + CreateDate + "</date>\r\n");
            sb_Entity.Append("    /// </author>\r\n");
            sb_Entity.Append("    /// </summary>\r\n");
            sb_Entity.Append("    [Description(\"" + ClassName + "\")]\r\n");
            sb_Entity.Append("    [PrimaryKey(\"" + Key + "\")]\r\n");
            sb_Entity.Append("    public class " + EntityName + " : BaseEntity\r\n");
            sb_Entity.Append("    {\r\n");

            if (!DataHelper.IsExistRows(Entity_dt))
            {
                sb_Entity.Append("        #region 获取/设置 字段值\r\n");
                for (int i = 0; i < Entity_dt.Rows.Count; i++)
                {
                    string column = Entity_dt.Rows[i]["column_name"].ToString();
                    string datatype = FindModelsType(Entity_dt.Rows[i]["data_type"].ToString());
                    string comments = Entity_dt.Rows[i]["comments"].ToString();
                    sb_Entity.Append("        /// <summary>\r\n");
                    sb_Entity.Append("        /// " + comments + "\r\n");
                    sb_Entity.Append("        /// </summary>\r\n");
                    sb_Entity.Append("        /// <returns></returns>\r\n");
                    sb_Entity.Append("        [DisplayName(\"" + comments + "\")]\r\n");
                    sb_Entity.Append("        public " + datatype + " " + column + " { get; set; }\r\n");
                }
                sb_Entity.Append("        #endregion\r\n\r\n");

                sb_Entity.Append("        #region 扩展操作\r\n");
                sb_Entity.Append("        /// <summary>\r\n");
                sb_Entity.Append("        /// 新增调用\r\n");
                sb_Entity.Append("        /// </summary>\r\n");
                sb_Entity.Append("        public override void Create()\r\n");
                sb_Entity.Append("        {\r\n");
                sb_Entity.Append("            this." + Key + " = CommonHelper.GetGuid;\r\n");
                sb_Entity.Append("            " + IsCreateDate(Entity_dt) + "");
                sb_Entity.Append("            " + IsCreateUserId(Entity_dt) + "");
                sb_Entity.Append("            " + IsCreateUserName(Entity_dt) + "");
                sb_Entity.Append("        }\r\n");
                sb_Entity.Append("        /// <summary>\r\n");
                sb_Entity.Append("        /// 编辑调用\r\n");
                sb_Entity.Append("        /// </summary>\r\n");
                sb_Entity.Append("        /// <param name=\"KeyValue\"></param>\r\n");
                sb_Entity.Append("        public override void Modify(string KeyValue)\r\n");
                sb_Entity.Append("        {\r\n");
                sb_Entity.Append("            this." + Key + " = KeyValue;\r\n");
                sb_Entity.Append("            " + IsModifyDate(Entity_dt) + "");
                sb_Entity.Append("            " + IsModifyUserId(Entity_dt) + "");
                sb_Entity.Append("            " + IsModifyUserName(Entity_dt) + "");
                sb_Entity.Append("        }\r\n");
                sb_Entity.Append("        #endregion\r\n");

            }
            sb_Entity.Append("    }\r\n");
            sb_Entity.Append("}");
            WriteCodeBuilder(table + "\\" + EntityName + ".cs", sb_Entity.ToString());
            return sb_Entity.ToString();
        }
        public string IsCreateDate(DataTable dt)
        {
            DataTable newdt = DataHelper.GetNewDataTable(dt, "column_name = 'CreateDate'");
            if (newdt.Rows.Count > 0)
            {
                return "this.CreateDate = DateTime.Now;\r\n";
            }
            return "";
        }
        public string IsCreateUserId(DataTable dt)
        {
            DataTable newdt = DataHelper.GetNewDataTable(dt, "column_name = 'CreateUserId'");
            if (newdt.Rows.Count > 0)
            {
                return "this.CreateUserId = ManageProvider.Provider.Current().UserId;\r\n";
            }
            return "";
        }
        public string IsCreateUserName(DataTable dt)
        {
            DataTable newdt = DataHelper.GetNewDataTable(dt, "column_name = 'CreateUserName'");
            if (newdt.Rows.Count > 0)
            {
                return "this.CreateUserName = ManageProvider.Provider.Current().UserName;\r\n";
            }
            return "";
        }

        public string IsModifyDate(DataTable dt)
        {
            DataTable newdt = DataHelper.GetNewDataTable(dt, "column_name = 'ModifyDate'");
            if (newdt.Rows.Count > 0)
            {
                return "this.ModifyDate = DateTime.Now;\r\n";
            }
            return "";
        }
        public string IsModifyUserId(DataTable dt)
        {
            DataTable newdt = DataHelper.GetNewDataTable(dt, "column_name = 'ModifyUserId'");
            if (newdt.Rows.Count > 0)
            {
                return "this.ModifyUserId = ManageProvider.Provider.Current().UserId;\r\n";
            }
            return "";
        }
        public string IsModifyUserName(DataTable dt)
        {
            DataTable newdt = DataHelper.GetNewDataTable(dt, "column_name = 'ModifyUserName'");
            if (newdt.Rows.Count > 0)
            {
                return "this.ModifyUserName = ManageProvider.Provider.Current().UserName;\r\n";
            }
            return "";
        }
        #endregion

        #region 业务逻辑类
        /// <summary>
        /// 生成业务逻辑类
        /// </summary>
        /// <param name="table">表</param>
        /// <returns></returns>
        public string GetCodeBuilderBusiness(string table)
        {
            StringBuilder sb_Business = new StringBuilder();
            sb_Business.Append("//=====================================================================================\r\n");
            sb_Business.Append("// All Rights Reserved , Copyright @ " + Company + " " + CreateYear + "\r\n");
            sb_Business.Append("// Software Developers @ " + Company + " " + CreateYear + "\r\n");
            sb_Business.Append("//=====================================================================================\r\n\r\n");
            sb_Business.Append("using LeaRun.Entity;\r\n");
            sb_Business.Append("using LeaRun.Repository;\r\n");
            sb_Business.Append("using LeaRun.Utilities;\r\n");
            sb_Business.Append("using System.Collections;\r\n");
            sb_Business.Append("using System.Collections.Generic;\r\n");
            sb_Business.Append("using System.Text;\r\n\r\n");
            sb_Business.Append("namespace " + ProjectName + "\r\n");
            sb_Business.Append("{\r\n");
            sb_Business.Append("    /// <summary>\r\n");
            sb_Business.Append("    /// " + ClassName + "\r\n");
            sb_Business.Append("    /// <author>\r\n");
            sb_Business.Append("    ///		<name>" + Author + "</name>\r\n");
            sb_Business.Append("    ///		<date>" + CreateDate + "</date>\r\n");
            sb_Business.Append("    /// </author>\r\n");
            sb_Business.Append("    /// </summary>\r\n");
            sb_Business.Append("    public class " + BusinessName + " : RepositoryFactory<" + EntityName + ">\r\n");
            sb_Business.Append("    {\r\n");
            sb_Business.Append("    }\r\n");
            sb_Business.Append("}");
            WriteCodeBuilder(table + "\\" + BusinessName + ".cs", sb_Business.ToString());
            return sb_Business.ToString();
        }
        #endregion

        #region 页面表单
        /// <summary>
        /// 页面表单
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="FromJson">显示表单字段</param>
        /// <param name="ColumnCount">显示列数模式</param>
        /// <param name="FormCss">表单css样式</param>
        /// <returns></returns>
        public string GetCodeBuilderFrom(string table, string FromJson, int ColumnCount, string FormCss)
        {
            StringBuilder sb_From = new StringBuilder();
            sb_From.Append("@{\r\n");
            sb_From.Append("    ViewBag.Title = \"" + ClassName + "》表单页面\";\r\n");
            sb_From.Append("    Layout = \"~/Views/Shared/_LayoutForm.cshtml\";\r\n");
            sb_From.Append("}\r\n");
            sb_From.Append("<script type=\"text/javascript\">\r\n");
            sb_From.Append("    var KeyValue = GetQuery('KeyValue');//主键\r\n");
            sb_From.Append("    $(function () {\r\n");
            sb_From.Append("        InitControl();\r\n");
            sb_From.Append("    })\r\n");
            sb_From.Append("    //得到一个对象实体\r\n");
            sb_From.Append("    function InitControl() {\r\n");
            sb_From.Append("        if (!!GetQuery('KeyValue')) {\r\n");
            sb_From.Append("            AjaxJson(\"/" + AreasName + "/" + StringHelper.DelLastLength(ControllerName, 10) + "/SetForm\", { KeyValue: KeyValue }, function (data) {\r\n");
            sb_From.Append("                SetWebControls(data);\r\n");
            sb_From.Append("            });\r\n");
            sb_From.Append("        }\r\n");
            sb_From.Append("    }\r\n");
            sb_From.Append("    //保存事件\r\n");
            sb_From.Append("    function AcceptClick() {\r\n");
            sb_From.Append("        if (!CheckDataValid('#form1')) {\r\n");
            sb_From.Append("            return false;\r\n");
            sb_From.Append("        }\r\n");
            sb_From.Append("        var postData = GetWebControls(\"#form1\");\r\n");
            sb_From.Append("        AjaxJson(\"/" + AreasName + "/" + StringHelper.DelLastLength(ControllerName, 10) + "/SubmitForm?KeyValue=\" + KeyValue, postData, function (data) {\r\n");
            sb_From.Append("            tipDialog(data.Message, 3, data.Code);\r\n");
            sb_From.Append("            top.frames[tabiframeId()].windowload();\r\n");
            sb_From.Append("            closeDialog();\r\n");
            sb_From.Append("        });\r\n");
            sb_From.Append("    }\r\n");
            sb_From.Append("</script>\r\n");
            sb_From.Append("<form id=\"form1\" style=\"margin: 1px\">\r\n");
            sb_From.Append("    <div id=\"message\" style=\"display: none\"></div>\r\n");
            List<Base_FormAttribute> ListData = new List<Base_FormAttribute>();
            ListData = (from itementity in FromJson.JonsToList<Base_FormAttribute>()
                        where itementity.Enabled == 1
                        orderby itementity.SortCode ascending
                        select itementity).ToList<Base_FormAttribute>();
            sb_From.Append("    " + Base_FormAttributeBll.Instance.CreateBuildFormTable(ColumnCount, ListData).Replace("Build_", ""));
            sb_From.Append("</form>\r\n");
            WriteCodeBuilder(table + "\\" + StringHelper.DelLastLength(ControllerName, 10) + "\\" + PageFormName + ".cshtml", sb_From.ToString());
            return sb_From.ToString();
        }
        #endregion

        #region 页面明细
        /// <summary>
        /// 页面明细
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="FromJson">显示表单字段</param>
        /// <param name="ColumnCount">显示列数模式</param>
        /// <param name="FormCss">表单css样式</param>
        /// <returns></returns>
        public string GetCodeBuilderFromDetail(string table, string FromJson, int ColumnCount, string FormCss)
        {
            StringBuilder sb_From = new StringBuilder();
            sb_From.Append("@{\r\n");
            sb_From.Append("    ViewBag.Title = \"" + ClassName + "》明细页面\";\r\n");
            sb_From.Append("    Layout = \"~/Views/Shared/_LayoutForm.cshtml\";\r\n");
            sb_From.Append("}\r\n");
            sb_From.Append("<script type=\"text/javascript\">\r\n");
            sb_From.Append("    $(function () {\r\n");
            sb_From.Append("        InitControl();\r\n");
            sb_From.Append("    })\r\n");
            sb_From.Append("    //得到一个对象实体\r\n");
            sb_From.Append("    function InitControl() {\r\n");
            sb_From.Append("        if (!!GetQuery('KeyValue')) {\r\n");
            sb_From.Append("            AjaxJson(\"/" + AreasName + "/" + StringHelper.DelLastLength(ControllerName, 10) + "/SetForm\", { KeyValue: GetQuery('KeyValue') }, function (data) {\r\n");
            sb_From.Append("                SetWebControls(data);\r\n");
            sb_From.Append("            });\r\n");
            sb_From.Append("        }\r\n");
            sb_From.Append("    }\r\n");
            sb_From.Append("</script>\r\n");
            sb_From.Append("<form id=\"form1\" style=\"margin: 1px\">\r\n");
            List<Base_FormAttribute> ListData = new List<Base_FormAttribute>();
            ListData = (from itementity in FromJson.JonsToList<Base_FormAttribute>()
                        where itementity.Enabled == 1
                        orderby itementity.SortCode ascending
                        select itementity).ToList<Base_FormAttribute>();
            sb_From.Append("    " + Base_FormAttributeBll.Instance.CreateBuildFormTable(ColumnCount, ListData).Replace("Build_", ""));
            sb_From.Append("</form>\r\n");
            WriteCodeBuilder(table + "\\" + StringHelper.DelLastLength(ControllerName, 10) + "\\" + PageFormDetailName + ".cshtml", sb_From.ToString());
            return sb_From.ToString();
        }
        #endregion

        #region 页面列表
        /// <summary>
        /// 表头显示/隐藏
        /// </summary>
        /// <param name="Field"></param>
        /// <returns></returns>
        public string IsShow_Field(string Field)
        {
            if (Field == "1")
            {
                return ",hidden: true";
            }
            return "";
        }
        /// <summary>
        /// 页面表格列表
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="showField">显示字段</param>
        /// <param name="AllowOrder">是否排序</param>
        /// <param name="OrderType">排序类型</param>
        /// <param name="OrderField">排序字段</param>
        /// <param name="AllowPageing">是否分页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="PageLayout">页面布局</param>
        /// <param name="method">操作</param>
        /// <returns></returns>
        public string GetCodeBuilderList(string table,
            string showFieldJson, string AllowOrder, string OrderType, string OrderField, string AllowPageing, string pageSize, string PageLayout, Hashtable method)
        {
            string PrimaryKeyColumns = base_databasebll.GetPrimaryKey(table);
            StringBuilder sb_List = new StringBuilder();
            StringBuilder sb_colModel = new StringBuilder();
            List<JqGridColumn> ListData = showFieldJson.JonsToList<JqGridColumn>();
            var query = from entity in ListData
                        orderby entity.SortCode ascending /*descending*/
                        select entity;
            if (ListData != null)
            {
                foreach (JqGridColumn item in query)
                {
                    if (item.label == null && item.name == null)
                    {
                        continue;
                    }
                    string PropertyName = item.label;
                    string ControlId = item.name;
                    int width = item.width;
                    string align = item.align;
                    string hidden = item.hidden;
                    string Sortable = item.sortable == "1" ? "true" : "false";
                    string Formatter = item.formatter;
                    int Enabled = CommonHelper.GetInt(item.Enabled);
                    if (Enabled == 1)
                    {
                        sb_colModel.Append("                { label: '" + PropertyName + "', name: '" + ControlId + "', index: '" + ControlId + "', width: " + width + ", align: '" + align + "',sortable: " + Sortable + " " + IsShow_Field(hidden) + " },\r\n");
                    }
                }
            }
            sb_List.Append("@{\r\n");
            sb_List.Append("    ViewBag.Title = \"" + ClassName + "\";\r\n");
            sb_List.Append("    Layout = \"~/Views/Shared/_LayoutIndex.cshtml\";\r\n");
            sb_List.Append("}\r\n");
            sb_List.Append("<script type=\"text/javascript\">\r\n");
            sb_List.Append("    $(function () {\r\n");
            sb_List.Append("        GetGrid();\r\n");
            sb_List.Append("    })\r\n");
            sb_List.Append("    //加载表格\r\n");
            sb_List.Append("    function GetGrid() {\r\n");
            sb_List.Append("        $(\"#gridTable\").jqGrid({\r\n");
            if (AllowPageing == "1")
            {
                sb_List.Append("        url: \"@Url.Content(\"~/" + AreasName + "/" + StringHelper.DelLastLength(ControllerName, 10) + "/GridPageJson\")\",\r\n");
            }
            else
            {
                sb_List.Append("        url: \"@Url.Content(\"~/" + AreasName + "/" + StringHelper.DelLastLength(ControllerName, 10) + "/GridJson\")\",\r\n");
            }
            sb_List.Append("        datatype: \"json\",\r\n");
            sb_List.Append("        height: $(window).height() - 149,\r\n");
            sb_List.Append("        autowidth: true,\r\n");
            sb_List.Append("        colModel: [\r\n" + sb_colModel + "        ],\r\n");
            if (AllowPageing == "1")
            {
                sb_List.Append("        pager: \"#gridPager\",\r\n");
            }
            else
            {
                sb_List.Append("        pager: false,\r\n");
            }
            sb_List.Append("        sortname: '" + OrderField + "',\r\n");
            sb_List.Append("        sortorder: '" + OrderType + "',\r\n");
            sb_List.Append("        rownumbers: true,\r\n");
            sb_List.Append("        shrinkToFit: false,\r\n");
            sb_List.Append("        gridview: true,\r\n");
            sb_List.Append("    });\r\n");
            sb_List.Append("}\r\n");
            if (method["AllowInsert"].ToString() == "checked")
            {
                sb_List.Append("    //新增\r\n");
                sb_List.Append("    function btn_add() {\r\n");
                sb_List.Append("        var url = \"/" + AreasName + "/" + StringHelper.DelLastLength(ControllerName, 10) + "/Form\"\r\n");
                sb_List.Append("        openDialog(url, \"Form\", \"新增" + ClassName + "\", 770, 395, function (iframe) {\r\n");
                sb_List.Append("            top.frames[iframe].AcceptClick()\r\n");
                sb_List.Append("        });\r\n");
                sb_List.Append("    }\r\n");
            }
            if (method["AllowUpdate"].ToString() == "checked")
            {
                sb_List.Append("    //编辑\r\n");
                sb_List.Append("    function btn_edit() {\r\n");
                sb_List.Append("        var KeyValue = GetJqGridRowValue(\"#gridTable\", \"" + PrimaryKeyColumns + "\");\r\n");
                sb_List.Append("        if (IsChecked(KeyValue)) {\r\n");
                sb_List.Append("            var url = \"/" + AreasName + "/" + StringHelper.DelLastLength(ControllerName, 10) + "/Form?KeyValue=\" + KeyValue;\r\n");
                sb_List.Append("            openDialog(url, \"Form\", \"编辑" + ClassName + "\", 770, 395, function (iframe) {\r\n");
                sb_List.Append("                top.frames[iframe].AcceptClick();\r\n");
                sb_List.Append("            });\r\n");
                sb_List.Append("        }\r\n");
                sb_List.Append("    }\r\n");
            }
            if (method["AllowDelete"].ToString() == "checked")
            {
                sb_List.Append("    //删除\r\n");
                sb_List.Append("    function btn_delete() {\r\n");
                sb_List.Append("        var KeyValue = GetJqGridRowValue(\"#gridTable\", \"" + PrimaryKeyColumns + "\");\r\n");
                sb_List.Append("        if (IsDelData(KeyValue)) {\r\n");
                sb_List.Append("            var delparm = 'KeyValue=' + KeyValue;\r\n");
                sb_List.Append("            delConfig('/" + AreasName + "/" + StringHelper.DelLastLength(ControllerName, 10) + "/Delete', delparm, KeyValue.split(\",\").length);\r\n");
                sb_List.Append("        }\r\n");
                sb_List.Append("    }\r\n");
            }
            if (method["AlloLookup"].ToString() == "checked")
            {
                sb_List.Append("    //明细\r\n");
                sb_List.Append("    function btn_detail() {\r\n");
                sb_List.Append("        var KeyValue = GetJqGridRowValue(\"#gridTable\", \"" + PrimaryKeyColumns + "\");\r\n");
                sb_List.Append("        if (IsChecked(KeyValue)) {\r\n");
                sb_List.Append("            var url = \"/" + AreasName + "/" + StringHelper.DelLastLength(ControllerName, 10) + "/Detail?KeyValue=\" + KeyValue;\r\n");
                sb_List.Append("            Dialog(url, \"Detail\", \"" + ClassName + "明细\", 820, 500, function (iframe) {\r\n");
                sb_List.Append("                top.frames[iframe].AcceptClick();\r\n");
                sb_List.Append("            });\r\n");
                sb_List.Append("        }\r\n");
                sb_List.Append("    }\r\n");
            }
            sb_List.Append("    //刷新\r\n");
            sb_List.Append("    function windowload() {\r\n");
            sb_List.Append("        $(\"#gridTable\").trigger(\"reloadGrid\"); //重新载入\r\n");
            sb_List.Append("        \r\n");
            sb_List.Append("    }\r\n");
            sb_List.Append("</script>\r\n");
            if (PageLayout == "1")//显示列表
            {
                #region 显示列表
                sb_List.Append("<div class=\"leftline rightline QueryArea\" style=\"margin: 1px; margin-top: 0px; margin-bottom: 0px;\">\r\n");
                sb_List.Append("    <table border=\"0\" class=\"form-find\" style=\"height: 45px;\">\r\n");
                sb_List.Append("        <tr>\r\n");
                sb_List.Append("            <th>关键字：</th>\r\n");
                sb_List.Append("            <td>\r\n");
                sb_List.Append("                <input id=\"keywords\" type=\"text\" class=\"txt\" style=\"width: 200px\" />\r\n");
                sb_List.Append("            </td>\r\n");
                sb_List.Append("            <td>\r\n");
                sb_List.Append("                <input id=\"btnSearch\" type=\"button\" class=\"btnSearch\" value=\"搜 索\" onclick=\"btn_Search()\" />\r\n");
                sb_List.Append("            </td>\r\n");
                sb_List.Append("        </tr>\r\n");
                sb_List.Append("    </table>\r\n");
                sb_List.Append("</div>\r\n");
                sb_List.Append("<div class=\"topline rightline\" style=\"margin: 1px; margin-top: -1px;\">\r\n");
                sb_List.Append("    <table id=\"gridTable\"></table>\r\n");
                if (AllowPageing == "1")//分页
                {
                    sb_List.Append("    <div id=\"gridPager\"></div>\r\n");
                }
                sb_List.Append("</div>\r\n");
                #endregion
            }
            else if (PageLayout == "2")//显示列表+工具栏按钮
            {
                #region 显示列表+工具栏按钮
                sb_List.Append("<!--工具栏-->\r\n");
                sb_List.Append("<div class=\"tools_bar leftline rightline\" style=\"margin: 1px; margin-bottom: 0px;\">\r\n");
                sb_List.Append("    <div class=\"PartialButton\">\r\n");
                sb_List.Append("        @Html.Partial(\"_PartialButton\")\r\n");
                sb_List.Append("    </div>\r\n");
                sb_List.Append("</div>\r\n");
                sb_List.Append("<div class=\"leftline rightline QueryArea\" style=\"margin: 1px; margin-top: 0px; margin-bottom: 0px;\">\r\n");
                sb_List.Append("    <table border=\"0\" class=\"form-find\" style=\"height: 45px;\">\r\n");
                sb_List.Append("        <tr>\r\n");
                sb_List.Append("            <th>关键字：</th>\r\n");
                sb_List.Append("            <td>\r\n");
                sb_List.Append("                <input id=\"keywords\" type=\"text\" class=\"txt\" style=\"width: 200px\" />\r\n");
                sb_List.Append("            </td>\r\n");
                sb_List.Append("            <td>\r\n");
                sb_List.Append("                <input id=\"btnSearch\" type=\"button\" class=\"btnSearch\" value=\"搜 索\" onclick=\"btn_Search()\" />\r\n");
                sb_List.Append("            </td>\r\n");
                sb_List.Append("        </tr>\r\n");
                sb_List.Append("    </table>\r\n");
                sb_List.Append("</div>\r\n");
                sb_List.Append("<div class=\"topline rightline\" style=\"margin: 1px; margin-top: -1px;\">\r\n");
                sb_List.Append("    <table id=\"gridTable\"></table>\r\n");
                if (AllowPageing == "1")//分页
                {
                    sb_List.Append("    <div id=\"gridPager\"></div>\r\n");
                }
                sb_List.Append("</div>\r\n");
                #endregion
            }
            else if (PageLayout == "3")//左边目录+显示列表
            {
                #region 左边目录+显示列表
                sb_List.Append("<div id=\"layout\" class=\"layout\">\r\n");
                sb_List.Append("    <!--左边-->\r\n");
                sb_List.Append("    <div class=\"layoutPanel layout-west\">\r\n");
                sb_List.Append("        <div class=\"btnbartitle\">\r\n");
                sb_List.Append("            <div>\r\n");
                sb_List.Append("                组织结构\r\n");
                sb_List.Append("            </div>\r\n");
                sb_List.Append("        </div>\r\n");
                sb_List.Append("        <div class=\"ScrollBar\" id=\"ItemsTree\"></div>\r\n");
                sb_List.Append("    </div>\r\n");
                sb_List.Append("    <!--中间-->\r\n");
                sb_List.Append("    <div class=\"layoutPanel layout-center\">\r\n");
                sb_List.Append("        <div class=\"btnbartitle\">\r\n");
                sb_List.Append("            <div>\r\n");
                sb_List.Append("                用户列表 <span id=\"CenterTitle\"></span>\r\n");
                sb_List.Append("            </div>\r\n");
                sb_List.Append("        </div>\r\n");
                sb_List.Append("        <!--列表-->\r\n");
                sb_List.Append("        <div id=\"grid_List\">\r\n");
                sb_List.Append("            <div class=\"bottomline QueryArea\" style=\"margin: 1px; margin-top: 0px; margin-bottom: 0px;\">\r\n");
                sb_List.Append("                <table border=\"0\" class=\"form-find\" style=\"height: 45px;\">\r\n");
                sb_List.Append("                    <tr>\r\n");
                sb_List.Append("                        <th>关键字：\r\n");
                sb_List.Append("                        </th>\r\n");
                sb_List.Append("                        <td>\r\n");
                sb_List.Append("                            <input id=\"keywords\" type=\"text\" class=\"txt\" style=\"width: 200px\" />\r\n");
                sb_List.Append("                        </td>\r\n");
                sb_List.Append("                        <td>\r\n");
                sb_List.Append("                            <input id=\"btnSearch\" type=\"button\" class=\"btnSearch\" value=\"搜 索\" onclick=\"btn_Search()\" />\r\n");
                sb_List.Append("                        </td>\r\n");
                sb_List.Append("                    </tr>\r\n");
                sb_List.Append("                </table>\r\n");
                sb_List.Append("            </div>\r\n");
                sb_List.Append("            <table id=\"gridTable\"></table>\r\n");
                if (AllowPageing == "1")//分页
                {
                    sb_List.Append("            <div id=\"gridPager\"></div>\r\n");
                }
                sb_List.Append("        </div>\r\n");
                sb_List.Append("    </div>\r\n");
                sb_List.Append("</div>\r\n");
                #endregion
            }
            else if (PageLayout == "4")//左边目录+显示列表+工具栏按钮
            {
                #region 左边目录+显示列表+工具栏按钮
                sb_List.Append("<div id=\"layout\" class=\"layout\">\r\n");
                sb_List.Append("    <!--左边-->\r\n");
                sb_List.Append("    <div class=\"layoutPanel layout-west\">\r\n");
                sb_List.Append("        <div class=\"btnbartitle\">\r\n");
                sb_List.Append("            <div>\r\n");
                sb_List.Append("                组织结构\r\n");
                sb_List.Append("            </div>\r\n");
                sb_List.Append("        </div>\r\n");
                sb_List.Append("        <div class=\"ScrollBar\" id=\"ItemsTree\"></div>\r\n");
                sb_List.Append("    </div>\r\n");
                sb_List.Append("    <!--中间-->\r\n");
                sb_List.Append("    <div class=\"layoutPanel layout-center\">\r\n");
                sb_List.Append("        <div class=\"btnbartitle\">\r\n");
                sb_List.Append("            <div>\r\n");
                sb_List.Append("                用户列表 <span id=\"CenterTitle\"></span>\r\n");
                sb_List.Append("            </div>\r\n");
                sb_List.Append("        </div>\r\n");
                sb_List.Append("        <!--工具栏-->\r\n");
                sb_List.Append("        <div class=\"tools_bar\" style=\"border-top: none; margin-bottom: 0px;\">\r\n");
                sb_List.Append("            <div class=\"PartialButton\">\r\n");
                sb_List.Append("                @Html.Partial(\"_PartialButton\")\r\n");
                sb_List.Append("            </div>\r\n");
                sb_List.Append("        </div>\r\n");
                sb_List.Append("        <!--列表-->\r\n");
                sb_List.Append("        <div id=\"grid_List\">\r\n");
                sb_List.Append("            <div class=\"bottomline QueryArea\" style=\"margin: 1px; margin-top: 0px; margin-bottom: 0px;\">\r\n");
                sb_List.Append("                <table border=\"0\" class=\"form-find\" style=\"height: 45px;\">\r\n");
                sb_List.Append("                    <tr>\r\n");
                sb_List.Append("                        <th>关键字：\r\n");
                sb_List.Append("                        </th>\r\n");
                sb_List.Append("                        <td>\r\n");
                sb_List.Append("                            <input id=\"keywords\" type=\"text\" class=\"txt\" style=\"width: 200px\" />\r\n");
                sb_List.Append("                        </td>\r\n");
                sb_List.Append("                        <td>\r\n");
                sb_List.Append("                            <input id=\"btnSearch\" type=\"button\" class=\"btnSearch\" value=\"搜 索\" onclick=\"btn_Search()\" />\r\n");
                sb_List.Append("                        </td>\r\n");
                sb_List.Append("                    </tr>\r\n");
                sb_List.Append("                </table>\r\n");
                sb_List.Append("            </div>\r\n");
                sb_List.Append("            <table id=\"gridTable\"></table>\r\n");
                if (AllowPageing == "1")//分页
                {
                    sb_List.Append("            <div id=\"gridPager\"></div>\r\n");
                }
                sb_List.Append("        </div>\r\n");
                sb_List.Append("    </div>\r\n");
                sb_List.Append("</div>\r\n");
                #endregion
            }
            WriteCodeBuilder(table + "\\" + StringHelper.DelLastLength(ControllerName, 10) + "\\" + PageListName + ".cshtml", sb_List.ToString());
            return sb_List.ToString();
        }
        #endregion

        #region 控制器
        /// <summary>
        /// 生成控制器
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns></returns>
        public string GetCodeBuilderController(string table)
        {
            StringBuilder sb_Controller = new StringBuilder();
            sb_Controller.Append("using LeaRun.Business;\r\n");
            sb_Controller.Append("using LeaRun.Entity;\r\n");
            sb_Controller.Append("using LeaRun.Utilities;\r\n");
            sb_Controller.Append("using System;\r\n");
            sb_Controller.Append("using System.Collections;\r\n");
            sb_Controller.Append("using System.Collections.Generic;\r\n");
            sb_Controller.Append("using System.Data;\r\n");
            sb_Controller.Append("using System.Linq;\r\n");
            sb_Controller.Append("using System.Web;\r\n");
            sb_Controller.Append("using System.Web.Mvc;\r\n\r\n");

            sb_Controller.Append("namespace LeaRun.WebApp.Areas." + AreasName + ".Controllers\r\n");
            sb_Controller.Append("{\r\n");

            sb_Controller.Append("    /// <summary>\r\n");
            sb_Controller.Append("    /// " + ClassName + "控制器\r\n");
            sb_Controller.Append("    /// </summary>\r\n");
            sb_Controller.Append("    public class " + ControllerName + " : PublicController<" + EntityName + ">\r\n");
            sb_Controller.Append("    {\r\n");
            sb_Controller.Append("    }\r\n");
            sb_Controller.Append("}");
            WriteCodeBuilder(table + "\\" + ControllerName + ".cs", sb_Controller.ToString());

            string strFilePath = "~/Areas/CodeMaticModule/DataModel/CodeMatic/" + table;
            string strZipPath = "~/Areas/CodeMaticModule/DataModel/CodeMatic/" + table + ".zip";
            GZipHelper.ZipFile(strFilePath, strZipPath);
            DirFileHelper.DeleteDirectory("~/Areas/CodeMaticModule/DataModel/CodeMatic/" + table);
            return sb_Controller.ToString();
        }
        #endregion

        /// <summary>
        /// 将生成代码写入文件
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="code">代码</param>
        public void WriteCodeBuilder(string filePath, string code)
        {
            string filepath = "/Areas/CodeMaticModule/DataModel/CodeMatic/" + filePath;
            DirFileHelper.CreateFile(filepath, code);
        }
        #endregion
    }
}
