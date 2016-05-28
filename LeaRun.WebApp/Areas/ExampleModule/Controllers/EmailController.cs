using LeaRun.Business;
using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Entity.EntityModel;
using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.ExampleModule.Controllers
{
    /// <summary>
    /// 电子邮件
    /// </summary>
    public class EmailController : PublicController<Base_Email>
    {
        Base_EmailBll base_emailbll = new Base_EmailBll();

        #region 邮件列表
        /// <summary>
        /// 统计邮件信息（未读数、草稿数、已发送数、已删除数）
        /// </summary>
        /// <returns></returns>
        public ActionResult CountEmailJson()
        {
            string UserId = ManageProvider.Provider.Current().UserId;
            DataTable DataList = base_emailbll.GetCountEmail(UserId);
            var JonsData = new
            {
                UnRead = DataList.Rows[0]["unread"].ToString(),
                Draft = DataList.Rows[0]["draft"].ToString(),
                Sended = DataList.Rows[0]["sended"].ToString(),
                Delete = DataList.Rows[0]["deleted"].ToString(),
            };
            return Content(JonsData.ToJson());
        }
        /// <summary>
        /// 邮件列表
        /// </summary>
        /// <param name="Category">分类：收件箱、草稿箱、已发送、已删除</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当大小</param>
        /// <returns></returns>
        public ActionResult GridPageList(int Category, int pageIndex, int pageSize)
        {
            StringBuilder table = new StringBuilder();
            int recordCount = 0;
            string UserId = ManageProvider.Provider.Current().UserId;
            DataTable dt = base_emailbll.EmailList(Category, UserId, pageIndex, pageSize, ref recordCount);
            if (!DataHelper.IsExistRows(dt))
            {
                foreach (DataRow item in dt.Rows)
                {
                    string EmailId = item["emailid"].ToString();
                    string Theme = item["theme"].ToString();
                    string ThemeColour = item["themecolour"].ToString();
                    string Addresser = item["addresser"].ToString();
                    string DataType = item["datatype"].ToString();
                    string DateArea = item["datearea"].ToString();
                    string EmailCnt = item["emailcnt"].ToString();
                    string SendDate = CommonHelper.GetDateTime(item["SendDate"]).ToString("HH:mm:ss");
                    int IsAccessory = CommonHelper.GetInt(item["isaccessory"]);
                    int IsRead = CommonHelper.GetInt(item["isread"]);
                    int UnRead = CommonHelper.GetInt(item["unread"]);
                    int Priority = CommonHelper.GetInt(item["priority"]);
                    int State = CommonHelper.GetInt(item["state"]);
                    if (DataType == "1")
                    {
                        string color = "";
                        if (DateArea == "今天")
                        {
                            color = "color: red;";
                        }
                        else if (DateArea == "昨天")
                        {
                            color = "color: blue;";
                        }
                        else
                        {
                            color = "color: green;";
                        }
                        table.Append("<tr class=\"group_tr\" style=\"border-bottom: 2px solid #999;\"><td colspan=\"7\"><div class=\"grouptitle\" style=\"" + color + ";\">" + DateArea + "</div>(共 " + EmailCnt + " 封，其中 未读邮件 " + UnRead + " 封)</td></tr>");
                    }
                    else
                    {
                        if (DateArea == "更早")
                        {
                            SendDate = CommonHelper.GetDateTime(item["SendDate"]).ToString("yyyy-MM-dd");
                        }
                        table.Append("<tr>");
                        table.Append("<td style=\"width: 50px;\"><input type=\"checkbox\" style=\"vertical-align: middle;\" value='" + EmailId + "' /></td>");
                        string ReadIcon = "email.png";
                        string TitleReadIcon = "未读";
                        if (IsRead > 0)
                        {
                            ReadIcon = "email_open.png";
                            TitleReadIcon = "已读";
                        }
                        if (State == 0)//判断是否草稿
                        {
                            ReadIcon = "email_error.png";
                            TitleReadIcon = "草稿";
                        }
                        table.Append("<td title=\"" + TitleReadIcon + "\" style=\"width: 20px;\"><img src=\"../../Content/Images/Icon16/" + ReadIcon + "\" /></td>");
                        table.Append("<td style=\"width: 180px;\">" + Addresser + "</td>");
                        if (State == 0)//判断是否草稿
                        {
                            table.Append("<td onclick=\"WriteEmail('" + EmailId + "')\" style=\"" + ThemeColour + ";\">" + Theme + "</td>");
                        }
                        else
                        {
                            table.Append("<td onclick=\"LookEmail('" + EmailId + "')\" style=\"" + ThemeColour + ";\">" + Theme + "</td>");
                        }
                        table.Append("<td style=\"width: 100px; text-align: center;\">" + SendDate + "</td>");
                        string logoIcon = "whiteflag.png";
                        string TitleIcon = "正常";
                        if (Priority == 1)
                        {
                            logoIcon = "redflag.png"; TitleIcon = "红旗邮件";
                        }
                        else if (Priority == 2)
                        {
                            logoIcon = "backlogflag.png"; TitleIcon = "待读邮件";
                        }
                        table.Append("<td title=\"" + TitleIcon + "\" style=\"width: 20px;text-align: center;\"><img src=\"../../Content/Images/" + logoIcon + "\" /></td>");
                        if (IsAccessory > 0)
                        {
                            table.Append("<td style=\"width: 50px; text-align: center;\"><img src=\"../../Content/Images/Icon16/attach.png\" /></td>");
                        }
                        else
                        {
                            table.Append("<td style=\"width: 50px; text-align: center;\"></td>");
                        }
                        table.Append("</tr>");
                    }
                }
            }
            var JsonData = new
            {
                total = (recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1),
                page = pageIndex,
                rows = table.ToString(),
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="Category">分类</param>
        /// <param name="DeleteMark">删除标识</param>
        /// <returns></returns>
        public ActionResult DeleteEmail(string KeyValue, string Category, int DeleteMark)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            string[] array = KeyValue.Split(',');
            try
            {
                string Id = "";
                foreach (string item in array)
                {
                    if (item.Length > 0)
                    {
                        Id = item;
                        string[] arrayId = item.Split('|');
                        if (arrayId.Length > 1)
                        {
                            Id = arrayId[0];
                            Category = arrayId[1];
                        }
                        if (Category == "1")//收件
                        {
                            Base_EmailAddressee emailaddressee = new Base_EmailAddressee();
                            emailaddressee.EmailAddresseeId = Id;
                            emailaddressee.DeleteMark = DeleteMark;
                            database.Update(emailaddressee, isOpenTrans);
                        }
                        else//发件
                        {
                            Base_Email base_email = new Base_Email();
                            base_email.EmailId = Id;
                            base_email.DeleteMark = DeleteMark;
                            database.Update(base_email, isOpenTrans);
                        }
                    }
                }
                database.Commit();
                return Content(new JsonMessage { Success = true, Code = "1", Message = "删除成功。" }.ToString());
            }
            catch (Exception ex)
            {
                database.Rollback();
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        #endregion

        #region 写信
        /// <summary>
        /// 写信视图
        /// </summary>
        /// <returns></returns>
        public ActionResult WriteEmail()
        {
            ViewBag.Addresser = ManageProvider.Provider.Current().UserName + "（" + ManageProvider.Provider.Current().Account + "）";
            return View();
        }
        /// <summary>
        /// 发送内部邮件
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="base_email">邮件信息</param>
        /// <param name="AddresseeList">收件人</param>
        /// <param name="AccessoryList">附件</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SendEmail(string KeyValue, Base_Email base_email, string AddresseeJson, string AccessoryJson)
        {
            try
            {
                List<Base_EmailAddressee> AddresseeList = AddresseeJson.JonsToList<Base_EmailAddressee>();
                List<Base_EmailAccessory> AccessoryList = AccessoryJson.JonsToList<Base_EmailAccessory>();
                base_email.ParentId = "0";
                base_email.Addresser = ManageProvider.Provider.Current().UserName + "（" + ManageProvider.Provider.Current().Account + "）";
                base_email.SendDate = DateTime.Now;
                if (AccessoryList.Count > 0)
                {
                    base_email.IsAccessory = 1;
                }
                int IsOk = base_emailbll.SendEmail(KeyValue, base_email, AddresseeList, AccessoryList);
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "发送成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "发送失败，错误：" + ex.Message }.ToString());
            }
        }
        #endregion

        #region 查看邮件
        /// <summary>
        /// 查看邮件
        /// </summary>
        /// <returns></returns>
        public ActionResult LookEmail()
        {
            return View();
        }
        /// <summary>
        /// 查看邮件
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult SetEmailControl(string KeyValue)
        {
            //设置邮件 已读
            base_emailbll.ReadEmail(KeyValue, ManageProvider.Provider.Current().UserId, 1);
            Base_Email entity = repositoryfactory.Repository().FindEntity(KeyValue);
            List<Base_EmailAddressee> EmailAddressee = base_emailbll.EmailAddresseeList(KeyValue);
            List<Base_EmailAccessory> EmailAccessory = base_emailbll.EmailAccessoryList(KeyValue);
            var JsonData = new
            {
                Email = entity,
                EmailAddressee = EmailAddressee,
                EmailAccessory = EmailAccessory,
            };
            return Content(JsonData.ToJson());
        }
        #endregion

        #region 文件上传、删除
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="Filedata"></param>
        /// <returns></returns>
        public ActionResult EmailUpload(HttpPostedFileBase Filedata)
        {
            try
            {
                FileProperty fileproperty = new FileProperty();
                //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    return HttpNotFound();
                }
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/Document/Email/{日期}/{guid}.{后缀名}
                //例如：/Resource/Document/Email/20130913/43CA215D947F8C1F1DDFCED383C4D706.jpg
                string fileGuid = CommonHelper.GetGuid;
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");

                string virtualPath = string.Format("~/Resource/Document/Email/{0}/{1}{2}", uploadDate, fileGuid, FileEextension);
                string fullFileName = this.Server.MapPath(virtualPath);
                //创建文件夹，保存文件
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    Filedata.SaveAs(fullFileName);
                    fileproperty.Id = fileGuid;
                    fileproperty.Name = Filedata.FileName;
                    fileproperty.Eextension = FileEextension;
                    fileproperty.CreateDate = DateTime.Now;
                    fileproperty.Path = virtualPath;
                    fileproperty.Size = SizeHelper.CountSize(filesize);
                }
                return Content(fileproperty.ToJson());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path">路径</param>
        /// <returns></returns>
        public ActionResult EmailDeleteFile(string Path)
        {
            try
            {
                string FilePath = this.Server.MapPath(Path);
                if (System.IO.File.Exists(FilePath))
                    System.IO.File.Delete(FilePath);
                return Content(new JsonMessage { Success = true, Code = "1", Message = "删除成功" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        #endregion

        #region 邮件分类
        /// <summary>
        /// 邮件分类视图
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailCategory()
        {
            return View();
        }
        /// <summary>
        /// 邮件分类列表返回JSON
        /// </summary>
        /// <returns></returns>
        public ActionResult EmailCategoryJson()
        {
            List<Base_EmailCategory> ListData = DataFactory.Database().FindList<Base_EmailCategory>("CreateUserId", ManageProvider.Provider.Current().UserId);
            return Content(ListData.ToJson());
        }
        /// <summary>
        /// 删除邮件分类
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public ActionResult DeleteEmailCategory(string KeyValue)
        {
            try
            {
                var Message = "删除失败。";
                Base_EmailCategory entity = new Base_EmailCategory();
                entity.EmailCategoryId = KeyValue;
                int IsOk = DataFactory.Database().Delete(entity);
                if (IsOk > 0)
                {
                    Message = "删除成功。";
                }
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 邮件分类表单赋值
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public virtual ActionResult SetFormEmailCategory(string KeyValue)
        {
            Base_EmailCategory entity = DataFactory.Database().FindEntity<Base_EmailCategory>(KeyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 提交邮件分类（新增、编辑）
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="FullName">文件夹名称</param>
        /// <returns></returns>
        public ActionResult SubmitFormEmailCategory(string KeyValue, string FullName)
        {
            try
            {
                Base_EmailCategory entity = new Base_EmailCategory();
                entity.FullName = FullName;
                int IsOk = 0;
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    entity.Modify(KeyValue);
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
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        #endregion
    }
}
