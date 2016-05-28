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
    /// 编码规则明细表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.03 16:53</date>
    /// </author>
    /// </summary>
    [Description("编码规则明细表")]
    [PrimaryKey("CodeRuleDetailId")]
    public class Base_CodeRuleDetail : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 编码规则明细主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("编码规则明细主键")]
        public string CodeRuleDetailId { get; set; }
        /// <summary>
        /// 编码规则主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("编码规则主键")]
        public string CodeRuleId { get; set; }
        /// <summary>
        /// 编码名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("编码名称")]
        public string FullName { get; set; }
        /// <summary>
        /// 是否常量
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否常量")]
        public string Consted { get; set; }
        /// <summary>
        /// 是否自动复位
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否自动复位")]
        public int? AutoReset { get; set; }
        /// <summary>
        /// 是否定长
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否定长")]
        public int? FixLength { get; set; }
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <returns></returns>
        [DisplayName("格式化字符串")]
        public string FormatStr { get; set; }
        /// <summary>
        /// 步长
        /// </summary>
        /// <returns></returns>
        [DisplayName("步长")]
        public int? StepValue { get; set; }
        /// <summary>
        /// 初始值
        /// </summary>
        /// <returns></returns>
        [DisplayName("初始值")]
        public int? InitValue { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        /// <returns></returns>
        [DisplayName("长度")]
        public int? FLength { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("类型")]
        public string FType { get; set; }
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
            this.CodeRuleDetailId = CommonHelper.GetGuid;
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
            this.CodeRuleDetailId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}