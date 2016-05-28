using LeaRun.Entity.EntityModel;
using LeaRun.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.ExampleModule.Controllers
{
    /// <summary>
    /// PDF阅读器控制器
    /// </summary>
    public class PDFReaderController : Controller
    {
        /// <summary>
        /// PDF阅读器
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// PDF目录列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Resource/PDF"));
            FileInfo[] files = dir.GetFiles("*.pdf", SearchOption.AllDirectories);
            FileDateSorter.QuickSort(files, 0, files.Length - 1);//按时间排序
            foreach (FileInfo fsi in files)
            {
                sb.Append("{");
                sb.Append("\"id\":\"" + fsi.Name + "\",");
                sb.Append("\"text\":\"" + fsi.Name + "\",");
                sb.Append("\"value\":\"" + fsi.Name + "\",");
                sb.Append("\"img\":\"/Content/Images/Icon16/file_extension_pdf.png\",");
                sb.Append("\"isexpand\":true,");
                sb.Append("\"hasChildren\":false");
                sb.Append("},");
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return Content(sb.ToString());
        }
        /// <summary>
        /// PDF文件查看
        /// </summary>
        /// <returns></returns>
        public ActionResult PDFViewer()
        {
            return View();
        }
        /// <summary>
        /// 上传PDF文件
        /// </summary>
        /// <returns></returns>
        public ActionResult Uploadify()
        {
            return View();
        }
        /// <summary>
        /// 提交上传
        /// </summary>
        /// <param name="Filedata">附件对象</param>
        /// <returns></returns>
        public ActionResult SubmitUploadify(HttpPostedFileBase Filedata)
        {
            try
            {
                string IsOk = "";
                FileProperty fileproperty = new FileProperty();
                //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    return HttpNotFound();
                }
                string fileGuid = CommonHelper.GetGuid;
                string filename = Filedata.FileName;//上载的文件的名称
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string swfToolPath = this.Server.MapPath("~/Content/Scripts/FlexPaper/pdf2swf.exe");//转换工具文件地址
                string uploadPath = this.Server.MapPath("~/Resource/PDF/");//上传文件地址
                string uploadPDFName = filename;
                string uploadWSFName = filename;
                string uploadPDFPath = uploadPath + uploadPDFName.Replace(".pdf", ".pdf");
                string uploadWSFPath = uploadPath + uploadWSFName.Replace(".pdf", ".swf");
                //创建文件夹，保存文件
                Directory.CreateDirectory(uploadPath);
                try
                {
                    if (FileEextension == ".pdf")
                    {
                        Filedata.SaveAs(uploadPDFPath);
                        fileproperty.Id = fileGuid;
                        fileproperty.Name = Filedata.FileName;
                        fileproperty.Eextension = "pdf";
                        fileproperty.CreateDate = DateTime.Now;
                        fileproperty.Path = uploadPDFPath;
                        fileproperty.Size = SizeHelper.CountSize(filesize);
                        /*-t: 源文件路径，即待转换的pdf文件路径。
                        * -s: 设置参数,这里我们设置为 flashversion=9 ，即可以转换为9 的版本
                        * -o: 输出文件的路径
                        * */
                        //文件路径包含到""内防止要转换的过程中，文件夹名字带有空格，导致失败
                        string cmdStr = "  -t  \"" + uploadPDFPath + "\" -s flashversion=9 -o \"" + uploadWSFPath + "\"";
                        bool iss = PdfToSwf(swfToolPath, cmdStr);//执行文件转换
                        if (iss)
                        {
                            IsOk = "1";
                        }
                        else
                        {
                            IsOk = "执行文件转换错误。";
                        }
                    }
                    else
                    {
                        throw new Exception("文件格式必须是pdf");
                    }
                }
                catch (Exception ex)
                {
                    IsOk = ex.Message;
                }
                var JsonData = new
                {
                    Status = IsOk,
                    FileInfo = fileproperty,
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>  //调用新进程,进行转换PDF到SWF文件转换 </summary>
        public static bool PdfToSwf(string toolsPath, string cmdStr)
        {
            bool iss = false;//判断是否转换成功，默认失败
            try
            {
                using (Process p = new Process())
                {
                    ProcessStartInfo psi = new ProcessStartInfo(toolsPath, cmdStr);
                    p.StartInfo = psi;
                    p.Start();
                    p.WaitForExit();
                    iss = true;//转换成功
                }
            }
            catch { }
            return iss;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="KeyValue">文件名</param>
        /// <returns></returns>
        public ActionResult DeleteFile(string KeyValue)
        {
            try
            {
                string FilePath = this.Server.MapPath("~/Resource/PDF/" + KeyValue);
                if (System.IO.File.Exists(FilePath))
                    System.IO.File.Delete(FilePath);
                return Content(new JsonMessage { Success = true, Code = "1", Message = "删除成功" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
    }
}
