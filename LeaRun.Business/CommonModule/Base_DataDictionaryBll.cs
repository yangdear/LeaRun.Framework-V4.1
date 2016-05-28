//=====================================================================================
// All Rights Reserved , Copyright @ Learun 2014
// Software Developers @ Learun 2014
//=====================================================================================

using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 数据字典表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.07.18 14:08</date>
    /// </author>
    /// </summary>
    public class Base_DataDictionaryBll : RepositoryFactory<Base_DataDictionary>
    {
        /// <summary>
        /// 获取数据字典明细列表
        /// </summary>
        /// <param name="DataDictionaryId">主表 主键值</param>
        /// <returns></returns>
        public List<Base_DataDictionaryDetail> GetDataDictionaryDetailList(string DataDictionaryId)
        {
            if (!string.IsNullOrEmpty(DataDictionaryId))
            {
                StringBuilder WhereSql = new StringBuilder();
                WhereSql.Append(" AND DataDictionaryId = @DataDictionaryId Order By SortCode ASC");
                List<DbParameter> parameter = new List<DbParameter>();
                parameter.Add(DbFactory.CreateDbParameter("@DataDictionaryId", DataDictionaryId));
                return DataFactory.Database().FindList<Base_DataDictionaryDetail>(WhereSql.ToString(), parameter.ToArray());
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取数据字典明细列表
        /// </summary>
        /// <param name="Code">分类编码</param>
        /// <returns></returns>
        public List<Base_DataDictionaryDetail> GetDataDictionaryDetailListByCode(string Code)
        {
            if (!string.IsNullOrEmpty(Code))
            {
                StringBuilder WhereSql = new StringBuilder();
                WhereSql.Append(" AND DataDictionaryId IN(SELECT DataDictionaryId FROM Base_DataDictionary WHERE Code=@Code)");
                WhereSql.Append(" ORDER BY SortCode ASC");
                List<DbParameter> parameter = new List<DbParameter>();
                parameter.Add(DbFactory.CreateDbParameter("@Code", Code));
                return DataFactory.Database().FindList<Base_DataDictionaryDetail>(WhereSql.ToString(), parameter.ToArray());
            }
            else
            {
                return null;
            }
        }
    }
}