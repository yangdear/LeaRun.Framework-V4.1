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
    public partial class Quotation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                QuotationBind();
            }
        }
        private void QuotationBind()
        {
            this.QuotationView.Reset();
            this.QuotationView.LocalReport.Dispose();
            this.QuotationView.LocalReport.DataSources.Clear();

            ReportDataSource QuotationDataSource = new ReportDataSource();
            QuotationDataSource.Name = "DataSet1";
            ReportBll reportbll = new ReportBll();
            string filepath = Server.MapPath("~/Content/Report/Quotation/QuotationJson.txt");
            QuotationDataSource.Value = reportbll.GetQuotationList(filepath, "");
            this.QuotationView.LocalReport.DisplayName = "报价单";
            this.QuotationView.LocalReport.ReportPath = Server.MapPath("~/Content/Report/Quotation/Quotation.rdlc");
            this.QuotationView.LocalReport.DataSources.Add(QuotationDataSource);
            this.QuotationView.LocalReport.Refresh();
        }
    }
}