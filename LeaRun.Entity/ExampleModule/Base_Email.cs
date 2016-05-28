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
    /// 邮件信息表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.26 16:55</date>
    /// </author>
    /// </summary>
    [Description("邮件信息表")]
    [PrimaryKey("EmailId")]
    public class Base_Email : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 邮件信息主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("邮件信息主键")]
        public string EmailId { get; set; }
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
        /// 主题
        /// </summary>
        /// <returns></returns>
        [DisplayName("主题")]
        public string Theme { get; set; }
        /// <summary>
        /// 色彩主题
        /// </summary>
        /// <returns></returns>
        [DisplayName("色彩主题")]
        public string ThemeColour { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        /// <returns></returns>
        [DisplayName("内容")]
        public string Content { get; set; }
        /// <summary>
        /// 发件人
        /// </summary>
        /// <returns></returns>
        [DisplayName("发件人")]
        public string Addresser { get; set; }
        /// <summary>
        /// 发送日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("发送日期")]
        public DateTime? SendDate { get; set; }
        /// <summary>
        /// 是否有附件
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否有附件")]
        public int? IsAccessory { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        /// <returns></returns>
        [DisplayName("优先级")]
        public int? Priority { get; set; }
        /// <summary>
        /// 需要回执
        /// </summary>
        /// <returns></returns>
        [DisplayName("需要回执")]
        public int? Receipt { get; set; }
        /// <summary>
        /// 是否定时发送
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否定时发送")]
        public int? IsDelayed { get; set; }
        /// <summary>
        /// 定时时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("定时时间")]
        public DateTime? DelayedTime { get; set; }
        /// <summary>
        /// 状态;1-已发送;0-草稿
        /// </summary>
        /// <returns></returns>
        [DisplayName("状态;1-已发送;0-草稿")]
        public int? State { get; set; }
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
            this.EmailId = CommonHelper.GetGuid;
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
            this.EmailId = KeyValue;
                                            }
        #endregion
    }
}