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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// 调试日志
        /// </summary>
        public static LeaRun.Utilities.LogHelper log = LeaRun.Utilities.LogFactory.GetLogger("LoginController");

        Base_UserBll base_userbll = new Base_UserBll();
        Base_ObjectUserRelationBll base_objectuserrelationbll = new Base_ObjectUserRelationBll();
        /// <summary>
        /// 默认页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Default()
        {
            return View();
        }
        /// <summary>
        /// 登录视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //string str = GetPage("http://s.taobao.com/search?q=%CA%F3%B1%EA&commend=all&ssid=s5-e&search_type=item&sourceId=tb.index&spm=1.7274553.1997520841.1&initiative_id=tbindexz_20141229");
            return View();
        }
        public static string GetPage(string url)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "www.csddt.com";
                request.Timeout = 20000;
                request.AllowAutoRedirect = false;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024)
                {
                    reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GB2312"));
                    string html = reader.ReadToEnd();
                    return html;
                }
            }
            catch { }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
                if (reader != null)
                    reader.Close();
                if (request != null)
                    request = null;
            }
            return string.Empty;
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            int codeW = 80;
            int codeH = 22;
            int fontSize = 16;
            string chkCode = string.Empty;
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            //写入Session、验证码加密
            Session["session_verifycode"] = Md5Helper.MD5(chkCode.ToLower(), 16);
            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画噪线 
            for (int i = 0; i < 1; i++)
            {
                int x1 = rnd.Next(codeW);
                int y1 = rnd.Next(codeH);
                int x2 = rnd.Next(codeW);
                int y2 = rnd.Next(codeH);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, fontSize);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18 + 2, (float)0);
            }
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return File(ms.ToArray(), @"image/Gif");
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="Account">账户</param>
        /// <param name="Password">密码</param>
        /// <returns></returns>
        public ActionResult CheckLogin(string Account, string Password, string Token)
        {
            string Msg = "";
            try
            {
                IPScanerHelper objScan = new IPScanerHelper();
                string IPAddress = NetHelper.GetIPAddress();
                objScan.IP = IPAddress;
                objScan.DataPath = Server.MapPath("~/Resource/IPScaner/QQWry.Dat");
                string IPAddressName = objScan.IPLocation();
                string outmsg = "";
                VerifyIPAddress(Account, IPAddress, IPAddressName, Token);
                //系统管理
                if (Account == ConfigHelper.AppSettings("CurrentUserName"))
                {
                    if (ConfigHelper.AppSettings("CurrentPassword") == Password)
                    {
                        IManageUser imanageuser = new IManageUser();
                        imanageuser.UserId = "System";
                        imanageuser.Account = "System";
                        imanageuser.UserName = "超级管理员";
                        imanageuser.Gender = "男";
                        imanageuser.Code = "System";
                        imanageuser.LogTime = DateTime.Now;
                        imanageuser.CompanyId = "系统";
                        imanageuser.DepartmentId = "系统";
                        imanageuser.IPAddress = IPAddress;
                        imanageuser.IPAddressName = IPAddressName;
                        imanageuser.IsSystem = true;
                        ManageProvider.Provider.AddCurrent(imanageuser);
                        //对在线人数全局变量进行加1处理
                        HttpContext rq = System.Web.HttpContext.Current;
                        rq.Application["OnLineCount"] = (int)rq.Application["OnLineCount"] + 1;
                        Msg = "3";//验证成功
                        Base_SysLogBll.Instance.WriteLog(Account, OperationType.Login, "1", "登陆成功、IP所在城市：" + IPAddressName);
                    }
                    else
                    {
                        return Content("4");
                    }
                }
                else
                {
                    Base_User base_user = base_userbll.UserLogin(Account, Password, out outmsg);
                    switch (outmsg)
                    {
                        case "-1":      //账户不存在
                            Msg = "-1";
                            Base_SysLogBll.Instance.WriteLog(Account, OperationType.Login, "-1", "账户不存在、IP所在城市：" + IPAddressName);
                            break;
                        case "lock":    //账户锁定
                            Msg = "2";
                            Base_SysLogBll.Instance.WriteLog(Account, OperationType.Login, "-1", "账户锁定、IP所在城市：" + IPAddressName);
                            break;
                        case "error":   //密码错误
                            Msg = "4";
                            Base_SysLogBll.Instance.WriteLog(Account, OperationType.Login, "-1", "密码错误、IP所在城市：" + IPAddressName);
                            break;
                        case "succeed": //验证成功
                            IManageUser imanageuser = new IManageUser();
                            imanageuser.UserId = base_user.UserId;
                            imanageuser.Account = base_user.Account;
                            imanageuser.UserName = base_user.RealName;
                            imanageuser.Gender = base_user.Gender;
                            imanageuser.Password = base_user.Password;
                            imanageuser.Code = base_user.Code;
                            imanageuser.Secretkey = base_user.Secretkey;
                            imanageuser.LogTime = DateTime.Now;
                            imanageuser.CompanyId = base_user.CompanyId;
                            imanageuser.DepartmentId = base_user.DepartmentId;
                            imanageuser.ObjectId = base_objectuserrelationbll.GetObjectId(imanageuser.UserId);
                            imanageuser.IPAddress = IPAddress;
                            imanageuser.IPAddressName = IPAddressName;
                            imanageuser.IsSystem = false;
                            ManageProvider.Provider.AddCurrent(imanageuser);
                            //对在线人数全局变量进行加1处理
                            HttpContext rq = System.Web.HttpContext.Current;
                            rq.Application["OnLineCount"] = (int)rq.Application["OnLineCount"] + 1;
                            Msg = "3";//验证成功
                            Base_SysLogBll.Instance.WriteLog(Account, OperationType.Login, "1", "登陆成功、IP所在城市：" + IPAddressName);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return Content(Msg);
        }
        /// <summary>
        /// 验证强迫退出 下线
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckForceOutLogin()
        {
            return null;
        }
        /// <summary>
        /// 同账号不允许重复登录
        /// </summary>
        /// <param name="Account">账户</param>
        /// <returns></returns>
        public bool CheckOnLine(string Account)
        {
            if (!CommonHelper.GetBool(ConfigHelper.AppSettings("CheckOnLine")))
            {
                return true;
            }
            ArrayList list = Session["OnLineList"] as ArrayList;
            if (list == null)
            {
                list = new ArrayList();
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (Account == (list[i] as string))
                {
                    //已经登录了，提示错误信息 
                    return false;
                }
            }
            Session.Add("OnLineList", list.Add(Account));
            return true;
        }
        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        public ActionResult OutLogin()
        {
            string UserId = ManageProvider.Provider.Current().UserId;
            //更改数据库用户表在线状态
            Base_User entity = new Base_User();
            entity.UserId = UserId; entity.Online = 0;
            base_userbll.Repository().Update(entity);
            //清空当前登录用户信息
            ManageProvider.Provider.EmptyCurrent();
            Session.Abandon();  //取消当前会话
            Session.Clear();    //清除当前浏览器所以Session
            return Content("1");
        }
        /// <summary>
        /// IP限制验证
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="IPAddress"></param>
        /// <param name="IPAddressName"></param>
        /// <param name="OpenId"></param>
        public void VerifyIPAddress(string Account, string IPAddress, string IPAddressName, string OpenId)
        {
            if (ConfigHelper.AppSettings("VerifyIPAddress") == "true")
            {
                List<DbParameter> parameter = new List<DbParameter>();
                parameter.Add(DbFactory.CreateDbParameter("@IPAddress", IPAddress));
                parameter.Add(DbFactory.CreateDbParameter("@IPAddressName", IPAddressName));
                parameter.Add(DbFactory.CreateDbParameter("@OpenId", DESEncrypt.Decrypt(OpenId)));
                int IsOk = DataFactory.Database().ExecuteByProc("[Login].dbo.[PROC_verify_IPAddress]", parameter.ToArray());
            }
        }
    }
}
