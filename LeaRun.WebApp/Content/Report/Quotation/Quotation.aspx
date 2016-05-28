<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="LeaRun.WebApp.Report.Quotation" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 20px; margin-left: auto; margin-right: auto; border: 1px solid #ccc; box-shadow: 0 0 10px #BDBDBD; background: #fff; padding: 10px; padding-left: 60px; padding-top: 25px; width: 770px;">
            <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="false" runat="server"></asp:ScriptManager>
            <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <rsweb:ReportViewer ID="QuotationView" runat="server" BackColor="WhiteSmoke" Height="100%" Width="750px"  ZoomMode="PageWidth" ShowBackButton="true" SizeToReportContent="True" BorderStyle="None" CssClass="gird">
                        </rsweb:ReportViewer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
    <style type="text/css">
        body {
            font-size: 12px;
            background: #F7F7F7;
        }

        #QuotationView_ctl05 {
            border: 1px solid #ccc;
            background-image: none;
            height:28px;
            border-radius:5px; 
            width:720px;
        }
    </style>
</body>
</html>
