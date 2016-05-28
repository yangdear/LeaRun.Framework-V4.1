using LeaRun.Business;
using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using Newtonsoft.Json;
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
    /// 用户管理控制器
    /// </summary>
    public class UserController : PublicController<Base_User>
    {
        Base_UserBll base_userbll = new Base_UserBll();
        Base_CompanyBll base_companybll = new Base_CompanyBll();
        Base_ObjectUserRelationBll base_objectuserrelationbll = new Base_ObjectUserRelationBll();

        #region 用户管理
        /// <summary>
        /// 查询前面50条用户信息（返回JSON）
        /// </summary>
        /// <param name="keywords">查询关键字</param>
        /// <returns></returns>
        public ActionResult Autocomplete(string keywords)
        {
            DataTable ListData = base_userbll.OptionUserList(keywords);
            return Content(ListData.ToJson());
        }
        /// <summary>
        /// 【用户管理】返回用户列表JSON
        /// </summary>
        /// <param name="keywords">查询关键字</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="DepartmentId">部门ID</param>
        /// <param name="jqgridparam">表格参数</param>
        /// <returns></returns>
        public ActionResult GridPageListJson(string keywords, string CompanyId, string DepartmentId, JqGridParam jqgridparam)
        {
            try
            {
                Stopwatch watch = CommonHelper.TimerStart();
                DataTable ListData = base_userbll.GetPageList(keywords, CompanyId, DepartmentId, ref jqgridparam);
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
        /// 【用户管理】提交表单
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="base_user">用户信息</param>
        /// <param name="base_employee">员工信息</param>
        /// <param name="BuildFormJson">自定义表单</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitUserForm(string KeyValue, Base_User base_user, Base_Employee base_employee, string BuildFormJson)
        {
            string ModuleId = DESEncrypt.Decrypt(CookieHelper.GetCookie("ModuleId"));
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    if (KeyValue == ManageProvider.Provider.Current().UserId)
                    {
                        throw new Exception("无权限编辑本人信息");
                    }
                    base_user.Modify(KeyValue);
                    base_employee.Modify(KeyValue);
                    database.Update(base_user, isOpenTrans);
                    database.Update(base_employee, isOpenTrans);
                }
                else
                {
                    base_user.Create();
                    base_user.SortCode = CommonHelper.GetInt(BaseFactory.BaseHelper().GetSortCode<Base_User>("SortCode"));
                    base_employee.Create();
                    base_employee.EmployeeId = base_user.UserId;
                    base_employee.UserId = base_user.UserId;
                    database.Insert(base_user, isOpenTrans);
                    database.Insert(base_employee, isOpenTrans);
                    Base_DataScopePermissionBll.Instance.AddScopeDefault(ModuleId, ManageProvider.Provider.Current().UserId, base_user.UserId, isOpenTrans);
                }
                Base_FormAttributeBll.Instance.SaveBuildForm(BuildFormJson, base_user.UserId, ModuleId, isOpenTrans);
                database.Commit();
                return Content(new JsonMessage { Success = true, Code = "1", Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                database.Rollback();
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 获取用户职员信息对象返回JSON
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SetUserForm(string KeyValue)
        {
            Base_User base_user = DataFactory.Database().FindEntity<Base_User>(KeyValue);
            if (base_user == null)
            {
                return Content("");
            }
            Base_Employee base_employee = DataFactory.Database().FindEntity<Base_Employee>(KeyValue);
            Base_Company base_company = DataFactory.Database().FindEntity<Base_Company>(base_user.CompanyId);
            string strJson = base_user.ToJson();
            //公司
            strJson = strJson.Insert(1, "\"CompanyName\":\"" + base_company.FullName + "\",");
            //员工信息
            strJson = strJson.Insert(1, base_employee.ToJson().Replace("{", "").Replace("}", "") + ",");
            //自定义
            strJson = strJson.Insert(1, Base_FormAttributeBll.Instance.GetBuildForm(KeyValue));
            return Content(strJson);
        }
        #endregion

        #region 修改登录密码
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ResetPassword()
        {
            ViewBag.Account = Request["Account"];
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="Password">新密码</param>
        /// <returns></returns>
        public ActionResult ResetPasswordSubmit(string KeyValue, string Password)
        {
            try
            {
                int IsOk = 0;
                Base_User base_user = new Base_User();
                base_user.UserId = KeyValue;
                base_user.ModifyDate = DateTime.Now;
                base_user.ModifyUserId = ManageProvider.Provider.Current().UserId;
                base_user.ModifyUserName = ManageProvider.Provider.Current().UserName;
                base_user.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
                base_user.Password = Md5Helper.MD5(DESEncrypt.Encrypt(Password, base_user.Secretkey).ToLower(), 32).ToLower();
                IsOk = repositoryfactory.Repository().Update(base_user);
                Base_SysLogBll.Instance.WriteLog(KeyValue, OperationType.Update, IsOk.ToString(), "修改密码");
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "密码修改成功。" }.ToString());
            }
            catch (Exception ex)
            {
                Base_SysLogBll.Instance.WriteLog(KeyValue, OperationType.Update, "-1", "密码修改失败：" + ex.Message);
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "密码修改失败：" + ex.Message }.ToString());
            }
        }
        #endregion

        #region 用户角色
        /// <summary>
        /// 用户角色
        /// </summary>
        /// <returns></returns>
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult UserRole()
        {
            return View();
        }
        /// <summary>
        /// 加载用户角色
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public ActionResult UserRoleList(string CompanyId, string UserId)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = base_userbll.UserRoleList(CompanyId, UserId);
            foreach (DataRow dr in dt.Rows)
            {
                string strchecked = "";
                if (!string.IsNullOrEmpty(dr["objectid"].ToString()))//判断是否选中
                {
                    strchecked = "selected";
                }
                sb.Append("<li title=\"" + dr["fullname"] + "(" + dr["code"] + ")" + "\" class=\"" + strchecked + "\">");
                sb.Append("<a id=\"" + dr["RoleId"] + "\"><img src=\"../../Content/Images/Icon16/role.png \">" + dr["fullname"] + "</a><i></i>");
                sb.Append("</li>");
            }
            return Content(sb.ToString());
        }
        /// <summary>
        /// 用户角色 - 提交保存
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="ObjectId">角色id:1,2,3,4,5,6</param>
        /// <returns></returns>
        public ActionResult UserRoleSubmit(string UserId, string ObjectId)
        {
            try
            {
                string[] array = ObjectId.Split(',');
                int IsOk = base_objectuserrelationbll.BatchAddObject(UserId, array, "2");
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "操作成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败，错误：" + ex.Message }.ToString());
            }
        }
        #endregion

        #region 个人中心
        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonCenter()
        {
            if (ManageProvider.Provider.Current().Gender == "男")
            {
                ViewBag.imgGender = "man.png";
            }
            else
            {
                ViewBag.imgGender = "woman.png";
            }
            ViewBag.strUserInfo = ManageProvider.Provider.Current().UserName + "（" + ManageProvider.Provider.Current().Account + "）";
            return View();
        }
        /// <summary>
        /// 验证旧密码
        /// </summary>
        /// <param name="OldPassword"></param>
        /// <returns></returns>
        public ActionResult ValidationOldPassword(string OldPassword)
        {
            if (ManageProvider.Provider.Current().Account == "System" || ManageProvider.Provider.Current().Account == "guest")
            {
                return Content(new JsonMessage { Success = true, Code = "0", Message = "当前账户不能修改密码" }.ToString());
            }
            OldPassword = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(OldPassword, 32).ToLower(), ManageProvider.Provider.Current().Secretkey).ToLower(), 32).ToLower();
            if (OldPassword != ManageProvider.Provider.Current().Password)
            {
                return Content(new JsonMessage { Success = true, Code = "0", Message = "原密码错误，请重新输入" }.ToString());
            }
            else
            {
                return Content(new JsonMessage { Success = true, Code = "1", Message = "通过信息验证" }.ToString());
            }
        }
        #endregion
    }
}