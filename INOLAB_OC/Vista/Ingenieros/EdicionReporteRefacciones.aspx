<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EdicionReporteRefacciones.aspx.cs" Inherits="INOLAB_OC.Vista.Ingenieros.EdicionReporteRefacciones" %>

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
                        
                    <asp:Button ID="Btn_Servicios_Asignados" runat="server" Text="Ir A Servicios Asignados" class="btn-encabezado"  UseSubmitBehavior="False" onClick="Servicios_Asignados"  />
                    <asp:Button ID="Btn_Buscador_Refacciones" runat="server" Text="Atras" class="btn-encabezado"  UseSubmitBehavior="False" onClick="Atras" />
                    <asp:Button id="Btn_Salir" runat="server" Text="Salir" class="btn-encabezado" UseSubmitBehavior="False"  onClick="Salir"  />                   
                    <label for="check" class="esconder-menu">
                        &#215
                    </label>
                </nap>        
            </header>

          <div>
           <section  runat="server"  >
            <asp:TextBox id="Txbox_Folio" runat="server" Text="Folio " CssClass="Btn-funciones" ></asp:TextBox>        
           </section> 
          </div>

            <section ID="Sect_Refacciones" runat="server" Width="97%" style="display: block;" >
            <div  style="max-height:400px;overflow-y:scroll;">

            <asp:GridView ID="Gv_Refacciones" runat="server" Width="97%"   AutoGenerateColumns="false" 
                OnRowCommand="Gv_Refacciones_RowCommand"  >
             <Columns >
                <asp:ButtonField runat="server" HeaderText="Editar" Text="Editar" ItemStyle-Width="10%" CommandName="Editar"  />
                <asp:BoundField HeaderText="Numero" DataField="NumeroRefaccion"  Visible="true"  ItemStyle-Width="10%"/>
                <asp:BoundField HeaderText="Cantidad Refaccion" DataField="CantidadRefaccion"  Visible="true" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Descripcion de Ingeniero" DataField="Descripcion"  Visible="true" ItemStyle-Width="30%" />
                <asp:BoundField HeaderText="Comentario Gerente" DataField="ComentarioGerente"  Visible="true" ItemStyle-Width="30%" />
                <asp:BoundField HeaderText="Fecha" DataField="FechaRegistro"  Visible="true" ItemStyle-Width="10%" />
            </Columns>
            <HeaderStyle BackColor="#5B2C6F" forecolor="#FBFCFC " />
            </asp:GridView>

            </div>
          </section>

            <!--Editar refaccion -->  
        <section  id="Sect_Editar_Refaccion" runat="server" style="display: none;">       
                <div class="buton" id="closebtnref1">
                    <asp:ImageButton Visible="true"  runat="server" ImageAlign="Right" ImageUrl="../../Imagenes/closeimg.png" Width="30px" Height="30px" OnClick="Cerrar_Ventana_Editar_Refaccion" />
                </div>
            
                    <table class="tabla-agregar-refaccion">
                        <thead>
                           <tr >                           
                               <asp:Label ID="lbl_agregar_refaccion_titulo" runat="server" Text="Editar Refaccion" ></asp:Label>                            
                           </tr>
                        </thead>
                        <tr>                     
                                <asp:Label ID="LBL_NUM_PARTES_REFACCION" runat="server" Text="N° de partes" ></asp:Label><br />
                                <asp:TextBox ID="txtbox_numero_de_partes" runat="server" autocomplete="off" AutoCompleteType="Disabled" CssClass="txbox-agregar-refaccion" ></asp:TextBox><br />                          
                        </tr>                 
                        <tr>    
                            <br />
                              <asp:Label ID="LBL_CANTIDAD_REFACCION" runat="server" Text="Cantidad"   ></asp:Label><br />
                              <asp:TextBox ID="txtbox_cantidad_refaccion" runat="server" Columns="2"  autocomplete="off" AutoCompleteType="Disabled" CssClass="txbox-agregar-refaccion"></asp:TextBox><br />
                        </tr>
                        <tr >
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                    ControlToValidate="txtbox_cantidad_refaccion" runat="server"
                                    ErrorMessage="Solo se permiten números"
                                    ValidationExpression="\d+">
                             </asp:RegularExpressionValidator>
                        </tr>
                        <tr>  
                              <br />
                              <asp:Label ID="LBL_DESCRIPCION_REFACCION" runat="server" Text="Descripción"  ></asp:Label><br />
                              <asp:TextBox ID="txtbox_descripcion_refaccion" runat="server" TextMode="MultiLine" Rows="5" MaxLength="240" autocomplete="off" AutoCompleteType="Disabled" CssClass="txbox-agregar-refaccion"></asp:TextBox>               
                        </tr>
                        <tr>
                            <br />
                            <asp:Label ID="Lbl_Comentario_Gerente" runat="server" Text="Comentario de Gerente"  ></asp:Label><br />
                            <asp:TextBox ID="Txtbox_Comentario_Gerente" runat="server" TextMode="MultiLine"  MaxLength="280" autocomplete="off" AutoCompleteType="Disabled" CssClass="txbox-agregar-refaccion"></asp:TextBox>
                        </tr>
                   </table>                
                <div id="Div_agregar_refaccion" >
                     <asp:Button runat="server" Text="Finalizar" BorderStyle="None"  ID="Btn_Finalizar_Edicion" />
                </div>
        </section>  
      </form>
</body>
</html>

