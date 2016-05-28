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
    /// 编码规则种子表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.12 13:49</date>
    /// </author>
    /// </summary>
    [Description("编码规则种子表")]
    [PrimaryKey("CodeSeriousId")]
    public class Base_CodeRuleSerious : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 种子主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("种子主键")]
        public string CodeSeriousId { get; set; }
        /// <summary>
        /// 编码规则主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("编码规则主键")]
        public string CodeRuleId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户主键")]
        public string UserId { get; set; }
        /// <summary>
        /// 种子值
        /// </summary>
        /// <returns></returns>
        [DisplayName("种子值")]
        public int? NowValue { get; set; }
        /// <summary>
        /// 种子类型（0-最大种子，1-用户占用种子）
        /// </summary>
        /// <returns></returns>
        [DisplayName("种子类型（0-最大种子，1-用户占用种子）")]
        public string ValueType { get; set; }
        /// <summary>
        /// 上次更新时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("上次更新时间")]
        public string LastUpdateDate { get; set; }
        /// <summary>
        /// 有效(1-未使用，0-已使用)
        /// </summary>
        /// <returns></returns>
        [DisplayName("有效(1-未使用，0-已使用)")]
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
            this.CodeSeriousId = Guid.NewGuid().ToString();
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
            this.CodeRuleId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}