using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using LeaRun.Business;

namespace LeaRun.WebApp.Report
{
    public partial class BankAnalyze : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BankAnalyzeBind();
            }
        }
        private void BankAnalyzeBind()
        {
            this.BankAnalyzeView.Reset();
            this.BankAnalyzeView.LocalReport.Dispose();
            this.BankAnalyzeView.LocalReport.DataSources.Clear();

            ReportDataSource BankAnalyzeDataSource = new ReportDataSource();
            BankAnalyzeDataSource.Name = "DataSet1";
            ReportBll reportbll = new ReportBll();
            string filepath = Server.MapPath("~/Content/Report/BankAnalyze/BankAnalyzeJson.txt");
            BankAnalyzeDataSource.Value = reportbll.GetBankAnalyzeList(filepath, "");

            this.BankAnalyzeView.LocalReport.ReportPath = Server.MapPath("~/Content/Report/BankAnalyze/BankAnalyze.rdlc");
            this.BankAnalyzeView.LocalReport.DataSources.Add(BankAnalyzeDataSource);
            this.BankAnalyzeView.LocalReport.Refresh();
        }
    }
}