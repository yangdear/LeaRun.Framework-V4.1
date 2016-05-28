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
    /// 职员信息
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.08.11 15:45</date>
    /// </author>
    /// </summary>
    [Description("职员信息")]
    [PrimaryKey("EmployeeId")]
    public class Base_Employee : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 职员主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("职员主键")]
        public string EmployeeId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户主键")]
        public string UserId { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        /// <returns></returns>
        [DisplayName("照片")]
        public string Photograph { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        /// <returns></returns>
        [DisplayName("身份证号码")]
        public string IDCard { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        /// <returns></returns>
        [DisplayName("年龄")]
        public int? Age { get; set; }
        /// <summary>
        /// 工资卡
        /// </summary>
        /// <returns></returns>
        [DisplayName("工资卡")]
        public string BankCode { get; set; }
        /// <summary>
        /// 办公短号
        /// </summary>
        /// <returns></returns>
        [DisplayName("办公短号")]
        public string OfficeCornet { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        /// <returns></returns>
        [DisplayName("办公电话")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 办公邮编
        /// </summary>
        /// <returns></returns>
        [DisplayName("办公邮编")]
        public string OfficeZipCode { get; set; }
        /// <summary>
        /// 办公地址
        /// </summary>
        /// <returns></returns>
        [DisplayName("办公地址")]
        public string OfficeAddress { get; set; }
        /// <summary>
        /// 办公传真
        /// </summary>
        /// <returns></returns>
        [DisplayName("办公传真")]
        public string OfficeFax { get; set; }
        /// <summary>
        /// 最高学历
        /// </summary>
        /// <returns></returns>
        [DisplayName("最高学历")]
        public string Education { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        /// <returns></returns>
        [DisplayName("毕业院校")]
        public string School { get; set; }
        /// <summary>
        /// 毕业时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("毕业时间")]
        public DateTime? GraduationDate { get; set; }
        /// <summary>
        /// 所学专业
        /// </summary>
        /// <returns></returns>
        [DisplayName("所学专业")]
        public string Major { get; set; }
        /// <summary>
        /// 最高学位
        /// </summary>
        /// <returns></returns>
        [DisplayName("最高学位")]
        public string Degree { get; set; }
        /// <summary>
        /// 工作时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("工作时间")]
        public DateTime? WorkingDate { get; set; }
        /// <summary>
        /// 家庭住址邮编
        /// </summary>
        /// <returns></returns>
        [DisplayName("家庭住址邮编")]
        public string HomeZipCode { get; set; }
        /// <summary>
        /// 家庭住址
        /// </summary>
        /// <returns></returns>
        [DisplayName("家庭住址")]
        public string HomeAddress { get; set; }
        /// <summary>
        /// 住宅电话
        /// </summary>
        /// <returns></returns>
        [DisplayName("住宅电话")]
        public string HomePhone { get; set; }
        /// <summary>
        /// 家庭传真
        /// </summary>
        /// <returns></returns>
        [DisplayName("家庭传真")]
        public string HomeFax { get; set; }
        /// <summary>
        /// 籍贯省
        /// </summary>
        /// <returns></returns>
        [DisplayName("籍贯省")]
        public string Province { get; set; }
        /// <summary>
        /// 籍贯市
        /// </summary>
        /// <returns></returns>
        [DisplayName("籍贯市")]
        public string City { get; set; }
        /// <summary>
        /// 籍贯区
        /// </summary>
        /// <returns></returns>
        [DisplayName("籍贯区")]
        public string Area { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        /// <returns></returns>
        [DisplayName("籍贯")]
        public string NativePlace { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        /// <returns></returns>
        [DisplayName("政治面貌")]
        public string Party { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        /// <returns></returns>
        [DisplayName("国籍")]
        public string Nation { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        /// <returns></returns>
        [DisplayName("民族")]
        public string Nationality { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        /// <returns></returns>
        [DisplayName("职务")]
        public string Duty { get; set; }
        /// <summary>
        /// 用工性质
        /// </summary>
        /// <returns></returns>
        [DisplayName("用工性质")]
        public string WorkingProperty { get; set; }
        /// <summary>
        /// 职业资格
        /// </summary>
        /// <returns></returns>
        [DisplayName("职业资格")]
        public string Competency { get; set; }
        /// <summary>
        /// 紧急联系
        /// </summary>
        /// <returns></returns>
        [DisplayName("紧急联系")]
        public string EmergencyContact { get; set; }
        /// <summary>
        /// 离职
        /// </summary>
        /// <returns></returns>
        [DisplayName("离职")]
        public int? IsDimission { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("离职日期")]
        public DateTime? DimissionDate { get; set; }
        /// <summary>
        /// 离职原因
        /// </summary>
        /// <returns></returns>
        [DisplayName("离职原因")]
        public string DimissionCause { get; set; }
        /// <summary>
        /// 离职去向
        /// </summary>
        /// <returns></returns>
        [DisplayName("离职去向")]
        public string DimissionWhither { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.EmployeeId = CommonHelper.GetGuid;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.EmployeeId = KeyValue;
        }
        #endregion
    }
}