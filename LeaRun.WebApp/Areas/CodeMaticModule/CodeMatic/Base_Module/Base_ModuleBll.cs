//=====================================================================================
// All Rights Reserved , Copyright ? Learun 2014
// Software Developers ? Learun 2014
//=====================================================================================

using LeaRun.Kernel;
using LeaRun.Utilities;
using System.Collections;
using System.Collections.Generic;
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
    public class Base_ModuleBll : IRepository<Base_Module>
    {
        private readonly Base_ModuleDal dal = new Base_ModuleDal();

        /// <summary>
        /// 获取【模块设置表】数据列表
        /// </summary>
        /// <param name="where">搜索条件</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public IList GetList(Hashtable where,string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder Sqlwhere = new StringBuilder();
            List<SqlParam> ListParam = new List<SqlParam>();
            return dal.GetListWhere(Sqlwhere, ListParam.ToArray(), orderField, orderType, pageIndex, pageSize, ref  count);
        }
    }
}