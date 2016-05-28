using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.Business
{
    /// <summary>
    /// 操作数据库
    /// </summary>
    public class Base_DataBaseBll
    {
        /// <summary>
        /// 获取数据库表结构
        /// <param name="tableName">表</param>
        /// </summary>
        /// <returns></returns>
        public DataTable GetList(string tableName = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"DECLARE @TableInfo TABLE
                            (
                              name VARCHAR(50) ,
                              sumrows VARCHAR(11) ,
                              reserved VARCHAR(50) ,
                              data VARCHAR(50) ,
                              index_size VARCHAR(50) ,
                              unused VARCHAR(50) ,
                              pk VARCHAR(50)
                            )
                        DECLARE @TableName TABLE ( name VARCHAR(50) )
                        DECLARE @name VARCHAR(50)
                        DECLARE @pk VARCHAR(50)
                        INSERT  INTO @TableName
                                ( name
                                )
                                SELECT  o.name
                                FROM    sysobjects o ,
                                        sysindexes i
                                WHERE   o.id = i.id
                                        AND o.Xtype = 'U'
                                        AND i.indid < 2
                                ORDER BY i.rows DESC ,
                                        o.name
                        WHILE EXISTS ( SELECT   1
                                       FROM     @TableName ) 
                            BEGIN
                                SELECT TOP 1
                                        @name = name
                                FROM    @TableName 
                                DELETE  @TableName
                                WHERE   name = @name
                                DECLARE @objectid INT
                                SET @objectid = OBJECT_ID(@name)
                                SELECT  @pk = COL_NAME(@objectid, colid)
                                FROM    sysobjects AS o
                                        INNER JOIN sysindexes AS i ON i.name = o.name
                                        INNER JOIN sysindexkeys AS k ON k.indid = i.indid
                                WHERE   o.xtype = 'PK'
                                        AND parent_obj = @objectid
                                        AND k.id = @objectid
                                INSERT  INTO @TableInfo
                                        ( name ,
                                          sumrows ,
                                          reserved ,
                                          data ,
                                          index_size ,
                                          unused                         
                                        )
                                        EXEC sys.sp_spaceused @name
                                UPDATE  @TableInfo
                                SET     pk = @pk
                                WHERE   name = @name
                            END   
                                   
                        SELECT  F.name ,
                                F.reserved ,
                                F.data ,
                                F.index_size ,
                                RTRIM(F.sumrows) AS sumrows ,
                                F.unused ,
                                ISNULL(p.tdescription,f.name) AS tdescription,
                                F.pk
                        FROM    @TableInfo F
                                LEFT JOIN ( SELECT  name = CASE WHEN A.COLORDER = 1 THEN D.NAME
                                                                ELSE ''
                                                           END ,
                                                    tdescription = CASE WHEN A.COLORDER = 1
                                                                        THEN ISNULL(F.VALUE, '')
                                                                        ELSE ''
                                                                   END
                                            FROM    SYSCOLUMNS A
                                                    LEFT JOIN SYSTYPES B ON A.XUSERTYPE = B.XUSERTYPE
                                                    INNER JOIN SYSOBJECTS D ON A.ID = D.ID
                                                                               AND D.XTYPE = 'U'
                                                                               AND D.NAME <> 'DTPROPERTIES'
                                                    LEFT JOIN sys.extended_properties F ON D.ID = F.major_id
                                            WHERE   a.COLORDER = 1
                                                    AND F.minor_id = 0
                                          ) P ON F.name = p.name WHERE   1 = 1");
            List<DbParameter> parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(tableName))
            {
                strSql.Append(" AND CAST(F.name AS VARCHAR(100)) like @tableCode  or CAST(p.tdescription  AS VARCHAR(100)) like @remark");
                parameter.Add(DbFactory.CreateDbParameter("@tableCode", '%' + tableName + '%'));
                parameter.Add(DbFactory.CreateDbParameter("@remark", '%' + tableName + '%'));
            }
            return DataFactory.Database().FindTableBySql(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取某一个表的所有字段
        /// </summary>
        /// <param name="tableName">查询指定表</param>
        /// <returns></returns>
        public DataTable GetColumnList(string tableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT
                             [number]=a.colorder,
                             [column] =a.name,
							 [datatype]=b.name,
							 [length]=COLUMNPROPERTY(a.id,a.name,'PRECISION'),
							 [identity]=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,
                             [key]=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                             SELECT name FROM sysindexes WHERE indid in(
                             SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid
                             ))) then '√' else '' end,
                             [isnullable]=case when a.isnullable=1 then '√'else '' end,
                             [default]=isnull(e.text,''),
                             [remark]=isnull(g.[value],a.name)
                             FROM syscolumns a
                             left join systypes b on a.xusertype=b.xusertype
                             inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
                             left join syscomments e on a.cdefault=e.id
                             left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id 
                             left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0");
            strSql.Append("where d.name='" + tableName + "' order by a.id,a.colorder");
            return DataFactory.Database().FindTableBySql(strSql.ToString());
        }
        /// <summary>
        /// 获取数据库表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ParameterJson">查询条件</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public DataTable GetDataTableList(string tableName, string ParameterJson, ref JqGridParam jqgridparam)
        {
            StringBuilder strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append("SELECT * FROM " + tableName + " WHERE 1=1");
            if (!string.IsNullOrEmpty(ParameterJson) && ParameterJson.Length > 2)
            {
                strSql.Append(ConditionBuilder.GetWhereSql(ParameterJson.JonsToList<Condition>(), out parameter));
            }
            int totalRow = jqgridparam.records;
            DataTable dt = DataFactory.Database().FindTablePageBySql(strSql.ToString(), parameter.ToArray(), jqgridparam.sidx, jqgridparam.sord, jqgridparam.page, jqgridparam.rows, ref totalRow);
            jqgridparam.records = totalRow;
            return dt;
        }
        /// <summary>
        /// 获取数据库表的主键字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public string GetPrimaryKey(string tableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME='" + tableName + "'");
            DataTable dt = DataFactory.Database().FindTableBySql(strSql.ToString());
            return dt.Rows[0]["column_name"].ToString();
        }
        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public void DeleteTable(string tableName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("drop table {0}", tableName));
            DataFactory.Database().ExecuteBySql(strSql);
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public void CreateTable(StringBuilder strSql, string KeyValue)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    database.ExecuteBySql(new StringBuilder(string.Format("drop table {0}", KeyValue)), isOpenTrans);
                }
                database.ExecuteBySql(strSql, isOpenTrans);
                database.Commit();
            }
            catch
            {
                database.Rollback();
            }
        }

        #region Sqlserver
        #endregion

        #region Oracle
        #endregion

        #region mysql
        #endregion

        #region #region 自动备份数据库
        /// <summary>
        /// 创建自动备份数据库计划
        /// </summary>
        /// <param name="entity">实体参数</param>
        /// <param name="_Mode">执行方式</param>
        /// <param name="_StartTime">执行时间</param>
        public void CreateDbBackup(Base_BackupJob entity, string _Mode, string _StartTime)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                int IsOk = DataFactory.Database().Insert(entity, isOpenTrans);
                if (IsOk > 0)
                {
                    //调用执行备份计划存储过程
                    List<DbParameter> parameter = new List<DbParameter>();
                    parameter.Add(DbFactory.CreateDbParameter("@ServerName", entity.ServerName));
                    parameter.Add(DbFactory.CreateDbParameter("@DbName", entity.DbName));
                    parameter.Add(DbFactory.CreateDbParameter("@JobName", entity.JobName));
                    parameter.Add(DbFactory.CreateDbParameter("@FilePath", entity.FilePath));
                    parameter.Add(DbFactory.CreateDbParameter("@DispatchType", _Mode));
                    parameter.Add(DbFactory.CreateDbParameter("@StartTime", _StartTime));
                    database.ExecuteByProc("Proc_CreateBackJob", parameter.ToArray(), isOpenTrans);
                }
                database.Commit();
            }
            catch
            {
                database.Rollback();
            }
        }
        /// <summary>
        /// 删除备份计划
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="JobName">计划名称</param>
        public void DeleteDbBackup(string KeyValue, string JobName)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                int IsOk = database.Delete<Base_BackupJob>(KeyValue, isOpenTrans);
                if (IsOk > 0)
                {
                    //删除计划
                    List<DbParameter> parameter = new List<DbParameter>();
                    parameter.Add(DbFactory.CreateDbParameter("@JobName", JobName));
                    database.ExecuteByProc("Proc_DropJob", parameter.ToArray(), isOpenTrans);
                }
                database.Commit();
            }
            catch
            {
                database.Rollback();
            }
        }
        #endregion
    }
}
