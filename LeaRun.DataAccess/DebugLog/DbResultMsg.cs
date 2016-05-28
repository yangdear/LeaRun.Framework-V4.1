using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.DataAccess
{
    /// <summary>
    /// 提交数据库信息
    /// 应用单件模式，保存状态
    /// 版本：2.0
    /// <author>
    ///		<name>shecixiong</name>
    ///		<date>2013.09.27</date>
    /// </author>
    /// </summary>
    public class DbResultMsg
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public static string ReturnMsg { get; set; }
        /// <summary>
        /// 耗时
        /// </summary>
        public static string TimeConsuming { get; set; }
    }
}
