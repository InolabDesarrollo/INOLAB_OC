<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitorReporteRefacciones.aspx.cs" Inherits="INOLAB_OC.Vista.Ingenieros.MonitorReporteRefacciones" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Servicios A signados</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="../../CSS/Normalize.css" rel="stylesheet" />
    <link href="../../CSS/ReporteRefacciones.css" rel="stylesheet" />
    <link href="../../CSS/GerenteReporteRefacciones.css" rel="stylesheet" />
    <link href="../../CSS/MonitorRefacciones.css" rel="stylesheet" />
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
                    <asp:Button ID="Btn_Servicios_Asignados" runat="server" Text="Ir A Servicios Asignados" class="btn-encabezado"  UseSubmitBehavior="False"  OnClick="Servicios_Asignados" />
                    <asp:Button id="Btn_Salir" runat="server" Text="Salir" class="btn-encabezado" UseSubmitBehavior="False"  OnClick="Salir"   />                   
                    <label for="check" class="esconder-menu">
                        &#215
                    </label>
                </nap>        
            </header>
           <section >
               <asp:DropDownList ID="List_Refacciones" runat="server"  AutoPostBack="True" class="Btn-funciones"  >
                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Revisados</asp:ListItem>
                                <asp:ListItem>Sin Revisar</asp:ListItem>
                                <asp:ListItem>Todos</asp:ListItem>
               </asp:DropDownList>    
           </section>
           <section id="Sect_Gv_Monitor_Folios" style="display: block;"  runat="server">
              <div  style="max-height:500px;overflow-y:scroll;" >
              <asp:GridView runat="server" ID="Gv_Folios"  Width="97%"  Class="GridView" AutoGenerateColumns="false" >
                  <Columns >
                    <asp:BoundField HeaderText="Folio" DataField="Folio"  Visible="true"  ItemStyle-Width="10%"/>                 
                    <asp:BoundField HeaderText="Reviso Gerente" DataField="RevisoGerente"  Visible="true"  ItemStyle-Width="10%"/>
                    <asp:BoundField HeaderText="Descripcion" DataField="Descripcion"  Visible="true"  ItemStyle-Width="30%"/>
                    <asp:BoundField HeaderText="Comentario Gerente" DataField="ComentarioGerente"  Visible="true"  ItemStyle-Width="40%"/>
                    <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro"  Visible="true"  ItemStyle-Width="10%"/>
                  </Columns>
                  <HeaderStyle BackColor="#2471A3" forecolor="#FBFCFC " />
              </asp:GridView>
             </div>
          </section>

      </form>
</body>
</html>
