<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Money.aspx.cs" Inherits="LeaRun.WebApp.Report.Money" %>

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
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right">年度：</td>
                                    <td>
                                        <asp:RadioButtonList ID="rblyear" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" Width="277px" OnSelectedIndexChanged="rblyear_SelectedIndexChanged">
                                            <asp:ListItem>2012</asp:ListItem>
                                            <asp:ListItem>2013</asp:ListItem>
                                            <asp:ListItem>2014</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <rsweb:ReportViewer ID="MoneyView" runat="server" BackColor="WhiteSmoke" Height="100%" Style="text-align: left" Width="750px" ShowBackButton="False" ShowExportControls="False" ShowFindControls="False" ShowPageNavigationControls="False" ShowPrintButton="False" ShowRefreshButton="False" ShowZoomControl="False">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
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
