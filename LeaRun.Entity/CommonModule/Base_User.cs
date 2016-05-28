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
    /// 用户管理
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.11 15:45</date>
    /// </author>
    /// </summary>
    [Description("用户管理")]
    [PrimaryKey("UserId")]
    public class Base_User : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户主键")]
        public string UserId { get; set; }
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
        /// 内部用户
        /// </summary>
        /// <returns></returns>
        [DisplayName("内部用户")]
        public int? InnerUser { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户编码")]
        public string Code { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>
        /// <returns></returns>
        [DisplayName("登录账户")]
        public string Account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        /// <returns></returns>
        [DisplayName("登录密码")]
        public string Password { get; set; }
        /// <summary>
        /// 密码秘钥
        /// </summary>
        /// <returns></returns>
        [DisplayName("密码秘钥")]
        public string Secretkey { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("姓名")]
        public string RealName { get; set; }
        /// <summary>
        /// 姓名拼音
        /// </summary>
        /// <returns></returns>
        [DisplayName("姓名拼音")]
        public string Spell { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        [DisplayName("性别")]
        public string Gender { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("出生日期")]
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        /// <returns></returns>
        [DisplayName("手机")]
        public string Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        [DisplayName("电话")]
        public string Telephone { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        /// <returns></returns>
        [DisplayName("QQ号码")]
        public string OICQ { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        /// <returns></returns>
        [DisplayName("电子邮件")]
        public string Email { get; set; }
        /// <summary>
        /// 最后修改密码日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("最后修改密码日期")]
        public DateTime? ChangePasswordDate { get; set; }
        /// <summary>
        /// 单点登录标识
        /// </summary>
        /// <returns></returns>
        [DisplayName("单点登录标识")]
        public int? OpenId { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        /// <returns></returns>
        [DisplayName("登录次数")]
        public int? LogOnCount { get; set; }
        /// <summary>
        /// 第一次访问时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("第一次访问时间")]
        public DateTime? FirstVisit { get; set; }
        /// <summary>
        /// 上一次访问时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("上一次访问时间")]
        public DateTime? PreviousVisit { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("最后访问时间")]
        public DateTime? LastVisit { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核状态")]
        public string AuditStatus { get; set; }
        /// <summary>
        /// 审核员主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核员主键")]
        public string AuditUserId { get; set; }
        /// <summary>
        /// 审核员
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核员")]
        public string AuditUserName { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("审核时间")]
        public DateTime? AuditDateTime { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否在线")]
        public int? Online { get; set; }
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
            this.UserId = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = ManageProvider.Provider.Current().UserId;
            this.CreateUserName = ManageProvider.Provider.Current().UserName;
            this.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
            this.Password = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(this.Password, 32).ToLower(), this.Secretkey).ToLower(), 32).ToLower();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.UserId = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            this.ModifyUserName = ManageProvider.Provider.Current().UserName;
            this.Password = null;
        }
        #endregion
    }
}