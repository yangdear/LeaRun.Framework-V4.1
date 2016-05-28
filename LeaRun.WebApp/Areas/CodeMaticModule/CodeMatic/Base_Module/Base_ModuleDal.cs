//=====================================================================================
// All Rights Reserved , Copyright ? Learun 2014
// Software Developers ? Learun 2014
//=====================================================================================

using LeaRun.Kernel;
using LeaRun.Utilities;
using System.Collections;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 模块设置表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.06.22 19:28</date>
    /// </author>
    /// </summary>
    public class Base_ModuleDal : Repository<Base_Module>
    {
        /// <summary>
        /// 获取【模块设置表】数据列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public IList GetListWhere(StringBuilder where, SqlParam[] param,string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT
							FROM Base_Module WHERE 1=1");
            strSql.Append(where);
            return DataFactory.SqlHelper().GetPageList<Base_Module>(strSql.ToString(), param, CommonHelper.ToOrderField("SortCode", orderField), orderType, pageIndex, pageSize, ref count);
        }
    }
}