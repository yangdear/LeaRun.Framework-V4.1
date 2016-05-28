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
    /// 表单附加属性实例
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.11.06 17:04</date>
    /// </author>
    /// </summary>
    [Description("表单附加属性实例")]
    [PrimaryKey("AttributeValueId")]
    public class Base_FormAttributeValue : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 附加属性实例主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("附加属性实例主键")]
        public string AttributeValueId { get; set; }
        /// <summary>
        /// 模块主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("模块主键")]
        public string ModuleId { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("对象主键")]
        public string ObjectId { get; set; }
        /// <summary>
        /// 参数Json
        /// </summary>
        /// <returns></returns>
        [DisplayName("参数Json")]
        public string ObjectParameterJson { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.AttributeValueId = CommonHelper.GetGuid;
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.AttributeValueId = KeyValue;
                                            }
        #endregion
    }
}