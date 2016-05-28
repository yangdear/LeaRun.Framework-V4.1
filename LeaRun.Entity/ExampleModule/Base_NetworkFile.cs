//=====================================================================================
// All Rights Reserved , Copyright @ Learun 2014
// Software Developers @ Learun 2014
//=====================================================================================

using LeaRun.DataAccess.Attributes;
using LeaRun.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeaRun.Entity
{
    /// <summary>
    /// 网络硬盘文件表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.16 10:54</date>
    /// </author>
    /// </summary>
    [Description("网络硬盘文件表")]
    [PrimaryKey("NetworkFileId")]
    public class Base_NetworkFile : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 网络硬盘文件主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("网络硬盘文件主键")]
        public string NetworkFileId { get; set; }
        /// <summary>
        /// 文件夹主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件夹主键")]
        public string FolderId { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件名称")]
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件路径")]
        public string FilePath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件大小")]
        public string FileSize { get; set; }
        /// <summary>
        /// 文件后缀名
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件后缀名")]
        public string FileExtensions { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件类型")]
        public string FileType { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        [DisplayName("图标")]
        public string Icon { get; set; }
        /// <summary>
        /// 文件共享
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件共享")]
        public int? Sharing { get; set; }
        /// <summary>
        /// 共享公共文件夹主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("共享公共文件夹主键")]
        public string SharingFolderId { get; set; }
        /// <summary>
        /// 共享开始时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("共享开始时间")]
        public DateTime? SharingCreateDate { get; set; }
        /// <summary>
        /// 共享结束时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("共享结束时间")]
        public DateTime? SharingEndDate { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        /// <returns></returns>
        [DisplayName("有效")]
        public int? Enabled { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [DisplayName("排序码")]
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除标记")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户主键")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户")]
        public string CreateUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.NetworkFileId = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = ManageProvider.Provider.Current().UserId;
            this.CreateUserName = ManageProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.NetworkFileId = KeyValue;
                                            }
        #endregion
    }
}