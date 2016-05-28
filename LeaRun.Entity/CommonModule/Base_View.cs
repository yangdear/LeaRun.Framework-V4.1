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
    /// 视图设置表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.04 10:09</date>
    /// </author>
    /// </summary>
    [Description("视图设置表")]
    [PrimaryKey("ViewId")]
    public class Base_View : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 视图主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("视图主键")]
        public string ViewId { get; set; }
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
        /// 显示列宽
        /// </summary>
        /// <returns></returns>
        [DisplayName("显示列宽")]
        public int? ColumnWidth { get; set; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        /// <returns></returns>
        [DisplayName("对齐方式")]
        public string TextAlign { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否显示")]
        public int? AllowShow { get; set; }
        /// <summary>
        /// 导出/打印
        /// </summary>
        /// <returns></returns>
        [DisplayName("导出/打印")]
        public int? AllowDerive { get; set; }
        /// <summary>
        /// 自定义转换
        /// </summary>
        /// <returns></returns>
        [DisplayName("自定义转换")]
        public string CustomSwitch { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ViewId = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
                                }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.ViewId = KeyValue;
                                            }
        #endregion
    }
}