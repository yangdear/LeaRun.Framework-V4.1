using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.DataAccess
{
    /// <summary>
    /// 操作数据库基类 接口
    /// </summary>
    public interface IDatabase : IDisposable
    {
        bool inTransaction { get; set; }
        DbTransaction BeginTrans();
        void Commit();
        void Rollback();
        void Close();

        bool BulkInsert(DataTable dt);

        int ExecuteBySql(StringBuilder strSql);
        int ExecuteBySql(StringBuilder strSql, DbTransaction isOpenTrans);
        int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters);
        int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters, DbTransaction isOpenTrans);

        int ExecuteByProc(string procName);
        int ExecuteByProc(string procName, DbTransaction isOpenTrans);
        int ExecuteByProc(string procName, DbParameter[] parameters);
        int ExecuteByProc(string procName, DbParameter[] parameters, DbTransaction isOpenTrans);

        int Insert<T>(T entity);
        int Insert<T>(T entity, DbTransaction isOpenTrans);
        int Insert<T>(List<T> entity);
        int Insert<T>(List<T> entity, DbTransaction isOpenTrans);
        int Insert(string tableName, Hashtable ht);
        int Insert(string tableName, Hashtable ht, DbTransaction isOpenTrans);

        int Update<T>(T entity);
        int Update<T>(T entity, DbTransaction isOpenTrans);
        int Update<T>(string propertyName, string propertyValue);
        int Update<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans);
        int Update<T>(List<T> entity);
        int Update<T>(List<T> entity, DbTransaction isOpenTrans);
        int Update(string tableName, Hashtable ht, string propertyName);
        int Update(string tableName, Hashtable ht, string propertyName, DbTransaction isOpenTrans);

        int Delete<T>(T entity);
        int Delete<T>(T entity, DbTransaction isOpenTrans);
        int Delete<T>(object propertyValue);
        int Delete<T>(object propertyValue, DbTransaction isOpenTrans);
        int Delete<T>(string propertyName, string propertyValue);
        int Delete<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans);
        int Delete(string tableName, string propertyName, string propertyValue);
        int Delete(string tableName, string propertyName, string propertyValue, DbTransaction isOpenTrans);
        int Delete(string tableName, Hashtable ht);
        int Delete(string tableName, Hashtable ht, DbTransaction isOpenTrans);
        int Delete<T>(object[] propertyValue);
        int Delete<T>(object[] propertyValue, DbTransaction isOpenTrans);
        int Delete<T>(string propertyName, object[] propertyValue);
        int Delete<T>(string propertyName, object[] propertyValue, DbTransaction isOpenTrans);
        int Delete(string tableName, string propertyName, object[] propertyValue);
        int Delete(string tableName, string propertyName, object[] propertyValue, DbTransaction isOpenTrans);

        List<T> FindListTop<T>(int Top) where T : new();
        List<T> FindListTop<T>(int Top, string propertyName, string propertyValue) where T : new();
        List<T> FindListTop<T>(int Top, string WhereSql) where T : new();
        List<T> FindListTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new();
        List<T> FindList<T>() where T : new();
        List<T> FindList<T>(string propertyName, string propertyValue) where T : new();
        List<T> FindList<T>(string WhereSql) where T : new();
        List<T> FindList<T>(string WhereSql, DbParameter[] parameters) where T : new();
        List<T> FindListBySql<T>(string strSql);
        List<T> FindListBySql<T>(string strSql, DbParameter[] parameters);
        List<T> FindListPage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        List<T> FindListPage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        List<T> FindListPage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        List<T> FindListPageBySql<T>(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);
        List<T> FindListPageBySql<T>(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);

        DataTable FindTableTop<T>(int Top) where T : new();
        DataTable FindTableTop<T>(int Top, string WhereSql) where T : new();
        DataTable FindTableTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new();
        DataTable FindTable<T>() where T : new();
        DataTable FindTable<T>(string WhereSql) where T : new();
        DataTable FindTable<T>(string WhereSql, DbParameter[] parameters) where T : new();
        DataTable FindTableBySql(string strSql);
        DataTable FindTableBySql(string strSql, DbParameter[] parameters);
        DataTable FindTablePage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        DataTable FindTablePage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        DataTable FindTablePage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new();
        DataTable FindTablePageBySql(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);
        DataTable FindTablePageBySql(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount);
        DataTable FindTableByProc(string procName);
        DataTable FindTableByProc(string procName, DbParameter[] parameters);

        DataSet FindDataSetBySql(string strSql);
        DataSet FindDataSetBySql(string strSql, DbParameter[] parameters);
        DataSet FindDataSetByProc(string procName);
        DataSet FindDataSetByProc(string procName, DbParameter[] parameters);

        T FindEntity<T>(object propertyValue) where T : new();
        T FindEntity<T>(string propertyName, object propertyValue) where T : new();
        T FindEntityByWhere<T>(string WhereSql) where T : new();
        T FindEntityByWhere<T>(string WhereSql, DbParameter[] parameters) where T : new();
        T FindEntityBySql<T>(string strSql);
        T FindEntityBySql<T>(string strSql, DbParameter[] parameters);

        Hashtable FindHashtable(string tableName, string propertyName, object propertyValue);
        Hashtable FindHashtable(string tableName, StringBuilder WhereSql);
        Hashtable FindHashtable(string tableName, StringBuilder WhereSql, DbParameter[] parameters);
        Hashtable FindHashtableBySql(string strSql);
        Hashtable FindHashtableBySql(string strSql, DbParameter[] parameters);

        int FindCount<T>() where T : new();
        int FindCount<T>(string propertyName, string propertyValue) where T : new();
        int FindCount<T>(string WhereSql) where T : new();
        int FindCount<T>(string WhereSql, DbParameter[] parameters) where T : new();
        int FindCountBySql(string strSql);
        int FindCountBySql(string strSql, DbParameter[] parameters);

        object FindMax<T>(string propertyName) where T : new();
        object FindMax<T>(string propertyName, string WhereSql) where T : new();
        object FindMax<T>(string propertyName, string WhereSql, DbParameter[] parameters) where T : new();
        object FindMaxBySql(string strSql);
        object FindMaxBySql(string strSql, DbParameter[] parameters);
    }
}
