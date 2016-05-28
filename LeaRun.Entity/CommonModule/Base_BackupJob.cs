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
    /// 数据库备份计划表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.23 17:30</date>
    /// </author>
    /// </summary>
    [Description("数据库备份计划表")]
    [PrimaryKey("BackupId")]
    public class Base_BackupJob : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 备份主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("备份主键")]
        public string BackupId { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        /// <returns></returns>
        [DisplayName("服务器地址")]
        public string ServerName { get; set; }
        /// <summary>
        /// 数据库
        /// </summary>
        /// <returns></returns>
        [DisplayName("数据库")]
        public string DbName { get; set; }
        /// <summary>
        /// 计划名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划名称")]
        public string JobName { get; set; }
        /// <summary>
        /// 执行方式
        /// </summary>
        /// <returns></returns>
        [DisplayName("执行方式")]
        public string Mode { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("执行时间")]
        public string StartTime { get; set; }
        /// <summary>
        /// 备份路径
        /// </summary>
        /// <returns></returns>
        [DisplayName("备份路径")]
        public string FilePath { get; set; }
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
        public string Enabled { get; set; }
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
            this.BackupId = CommonHelper.GetGuid;
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
            this.BackupId = KeyValue;
        }
        #endregion
    }
}