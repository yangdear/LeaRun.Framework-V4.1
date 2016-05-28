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
    public partial class Money : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MoneyBind("2014");
            }
        }
        private void MoneyBind(string year)
        {
            this.MoneyView.Reset();
            this.MoneyView.LocalReport.Dispose();
            this.MoneyView.LocalReport.DataSources.Clear();

            ReportDataSource MoneyDataSource = new ReportDataSource();
            MoneyDataSource.Name = "DataSet1";
            ReportBll reportbll = new ReportBll();
            string filepath = Server.MapPath("~/Content/Report/Money/MoneyJson.txt");
            MoneyDataSource.Value = reportbll.GetMoneyBalList(filepath, year);

            this.MoneyView.LocalReport.ReportPath = Server.MapPath("~/Content/Report/Money/Money.rdlc");
            this.MoneyView.LocalReport.DataSources.Add(MoneyDataSource);
            this.MoneyView.LocalReport.Refresh();
        }

        protected void rblyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            MoneyBind(this.rblyear.SelectedValue);
        }
    }
}