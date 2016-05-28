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
    /// 手机短信表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.22 11:03</date>
    /// </author>
    /// </summary>
    [Description("手机短信表")]
    [PrimaryKey("PhoneNoteId")]
    public class Base_PhoneNote : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 手机短信主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("手机短信主键")]
        public string PhoneNoteId { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        /// <returns></returns>
        [DisplayName("手机号码")]
        public string PhonenNumber { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        /// <returns></returns>
        [DisplayName("发送内容")]
        public string SendContent { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("发送时间")]
        public DateTime? SendTime { get; set; }
        /// <summary>
        /// 发送状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("发送状态")]
        public string SendStatus { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备名称")]
        public string DeviceName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string Remark { get; set; }
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
            this.PhoneNoteId = CommonHelper.GetGuid;
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
            this.PhoneNoteId = KeyValue;
                                            }
        #endregion
    }
}