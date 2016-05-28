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
    /// 接口参数
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.11.05 10:37</date>
    /// </author>
    /// </summary>
    [Description("接口参数")]
    [PrimaryKey("InterfaceParameterId")]
    public class Base_InterfaceManageParameter : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 接口参数主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("接口参数主键")]
        public string InterfaceParameterId { get; set; }
        /// <summary>
        /// 接口主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("接口主键")]
        public string InterfaceId { get; set; }
        /// <summary>
        /// 参数字段
        /// </summary>
        /// <returns></returns>
        [DisplayName("参数字段")]
        public string Field { get; set; }
        /// <summary>
        /// 参数说明
        /// </summary>
        /// <returns></returns>
        [DisplayName("参数说明")]
        public string FieldMemo { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("参数类型")]
        public string FieldType { get; set; }
        /// <summary>
        /// 参数长度
        /// </summary>
        /// <returns></returns>
        [DisplayName("参数长度")]
        public string FieldMaxLength { get; set; }
        /// <summary>
        /// 允许空
        /// </summary>
        /// <returns></returns>
        [DisplayName("允许空")]
        public int? AllowNull { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.InterfaceParameterId = CommonHelper.GetGuid;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.InterfaceParameterId = KeyValue;
        }
        #endregion
    }
}