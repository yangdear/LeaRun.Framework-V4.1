using LeaRun.Business;
using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.ExampleModule.Controllers
{
    /// <summary>
    /// 发送短信
    /// </summary>
    public class PhoneNoteController : PublicController<Base_PhoneNote>
    {
        Base_PhoneNoteBll base_phonenotebll = new Base_PhoneNoteBll();
        /// <summary>
        /// 【手机短信】返回列表JSON
        /// </summary>
        /// <param name="PhonenNumber">手机号码</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public ActionResult GridPageListJson(string PhonenNumber, string StartTime, string EndTime, JqGridParam jqgridparam)
        {
            try
            {
                Stopwatch watch = CommonHelper.TimerStart();
                string UserId = ManageProvider.Provider.Current().UserId;
                List<Base_PhoneNote> ListData = base_phonenotebll.GetPageList(UserId, PhonenNumber, StartTime, EndTime, jqgridparam);
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
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="PhonenNumber">手机号码</param>
        /// <param name="SendContent">发送内容</param>
        /// <returns></returns>
        public ActionResult SubmitSendNote(string PhonenNumber, string SendContent)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                string[] array = PhonenNumber.Split(',');
                int index = 1;
                foreach (string mobilephone in array)
                {
                    Base_PhoneNote entity = new Base_PhoneNote();
                    entity.Create();
                    entity.SendTime = DateTime.Now;
                    entity.PhonenNumber = mobilephone;
                    entity.SendContent = SendContent;
                    entity.SendStatus = SendBestMail(mobilephone, SendContent).ToString();
                    entity.SortCode = index;
                    database.Insert(entity, isOpenTrans);
                    index++;
                }
                database.Commit();
                return Content(new JsonMessage { Success = true, Code = "1", Message = "发送成功。" }.ToString());
            }
            catch (Exception ex)
            {
                database.Rollback();
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 发送短信猫设备
        /// </summary>
        /// <param name="PhonenNumber">手机号码</param>
        /// <param name="SendContent">发送内容</param>
        private int SendBestMail(string PhonenNumber, string SendContent)
        {
            return 1;
        }
    }
}
