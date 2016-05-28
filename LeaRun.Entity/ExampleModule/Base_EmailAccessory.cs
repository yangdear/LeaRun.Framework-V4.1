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
    /// 邮箱附件表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.26 16:56</date>
    /// </author>
    /// </summary>
    [Description("邮箱附件表")]
    [PrimaryKey("EmailAccessoryId")]
    public class Base_EmailAccessory : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 邮箱附件主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("邮箱附件主键")]
        public string EmailAccessoryId { get; set; }
        /// <summary>
        /// 邮件信息主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("邮件信息主键")]
        public string EmailId { get; set; }
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
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("文件类型")]
        public string FileType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CreateDate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.EmailAccessoryId = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.EmailAccessoryId = KeyValue;
        }
        #endregion
    }
}