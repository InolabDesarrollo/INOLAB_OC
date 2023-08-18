<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GerenteReporteRefacciones.aspx.cs" Inherits="INOLAB_OC.Vista.Ingenieros.GerenteReporteRefaccion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Servicios A signados</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="../../CSS/Normalize.css" rel="stylesheet" />
    <link href="../../CSS/ReporteRefacciones.css" rel="stylesheet" />
    <link href="../../CSS/EncabezadoInolab.css" rel="stylesheet" />
 </head>
    <body>
      <form  runat="server">
            <header class="header_inolab">
                
                <asp:Image ID="Image1" runat="server" Height="59px" Width="190px" src="../../Imagenes/LOGO_Blanco_Lineas.png"/>
                <asp:Label ID="label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White" ></asp:Label>
                <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" ></asp:Label>
                
                <input type="checkbox" id="check" />
                <label for="check" class="mostrar-menu">
                    &#8801
                </label>

                <nap class="menu"> 
                        
                    <asp:Button ID="Btn_Servicios_Asignados" runat="server" Text="Ir A Servicios Asignados" class="btn-encabezado"  UseSubmitBehavior="False"  />
                    <asp:Button ID="Btn_Atras" runat="server" Text="Atras" class="btn-encabezado"  UseSubmitBehavior="False"  />
                    <asp:Button id="Btn_Salir" runat="server" Text="Salir" class="btn-encabezado" UseSubmitBehavior="False"  />                   
                    <label for="check" class="esconder-menu">
                        &#215
                    </label>
                </nap>        
            </header>
      </form>
</body>
</html>

