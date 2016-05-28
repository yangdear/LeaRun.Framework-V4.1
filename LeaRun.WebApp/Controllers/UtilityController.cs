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
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Controllers
{
    /// <summary>
    /// 公共控制器
    /// </summary>
    public class UtilityController : Controller
    {
        #region 工具栏/右击栏 按钮
        /// <summary>
        /// 加载工具栏/右击栏 按钮
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadButton()
        {
            Base_ButtonPermissionBll base_buttonpermissionbll = new Base_ButtonPermissionBll();
            string ObjectId = ManageProvider.Provider.Current().ObjectId;
            string ModuleId = DESEncrypt.Decrypt(CookieHelper.GetCookie("ModuleId"));
            List<Base_Button> list = base_buttonpermissionbll.GetButtonList(ObjectId, ModuleId).FindAll(t => t.Enabled == 1);
            return Content(list.ToJson().Replace("&nbsp;", ""));
        }
        #endregion

        #region 表格视图列名
        /// <summary>
        /// 表格视图列名
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadViewColumn()
        {
            Base_ViewPermissionBll base_viewpermissionbll = new Base_ViewPermissionBll();
            string ObjectId = ManageProvider.Provider.Current().ObjectId;
            string ModuleId = DESEncrypt.Decrypt(CookieHelper.GetCookie("ModuleId"));
            List<Base_View> list = base_viewpermissionbll.GetViewList(ObjectId, ModuleId).FindAll(t => t.Enabled == 1);
            return Content(list.ToJson());
        }
        /// <summary>
        /// 自定义格式化
        /// </summary>
        /// <param name="CustomSwitch"></param>
        /// <returns></returns>
        public string ToFormatter(string CustomSwitch)
        {
            StringBuilder str = new StringBuilder();
            str.Append("function (cellvalue, options, rowObject) {");
            Hashtable ht = HashtableHelper.JsonToHashtable(CustomSwitch);
            if (ht != null && ht.Count > 0)
            {
                foreach (string key in ht.Keys)
                {
                    str.Append("if (cellvalue == '" + key + "')");
                    str.Append("    return \"" + ht[key] + "\";");
                }
            }
            else
            {
                str.Append("return \"<img src='../../Content/Images/\" + cellvalue + \"'/>\";");
            }
            str.Append("}");
            return str.ToString();
        }
        #endregion

        #region 公共查询
        public Base_QueryRecordBll base_queryrecordbll = new Base_QueryRecordBll();
        /// <summary>
        /// 【查询页面】
        /// </summary>
        /// <returns></returns>
        public ActionResult QueryPage()
        {
            return View();
        }
        /// <summary>
        /// 【查询页面】方案列表
        /// </summary>
        /// <param name="ModuleId">模块ID</param>
        /// <returns></returns>
        public ActionResult QueryListProject(string ModuleId)
        {
            string CreateUserId = ManageProvider.Provider.Current().UserId;
            List<Base_QueryRecord> list = base_queryrecordbll.GetList(ModuleId, CreateUserId);
            string JsonData = list.ToJson().Replace("ParamName", "FieldName").Replace("Operation", "CompareValue").Replace("ParamValue", "FilterValue");
            return Content(JsonData);
        }
        /// <summary>
        /// 【查询页面】删除方案
        /// </summary>
        /// <param name="QueryRecordId"></param>
        /// <returns></returns>
        public ActionResult QueryDeleteProject(string QueryRecordId)
        {
            try
            {
                var Message = "删除失败。";
                int IsOk = DataFactory.Database().Delete<Base_QueryRecord>(QueryRecordId);
                if (IsOk > 0)
                {
                    Message = "删除成功。";
                }
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败，错误：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 【查询页面】保存方案
        /// </summary>
        /// <param name="QueryRecordId">主键</param>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        public ActionResult QuerySaveProject(string QueryRecordId, Base_QueryRecord entity)
        {
            try
            {
                int IsOk = 0;
                string Message = QueryRecordId == "" ? "新增成功。" : "编辑成功。";
                if (!string.IsNullOrEmpty(QueryRecordId))
                {
                    entity.Modify(QueryRecordId);
                    IsOk = DataFactory.Database().Update(entity);
                }
                else
                {
                    entity.Create();
                    IsOk = DataFactory.Database().Insert(entity);
                }
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败，错误：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 【查询页面】设置初始化默认方案
        /// </summary>
        /// <param name="ModuleId">模块ID</param>
        /// <param name="QueryRecordId">主键</param>
        /// <returns></returns>
        public ActionResult QueryDefaultProject(string ModuleId, string QueryRecordId)
        {
            try
            {
                int IsOk = base_queryrecordbll.DefaultProject(ModuleId, QueryRecordId);
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "操作成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败，错误：" + ex.Message }.ToString());
            }
        }
        #endregion

        #region 省、市、区 列表
        Base_ProvinceCityBll base_provincecitybll = new Base_ProvinceCityBll();
        /// <summary>
        /// 获取省、市、区 列表
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public ActionResult GetProvinceCityListJson(string ParentId)
        {
            List<Base_ProvinceCity> ListData = base_provincecitybll.GetList(ParentId);
            return Content(ListData.ToJson());
        }
        #endregion

        #region 验证对象值不能重复
        /// <summary>
        /// 验证对象值不能重复
        /// </summary>
        /// <param name="tablename">实体类</param>
        /// <param name="fieldname">属性字段</param>
        /// <param name="fieldvalue">属性字段值</param>
        /// <param name="keyfield">主键字段</param>
        /// <param name="keyvalue">主键字段值</param>
        /// <returns></returns>
        public ActionResult FieldExist(string tablename, string fieldname, string fieldvalue, string keyfield, string keyvalue)
        {
            bool IsOk = BaseFactory.BaseHelper().FieldExist(tablename, fieldname, fieldvalue, keyfield, keyvalue);
            return Content(IsOk.ToString().ToLower());
        }
        #endregion

        #region JqGrid导出Excel
        /// <summary>
        /// 获取要导出表头字段
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDeriveExcelColumn()
        {
            string JsonColumn = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonColumn_DeriveExcel"));
            return Content(JsonColumn);
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="ExportField">要导出字段</param>
        public void GetDeriveExcel(string ExportField)
        {
            string JsonColumn = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonColumn_DeriveExcel"));
            string JsonData = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonData_DeriveExcel"));
            string JsonFooter = GZipHelper.Uncompress(CookieHelper.GetCookie("JsonFooter_DeriveExcel"));
            string fileName = GZipHelper.Uncompress(CookieHelper.GetCookie("FileName_DeriveExcel"));
            DeriveExcel.JqGridToExcel(JsonColumn, JsonData, ExportField, fileName);


            //CookieHelper.DelCookie("JsonColumn_DeriveExcel");
            //CookieHelper.DelCookie("JsonData_DeriveExcel");
            //CookieHelper.DelCookie("JsonFooter_DeriveExcel");
            //CookieHelper.DelCookie("FileName_DeriveExcel");
        }
        /// <summary>
        /// 写入数据到Cookie
        /// </summary>
        /// <param name="JsonColumn">表头</param>
        /// <param name="JsonData">数据</param>
        /// <param name="JsonFooter">底部合计</param>
        [ValidateInput(false)]
        public void SetDeriveExcel(string JsonColumn, string JsonData, string JsonFooter, string FileName)
        {
            CookieHelper.WriteCookie("JsonColumn_DeriveExcel", GZipHelper.Compress(JsonColumn));
            CookieHelper.WriteCookie("JsonData_DeriveExcel", GZipHelper.Compress(JsonData));
            CookieHelper.WriteCookie("JsonFooter_DeriveExcel", GZipHelper.Compress(JsonFooter));
            CookieHelper.WriteCookie("FileName_DeriveExcel", GZipHelper.Compress(FileName));
        }
        #endregion

        #region 选择用户
        Base_UserBll base_userbll = new Base_UserBll();
        /// <summary>
        /// 选择用户
        /// </summary>
        /// <returns></returns>
        public ActionResult OptionUser()
        {
            return View();
        }
        /// <summary>
        ///用户列表JSON
        /// </summary>
        /// <param name="keyword">模糊查询</param>
        /// <returns></returns>
        public ActionResult OptionUserJson(string keyword)
        {
            DataTable ListData = base_userbll.OptionUserList(keyword);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (!DataHelper.IsExistRows(ListData))
            {
                foreach (DataRow item in ListData.Rows)
                {
                    sb.Append("{");
                    sb.Append("\"id\":\"" + item["userid"] + "\",");
                    sb.Append("\"text\":\"" + item["realname"] + "（" + item["account"] + "）\",");
                    sb.Append("\"account\":\"" + item["account"] + "\",");
                    sb.Append("\"code\":\"" + item["code"] + "\",");
                    sb.Append("\"realname\":\"" + item["realname"] + "\",");
                    string Genderimg = "user_female.png";
                    if (item["Gender"].ToString() == "男")
                    {
                        Genderimg = "user_green.png";
                    }
                    sb.Append("\"img\":\"/Content/Images/Icon16/" + Genderimg + "\",");
                    sb.Append("\"isexpand\":true,");
                    sb.Append("\"hasChildren\":false");
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            return Content(sb.ToString());
        }
        #endregion

        #region 生成打印
        /// <summary>
        /// 打印当前页
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintPage()
        {
            return View();
        }
        #endregion
    }
}
