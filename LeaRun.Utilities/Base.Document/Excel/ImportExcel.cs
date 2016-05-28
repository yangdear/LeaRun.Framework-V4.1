using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace LeaRun.Utilities
{
    /// <summary>
    /// 导入Excel帮助类
    /// </summary>
    public class ImportExcel
    {
        /// <summary>
        /// Excel检查版本
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string ConnectionString(string fileName)
        {
            bool isExcel2003 = fileName.EndsWith(".xls");
            string connectionString = string.Format(
                isExcel2003
                    ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;"
                    : "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"",
                fileName);
            return connectionString;
        }
        /// <summary>
        /// Excel导入数据源
        /// </summary>
        /// <param name="sheet">sheet</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string sheet, string filename)
        {
            OleDbConnection myConn = new OleDbConnection(ConnectionString(filename));
            try
            {
                DataSet ds;
                string strCom = " SELECT * FROM [Sheet1$]";
                myConn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
                ds = new DataSet();
                myCommand.Fill(ds);
                myConn.Close();
                return ds.Tables[0];
            }
            catch (Exception)
            {
                myConn.Close();
                myConn.Dispose();
                throw;
            }
        }
    }
}
