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
    /// 定义通用的Repository接口
    /// <author>
    ///		<name>shecixiong</name>
    ///		<date>2014.02.28</date>
    /// </author>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : new()
    {
        #region 事务
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        DbTransaction BeginTrans();
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        void Close();
        #endregion

        #region SqlBulkCopy大批量数据插入
        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="datatable">资料表</param>
        /// <returns></returns>
        bool BulkInsert(DataTable datatable);
        #endregion

        #region 执行SQL语句
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        int ExecuteBySql(StringBuilder strSql);
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int ExecuteBySql(StringBuilder strSql, DbTransaction isOpenTrans);
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters);
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters, DbTransaction isOpenTrans);
        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <returns></returns>
        int ExecuteByProc(string procName);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int ExecuteByProc(string procName, DbTransaction isOpenTrans);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        int ExecuteByProc(string procName, DbParameter[] parameters);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int ExecuteByProc(string procName, DbParameter[] parameters, DbTransaction isOpenTrans);
        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        int Insert(T entity);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Insert(T entity, DbTransaction isOpenTrans);
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        int Insert(List<T> entity);
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Insert(List<T> entity, DbTransaction isOpenTrans);
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        int Update(T entity);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Update(T entity, DbTransaction isOpenTrans);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        int Update(string propertyName, string propertyValue);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Update(string propertyName, string propertyValue, DbTransaction isOpenTrans);
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        int Update(List<T> entity);
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Update(List<T> entity, DbTransaction isOpenTrans);
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        int Delete(T entity);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Delete(T entity, DbTransaction isOpenTrans);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <returns></returns>
        int Delete(object propertyValue);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Delete(object propertyValue, DbTransaction isOpenTrans);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        int Delete(string propertyName, string propertyValue);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Delete(string propertyName, string propertyValue, DbTransaction isOpenTrans);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">键值生成SQL条件</param>
        /// <returns></returns>
        int Delete(string tableName, Hashtable ht);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">键值生成SQL条件</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Delete(string tableName, Hashtable ht, DbTransaction isOpenTrans);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        int Delete(object[] propertyValue);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6.....</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Delete(object[] propertyValue, DbTransaction isOpenTrans);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        int Delete(string propertyName, object[] propertyValue);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        int Delete(string propertyName, object[] propertyValue, DbTransaction isOpenTrans);
        #endregion

        #region 查询数据列表、返回List
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <returns></returns>
        List<T> FindListTop(int Top);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        List<T> FindListTop(int Top, string propertyName, string propertyValue);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        List<T> FindListTop(int Top, string WhereSql);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        List<T> FindListTop(int Top, string WhereSql, DbParameter[] parameters);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <returns></returns>
        List<T> FindList();
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        List<T> FindList(string propertyName, string propertyValue);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        List<T> FindList(string WhereSql);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        List<T> FindList(string WhereSql, DbParameter[] parameters);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        List<T> FindListBySql(string strSql);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        List<T> FindListBySql(string strSql, DbParameter[] parameters);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        List<T> FindListPage(ref JqGridParam jqgridparam);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        List<T> FindListPage(string WhereSql, ref JqGridParam jqgridparam);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        List<T> FindListPage(string WhereSql, DbParameter[] parameters, ref JqGridParam jqgridparam);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        List<T> FindListPageBySql(string strSql, ref JqGridParam jqgridparam);
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        List<T> FindListPageBySql(string strSql, DbParameter[] parameters, ref JqGridParam jqgridparam);
        #endregion

        #region 查询数据列表、返回DataTable
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <returns></returns>
        DataTable FindTableTop(int Top);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        DataTable FindTableTop(int Top, string WhereSql);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        DataTable FindTableTop(int Top, string WhereSql, DbParameter[] parameters);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <returns></returns>
        DataTable FindTable();
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        DataTable FindTable(string WhereSql);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        DataTable FindTable(string WhereSql, DbParameter[] parameters);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        DataTable FindTableBySql(string strSql);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        DataTable FindTableBySql(string strSql, DbParameter[] parameters);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        DataTable FindTablePage(ref JqGridParam jqgridparam);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        DataTable FindTablePage(string WhereSql, ref JqGridParam jqgridparam);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        DataTable FindTablePage(string WhereSql, DbParameter[] parameters, ref JqGridParam jqgridparam);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        DataTable FindTablePageBySql(string strSql, ref JqGridParam jqgridparam);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="jqgridparam">分页参数</param>
        /// <returns></returns>
        DataTable FindTablePageBySql(string strSql, DbParameter[] parameters, ref JqGridParam jqgridparam);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <returns></returns>
        DataTable FindTableByProc(string procName);
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        DataTable FindTableByProc(string procName, DbParameter[] parameters);
        #endregion

        #region 查询数据列表、返回DataSet
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        DataSet FindDataSetBySql(string strSql);
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        DataSet FindDataSetBySql(string strSql, DbParameter[] parameters);
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">存储过程</param>
        /// <returns></returns>
        DataSet FindDataSetByProc(string procName);
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        DataSet FindDataSetByProc(string procName, DbParameter[] parameters);
        #endregion

        #region 查询对象、返回实体
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <returns></returns>
        T FindEntity(object propertyValue);
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        T FindEntity(string propertyName, object propertyValue);
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        T FindEntityByWhere(string WhereSql);
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        T FindEntityByWhere(string WhereSql, DbParameter[] parameters);
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        T FindEntityBySql(string strSql);
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        T FindEntityBySql(string strSql, DbParameter[] parameters);
        #endregion

        #region 查询数据、返回条数
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <returns></returns>
        int FindCount();
        /// <summary>
        /// 查询数据、返回条数
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// </summary>
        /// <returns></returns>
        int FindCount(string propertyName, string propertyValue);
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        int FindCount(string WhereSql);
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        int FindCount(string WhereSql, DbParameter[] parameters);
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        int FindCountBySql(string strSql);
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        int FindCountBySql(string strSql, DbParameter[] parameters);
        #endregion

        #region 查询数据、返回最大数
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <returns></returns>
        object FindMax(string propertyName);
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        object FindMax(string propertyName, string WhereSql);
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        object FindMax(string propertyName, string WhereSql, DbParameter[] parameters);
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        object FindMaxBySql(string strSql);
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        object FindMaxBySql(string strSql, DbParameter[] parameters);
        #endregion
    }
}
