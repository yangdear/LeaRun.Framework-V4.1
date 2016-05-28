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
    /// 邮箱收件人表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.26 16:56</date>
    /// </author>
    /// </summary>
    [Description("邮箱收件人表")]
    [PrimaryKey("EmailAddresseeId")]
    public class Base_EmailAddressee : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 邮箱收件人主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("邮箱收件人主键")]
        public string EmailAddresseeId { get; set; }
        /// <summary>
        /// 邮件信息主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("邮件信息主键")]
        public string EmailId { get; set; }
        /// <summary>
        /// 收件人主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("收件人主键")]
        public string AddresseeId { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        /// <returns></returns>
        [DisplayName("收件人")]
        public string AddresseeName { get; set; }
        /// <summary>
        /// 状态: 0-收件;1-抄送;2-密送
        /// </summary>
        /// <returns></returns>
        [DisplayName("状态: 0-收件;1-抄送;2-密送")]
        public int? AddresseeIdState { get; set; }
        /// <summary>
        /// 是否阅读
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否阅读")]
        public int? IsRead { get; set; }
        /// <summary>
        /// 阅读次数
        /// </summary>
        /// <returns></returns>
        [DisplayName("阅读次数")]
        public int? ReadCount { get; set; }
        /// <summary>
        /// 阅读日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("阅读日期")]
        public DateTime? ReadDate { get; set; }
        /// <summary>
        /// 最后阅读日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("最后阅读日期")]
        public DateTime? EndReadDate { get; set; }
        /// <summary>
        /// 设置红旗
        /// </summary>
        /// <returns></returns>
        [DisplayName("设置红旗")]
        public int? Highlight { get; set; }
        /// <summary>
        /// 设置待办
        /// </summary>
        /// <returns></returns>
        [DisplayName("设置待办")]
        public int? Backlog { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除标记")]
        public int? DeleteMark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.EmailAddresseeId = CommonHelper.GetGuid;
            this.IsRead = 0;
            this.DeleteMark = 0;
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.EmailAddresseeId = KeyValue;
        }
        #endregion
    }
}