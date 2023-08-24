<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GerenteReporteRefacciones.aspx.cs" Inherits="INOLAB_OC.Vista.Ingenieros.GerenteReporteRefaccion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Servicios A signados</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="../../CSS/Normalize.css" rel="stylesheet" />
    <link href="../../CSS/ReporteRefacciones.css" rel="stylesheet" />
    <link href="../../CSS/GerenteReporteRefacciones.css" rel="stylesheet" />

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
                        
                    <asp:Button ID="Btn_Servicios_Asignados" runat="server" Text="Ir A Servicios Asignados" class="btn-encabezado"  UseSubmitBehavior="False" onClick="Servicios_Asignados" />
                    <asp:Button id="Btn_Salir" runat="server" Text="Salir" class="btn-encabezado" UseSubmitBehavior="False"  onClick="Salir"  />                   
                    <label for="check" class="esconder-menu">
                        &#215
                    </label>
                </nap>        
            </header>


          <section ID="Sect_Buscar">
            <asp:Label runat="server" ID="Lbl_BuscarPor" Text="Buscar reporte por: " class="Btn-funciones"></asp:Label>
            <asp:DropDownList ID="List_BuscarReporte" runat="server"  AutoPostBack="True" class="Btn-funciones" OnSelectedIndexChanged="List_Buscar_Reporte" >
                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Folio</asp:ListItem>
                                <asp:ListItem>Ingeniero</asp:ListItem>
              </asp:DropDownList>
         </section>
         <section runat="server" ID="Sect_Buscar_Por" style="display: none;" >
             <asp:Label runat="server" ID="Lbl_Buscar_Por" ></asp:Label>
             <asp:TextBox runat="server" ID="Txbox_Buscar_Por"></asp:TextBox>
             <asp:Button runat="server" ID="Btn_Buscar" Text="Buscar"  onClick="Buscar_Folio" Visible="false"/>
             <asp:Button runat="server" ID="Btn_Buscar_Ingeniero" Text="Buscar" Visible="false" onClick="Buscar_Ingeniero" />
         </section>

          <section id="Sect_Gv_Folios" style="display: none;"  runat="server">
              <div  style="max-height:500px;overflow-y:scroll;" >
              <asp:GridView runat="server" ID="Gv_Folios"  Width="97%"  Class="GridView" AutoGenerateColumns="false"
                  OnRowCommand="Gv_Folios_RowCommand" >
                  <Columns >
                    <asp:ButtonField HeaderText="Seleccionar" Text="Seleccionar"  ItemStyle-Width="10%" CommandName="Seleccionar" />

                    <asp:BoundField HeaderText="Folio" DataField="Folio"  Visible="true"  ItemStyle-Width="10%"/>
                    <asp:BoundField HeaderText="Id Ingeniero" DataField="Id_Ingeniero"  Visible="true"  ItemStyle-Width="10%"/>
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre"  Visible="true"  ItemStyle-Width="10%"/>
                    <asp:BoundField HeaderText="Cliente" DataField="Cliente"  Visible="true"  ItemStyle-Width="50%"/>
                    <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro"  Visible="true"  ItemStyle-Width="10%"/>
                  </Columns>
                  <HeaderStyle BackColor="#2471A3" forecolor="#FBFCFC " />
              </asp:GridView>
             </div>
          </section>

           <section id="Sect_Gv_Ingenieros" style="display: none;" runat="server">
              <div  style="max-height:500px;overflow-y:scroll;" >
              <asp:GridView runat="server" ID="Gv_Ingenieros"  Width="97%" Class="GridView" AutoGenerateColumns="false" 
                  OnRowCommand="Gv_Ingenieros_RowCommand">
                  <Columns >
                    <asp:ButtonField HeaderText="Seleccionar" Text="Seleccionar"  ItemStyle-Width="10%" CommandName="Seleccionar"/>

                    <asp:BoundField HeaderText="Ingeniero" DataField="Nombre"  Visible="true"  ItemStyle-Width="30%"/>
                    <asp:BoundField HeaderText="Folio" DataField="Folio"  Visible="true"  ItemStyle-Width="30%"/>
                    <asp:BoundField HeaderText="Cliente" DataField="Cliente"  Visible="true"  ItemStyle-Width="40%"/>
                  </Columns>
                  <HeaderStyle BackColor="#2471A3" forecolor="#FBFCFC " />
              </asp:GridView>
             </div>
          </section>

      </form>
</body>
</html>

