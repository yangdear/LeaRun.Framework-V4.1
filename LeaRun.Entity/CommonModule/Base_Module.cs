//=====================================================================================
// All Rights Reserved , Copyright © Learun 2014
// Software Developers © Learun 2014
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
    /// 模块设置
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.07.09 12:11</date>
    /// </author>
    /// </summary>
    [Description("模块设置")]
    [PrimaryKey("ModuleId")]
    public class Base_Module : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 模块主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("模块主键")]
        public string ModuleId { get; set; }
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
        /// 编码
        /// </summary>
        /// <returns></returns>
        [DisplayName("编码")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("名称")]
        public string FullName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        [DisplayName("图标")]
        public string Icon { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        /// <returns></returns>
        [DisplayName("访问地址")]
        public string Location { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        /// <returns></returns>
        [DisplayName("目标")]
        public string Target { get; set; }
        /// <summary>
        /// 级别层次
        /// </summary>
        /// <returns></returns>
        [DisplayName("级别层次")]
        public int? Level { get; set; }
        /// <summary>
        /// 展开
        /// </summary>
        /// <returns></returns>
        [DisplayName("展开")]
        public int? Isexpand { get; set; }
        /// <summary>
        /// 动态按钮
        /// </summary>
        /// <returns></returns>
        [DisplayName("动态按钮")]
        public int? AllowButton { get; set; }
        /// <summary>
        /// 动态视图
        /// </summary>
        /// <returns></returns>
        [DisplayName("动态视图")]
        public int? AllowView { get; set; }
        /// <summary>
        /// 动态表单
        /// </summary>
        /// <returns></returns>
        [DisplayName("动态表单")]
        public int? AllowForm { get; set; }
        /// <summary>
        /// 访问权限
        /// </summary>
        /// <returns></returns>
        [DisplayName("访问权限")]
        public int? Authority { get; set; }
        /// <summary>
        /// 数据范围
        /// </summary>
        /// <returns></returns>
        [DisplayName("数据范围")]
        public int? DataScope { get; set; }
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
            this.ModuleId = CommonHelper.GetGuid;
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
            this.ModuleId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}