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
    /// Excel导入关系明细表
    /// <author>
    ///		<name>Liu</name>
    ///		<date>2014.08.25 22:41</date>
    /// </author>
    /// </summary>
    [Description("Excel导入关系明细表")]
    [PrimaryKey("ImportDetailId")]
    public class Base_ExcelImportDetail : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 导入关系明细主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("导入主键")]
        public string ImportDetailId { get; set; }
        /// <summary>
        /// 导入主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("导入主键")]
        public string ImportId { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        /// <returns></returns>
        [DisplayName("字段名")]
        public string FieldName { get; set; }
        /// <summary>
        /// Excel列名
        /// </summary>
        /// <returns></returns>
        [DisplayName("Excel列名")]
        public string ColumnName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("数据类型")]
        public string DataType { get; set; }
        /// <summary>
        /// 关联的外表
        /// </summary>
        /// <returns></returns>
        [DisplayName("关联的外表")]
        public string ForeignTable { get; set; }
        /// <summary>
        /// 返回的外键值
        /// </summary>
        /// <returns></returns>
        [DisplayName("返回的外键值")]
        public string BackField { get; set; }
        /// <summary>
        /// 对比字段
        /// </summary>
        /// <returns></returns>
        [DisplayName("对比字段")]
        public string CompareField { get; set; }
        /// <summary>
        /// 附加条件
        /// </summary>
        /// <returns></returns>
        [DisplayName("附加条件")]
        public string AttachCondition { get; set; }
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
        /// 字段备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("字段备注")]
        public string FieldRemark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ImportDetailId = CommonHelper.GetGuid;
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
            this.ImportDetailId = KeyValue;
                                            }
        #endregion
    }
}