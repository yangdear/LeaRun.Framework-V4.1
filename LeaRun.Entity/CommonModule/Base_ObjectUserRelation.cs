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
    /// 对象用户关系表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.18 14:01</date>
    /// </author>
    /// </summary>
    [Description("对象用户关系表")]
    [PrimaryKey("ObjectUserRelationId")]
    public class Base_ObjectUserRelation : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 对象用户关系主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("对象用户关系主键")]
        public string ObjectUserRelationId { get; set; }
        /// <summary>
        /// 对象分类:1-部门2-角色3-岗位4-群组
        /// </summary>
        /// <returns></returns>
        [DisplayName("对象分类:1-部门2-角色3-岗位4-群组")]
        public string Category { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("对象主键")]
        public string ObjectId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户主键")]
        public string UserId { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ObjectUserRelationId = CommonHelper.GetGuid;
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
            this.ObjectUserRelationId = KeyValue;
                                            }
        #endregion
    }
}