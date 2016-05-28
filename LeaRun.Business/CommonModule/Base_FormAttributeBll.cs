//=====================================================================================
// All Rights Reserved , Copyright @ Learun 2014
// Software Developers @ Learun 2014
//=====================================================================================

using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 表单附加属性
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.11.06 17:03</date>
    /// </author>
    /// </summary>
    public class Base_FormAttributeBll : RepositoryFactory<Base_FormAttribute>
    {
        private static Base_FormAttributeBll item;
        /// <summary>
        /// 静态化
        /// </summary>
        public static Base_FormAttributeBll Instance
        {
            get
            {
                if (item == null)
                {
                    item = new Base_FormAttributeBll();
                }
                return item;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="ModuleId">模块Id</param>
        /// <returns></returns>
        public List<Base_FormAttribute> GetList(string ModuleId)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append("SELECT * FROM Base_FormAttribute WHERE 1=1");
            strSql.Append(" AND ModuleId = @ModuleId AND Enabled=1");
            strSql.Append(" ORDER BY SortCode");
            parameter.Add(DbFactory.CreateDbParameter("@ModuleId", ModuleId));
            return Repository().FindListBySql(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 保存动态表单数据
        /// </summary>
        /// <param name="BuildFormJson">表单JSON对象</param>
        /// <param name="ObjectId">对象Id</param>
        /// <param name="ModuleId">模块Id</param>
        public void SaveBuildForm(string BuildFormJson, string ObjectId, string ModuleId, DbTransaction isOpenTrans)
        {
            try
            {
                Base_FormAttributeValue formattributevalue = new Base_FormAttributeValue();
                formattributevalue.Create();
                formattributevalue.ObjectId = ObjectId;
                formattributevalue.ModuleId = ModuleId;
                formattributevalue.ObjectParameterJson = BuildFormJson;
                DataFactory.Database().Delete<Base_FormAttributeValue>("ObjectId", ObjectId, isOpenTrans);
                DataFactory.Database().Insert<Base_FormAttributeValue>(formattributevalue, isOpenTrans);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("自定义表单，" + ex);
            }
        }
        /// <summary>
        /// 获取动态表单数据(返回JSON)
        /// </summary>
        /// <returns></returns>
        public string GetBuildForm(string ObjectId)
        {
            Base_FormAttributeValue formattributevalue = DataFactory.Database().FindEntity<Base_FormAttributeValue>("ObjectId", ObjectId);
            if (!string.IsNullOrEmpty(formattributevalue.ObjectParameterJson) && formattributevalue.ObjectParameterJson.Length > 2)
            {
                return formattributevalue.ObjectParameterJson.Replace("{", "").Replace("}", "") + ",";
            }
            else
            {
                return "";
            }
        }

        #region 拼接表单（返回html）
        /// <summary>
        /// 生成表单table
        /// </summary>
        /// <param name="ColumnCount">排版模式：1代表2列表，2代表4列，3代表 6列</param>
        /// <param name="ModuleId">模块Id</param>
        /// <returns></returns>
        public string CreateBuildFormTable(int ColumnCount, string ModuleId)
        {
            List<Base_FormAttribute> ListData = this.GetList(ModuleId); ;
            return CreateBuildFormTable(ColumnCount, ListData); ;
        }
        /// <summary>
        /// 生成表单table
        /// </summary>
        /// <param name="ColumnCount">排版模式：1代表2列表，2代表4列，3代表 6列</param>
        /// <param name="ListData">数据源</param>
        /// <returns></returns>
        public string CreateBuildFormTable(int ColumnCount, List<Base_FormAttribute> ListData)
        {
            StringBuilder FormTable = new StringBuilder();
            FormTable.Append("<table class=\"form\">\r\n        <tr>\r\n");
            int RowIndex = 1;
            foreach (Base_FormAttribute entity in ListData)
            {
                if (entity.PropertyName == null && entity.ControlId == null)
                {
                    continue;
                }
                string PropertyName = entity.PropertyName;                                            //属性名称
                int ControlColspan = CommonHelper.GetInt(entity.ControlColspan);                      //合并列
                FormTable.Append("            <th class='formTitle'>" + PropertyName + "：</th>\r\n");
                if (ControlColspan == 0)
                {
                    FormTable.Append("            <td class='formValue'>\r\n                " + CreateControl(entity) + "\r\n            </td>\r\n");
                }
                else
                {
                    FormTable.Append("            <td colspan='" + ControlColspan + "' class='formValue'>\r\n                " + CreateControl(entity) + "\r\n            </td>\r\n");
                    FormTable.Append("        </tr>\r\n        <tr>\r\n");
                    RowIndex += ControlColspan - 1;
                }
                if (RowIndex % ColumnCount == 0)
                {
                    FormTable.Append("        </tr>\r\n        <tr>\r\n");
                }
                RowIndex++;
            }
            FormTable.Append("        </tr>\r\n    </table>\r\n");
            return FormTable.ToString().Replace("<tr>\r\n</tr>", "");
        }
        /// <summary>
        /// 生成控件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CreateControl(Base_FormAttribute entity)
        {
            StringBuilder sbControl = new StringBuilder();
            string PropertyName = entity.PropertyName;                          //属性名称
            string ControlId = entity.ControlId;                                //控件Id
            string ControlType = entity.ControlType;                            //控件类型
            string ControlStyle = entity.ControlStyle;                          //控件样式
            string ControlValidator = "";
            if (entity.ControlValidator != null)
            {
                ControlValidator = entity.ControlValidator.Replace("&nbsp;", "");                  //控件验证
            }
            string ImportLength = entity.ImportLength.ToString().Replace("&nbsp;", "");        //输入长度
            string AttributesProperty = "";
            if (entity.AttributesProperty != null)
            {
                AttributesProperty = entity.AttributesProperty.Replace("&nbsp;", "");              //自定义属性
            }
            int DataSourceType = CommonHelper.GetInt(entity.DataSourceType);    //控件数据源类型
            string DataSource = entity.DataSource;                              //控件数据源
            if (!string.IsNullOrEmpty(ControlValidator.Trim()))
            {
                ControlValidator = "datacol=\"yes\" err=\"" + PropertyName + "\" checkexpession=\"" + ControlValidator + "\"";
            }
            string maxlength = "";
            if (!string.IsNullOrEmpty(ImportLength))
            {
                maxlength = "maxlength=" + ImportLength + "";
            }
            switch (ControlType)
            {
                case "1"://文本框
                    sbControl.Append("<input id=\"Build_" + ControlId + "\" " + maxlength + " type=\"text\" class=\"" + ControlStyle + "\" " + ControlValidator + " " + AttributesProperty + " />");
                    break;
                case "2"://下拉框
                    if (!string.IsNullOrEmpty(DataSource))
                    {
                        if (DataSourceType == 0)
                        {
                            sbControl.Append("<select id=\"Build_" + ControlId + "\"class=\"" + ControlStyle + "\" " + ControlValidator + " " + AttributesProperty + ">");
                            sbControl.Append(DataSource);
                            sbControl.Append("</select>");
                        }
                        else
                        {
                            sbControl.Append("<select dictionaryValue=\"" + DataSource + "\" dictionary=\"yes\" id=\"Build_" + ControlId + "\"class=\"" + ControlStyle + "\" " + ControlValidator + " " + AttributesProperty + ">");
                            sbControl.Append(CreateBindDrop(DataSource));
                            sbControl.Append("</select>");
                        }
                    }
                    else
                    {
                        sbControl.Append("<select id=\"Build_" + ControlId + "\"class=\"" + ControlStyle + "\" " + ControlValidator + " " + AttributesProperty + ">");
                        sbControl.Append("</select>");
                    }
                    break;
                case "3"://日期框
                    sbControl.Append("<input id=\"Build_" + ControlId + "\" " + maxlength + " type=\"text\" class=\"" + ControlStyle + "\" " + ControlValidator + " " + AttributesProperty + "/>");
                    break;
                case "4"://标  签
                    sbControl.Append("<label id=\"Build_" + ControlId + "\" " + AttributesProperty + "/>");
                    break;
                case "5"://多行文本框
                    sbControl.Append("<textarea id=\"Build_" + ControlId + "\" " + maxlength + " class=\"" + ControlStyle + "\" " + ControlValidator + " " + AttributesProperty + "></textarea>");
                    break;
                default:
                    return "内部错误，配置有错误";
            }
            return sbControl.ToString();
        }
        /// <summary>
        /// 绑定数据字典（下拉框）
        /// </summary>
        /// <param name="DataSource">数据字典数据源</param>
        /// <returns></returns>
        public string CreateBindDrop(string DataSource)
        {
            StringBuilder sb = new StringBuilder("<option value=''>==请选择==</option>");
            Base_DataDictionaryBll base_datadictionarybll = new Base_DataDictionaryBll();
            List<Base_DataDictionaryDetail> ListData = base_datadictionarybll.GetDataDictionaryDetailListByCode(DataSource);
            if (ListData != null)
            {
                foreach (Base_DataDictionaryDetail item in ListData)
                {
                    sb.Append("<option value=\"" + item.Code + "\">" + item.FullName + "</option>");
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}