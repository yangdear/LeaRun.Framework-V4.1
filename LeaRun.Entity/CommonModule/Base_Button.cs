//=====================================================================================
// All Rights Reserved , Copyright @ Learun 2014
// Software Developers @ Learun 2014
//=====================================================================================

using LeaRun.DataAccess.Attributes;
using LeaRun.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeaRun.Entity
{
    /// <summary>
    /// 系统按钮
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.07.09 12:41</date>
    /// </author>
    /// </summary>
    [DisplayName("按钮设置")]
    [PrimaryKey("ButtonId")]
    public class Base_Button : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 模块按钮主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("按钮设置主键")]
        public string ButtonId { get; set; }
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
        /// 分类
        /// </summary>
        /// <returns></returns>
        [DisplayName("分类")]
        public string Category { get; set; }
        /// <summary>
        /// js事件方法
        /// </summary>
        /// <returns></returns>
        [DisplayName("js事件方法")]
        public string JsEvent { get; set; }
        /// <summary>
        /// Action事件地址
        /// </summary>
        /// <returns></returns>
        [DisplayName("Action事件地址")]
        public string ActionEvent { get; set; }
        /// <summary>
        /// 分开线
        /// </summary>
        /// <returns></returns>
        [DisplayName("分开线")]
        public int? Split { get; set; }
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
            this.ButtonId = CommonHelper.GetGuid;
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
            this.ButtonId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}