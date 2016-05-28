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
    /// 岗位管理
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.07 15:34</date>
    /// </author>
    /// </summary>
    [Description("岗位管理")]
    [PrimaryKey("PostId")]
    public class Base_Post : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 岗位主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("岗位主键")]
        public string PostId { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("公司主键")]
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("部门主键")]
        public string DepartmentId { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("角色主键")]
        public string RoleId { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        /// <returns></returns>
        [DisplayName("岗位编码")]
        public string Code { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("岗位名称")]
        public string FullName { get; set; }
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
            this.PostId = CommonHelper.GetGuid;
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
            this.PostId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}