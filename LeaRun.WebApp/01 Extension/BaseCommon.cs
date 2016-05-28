using LeaRun.Business;
using LeaRun.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.WebApp
{
    /// <summary>
    /// 通用方法
    /// </summary>
    public class BaseCommon
    {
        /// <summary>
        /// 拼接表单（返回html）
        /// </summary>
        /// <param name="ColumnCount"></param>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public static string CreateBuildForm(int ColumnCount)
        {
            string ModuleId = DESEncrypt.Decrypt(CookieHelper.GetCookie("ModuleId"));
            Base_FormAttributeBll base_formattributebll = new Base_FormAttributeBll();
            return base_formattributebll.CreateBuildFormTable(ColumnCount, ModuleId);
        }
    }
}