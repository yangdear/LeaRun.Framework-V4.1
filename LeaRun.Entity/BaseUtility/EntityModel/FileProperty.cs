using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Entity.EntityModel
{
    /// <summary>
    /// 文件属性
    /// </summary>
    public class FileProperty
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 文件后缀名
        /// </summary>
        public string Eextension { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
