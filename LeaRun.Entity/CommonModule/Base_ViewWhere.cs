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
    /// 视图查询条件表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.04 10:10</date>
    /// </author>
    /// </summary>
    [Description("视图查询条件表")]
    [PrimaryKey("ViewWhereId")]
    public class Base_ViewWhere : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 视图查询条件主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("视图查询条件主键")]
        public string ViewWhereId { get; set; }
        /// <summary>
        /// 模块主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("模块主键")]
        public string ModuleId { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("控件类型")]
        public string ControlType { get; set; }
        /// <summary>
        /// 控件默认值
        /// </summary>
        /// <returns></returns>
        [DisplayName("控件默认值")]
        public string ControlDefault { get; set; }
        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <returns></returns>
        [DisplayName("绑定数据源")]
        public string ControlSource { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("字段名称")]
        public string FieldName { get; set; }
        /// <summary>
        /// 内部名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("内部名称")]
        public string FullName { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("显示名称")]
        public string ShowName { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否显示")]
        public int? AllowShow { get; set; }
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
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 自定义
        /// </summary>
        /// <returns></returns>
        [DisplayName("自定义")]
        public string ControlCustom { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ViewWhereId = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.ViewWhereId = KeyValue;
        }
        #endregion
    }
}