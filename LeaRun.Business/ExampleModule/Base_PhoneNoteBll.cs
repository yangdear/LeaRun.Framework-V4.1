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
    /// 手机短信表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.22 11:03</date>
    /// </author>
    /// </summary>
    public class Base_PhoneNoteBll : RepositoryFactory<Base_PhoneNote>
    {
        /// <summary>
        /// 手机短信记录列表
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="PhonenNumber">手机号码</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public List<Base_PhoneNote> GetPageList(string UserId, string PhonenNumber, string StartTime, string EndTime, JqGridParam jqgridparam)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append("SELECT * FROM Base_PhoneNote WHERE 1=1");
            //手机号码
            if (!string.IsNullOrEmpty(PhonenNumber))
            {
                strSql.Append(" AND PhonenNumber like @PhonenNumber ");
                parameter.Add(DbFactory.CreateDbParameter("@PhonenNumber", '%' + PhonenNumber + '%'));
            }
            //发送时间
            if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
            {
                strSql.Append(" AND SendTime Between @StartTime AND @EndTime ");
                parameter.Add(DbFactory.CreateDbParameter("@StartTime", CommonHelper.GetDateTime(StartTime + " 00:00")));
                parameter.Add(DbFactory.CreateDbParameter("@EndTime", CommonHelper.GetDateTime(EndTime + " 23:59")));
            }
            strSql.Append(" AND CreateUserId = @UserId");
            parameter.Add(DbFactory.CreateDbParameter("@UserId", UserId));
            return Repository().FindListPageBySql(strSql.ToString(), parameter.ToArray(), ref jqgridparam);
        }
    }
}