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
    /// 订单分录
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.27 12:04</date>
    /// </author>
    /// </summary>
    [Description("订单分录")]
    [PrimaryKey("POOrderEntryId")]
    public class POOrderEntry : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 订单分录主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("订单分录主键")]
        public string POOrderEntryId { get; set; }
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("订单主键")]
        public string POOrderId { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        /// <returns></returns>
        [DisplayName("批号")]
        public string BatchNo { get; set; }
        /// <summary>
        /// 物料主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料主键")]
        public string ItemId { get; set; }
        /// <summary>
        /// 物料代码
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料代码")]
        public string ItemCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料名称")]
        public string ItemName { get; set; }
        /// <summary>
        /// 物料型号
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料型号")]
        public string ItemModel { get; set; }
        /// <summary>
        /// 单位主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("单位主键")]
        public string UnitId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        [DisplayName("数量")]
        public string Qty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>
        [DisplayName("单价")]
        public string Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        /// <returns></returns>
        [DisplayName("金额")]
        public string PriceAmount { get; set; }
        /// <summary>
        /// 含税单价
        /// </summary>
        /// <returns></returns>
        [DisplayName("含税单价")]
        public string PlusPrice { get; set; }
        /// <summary>
        /// 含税金额
        /// </summary>
        /// <returns></returns>
        [DisplayName("含税金额")]
        public string PlusPriceAmount { get; set; }
        /// <summary>
        /// 税率(%)
        /// </summary>
        /// <returns></returns>
        [DisplayName("税率(%)")]
        public string CESS { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        /// <returns></returns>
        [DisplayName("税额")]
        public string CESSAmount { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        /// <returns></returns>
        [DisplayName("说明")]
        public string Description { get; set; }
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
            this.POOrderEntryId = CommonHelper.GetGuid;
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
            this.POOrderEntryId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}