<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaFin.aspx.cs" Inherits="INOLAB_OC.CargaFin" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link rel="stylesheet" href="../../CSS/EstiloVista.css" />
    <link rel="stylesheet" href="../../CSS/EncabezadoComun.css" />
    <link rel="stylesheet" href="../../CSS/drop.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css"/>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css"/>
    
    <script src="https://cdn.jsdelivr.net/npm/signature_pad@2.3.2/dist/signature_pad.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

   
    <style type="text/css">
        .auto-style1 {
            width: 98%;
            max-width: 10000px;
            margin: auto;
            height: 69px;
        }
        .auto-style6 {
            height: 48px;
        }
        .auto-style7 {
            padding-top: 10px;
            left: 0px;
            top: 0px;
        }
        </style>
</head>

<body style="overflow:auto;" onload="window.history.forward();">
    <form id="form1" runat="server">
        <header class="header2" style="position:relative" runat="server" id="headerid">
            <div id="headerone" class="auto-style1" runat="server">
                <div class="logo" style="height: 67px"><img src="../../Imagenes/LOGO_Blanco_Lineas.png"  class="logo"/></div>
                <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo" ></asp:Label>
                <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo"></asp:Label>
            </div>
        </header>
                <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="25000" />
        <section class="auto-style7" id="sectionreport" runat="server">
            <div id="reportdiv" runat="server" class="reportclass">
                <asp:ScriptManager runat="server"></asp:ScriptManager>        
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" Width="100%" ShowExportControls="False" ShowFindControls="False"></rsweb:ReportViewer>
            </div>
        </section>
        
        <footer runat="server" id="footerid" class="footercl">
            <asp:Label ID="Label6" runat="server" Text="Guardando Datos, espere un momento..." Font-Bold="True" ForeColor="White"  class="logo" ></asp:Label>
        </footer>
    </form>
</body>
</html>