//=====================================================================================
// All Rights Reserved , Copyright @ Learun 2014
// Software Developers @ Learun 2014
//=====================================================================================

using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 邮件信息表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.09.26 16:55</date>
    /// </author>
    /// </summary>
    public class Base_EmailBll : RepositoryFactory<Base_Email>
    {
        /// <summary>
        /// 统计邮件信息（未读数、草稿数、已发送数、已删除数）
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataTable GetCountEmail(string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  SUM(CASE a.IsRead
                                          WHEN 0 THEN 1
                                          ELSE 0
                                        END) AS UnRead ,	   	     --未读数
                                    MAX(EA.Draft) AS Draft ,		                                            --草稿数
                                    MAX(EA.Sended) - MAX(ISNULL(EA.DeleteMark, 0))-MAX(ISNULL(EA.DropMark, 0)) AS Sended ,					--已发送数
                                    SUM(CASE a.DeleteMark WHEN 1 THEN 1 ELSE 0 END) + MAX(ISNULL(EA.DeleteMark, 0)) AS DELETED 	--已删除数
                            FROM    Base_Email e
                                    LEFT JOIN Base_EmailAddressee a ON e.EmailId = a.EmailId
                                                                       AND a.AddresseeId = @UserId
                                    LEFT JOIN ( SELECT  SUM(CASE State WHEN 1 THEN 1 ELSE 0 END) AS Sended ,
                                                        SUM(case State WHEN 0 THEN 1 ELSE 0 END) AS Draft ,
                                                        MAX(CreateUserId) AS CreateUserId ,
                                                        SUM(CASE DeleteMark WHEN 1 THEN 1 ELSE 0 END) AS DeleteMark,
                                                        SUM(CASE DeleteMark WHEN 2 THEN 1 ELSE 0 END) AS DropMark
                                                FROM    Base_Email
                                                WHERE   CreateUserId = @UserId
                                              ) EA ON e.CreateUserId = EA.CreateUserId");
            List<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter("@UserId", UserId));
            return DataFactory.Database().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 邮件列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="UserId">用户Id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">总数</param>
        /// <returns></returns>
        public DataTable EmailList(int type, string UserId, int pageIndex, int pageSize, ref int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            switch (type)
            {
                case 1://收件箱 
                    strSql.Append(@"SELECT    CASE DATEDIFF(day,SendDate, GETDATE())WHEN 0 THEN '今天' WHEN 1 THEN '昨天' ELSE '更早' END AS DateArea ,
                                                        CASE DATEDIFF(day,  SendDate,  GETDATE()) WHEN 0 THEN 0 WHEN 1 THEN 1 ELSE 2
                                                        END AS sort ,
                                                        a.EmailAddresseeId AS EmailId ,
                                                        '2' AS datatype ,
                                                        e.Theme ,
                                                        e.ThemeColour ,
                                                        e.Addresser ,
                                                        e.SendDate ,
                                                        ISNULL(e.IsAccessory,
                                                              0) AS IsAccessory ,
                                                        a.IsRead ,
                                                        0 AS EmailCnt ,
                                                        0 AS UnRead ,
                                                        e.Priority ,
                                                        e.State
                                              FROM      Base_Email e
                                                        INNER JOIN dbo.Base_EmailAddressee a ON e.EmailId = a.EmailId
                                              WHERE     1 = 1
                                                        AND a.AddresseeId = @UserId
                                                        AND a.DeleteMark = 0
                                              UNION
                                              SELECT    DateArea ,
                                                        sort ,
                                                        '' AS EmailId ,
                                                        '1' AS datatype ,
                                                        '' AS Theme ,
                                                        '' AS ThemeColour ,
                                                        '' AS Addresser ,
                                                        '' AS SendDate ,
                                                        0 AS IsAccessory ,
                                                        0 AS IsRead ,
                                                        COUNT(1) AS EmailCnt ,
                                                        SUM(CASE IsReaded WHEN 0 THEN 1 ELSE 0 END  ) AS UnRead ,
                                                        0 AS Priority ,
                                                        0 AS State
                                              FROM      ( SELECT
                                                              CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                              WHEN 0 THEN '今天'
                                                              WHEN 1 THEN '昨天'
                                                              ELSE '更早'
                                                              END AS DateArea ,
                                                              CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                              WHEN 0 THEN 0
                                                              WHEN 1 THEN 1
                                                              ELSE 2
                                                              END AS sort ,
                                                              a.IsRead AS IsReaded
                                                          FROM
                                                              Base_Email e
                                                              INNER JOIN dbo.Base_EmailAddressee a ON e.EmailId = a.EmailId
                                                          WHERE
                                                              1 = 1
                                                              AND a.AddresseeId = @UserId
                                                        ) Gorup
                                              GROUP BY  DateArea , sort");
                    break;
                case 2://草稿
                    strSql.Append(@"SELECT    CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                          WHEN 0 THEN '今天'
                                                          WHEN 1 THEN '昨天'
                                                          ELSE '更早'
                                                        END AS DateArea ,
                                                        CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                          WHEN 0 THEN 0
                                                          WHEN 1 THEN 1
                                                          ELSE 2
                                                        END AS sort ,
                                                        e.EmailId ,
                                                        '2' AS datatype ,
                                                        e.Theme ,
                                                        e.ThemeColour ,
                                                        e.Addresser ,
                                                        e.SendDate ,
                                                        ISNULL(e.IsAccessory,
                                                              0) AS IsAccessory ,
                                                        1 AS IsRead ,
                                                        0 AS EmailCnt ,
                                                        0 AS UnRead ,
                                                        e.Priority ,
                                                        e.State
                                              FROM      Base_Email e
                                              WHERE     1 = 1
                                                        AND e.CreateUserId = @UserId
                                                        AND e.DeleteMark = 0
                                                        AND e.State = 0
                                              UNION
                                              SELECT    DateArea ,
                                                        sort ,
                                                        '' AS EmailId ,
                                                        '1' AS datatype ,
                                                        '' AS Theme ,
                                                        '' AS ThemeColour ,
                                                        '' AS Addresser ,
                                                        '' AS SendDate ,
                                                        0 AS IsAccessory ,
                                                        0 AS IsRead ,
                                                        COUNT(1) AS EmailCnt ,
                                                        SUM(1
                                                            - ISNULL(IsReaded,
                                                              0)) AS UnRead ,
                                                        0 AS Priority ,
                                                        0 AS State
                                              FROM      ( SELECT
                                                              CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                              WHEN 0 THEN '今天'
                                                              WHEN 1 THEN '昨天'
                                                              ELSE '更早'
                                                              END AS DateArea ,
                                                              CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                              WHEN 0 THEN 0
                                                              WHEN 1 THEN 1
                                                              ELSE 2
                                                              END AS sort ,
                                                              1 AS IsReaded
                                                          FROM
                                                              Base_Email e
                                                          WHERE
                                                              1 = 1
                                                              AND e.CreateUserId = @UserId
                                                              AND e.DeleteMark = 0
                                                              AND e.State = 0
                                                        ) Gorup
                                              GROUP BY  DateArea ,sort");
                    break;
                case 3://已发送
                    strSql.Append(@"SELECT    CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                          WHEN 0 THEN '今天'
                                                          WHEN 1 THEN '昨天'
                                                          ELSE '更早'
                                                        END AS DateArea ,
                                                        CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                          WHEN 0 THEN 0
                                                          WHEN 1 THEN 1
                                                          ELSE 2
                                                        END AS sort ,
                                                        e.EmailId ,
                                                        '2' AS datatype ,
                                                        e.Theme ,
                                                        e.ThemeColour ,
                                                        e.Addresser ,
                                                        e.SendDate ,
                                                        ISNULL(e.IsAccessory,
                                                              0) AS IsAccessory ,
                                                        1 AS IsRead ,
                                                        0 AS EmailCnt ,
                                                        0 AS UnRead ,
                                                        e.Priority ,
                                                        e.State
                                              FROM      Base_Email e
                                              WHERE     1 = 1
                                                        AND e.CreateUserId = @UserId
                                                        AND e.DeleteMark = 0
                                                        AND e.State = 1
                                              UNION
                                              SELECT    DateArea ,
                                                        sort ,
                                                        '' AS EmailId ,
                                                        '1' AS datatype ,
                                                        '' AS Theme ,
                                                        '' AS ThemeColour ,
                                                        '' AS Addresser ,
                                                        '' AS SendDate ,
                                                        0 AS IsAccessory ,
                                                        0 AS IsRead ,
                                                        COUNT(1) AS EmailCnt ,
                                                        SUM(1
                                                            - ISNULL(IsReaded,
                                                              0)) AS UnRead ,
                                                        0 AS Priority ,
                                                        0 AS State
                                              FROM      ( SELECT
                                                              CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                              WHEN 0 THEN '今天'
                                                              WHEN 1 THEN '昨天'
                                                              ELSE '更早'
                                                              END AS DateArea ,
                                                              CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                              WHEN 0 THEN 0
                                                              WHEN 1 THEN 1
                                                              ELSE 2
                                                              END AS sort ,
                                                              1 AS IsReaded
                                                          FROM
                                                              Base_Email e
                                                          WHERE
                                                              1 = 1
                                                              AND e.CreateUserId = @UserId
                                                              AND e.DeleteMark = 0
                                                              AND e.State = 1
                                                        ) Gorup
                                              GROUP BY  DateArea , sort");
                    break;
                case 4://已删除
                    strSql.Append(@"SELECT    CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                          WHEN 0 THEN '今天'
                                                          WHEN 1 THEN '昨天'
                                                          ELSE '更早'
                                                        END AS DateArea ,
                                                        CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                          WHEN 0 THEN 0
                                                          WHEN 1 THEN 1
                                                          ELSE 2
                                                        END AS sort ,
                                                        e.EmailId+'|3' AS EmailId,
                                                        '2' AS datatype ,
                                                        e.Theme ,
                                                        e.ThemeColour ,
                                                        e.Addresser ,
                                                        e.SendDate ,
                                                        ISNULL(e.IsAccessory,
                                                              0) AS IsAccessory ,
                                                        1 AS IsRead ,
                                                        0 AS EmailCnt ,
                                                        0 AS UnRead ,
                                                        e.Priority ,
                                                        e.State
                                              FROM      Base_Email e
                                              WHERE     1 = 1
                                                        AND e.CreateUserId = @UserId
                                                        AND e.DeleteMark = 1
                                              UNION
                                              SELECT    CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                          WHEN 0 THEN '今天'
                                                          WHEN 1 THEN '昨天'
                                                          ELSE '更早'
                                                        END AS DateArea ,
                                                        CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                          WHEN 0 THEN 0
                                                          WHEN 1 THEN 1
                                                          ELSE 2
                                                        END AS sort ,
                                                        a.EmailAddresseeId +'|1' AS EmailId,
                                                        '2' AS datatype ,
                                                        e.Theme ,
                                                        e.ThemeColour ,
                                                        e.Addresser ,
                                                        e.SendDate ,
                                                        ISNULL(e.IsAccessory,
                                                              0) AS IsAccessory ,
                                                        a.IsRead,
                                                        0 AS EmailCnt ,
                                                        0 AS UnRead ,
                                                        e.Priority ,
                                                        e.State
                                              FROM      Base_Email e
                                                        INNER JOIN dbo.Base_EmailAddressee a ON e.EmailId = a.EmailId
                                              WHERE     1 = 1
                                                        AND a.AddresseeId = @UserId
                                                        AND a.DeleteMark = 1
                                              UNION
                                              SELECT    DateArea ,
                                                        sort ,
                                                        '' AS EmailId ,
                                                        '1' AS datatype ,
                                                        '' AS Theme ,
                                                        '' AS ThemeColour ,
                                                        '' AS Addresser ,
                                                        '' AS SendDate ,
                                                        0 AS IsAccessory ,
                                                        0 AS IsRead ,
                                                        COUNT(1) AS EmailCnt ,
                                                        SUM(CASE IsReaded WHEN 0 THEN 1 ELSE 0 END) AS UnRead ,
                                                        0 AS Priority ,
                                                        0 AS State
                                              FROM      ( SELECT
                                                              CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                              WHEN 0 THEN '今天'
                                                              WHEN 1 THEN '昨天'
                                                              ELSE '更早'
                                                              END AS DateArea ,
                                                              CASE DATEDIFF(day,
                                                              SendDate,
                                                              GETDATE())
                                                              WHEN 0 THEN 0
                                                              WHEN 1 THEN 1
                                                              ELSE 2
                                                              END AS sort ,
                                                              1 AS IsReaded
                                                          FROM
                                                              Base_Email e
                                                          WHERE
                                                              1 = 1
                                                              AND e.CreateUserId = @UserId
                                                              AND e.DeleteMark = 1
                                                          UNION
                                                          SELECT
                                                              CASE DATEDIFF(day,
                                                              CreateDate,
                                                              GETDATE())
                                                              WHEN 0 THEN '今天'
                                                              WHEN 1 THEN '昨天'
                                                              ELSE '更早'
                                                              END AS DateArea ,
                                                              CASE DATEDIFF(day,
                                                              CreateDate,
                                                              GETDATE())
                                                              WHEN 0 THEN 0
                                                              WHEN 1 THEN 1
                                                              ELSE 2
                                                              END AS sort ,
                                                              1 AS IsReaded
                                                          FROM
                                                              dbo.Base_EmailAddressee a
                                                          WHERE
                                                              1 = 1
                                                              AND a.AddresseeId = @UserId
                                                              AND a.DeleteMark = 1
                                                        ) Gorup
                                              GROUP BY  DateArea ,
                                                        sort");
                    break;
                default:
                    break;
            }
            List<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter("@UserId", UserId));
            return DataFactory.Database().FindTablePageBySql(strSql.ToString(), parameter.ToArray(), "sort,DataType ,SendDate", "DESC", pageIndex, pageSize, ref recordCount);
        }
        /// <summary>
        /// 收件人列表
        /// </summary>
        /// <param name="EmailId">邮件主键</param>
        /// <returns></returns>
        public List<Base_EmailAddressee> EmailAddresseeList(string EmailId)
        {
            return DataFactory.Database().FindList<Base_EmailAddressee>("EmailId", EmailId);
        }
        /// <summary>
        /// 附件列表
        /// </summary>
        /// <param name="EmailId">邮件主键</param>
        /// <returns></returns>
        public List<Base_EmailAccessory> EmailAccessoryList(string EmailId)
        {
            return DataFactory.Database().FindList<Base_EmailAccessory>("EmailId", EmailId);
        }
        /// <summary>
        /// 发送内部邮件
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="base_email">邮件信息</param>
        /// <param name="AddresseeList">收件人</param>
        /// <param name="AccessoryList">附件</param>
        /// <returns></returns>
        public int SendEmail(string KeyValue, Base_Email base_email, List<Base_EmailAddressee> AddresseeList, List<Base_EmailAccessory> AccessoryList)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    base_email.Modify(KeyValue);
                    database.Update(base_email, isOpenTrans);
                    database.Delete<Base_EmailAddressee>("EmailId", KeyValue, isOpenTrans);
                    database.Delete<Base_EmailAccessory>("EmailId", KeyValue, isOpenTrans);
                }
                else
                {
                    base_email.Create();
                    database.Insert(base_email, isOpenTrans);
                }
                foreach (Base_EmailAddressee emailaddressee in AddresseeList)
                {
                    emailaddressee.Create();
                    emailaddressee.EmailAddresseeId = CommonHelper.GetGuid;
                    emailaddressee.EmailId = base_email.EmailId;
                    emailaddressee.AddresseeIdState = 0;
                    database.Insert(emailaddressee, isOpenTrans);
                }
                foreach (Base_EmailAccessory emailaccessory in AccessoryList)
                {
                    emailaccessory.Create();
                    emailaccessory.EmailId = base_email.EmailId;
                    emailaccessory.FileType = emailaccessory.FileName.Substring(emailaccessory.FileName.IndexOf("."));
                    database.Insert(emailaccessory, isOpenTrans);
                }
                database.Commit();
                return 1;
            }
            catch
            {
                database.Rollback();
                return -1;
            }
        }
        /// <summary>
        /// 设置邮件 已读、未读
        /// </summary>
        /// <param name="EmailId">邮件主键</param>
        /// <param name="UserId">当前用户</param>
        /// <param name="IsRead">是否已读：0-未读；1-已读</param>
        /// <returns></returns>
        public void ReadEmail(string EmailId, string UserId, int IsRead)
        {
            Base_EmailAddressee entity = this.GetEmailAddressee(EmailId, UserId);
            if (entity.ReadDate == null)
            {
                entity.ReadDate = DateTime.Now;
            }
            if (entity.ReadCount == null)
            {
                entity.ReadCount = 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"UPDATE  Base_EmailAddressee
                            SET     IsRead = @IsRead ,
                                    ReadCount = @ReadCount ,
                                    ReadDate = @ReadDate ,
                                    EndReadDate = @EndReadDate
                            WHERE   AddresseeId = @AddresseeId
                                    AND EmailId = @EmailId");
            List<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter("@IsRead", IsRead));
            parameter.Add(DbFactory.CreateDbParameter("@ReadCount", entity.ReadCount + 1));
            parameter.Add(DbFactory.CreateDbParameter("@ReadDate", entity.ReadDate));
            parameter.Add(DbFactory.CreateDbParameter("@EndReadDate", DateTime.Now));
            parameter.Add(DbFactory.CreateDbParameter("@AddresseeId", UserId));
            parameter.Add(DbFactory.CreateDbParameter("@EmailId", EmailId));
            int IsOk = DataFactory.Database().ExecuteBySql(strSql, parameter.ToArray());
        }
        /// <summary>
        /// 获取收件人表对象信息
        /// </summary>
        /// <param name="EmailId">邮件主键</param>
        /// <param name="UserId">当前用户</param>
        /// <returns></returns>
        public Base_EmailAddressee GetEmailAddressee(string EmailId, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM Base_EmailAddressee WHERE 1=1
                                     AND AddresseeId = @AddresseeId
                                     AND EmailId = @EmailId");
            List<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter("@AddresseeId", UserId));
            parameter.Add(DbFactory.CreateDbParameter("@EmailId", EmailId));
            return DataFactory.Database().FindEntityBySql<Base_EmailAddressee>(strSql.ToString(), parameter.ToArray());
        }
    }
}