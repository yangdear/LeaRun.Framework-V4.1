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
    /// Excel导入关系表
    /// <author>
    ///		<name>Liu</name>
    ///		<date>2014.08.25 22:41</date>
    /// </author>
    /// </summary>
    [Description("Excel导入关系表")]
    [PrimaryKey("ImportId")]
    public class Base_ExcelImport : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 导入主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("导入主键")]
        public string ImportId { get; set; }
        /// <summary>
        /// 模板编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("模板编号")]
        public string Code { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("导入名称")]
        public string ImportName { get; set; }
        /// <summary>
        /// 要导入的表
        /// </summary>
        /// <returns></returns>
        [DisplayName("要导入的表")]
        public string ImportTable { get; set; }
        /// <summary>
        /// 表备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("表备注")]
        public string ImportTableName { get; set; }
        /// <summary>
        /// 导入Excel的文件名
        /// </summary>
        /// <returns></returns>
        [DisplayName("导入Excel的文件名")]
        public string ImportFileName { get; set; }
        /// <summary>
        /// 遇到错误的处理机制：0-停止，1-跳过
        /// </summary>
        /// <returns></returns>
        [DisplayName("遇到错误的处理机制：0-停止，1-跳过")]
        public string ErrorHanding { get; set; }
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
        /// <summary>
        /// 对应模块
        /// </summary>
        /// <returns></returns>
        [DisplayName("对应模块")]
        public string ModuleId { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ImportId = CommonHelper.GetGuid;
            this.Enabled = 1;
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
            this.ImportId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;                               
        }
        #endregion
    }
}