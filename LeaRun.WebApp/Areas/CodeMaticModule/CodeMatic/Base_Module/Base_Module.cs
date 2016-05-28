//=====================================================================================
// All Rights Reserved , Copyright ? Learun 2014
// Software Developers ? Learun 2014
//=====================================================================================

using LeaRun.Kernel;
using System;
using System.ComponentModel;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 模块设置表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.06.22 19:28</date>
    /// </author>
    /// </summary>
    [Description("模块设置表")]
    [Key("ModuleId")]
    [MaxField("SortCode")]
    public class Base_Module
    {
        /// <summary>
        /// 模块主键
        /// </summary>
        /// <returns></returns>
        [Description("模块主键")]
        [Display(Name = "模块主键")]
        public string ModuleId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        [Description("父级主键")]
        [Display(Name = "父级主键")]
        public string ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        [Description("编码")]
        [Display(Name = "编码")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        [Description("名称")]
        [Display(Name = "名称")]
        public string FullName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        [Description("图标")]
        [Display(Name = "图标")]
        public string Icon { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        /// <returns></returns>
        [Description("访问地址")]
        [Display(Name = "访问地址")]
        public string Location { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        /// <returns></returns>
        [Description("目标")]
        [Display(Name = "目标")]
        public string Target { get; set; }
        /// <summary>
        /// 级别层次
        /// </summary>
        /// <returns></returns>
        [Description("级别层次")]
        [Display(Name = "级别层次")]
        public int? Level { get; set; }
        /// <summary>
        /// 动态按钮
        /// </summary>
        /// <returns></returns>
        [Description("动态按钮")]
        [Display(Name = "动态按钮")]
        public int? AllowButton { get; set; }
        /// <summary>
        /// 动态视图
        /// </summary>
        /// <returns></returns>
        [Description("动态视图")]
        [Display(Name = "动态视图")]
        public int? AllowView { get; set; }
        /// <summary>
        /// 动态表单
        /// </summary>
        /// <returns></returns>
        [Description("动态表单")]
        [Display(Name = "动态表单")]
        public int? AllowForm { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Description("备注")]
        [Display(Name = "备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 有效
        /// </summary>
        /// <returns></returns>
        [Description("有效")]
        [Display(Name = "有效")]
        public int? Enabled { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Description("排序码")]
        [Display(Name = "排序码")]
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Description("删除标记")]
        [Display(Name = "删除标记")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Description("创建时间")]
        [Display(Name = "创建时间")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Description("创建用户主键")]
        [Display(Name = "创建用户主键")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Description("创建用户")]
        [Display(Name = "创建用户")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [Description("修改时间")]
        [Display(Name = "修改时间")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Description("修改用户主键")]
        [Display(Name = "修改用户主键")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Description("修改用户")]
        [Display(Name = "修改用户")]
        public string ModifyUserName { get; set; }
    }
}