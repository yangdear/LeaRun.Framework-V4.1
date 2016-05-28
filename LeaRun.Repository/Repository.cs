using LeaRun.DataAccess;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.Repository
{
    /// <summary>
    /// LeaRun.ORM 定义通用的Repository
    /// <author>
    ///		<name>shecixiong</name>
    ///		<date>2014.02.28</date>
    /// </author>
    /// </summary>
    /// <typeparam name="T">定义泛型，约束其是一个类</typeparam>
    public class Repository<T> : IRepository<T> where T : new()
    {
        #region 事务
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public DbTransaction BeginTrans()
        {
            return DataFactory.Database().BeginTrans();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            DataFactory.Database().Commit();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            DataFactory.Database().Rollback();
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            DataFactory.Database().Close();
        }
        #endregion

        #region SqlBulkCopy大批量数据插入
        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="datatable">资料表</param>
        /// <returns></returns>
        public bool BulkInsert(DataTable datatable)
        {
            return DataFactory.Database().BulkInsert(datatable);
        }
        #endregion

        #region 执行SQL语句
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql)
        {
            return DataFactory.Database().ExecuteBySql(strSql);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().ExecuteBySql(strSql, isOpenTrans);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters)
        {
            return DataFactory.Database().ExecuteBySql(strSql, parameters);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().ExecuteBySql(strSql, parameters, isOpenTrans);
        }
        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName)
        {
            return DataFactory.Database().ExecuteByProc(procName);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().ExecuteByProc(procName, isOpenTrans);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, DbParameter[] parameters)
        {
            return DataFactory.Database().ExecuteByProc(procName, parameters);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, DbParameter[] parameters, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().ExecuteByProc(procName, parameters, isOpenTrans);
        }
        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        public int Insert(T entity)
        {
            return DataFactory.Database().Insert<T>(entity);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Insert(T entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Insert<T>(entity, isOpenTrans);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        public int Insert(List<T> entity)
        {
            return DataFactory.Database().Insert<T>(entity);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Insert(List<T> entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Insert<T>(entity, isOpenTrans);
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int Update(T entity)
        {
            return DataFactory.Database().Update<T>(entity);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update(T entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Update<T>(entity, isOpenTrans);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public int Update(string propertyName, string propertyValue)
        {
            return DataFactory.Database().Update<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update(string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Update<T>(propertyName, propertyValue, isOpenTrans);
        }
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int Update(List<T> entity)
        {
            return DataFactory.Database().Update<T>(entity);
        }
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update(List<T> entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Update<T>(entity, isOpenTrans);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public int Delete(T entity)
        {
            return DataFactory.Database().Delete<T>(entity);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(T entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Delete<T>(entity, isOpenTrans);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <returns></returns>
        public int Delete(object propertyValue)
        {
            return DataFactory.Database().Delete<T>(propertyValue);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(object propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Delete<T>(propertyValue, isOpenTrans);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public int Delete(string propertyName, string propertyValue)
        {
            return DataFactory.Database().Delete<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Delete<T>(propertyName, propertyValue, isOpenTrans);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">键值生成SQL条件</param>
        /// <returns></returns>
        public int Delete(string tableName, Hashtable ht)
        {
            return DataFactory.Database().Delete(tableName, ht);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">键值生成SQL条件</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(string tableName, Hashtable ht, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Delete(tableName, ht, isOpenTrans);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public int Delete(object[] propertyValue)
        {
            return DataFactory.Database().Delete<T>(propertyValue);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6.....</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(object[] propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Delete<T>(propertyValue, isOpenTrans);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public int Delete(string propertyName, object[] propertyValue)
        {
            return DataFactory.Database().Delete<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(string propertyName, object[] propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database().Delete<T>(propertyName, propertyValue, isOpenTrans);
        }
        #endregion

        #region 查询数据列表、返回List
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <returns></returns>
        public List<T> FindListTop(int Top)
        {
            return DataFactory.Database().FindListTop<T>(Top);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public List<T> FindListTop(int Top, string propertyName, string propertyValue)
        {
            return DataFactory.Database().FindListTop<T>(Top, propertyName, propertyValue);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public List<T> FindListTop(int Top, string WhereSql)
        {
            return DataFactory.Database().FindListTop<T>(Top, WhereSql);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public List<T> FindListTop(int Top, string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindListTop<T>(Top, WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <returns></returns>
        public List<T> FindList()
        {
            return DataFactory.Database().FindList<T>();
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public List<T> FindList(string propertyName, string propertyValue)
        {
            return DataFactory.Database().FindList<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public List<T> FindList(string WhereSql)
        {
            return DataFactory.Database().FindList<T>(WhereSql);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public List<T> FindList(string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindList<T>(WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public List<T> FindListBySql(string strSql)
        {
            return DataFactory.Database().FindListBySql<T>(strSql);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public List<T> FindListBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindListBySql<T>(strSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public List<T> FindListPage(ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            List<T> List = DataFactory.Database().FindListPage<T>(orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return List;
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public List<T> FindListPage(string WhereSql, ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            List<T> List = DataFactory.Database().FindListPage<T>(WhereSql, orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return List;
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public List<T> FindListPage(string WhereSql, DbParameter[] parameters, ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            List<T> List = DataFactory.Database().FindListPage<T>(WhereSql, parameters, orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return List;
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public List<T> FindListPageBySql(string strSql, ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            List<T> List = DataFactory.Database().FindListPageBySql<T>(strSql, orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return List;
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public List<T> FindListPageBySql(string strSql, DbParameter[] parameters, ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            List<T> List = DataFactory.Database().FindListPageBySql<T>(strSql, parameters, orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return List;
        }
        #endregion

        #region 查询数据列表、返回DataTable
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <returns></returns>
        public DataTable FindTableTop(int Top)
        {
            return DataFactory.Database().FindTableTop<T>(Top);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public DataTable FindTableTop(int Top, string WhereSql)
        {
            return DataFactory.Database().FindTableTop<T>(Top, WhereSql);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTableTop(int Top, string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindTableTop<T>(Top, WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable FindTable()
        {
            return DataFactory.Database().FindTable<T>();
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public DataTable FindTable(string WhereSql)
        {
            return DataFactory.Database().FindTable<T>(WhereSql);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTable(string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindTable<T>(WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public DataTable FindTableBySql(string strSql)
        {
            return DataFactory.Database().FindTableBySql(strSql);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTableBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindTableBySql(strSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public DataTable FindTablePage(ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            DataTable dt = DataFactory.Database().FindTablePage<T>(orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return dt;
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public DataTable FindTablePage(string WhereSql, ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            DataTable dt = DataFactory.Database().FindTablePage<T>(WhereSql, orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return dt;
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public DataTable FindTablePage(string WhereSql, DbParameter[] parameters, ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            DataTable dt = DataFactory.Database().FindTablePage<T>(WhereSql, parameters, orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return dt;
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public DataTable FindTablePageBySql(string strSql, ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            DataTable dt = DataFactory.Database().FindTablePageBySql(strSql, orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return dt;
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        public DataTable FindTablePageBySql(string strSql, DbParameter[] parameters, ref JqGridParam jqgridparam)
        {
            string orderField = jqgridparam.sidx;
            string orderType = jqgridparam.sord;
            int pageIndex = jqgridparam.page;
            int pageSize = jqgridparam.rows;
            int totalRow = jqgridparam.records;
            DataTable dt = DataFactory.Database().FindTablePageBySql(strSql, parameters, orderField, orderType, pageIndex, pageSize, ref totalRow);
            jqgridparam.records = totalRow;
            return dt;
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <returns></returns>
        public DataTable FindTableByProc(string procName)
        {
            return DataFactory.Database().FindTableByProc(procName);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTableByProc(string procName, DbParameter[] parameters)
        {
            return DataFactory.Database().FindTableByProc(procName, parameters);
        }
        #endregion

        #region 查询数据列表、返回DataSet
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public DataSet FindDataSetBySql(string strSql)
        {
            return DataFactory.Database().FindDataSetBySql(strSql);
        }
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataSet FindDataSetBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindDataSetBySql(strSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">存储过程</param>
        /// <returns></returns>
        public DataSet FindDataSetByProc(string procName)
        {
            return DataFactory.Database().FindDataSetByProc(procName);
        }
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataSet FindDataSetByProc(string procName, DbParameter[] parameters)
        {
            return DataFactory.Database().FindDataSetByProc(procName, parameters);
        }
        #endregion

        #region 查询对象、返回实体
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <returns></returns>
        public T FindEntity(object propertyValue)
        {
            return DataFactory.Database().FindEntity<T>(propertyValue);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public T FindEntity(string propertyName, object propertyValue)
        {
            return DataFactory.Database().FindEntity<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public T FindEntityByWhere(string WhereSql)
        {
            return DataFactory.Database().FindEntity<T>(WhereSql);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public T FindEntityByWhere(string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindEntity<T>(WhereSql, parameters);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public T FindEntityBySql(string strSql)
        {
            return DataFactory.Database().FindEntityBySql<T>(strSql);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public T FindEntityBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindEntityBySql<T>(strSql, parameters);
        }
        #endregion

        #region 查询数据、返回条数
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <returns></returns>
        public int FindCount()
        {
            return DataFactory.Database().FindCount<T>();
        }
        /// <summary>
        /// 查询数据、返回条数
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// </summary>
        /// <returns></returns>
        public int FindCount(string propertyName, string propertyValue)
        {
            return DataFactory.Database().FindCount<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public int FindCount(string WhereSql)
        {
            return DataFactory.Database().FindCount<T>(WhereSql);
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int FindCount(string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindCount<T>(WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public int FindCountBySql(string strSql)
        {
            return DataFactory.Database().FindCountBySql(strSql);
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int FindCountBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindCountBySql(strSql, parameters);
        }
        #endregion

        #region 查询数据、返回最大数
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <returns></returns>
        public object FindMax(string propertyName)
        {
            return DataFactory.Database().FindMax<T>(propertyName);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public object FindMax(string propertyName, string WhereSql)
        {
            return DataFactory.Database().FindMax<T>(propertyName, WhereSql);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public object FindMax(string propertyName, string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindMax<T>(propertyName, WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public object FindMaxBySql(string strSql)
        {
            return DataFactory.Database().FindMaxBySql(strSql);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public object FindMaxBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database().FindMaxBySql(strSql, parameters);
        }
        #endregion
    }
}
