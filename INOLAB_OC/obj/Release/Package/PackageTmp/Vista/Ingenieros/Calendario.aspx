<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Calendario.aspx.cs" Inherits="Calendario" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link rel="stylesheet" href="../../CSS/EstiloVista.css" />
    <link rel="stylesheet" href="../../CSS/EncabezadoCalendario.css" />
    <link rel="stylesheet" href="../../CSS/drop.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" />
    
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    
    <script type="text/javascript">
        function redireccionar() {
            window.locationf="FSR.aspx";
            } 
        setTimeout ("redireccionar()", 5000); //tiempo expresado en milisegundos
    </script>

    <style type="text/css">
        .auto-style7 {
            margin-bottom: 0;
        }
        </style>

    </head>
<body>
      <form id="form1" runat="server">
        <header class="header2" style="position:relative;" runat="server" id="headerid">
                <div class="wrapper">
                    <div class="logo" style="height:65px"><img src="../../Imagenes/LOGO_Blanco_Lineas.png" class="logo" style="height:65px"/></div>
                    <asp:Label ID="label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White" class="logo" Width="65px" Height="65px"></asp:Label>
                    <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo" Height="65px"></asp:Label>
                    <nav style="width :500px">
                        <div class="dropdown">
                        </div>
                                                 
                        <asp:Button ID="Button5" runat="server" Text="Salir" class="dropbtn" UseSubmitBehavior="False" OnClick="Cerrar_sesion_Click" Height="37px" width="100px"/>
                   
                        <div>
                            <asp:DropDownList ID="mes" runat="server" Width="200px" OnSelectedIndexChanged="mes_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Mes" />
                                <asp:ListItem Text="Enero" />
                                <asp:ListItem Text="Febrero" />
                                <asp:ListItem Text="Marzo" />
                                <asp:ListItem Text="Abril" />
                                <asp:ListItem Text="Mayo" />
                                <asp:ListItem Text="Junio" />
                                <asp:ListItem Text="Julio" />
                                <asp:ListItem Text="Agosto" />
                                <asp:ListItem Text="Septiembre" />
                                <asp:ListItem Text="Octubre" />
                                <asp:ListItem Text="Noviembre" />
                                <asp:ListItem Text="Diciembre" />
                            </asp:DropDownList>
                            <asp:Button ID="Button2" runat="server" Text="⇐" Style="width:80px; height: 40px; margin-left:10px; margin-right:10px" OnClick="Verificar_semana_antes_de_fecha_consulta_Click"/>
                            <asp:Button ID="buscar" runat="server" Text="⇒" Style="width:80px; height: 40px; margin-left:10px; margin-right:10px" OnClick="Verificar_semana_despues_de_fecha_consulta_Click"/>
                            
                        </div>
                    </nav>                
                </div>
            </header>
        <section class="auto-style7" id="sectionreport2" runat="server" style="width:100%; max-height:100%; margin-top:0px">
            <div id="reportdiv" runat="server" class="reportclass2">
                <asp:ScriptManager runat="server"></asp:ScriptManager>        
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" Width="100%" Height="550px" ShowBackButton="False" ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowExportControls="true" ShowFindControls="False" ShowPageNavigationControls="False" ShowRefreshButton="False"></rsweb:ReportViewer>
            </div>
        </section>
      </form>
</body>
</html>
