using LeaRun.Business;
using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Entity.EntityModel;
using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.ExampleModule.Controllers
{
    /// <summary>
    /// 文档管理
    /// </summary>
    public class DocumentController : PublicController<Base_NetworkFolder>
    {
        Base_NetworkFileBll base_networkfilebll = new Base_NetworkFileBll();

        #region 文件夹管理
        /// <summary>
        /// 加载文件夹目录
        /// </summary>
        /// <param name="FolderId">文件夹Id</param>
        /// <returns></returns>
        public ActionResult TreeJson(string FolderId)
        {
            List<Base_NetworkFolder> list = new List<Base_NetworkFolder>();
            if (!string.IsNullOrEmpty(FolderId))
            {
                //查询所有子文件夹（递归）
                list = base_networkfilebll.GetChildrenNodeList(FolderId);
            }
            else
            {
                list = repositoryfactory.Repository().FindList();
            }
            List<TreeJsonEntity> TreeList = new List<TreeJsonEntity>();
            int ParentId_index = 0;
            foreach (Base_NetworkFolder item in list)
            {
                TreeJsonEntity tree = new TreeJsonEntity();
                bool hasChildren = false;
                List<Base_NetworkFolder> childnode = list.FindAll(t => t.ParentId == item.FolderId);
                if (childnode.Count > 0)
                {
                    hasChildren = true;
                }
                tree.id = item.FolderId;
                tree.text = item.FolderName;
                tree.value = item.FolderId;
                if (item.ParentId == "0")
                {
                    if (ParentId_index == 0)
                    {
                        tree.isexpand = true;
                    }
                    else
                    {
                        tree.isexpand = false;
                    }
                    ParentId_index++;
                }
                else
                {
                    tree.isexpand = false;
                }
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.Attribute = "IsPublic";
                tree.AttributeValue = item.IsPublic.ToString();
                tree.parentId = item.ParentId;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }
        /// <summary>
        /// 删除文件夹（包含文件夹下面所有文件）
        /// </summary>
        /// <param name="FolderId">文件夹主键</param>
        /// <returns></returns>
        public ActionResult DeleteFolder(string FolderId)
        {
            List<Base_NetworkFolder> FolderList = base_networkfilebll.GetChildrenNodeList(FolderId);
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                foreach (Base_NetworkFolder entity in FolderList)
                {
                    database.Delete<Base_NetworkFolder>(entity.FolderId, isOpenTrans);
                    database.Delete<Base_NetworkFile>("FolderId", entity.FolderId, isOpenTrans);
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

        #region 文件管理
        /// <summary>
        /// 【文档管理】返回文件（夹）列表JSON
        /// </summary>
        /// <param name="keywords">文件名搜索条件</param>
        /// <param name="FolderId">文件夹ID</param>
        /// <param name="IsPublic">是否公共 1-公共、0-我的</param>
        /// <returns></returns>
        public ActionResult GridListJson(string keywords, string FolderId, string IsPublic)
        {
            try
            {
                string UserId = ManageProvider.Provider.Current().UserId;
                if (IsPublic == null || IsPublic == "1")
                {
                    UserId = "";
                }
                DataTable ListData = base_networkfilebll.GetList(keywords, FolderId, UserId);
                var JsonData = new
                {
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
        /// 删除文件
        /// </summary>
        /// <param name="NetworkFileId">文件主键</param>
        /// <returns></returns>
        public ActionResult DeleteFile(string NetworkFileId)
        {
            try
            {
                Base_NetworkFile entity = base_networkfilebll.Repository().FindEntity(NetworkFileId);
                base_networkfilebll.Repository().Delete(NetworkFileId);
                string FilePath = this.Server.MapPath(entity.FilePath);
                if (System.IO.File.Exists(FilePath))
                    System.IO.File.Delete(FilePath);
                return Content(new JsonMessage { Success = true, Code = "1", Message = "删除成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 文件（夹）重命名
        /// </summary>
        /// <returns></returns>
        public ActionResult FileReName()
        {
            return View();
        }
        /// <summary>
        /// 文件（夹） 重命名 提交保存
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="FileName">重命名</param>
        /// <returns></returns>
        public ActionResult SubmitFileReName(string KeyValue, string FileName)
        {
            try
            {
                Base_NetworkFile entity = new Base_NetworkFile();
                entity.NetworkFileId = KeyValue;
                entity.FileName = FileName;
                int IsOk = base_networkfilebll.Repository().Update(entity);
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "编辑成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }

        }
        /// <summary>
        /// 移动文件（夹）
        /// </summary>
        /// <returns></returns>
        public ActionResult MoveLocation()
        {
            return View();
        }
        /// <summary>
        /// 移动文件（夹）提交保存
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="MoveFolderId">选择移动文件夹Id</param>
        /// <param name="sort">分类0-文件、1-文件夹</param>
        /// <returns></returns>
        public ActionResult SubmitMoveLocation(string KeyValue, string MoveFolderId, string sort)
        {
            try
            {
                int IsOk = 0;
                if (sort == "1")
                {
                    Base_NetworkFolder networkfolder = new Base_NetworkFolder();
                    networkfolder.FolderId = KeyValue;
                    networkfolder.ParentId = MoveFolderId;
                    IsOk = repositoryfactory.Repository().Update(networkfolder);
                }
                else
                {
                    Base_NetworkFile networkfile = new Base_NetworkFile();
                    networkfile.NetworkFileId = KeyValue;
                    networkfile.FolderId = MoveFolderId;
                    IsOk = base_networkfilebll.Repository().Update(networkfile);
                }
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = "移动成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败：" + ex.Message }.ToString());
            }

        }
        #endregion

        #region 文件上传、下载
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult Uploadify()
        {
            return View();
        }
        /// <summary>
        /// 提交上传
        /// </summary>
        /// <param name="FolderId">文件夹主键</param>
        /// <param name="Filedata">附件对象</param>
        /// <returns></returns>
        public ActionResult SubmitUploadify(string FolderId, HttpPostedFileBase Filedata)
        {
            try
            {
                Thread.Sleep(1000);////延迟500毫秒
                Base_NetworkFile entity = new Base_NetworkFile();
                string IsOk = "";
                //没有文件上传，直接返回
                if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
                {
                    return HttpNotFound();
                }
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/Document/NetworkDisk/{日期}/{guid}.{后缀名}
                //例如：/Resource/Document/Email/20130913/43CA215D947F8C1F1DDFCED383C4D706.jpg
                string fileGuid = CommonHelper.GetGuid;
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string UserId = ManageProvider.Provider.Current().UserId;

                string virtualPath = string.Format("~/Resource/Document/NetworkDisk/{0}/{1}/{2}{3}", UserId, uploadDate, fileGuid, FileEextension);
                string fullFileName = this.Server.MapPath(virtualPath);
                //创建文件夹，保存文件
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    Filedata.SaveAs(fullFileName);
                    try
                    {
                        //文件信息写入数据库
                        entity.Create();
                        entity.NetworkFileId = fileGuid;
                        entity.FolderId = FolderId;
                        entity.FileName = Filedata.FileName;
                        entity.FilePath = virtualPath;
                        entity.FileSize = filesize.ToString();
                        entity.FileExtensions = FileEextension;
                        string _FileType = "";
                        string _Icon = "";
                        this.DocumentType(FileEextension, ref _FileType, ref _Icon);
                        entity.Icon = _Icon;
                        entity.FileType = _FileType;
                        IsOk = DataFactory.Database().Insert<Base_NetworkFile>(entity).ToString();
                    }
                    catch (Exception ex)
                    {
                        IsOk = ex.Message;
                        //System.IO.File.Delete(virtualPath);
                    }
                }
                var JsonData = new
                {
                    Status = IsOk,
                    NetworkFile = entity,
                };
                return Content(JsonData.ToJson());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public void Download(string KeyValue)
        {
            Base_NetworkFile entity = base_networkfilebll.Repository().FindEntity(KeyValue);
            string filename = Server.UrlDecode(entity.FileName);//返回客户端文件名称
            string filepath = Server.UrlDecode(entity.FilePath);//文件虚拟路径
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
        /// <summary>
        /// 获取文件类型、文件图标
        /// </summary>
        /// <param name="Eextension">后缀名</param>
        /// <param name="FileType">要返回文件类型</param>
        /// <param name="Icon">要返回文件图标</param>
        public void DocumentType(string Eextension, ref string FileType, ref string Icon)
        {
            string _FileType = "";
            string _Icon = "";
            switch (Eextension)
            {
                case ".docx":
                    _FileType = "word文件";
                    _Icon = "doc";
                    break;
                case ".doc":
                    _FileType = "word文件";
                    _Icon = "doc";
                    break;
                case ".xlsx":
                    _FileType = "excel文件";
                    _Icon = "xls";
                    break;
                case ".xls":
                    _FileType = "excel文件";
                    _Icon = "xls";
                    break;
                case ".pptx":
                    _FileType = "ppt文件";
                    _Icon = "ppt";
                    break;
                case ".ppt":
                    _FileType = "ppt文件";
                    _Icon = "ppt";
                    break;
                case ".txt":
                    _FileType = "记事本文件";
                    _Icon = "txt";
                    break;
                case ".pdf":
                    _FileType = "pdf文件";
                    _Icon = "pdf";
                    break;
                case ".zip":
                    _FileType = "压缩文件";
                    _Icon = "zip";
                    break;
                case ".rar":
                    _FileType = "压缩文件";
                    _Icon = "rar";
                    break;
                case ".png":
                    _FileType = "png图片";
                    _Icon = "png";
                    break;
                case ".gif":
                    _FileType = "gif图片";
                    _Icon = "gif";
                    break;
                case ".jpg":
                    _FileType = "jpg图片";
                    _Icon = "jpeg";
                    break;
                case ".mp3":
                    _FileType = "mp3文件";
                    _Icon = "mp3";
                    break;
                case ".html":
                    _FileType = "html文件";
                    _Icon = "html";
                    break;
                case ".css":
                    _FileType = "css文件";
                    _Icon = "css";
                    break;
                case ".mpeg":
                    _FileType = "mpeg文件";
                    _Icon = "mpeg";
                    break;
                case ".pds":
                    _FileType = "pds文件";
                    _Icon = "pds";
                    break;
                case ".ttf":
                    _FileType = "ttf文件";
                    _Icon = "ttf";
                    break;
                case ".swf":
                    _FileType = "swf文件";
                    _Icon = "swf";
                    break;
                default:
                    _FileType = "其他文件";
                    _Icon = "new";
                    //return "else.png";
                    break;
            }
            FileType = _FileType;
            Icon = _Icon;
        }
        #endregion
    }
}
