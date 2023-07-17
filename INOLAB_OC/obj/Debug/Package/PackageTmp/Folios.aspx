<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Folios.aspx.cs" Inherits="INOLAB_OC.Folios" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="11pt" ZoomMode="FullPage" Width="1539px"></rsweb:ReportViewer>
        <asp:Label ID="lbluser" runat="server" Text="Label" Font-Bold="False" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>