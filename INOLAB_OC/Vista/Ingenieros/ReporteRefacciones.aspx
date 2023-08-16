<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteRefacciones.aspx.cs" Inherits="INOLAB_OC.Vista.Ingenieros.ReporteRefacciones" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Servicios A signados</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link href="../../CSS/Normalize.css" rel="stylesheet" />
    <link href="../../CSS/ServiciosAsignados.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../CSS/ReporteRefacciones.css" /> 

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    

 </head>

    <body>
      <form  runat="server">
            <header class="header2">
                
                <asp:Image ID="Image1" runat="server" Height="59px" Width="190px" src="../../Imagenes/LOGO_Blanco_Lineas.png"/>
                <asp:Label ID="label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White" ></asp:Label>
                <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" ></asp:Label>
                
                <input type="checkbox" id="check" />
                <label for="check" class="mostrar-menu">
                    &#8801
                </label>

                <nap class="menu"> 
                        
                    <asp:Button ID="Btn_Servicios_Asignados" runat="server" Text="Ir A Servicios Asignados" class="btn-encabezado"  UseSubmitBehavior="False" onClick="Servicios_Asignados" />
                    <asp:Button ID="Btn_Atras" runat="server" Text="Atras" class="btn-encabezado"  UseSubmitBehavior="False" onClick="Atras" />
                    <asp:Button id="Btn_Salir" runat="server" Text="Salir" class="btn-encabezado" UseSubmitBehavior="False"  onClick="Salir"/>                   
                    <label for="check" class="esconder-menu">
                        &#215
                    </label>
                </nap>        
            </header>

           <section  runat="server">
              <asp:Button runat="server" Text="Agregar" class="Btn-funciones" id="Btn_agregar"  OnClick="Mostrar_Ventana_Nueva_Refaccion" />
              <asp:Label  runat="server" Text="N° de Folio: "  class="Btn-funciones" id="Lbl_Folio"></asp:Label>
           </section>  
          
           <section ID="Sect_Refacciones" runat="server" Width="97%" style="display: block;" >
            <div  >
            <asp:GridView ID="Gv_Refacciones" runat="server" Width="97%"   AutoGenerateColumns="false" >
             <Columns >
                <asp:BoundField HeaderText="Id" DataField="idReporteRefaccion"  Visible="true" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Numero" DataField="NumeroRefaccion"  Visible="true"  ItemStyle-Width="10%"/>
                <asp:BoundField HeaderText="Cantidad Refaccion" DataField="CantidadRefaccion"  Visible="true" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Descripcion" DataField="Descripcion"  Visible="true" ItemStyle-Width="70%" />
            </Columns>
            <HeaderStyle BackColor="#5B2C6F" forecolor="#FBFCFC " />
            </asp:GridView>
            </div>
          </section> 

          <!--Agregar refaccion -->  
        <section  id="Sect_agregar_refaccion" runat="server" style="display: none;">       
                <div class="buton" id="closebtnref1">
                    <asp:ImageButton Visible="true"  runat="server" ImageAlign="Right" ImageUrl="../../Imagenes/closeimg.png" Width="30px" Height="30px" onClick="Cerrar_Ventana_Refacciones" />
                </div>
            
                    <table class="tabla-agregar-refaccion">
                        <thead>
                           <tr >                           
                               <asp:Label ID="lbl_agregar_refaccion_titulo" runat="server" Text="Agregar refacciónes" ></asp:Label>                            
                           </tr>
                        </thead>
                        <tr>                     
                                <asp:Label ID="LBL_NUM_PARTES_REFACCION" runat="server" Text="N° de partes" ></asp:Label><br />
                                <asp:TextBox ID="txtbox_numero_de_partes" runat="server" autocomplete="off" AutoCompleteType="Disabled" CssClass="txbox-agregar-refaccion" ></asp:TextBox><br />                          
                        </tr>
                        <tr>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                    ControlToValidate="txtbox_numero_de_partes" runat="server"
                                    ErrorMessage="Solo se permiten números"
                                    ValidationExpression="\d+">
                             </asp:RegularExpressionValidator>
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
                   </table>
            
                 
                <div id="Div_agregar_refaccion" >
                     <asp:Button runat="server" Text="Agregar" BorderStyle="None"  ID="Btn_Agregar_Refaccion" OnClick="Agregar_nueva_refaccion"/>
                </div>
        </section>  
         
      </form>
</body>
</html>
