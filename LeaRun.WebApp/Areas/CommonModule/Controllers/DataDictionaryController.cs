using LeaRun.Business;
using LeaRun.DataAccess;
using LeaRun.Entity;
using LeaRun.Repository;
using LeaRun.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.CommonModule.Controllers
{
    /// <summary>
    /// 数据字典表控制器
    /// </summary>
    public class DataDictionaryController : PublicController<Base_DataDictionaryDetail>
    {
        private Base_DataDictionaryBll base_datadictionarybll = new Base_DataDictionaryBll();

        #region 分类管理
        /// <summary>
        /// 分类管理视图
        /// </summary>
        /// <returns></returns>
        [ManagerPermission(PermissionMode.Enforce)]
        public ActionResult SortManage()
        {
            ViewBag.SortCode = BaseFactory.BaseHelper().GetSortCode<Base_DataDictionary>("SortCode").ToString();
            return View();
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteSortManage(string KeyValue)
        {
            try
            {
                var Message = "删除失败。";
                int IsOk = 0;
                if (DataFactory.Database().FindCount<Base_DataDictionary>("ParentId", KeyValue) == 0)
                {
                    IsOk = DataFactory.Database().Delete<Base_DataDictionary>(KeyValue);
                    if (IsOk > 0)
                    {
                        Message = string.Format("成功删除 {0} 条。", 1);
                    }
                }
                else
                {
                    throw new Exception("当前所选有子节点数据，不能删除。");
                }
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = Message }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "错误：" + ex.Message }.ToString());
            }
        }
        /// <summary>
        /// 表单赋值
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SetFormSortManage(string KeyValue)
        {
            Base_DataDictionary entity = DataFactory.Database().FindEntity<Base_DataDictionary>(KeyValue);
            string JsonData = entity.ToJson();
            JsonData = JsonData.Insert(1, "\"ParentName\":\"" + DataFactory.Database().FindEntity<Base_DataDictionary>(entity.ParentId).FullName + "\",");
            return Content(JsonData);
        }
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitFormSortManage(Base_DataDictionary entity, string KeyValue)
        {
            try
            {
                int IsOk = 0;
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    entity.Modify(KeyValue);
                    IsOk = DataFactory.Database().Update(entity);
                }
                else
                {
                    entity.Create();
                    IsOk = DataFactory.Database().Insert(entity);
                }
                return Content(new JsonMessage { Success = true, Code = IsOk.ToString(), Message = KeyValue == "" ? "新增成功。" : "编辑成功。" }.ToString());
            }
            catch (Exception ex)
            {
                return Content(new JsonMessage { Success = false, Code = "-1", Message = "操作失败，错误：" + ex.Message }.ToString());
            }
        }
        #endregion

        /// <summary>
        /// 左边分类列表（返回树JSON）
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeJson()
        {
            List<Base_DataDictionary> list = DataFactory.Database().FindList<Base_DataDictionary>("ORDER BY SortCode ASC");
            List<TreeJsonEntity> TreeList = new List<TreeJsonEntity>();
            foreach (Base_DataDictionary item in list)
            {
                string DataDictionaryId = item.DataDictionaryId;
                bool hasChildren = false;
                List<Base_DataDictionary> childnode = list.FindAll(t => t.ParentId == DataDictionaryId);
                if (childnode.Count > 0)
                {
                    hasChildren = true;
                }
                TreeJsonEntity tree = new TreeJsonEntity();
                tree.id = DataDictionaryId;
                tree.text = item.FullName;
                tree.value = item.Code;
                tree.Attribute = "IsTree";
                tree.AttributeValue = item.IsTree.ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }
        /// <summary>
        /// 数据字典列表（返回树JSON）
        /// </summary>
        /// <param name="DataDictionaryId"></param>
        /// <returns></returns>
        public ActionResult TreeGridListJson(string DataDictionaryId)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(DataDictionaryId))
            {
                List<Base_DataDictionaryDetail> ListData = base_datadictionarybll.GetDataDictionaryDetailList(DataDictionaryId);
                sb.Append("{ \"rows\": [");
                sb.Append(TreeGridJson(ListData, -1));
                sb.Append("]}");
            }
            return Content(sb.ToString());
        }
        int lft = 1, rgt = 1000000;
        public string TreeGridJson(List<Base_DataDictionaryDetail> ListData, int index, string ParentId = "0")
        {
            StringBuilder sb = new StringBuilder();
            List<Base_DataDictionaryDetail> ChildNodeList = ListData.FindAll(t => t.ParentId == ParentId);
            if (ChildNodeList.Count > 0) { index++; }
            foreach (Base_DataDictionaryDetail entity in ChildNodeList)
            {
                string strJson = entity.ToJson();
                strJson = strJson.Insert(1, "\"DataDictionaryDetailName\":\"" + entity.FullName + "\",");
                strJson = strJson.Insert(1, "\"level\":" + index + ",");
                strJson = strJson.Insert(1, "\"isLeaf\":" + (ListData.Count<Base_DataDictionaryDetail>(t => t.ParentId == entity.DataDictionaryDetailId) == 0 ? true : false).ToString().ToLower() + ",");
                strJson = strJson.Insert(1, "\"expanded\":true,");
                strJson = strJson.Insert(1, "\"lft\":" + lft++ + ",");
                strJson = strJson.Insert(1, "\"rgt\":" + rgt-- + ",");
                sb.Append(strJson);
                sb.Append(TreeGridJson(ListData, index, entity.DataDictionaryDetailId));
            }
            return sb.ToString().Replace("}{", "},{");
        }
        /// <summary>
        /// 根据分类编码》获取数据字典明显列表（返回JSON）
        /// </summary>
        /// <param name="Code">分类编码</param>
        /// <returns></returns>
        public ActionResult BinDingItemsJson(string Code)
        {
            List<Base_DataDictionaryDetail> list = base_datadictionarybll.GetDataDictionaryDetailListByCode(Code);
            return Content(list.ToJson());
        }
        /// <summary>
        /// 根据分类Id》获取数据字典明显列表（返回树JSON）
        /// </summary>
        /// <param name="DataDictionaryId">分类主键</param>
        /// <returns></returns>
        public ActionResult DataDictionaryDetailJson(string DataDictionaryId)
        {
            List<Base_DataDictionaryDetail> list = base_datadictionarybll.GetDataDictionaryDetailList(DataDictionaryId);
            List<TreeJsonEntity> TreeList = new List<TreeJsonEntity>();
            foreach (Base_DataDictionaryDetail item in list)
            {
                TreeJsonEntity tree = new TreeJsonEntity();
                tree.id = item.DataDictionaryDetailId;
                tree.text = item.FullName;
                tree.value = item.Code;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.parentId = item.ParentId;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }
    }
}