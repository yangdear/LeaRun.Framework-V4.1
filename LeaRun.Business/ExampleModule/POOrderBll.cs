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
    /// 订单主表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.27 12:04</date>
    /// </author>
    /// </summary>
    public class POOrderBll : RepositoryFactory<POOrder>
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="BillNo">制单编号</param>
        /// <param name="StartTime">制单开始时间</param>
        /// <param name="EndTime">制单结束时间</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public List<POOrder> GetOrderList(string BillNo, string StartTime, string EndTime, JqGridParam jqgridparam)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT * FROM POOrder WHERE 1=1");
            //制单编号
            if (!string.IsNullOrEmpty(BillNo))
            {
                strSql.Append(" AND BillNo like @BillNo ");
                parameter.Add(DbFactory.CreateDbParameter("@BillNo", '%' + BillNo + '%'));
            }
            //制单开始
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                strSql.Append(" AND BillDate Between @StartTime AND @EndTime ");
                parameter.Add(DbFactory.CreateDbParameter("@StartTime", CommonHelper.GetDateTime(StartTime + " 00:00")));
                parameter.Add(DbFactory.CreateDbParameter("@EndTime", CommonHelper.GetDateTime(EndTime + " 23:59")));
            }
            return Repository().FindListPageBySql(strSql.ToString(), parameter.ToArray(), ref jqgridparam);
        }
        /// <summary>
        /// 订单明细列表
        /// </summary>
        /// <param name="POOrderId">订单主键</param>
        /// <returns></returns>
        public List<POOrderEntry> GetOrderEntryList(string POOrderId)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"SELECT * FROM POOrderEntry WHERE 1=1");
            strSql.Append(" AND POOrderId = @POOrderId");
            parameter.Add(DbFactory.CreateDbParameter("@POOrderId", POOrderId));
            return DataFactory.Database().FindListBySql<POOrderEntry>(strSql.ToString(), parameter.ToArray());
        }
    }
}