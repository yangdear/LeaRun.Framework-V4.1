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
    /// 网络硬盘文件夹表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.16 10:53</date>
    /// </author>
    /// </summary>
    [Description("网络硬盘文件夹表")]
    [PrimaryKey("FolderId")]
    public class Base_NetworkFolder : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 文件夹主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件夹主键")]
        public string FolderId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("父级主键")]
        public string ParentId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        [DisplayName("分类")]
        public string Category { get; set; }
        /// <summary>
        /// 文件夹
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件夹")]
        public string FolderName { get; set; }
        /// <summary>
        /// 是公开
        /// </summary>
        /// <returns></returns>
        [DisplayName("是公开")]
        public int? IsPublic { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        /// <returns></returns>
        [DisplayName("有效")]
        public int? Enabled { get; set; }
        /// <summary>
        /// 文件夹共享
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
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改时间")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户主键")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户")]
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.FolderId = CommonHelper.GetGuid;
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
            this.FolderId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}