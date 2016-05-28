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
    /// 订单主表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.27 12:04</date>
    /// </author>
    /// </summary>
    [Description("订单主表")]
    [PrimaryKey("POOrderId")]
    public class POOrder : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("订单主键")]
        public string POOrderId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("父级主键")]
        public string ParentId { get; set; }
        /// <summary>
        /// 订单单号
        /// </summary>
        /// <returns></returns>
        [DisplayName("订单单号")]
        public string BillNo { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("订单日期")]
        public DateTime? BillDate { get; set; }
        /// <summary>
        /// 订单方式
        /// </summary>
        /// <returns></returns>
        [DisplayName("订单方式")]
        public string Method { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        /// <returns></returns>
        [DisplayName("结算方式")]
        public string Clearing { get; set; }
        /// <summary>
        /// 结算日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("结算日期")]
        public DateTime? ClearingTime { get; set; }
        /// <summary>
        /// 币别
        /// </summary>
        /// <returns></returns>
        [DisplayName("币别")]
        public string Currency { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        /// <returns></returns>
        [DisplayName("汇率")]
        public string ExchangeRate { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        /// <returns></returns>
        [DisplayName("供应商")]
        public string SupplierId { get; set; }
        /// <summary>
        /// 交货地点
        /// </summary>
        /// <returns></returns>
        [DisplayName("交货地点")]
        public string FetchAdd { get; set; }
        /// <summary>
        /// 采购员主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("采购员主键")]
        public string SalesmanId { get; set; }
        /// <summary>
        /// 采购员
        /// </summary>
        /// <returns></returns>
        [DisplayName("采购员")]
        public string Salesman { get; set; }
        /// <summary>
        /// 录入类型：0-PC手工录入,1-外部引入,2-PDA录入,3-手机端录入,4-其它录入
        /// </summary>
        /// <returns></returns>
        [DisplayName("录入类型：0-PC手工录入,1-外部引入,2-PDA录入,3-手机端录入,4-其它录入")]
        public int? POOrderType { get; set; }
        /// <summary>
        /// 关闭：0-正常，1-作废
        /// </summary>
        /// <returns></returns>
        [DisplayName("关闭：0-正常，1-作废")]
        public int? Cancellation { get; set; }
        /// <summary>
        /// 制单部门主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("制单部门主键")]
        public string CreateDepartmentId { get; set; }
        /// <summary>
        /// 制单部门
        /// </summary>
        /// <returns></returns>
        [DisplayName("制单部门")]
        public string CreateDepartmentName { get; set; }
        /// <summary>
        /// 制单人主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("制单人主键")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        /// <returns></returns>
        [DisplayName("制单人")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 制单时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("制单时间")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 提交订单；0-未提交,1-已提交
        /// </summary>
        /// <returns></returns>
        [DisplayName("提交订单；0-未提交,1-已提交")]
        public int? IsSubmit { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除标记")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 变更部门主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("变更部门主键")]
        public string ModifyDepartmentId { get; set; }
        /// <summary>
        /// 变更部门
        /// </summary>
        /// <returns></returns>
        [DisplayName("变更部门")]
        public string ModifyDepartmentName { get; set; }
        /// <summary>
        /// 变更人主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("变更人主键")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 变更人
        /// </summary>
        /// <returns></returns>
        [DisplayName("变更人")]
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 变更时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("变更时间")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 审核状态码
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核状态码")]
        public string AuditStatus { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核状态")]
        public string AuditStatusName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.POOrderId = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = ManageProvider.Provider.Current().UserId;
            this.CreateUserName = ManageProvider.Provider.Current().UserName;
            this.CreateDepartmentId = ManageProvider.Provider.Current().DepartmentId;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.POOrderId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
            this.ModifyDepartmentId = ManageProvider.Provider.Current().DepartmentId;
        }
        #endregion
    }
}