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
    /// 编码规则主表
    /// <author>
    ///		<name>she</name>
    ///		<date>2014.10.03 16:51</date>
    /// </author>
    /// </summary>
    public class Base_CodeRuleBll : RepositoryFactory<Base_CodeRule>
    {
        #region 获取编码规则列表
        /// <summary>
        /// 获取编码规划列表
        /// </summary>
        /// <returns></returns>
        public List<Base_CodeRule> GetList()
        {
            return Repository().FindList();
        }
        #endregion

        #region 表单提交
        /// <summary>
        /// 【Excel模板设置】表单提交事件
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="Entity">导入模板实体</param>
        /// <param name="ExcelImportDetailJson">导入模板明细Json</param>
        /// <returns></returns>
        public int SubmitForm(string KeyValue, Base_CodeRule base_coderule, string CodeRuleDetailJson)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            try
            {
                List<Base_CodeRuleDetail> CodeRuleDetailList = CodeRuleDetailJson.JonsToList<Base_CodeRuleDetail>();
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    base_coderule.Modify(KeyValue);
                    database.Update(base_coderule, isOpenTrans);
                    database.Delete("Base_CodeRuleDetail", "CodeRuleId", base_coderule.CodeRuleId);//将原有明细删除掉，后面新增进来，确保不会有重复明细值
                }
                else
                {

                    base_coderule.Create(); database.Insert(base_coderule, isOpenTrans);
                    //插入流水号种子
                    Base_CodeRuleSerious base_coderuleserious = new Base_CodeRuleSerious();
                    base_coderuleserious.Create();
                    base_coderuleserious.CodeRuleId = base_coderule.CodeRuleId;
                    base_coderuleserious.NowValue = 1;
                    database.Insert<Base_CodeRuleSerious>(base_coderuleserious, isOpenTrans);
                }
                int i = 1;
                foreach (Base_CodeRuleDetail base_coderuledetail in CodeRuleDetailList)
                {
                    //跳过空行
                    if (string.IsNullOrEmpty(base_coderuledetail.FormatStr))
                    {
                        continue;
                    }
                    base_coderuledetail.CodeRuleId = base_coderule.CodeRuleId;
                    base_coderuledetail.CodeRuleDetailId = CommonHelper.GetGuid;
                    base_coderuledetail.SortCode = i;
                    i++;
                    database.Insert(base_coderuledetail, isOpenTrans);
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
        #endregion

        #region 单据编码处理
        /// <summary>
        /// 获得单前模块的单据编号如果没有定义规则就返回空
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <returns>单据号</returns>
        public string GetBillCode(string userId, string moduleId)
        {
            IDatabase database = DataFactory.Database();
            DbTransaction isOpenTrans = database.BeginTrans();
            //获得模板ID
            string billCode = "";//单据号
            Base_User base_user = database.FindEntity<Base_User>(userId);
            Base_CodeRule base_coderule = Repository().FindEntity("ModuleId", moduleId);
            try
            {
                int nowSerious = 0;
                //取得流水号种子
                List<Base_CodeRuleSerious> base_coderuleseriouslist = database.FindList<Base_CodeRuleSerious>("CodeRuleId", base_coderule.CodeRuleId);
                //取得最大种子
                Base_CodeRuleSerious maxCodeRuleSerious = base_coderuleseriouslist.Find(delegate(Base_CodeRuleSerious p) { return p.ValueType == "0" && p.UserId == null; });
                if (!string.IsNullOrEmpty(base_coderule.CodeRuleId))
                {
                    List<Base_CodeRuleDetail> base_coderuledetailList = database.FindList<Base_CodeRuleDetail>("CodeRuleId", base_coderule.CodeRuleId);
                    foreach (Base_CodeRuleDetail base_coderuledetail in base_coderuledetailList)
                    {
                        switch (base_coderuledetail.FullName)
                        {
                            //自定义项
                            case "0":
                                billCode = billCode + base_coderuledetail.FormatStr;
                                break;
                            //日期
                            case "1":
                                //日期字符串类型
                                billCode = billCode + DateTime.Now.ToString(base_coderuledetail.FormatStr);
                                //处理自动更新流水号
                                if (base_coderuledetail.AutoReset == 1)
                                {
                                    //判断是否有流水号
                                    if (maxCodeRuleSerious != null)
                                    {
                                        //当上次更新时间跟本次日期不一致时重置流水号种子
                                        if (maxCodeRuleSerious.LastUpdateDate != DateTime.Now.ToString(base_coderuledetail.FormatStr))
                                        {
                                            maxCodeRuleSerious.LastUpdateDate = DateTime.Now.ToString(base_coderuledetail.FormatStr);//更新最后更新时间
                                            maxCodeRuleSerious.NowValue = 1;//重置种子
                                            database.Update<Base_CodeRuleSerious>(maxCodeRuleSerious, isOpenTrans);
                                            //重置种子以后删除掉之前用户占用了的种子。
                                            StringBuilder deleteSql = new StringBuilder(string.Format("delete Base_CodeRuleSerious where CodeRuleId='{0} AND UserId IS NOT NULL '", base_coderule.CodeRuleId));
                                            database.ExecuteBySql(deleteSql, isOpenTrans);
                                        }
                                    }
                                }
                                break;
                            //流水号
                            case "2":
                                //查找当前用户是否已有之前未用掉的种子
                                Base_CodeRuleSerious base_coderuleserious = base_coderuleseriouslist.Find(delegate(Base_CodeRuleSerious p) { return p.UserId == userId && p.Enabled == 1; });
                                //如果没有就取当前最大的种子
                                if (base_coderuleserious == null)
                                {
                                    //取得系统最大的种子
                                    int maxSerious = (int)maxCodeRuleSerious.NowValue;
                                    nowSerious = maxSerious;
                                    base_coderuleserious = new Base_CodeRuleSerious();
                                    base_coderuleserious.Create();
                                    base_coderuleserious.NowValue = maxSerious;
                                    base_coderuleserious.UserId = userId;
                                    base_coderuleserious.ValueType = "1";
                                    base_coderuleserious.Enabled = 1;
                                    base_coderuleserious.CodeRuleId = base_coderule.CodeRuleId;
                                    database.Insert<Base_CodeRuleSerious>(base_coderuleserious, isOpenTrans);
                                    //处理种子更新
                                    maxCodeRuleSerious.NowValue += 1;//种子自增
                                    database.Update<Base_CodeRuleSerious>(maxCodeRuleSerious, isOpenTrans);
                                }
                                else
                                {
                                    nowSerious = (int)base_coderuleserious.NowValue;
                                }
                                string seriousStr = new string('0', (int)(base_coderuledetail.FLength)) + nowSerious.ToString();
                                seriousStr = seriousStr.Substring(seriousStr.Length - (int)(base_coderuledetail.FLength));
                                billCode = billCode + seriousStr;
                                break;
                            //部门
                            case "3":

                                Base_Department base_department = database.FindEntity<Base_Department>(base_user.DepartmentId);
                                billCode = billCode + base_coderuledetail.FormatStr;
                                if (base_coderuledetail.FormatStr == "code")
                                {
                                    billCode = billCode + base_department.Code;
                                }
                                else
                                {
                                    billCode = billCode + base_department.FullName;
                                }
                                break;
                            //公司
                            case "4":
                                Base_Company base_company = database.FindEntity<Base_Company>(base_user.CompanyId);
                                if (base_coderuledetail.FormatStr == "code")
                                {
                                    billCode = billCode + base_company.Code;
                                }
                                else
                                {
                                    billCode = billCode + base_company.FullName;
                                }
                                break;
                            //用户
                            case "5":
                                if (base_coderuledetail.FormatStr == "code")
                                {
                                    billCode = billCode + base_user.Code;
                                }
                                else
                                {
                                    billCode = billCode + base_user.Account;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Base_SysLogBll.Instance.WriteLog("", OperationType.Other, "-1", string.Format("{0}在获取{1}单据编码时错误：", base_user.RealName, base_coderule.FullName) + ex.Message);
                database.Rollback();
                return billCode;
            }
            database.Commit();
            return billCode;
        }
        /// <summary>
        /// 占用单据号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <returns>true/false</returns>
        public bool OccupyBillCode(string userId, string moduleId, DbTransaction isOpenTrans = null)
        {
            Base_CodeRule base_coderule = Repository().FindEntity("ModuleId", moduleId);
            try
            {
                IDatabase database = DataFactory.Database();
                if (base_coderule != null)
                {
                    List<Base_CodeRuleSerious> base_coderuleseriouslist = database.FindList<Base_CodeRuleSerious>("CodeRuleId", base_coderule.CodeRuleId);
                    //查找当前用户是否已有之前未用掉的种子
                    Base_CodeRuleSerious base_coderuleserious = base_coderuleseriouslist.Find(delegate(Base_CodeRuleSerious p) { return p.UserId == userId && p.Enabled == 1; });
                    if (base_coderuleserious != null)
                    {
                        database.Delete<Base_CodeRuleSerious>(base_coderuleserious);
                    }
                }
            }
            catch (Exception ex)
            {
                Base_SysLogBll.Instance.WriteLog("", OperationType.Other, "-1", string.Format("占用{0}单据编码时错误：", base_coderule.FullName) + ex.Message);
                return false;
            }
            return true;
        }
        #endregion
    }
}