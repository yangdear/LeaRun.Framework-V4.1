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
    /// 表单附加属性
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.11.06 17:03</date>
    /// </author>
    /// </summary>
    [Description("表单附加属性")]
    [PrimaryKey("FormAttributeId")]
    public class Base_FormAttribute : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 表单附加属性主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("表单附加属性主键")]
        public string FormAttributeId { get; set; }
        /// <summary>
        /// 模块主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("模块主键")]
        public string ModuleId { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("属性名称")]
        public string PropertyName { get; set; }
        /// <summary>
        /// 控件Id
        /// </summary>
        /// <returns></returns>
        [DisplayName("控件Id")]
        public string ControlId { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("控件类型")]
        public string ControlType { get; set; }
        /// <summary>
        /// 控件样式
        /// </summary>
        /// <returns></returns>
        [DisplayName("控件样式")]
        public string ControlStyle { get; set; }
        /// <summary>
        /// 控件验证
        /// </summary>
        /// <returns></returns>
        [DisplayName("控件验证")]
        public string ControlValidator { get; set; }
        /// <summary>
        /// 输入长度
        /// </summary>
        /// <returns></returns>
        [DisplayName("输入长度")]
        public int? ImportLength { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        /// <returns></returns>
        [DisplayName("默认值")]
        public string DefaultVlaue { get; set; }
        /// <summary>
        /// 自定义属性
        /// </summary>
        /// <returns></returns>
        [DisplayName("自定义属性")]
        public string AttributesProperty { get; set; }
        /// <summary>
        /// 控件数据源类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("控件数据源类型")]
        public int? DataSourceType { get; set; }
        /// <summary>
        /// 控件数据源
        /// </summary>
        /// <returns></returns>
        [DisplayName("控件数据源")]
        public string DataSource { get; set; }
        /// <summary>
        /// 合并列
        /// </summary>
        /// <returns></returns>
        [DisplayName("合并列")]
        public string ControlColspan { get; set; }
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
            this.FormAttributeId = CommonHelper.GetGuid;
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
            this.FormAttributeId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}