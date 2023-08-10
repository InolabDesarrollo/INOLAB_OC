<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteRefacciones.aspx.cs" Inherits="INOLAB_OC.Vista.Ingenieros.ReporteRefacciones" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<head  runat="server">
    <title>Reporte Refacciones</title>
    <link href="../../CSS/Normalize.css" rel="stylesheet" />
    <link href="../../CSS/ReporteRefacciones.css" rel="stylesheet" />
 </head>

    <body>
      <form  runat="server">
            <header class="header2">
                        
                <asp:Image ID="Logotipo" runat="server" Height="59px" Width="190px" src="../../Imagenes/LOGO_Blanco_Lineas.png"/>
                <asp:Label ID="lbl_Usuario" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White" ></asp:Label>
                <asp:Label ID="Usuario" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" ></asp:Label>
                
                <input type="checkbox" id="check" />
                <label for="check" class="mostrar-menu">
                    &#8801
                </label>

                <nav class="menu-encabezado"> <!--Aqui se indica la navegacion de nuestra wep-->                                                                     
                           
                    <asp:Button ID="Btn_Servicios_Asignados" runat="server" Text="Ir A Servicios Asignados" class="btn-encabezado"  UseSubmitBehavior="False" onClick="Servicios_Asignados" />
                    <asp:Button ID="Btn_Atras" runat="server" Text="Atras" class="btn-encabezado"  UseSubmitBehavior="False" onClick="Atras" />
                    <asp:Button id="Btn_Salir" runat="server" Text="Salir" class="btn-encabezado" UseSubmitBehavior="False"  onClick="Salir"/>

                    <label for="check" class="esconder-menu">
                        &#215
                    </label>
                </nav>
            </header>

        <!-- Rgistro de Refacciones-->      
        <section class="registro-refacciones"  id="Refacciones" runat="server" style="display: block;">
            <div class="div-refacciones" style="background-color: RGBA(255,255,255,1); padding:30px;" id="sectionf2">
                <div class="buton" id="closebtnref2">
                    <asp:ImageButton Visible="true" ID="ImageButton2" runat="server" ImageAlign="Right" ImageUrl="../../Imagenes/closeimg.png" Width="30px" Height="30px" />
                </div>

                <div>
                <!--Aquí va la lista de refacciones que ya se han agregado con anterioridad -->
                    <div class="form-style-2-heading">
                        <asp:Label ID="Label4" runat="server">Detalle de Refacciones</asp:Label>
                    </div>
                        
                    <asp:Table ID="Table1" runat="server" Width="100%" GridLines="Vertical" BorderColor="#252932"> 
                    <asp:TableRow BackColor="#252932" ForeColor="White" HorizontalAlign="Center">
                    <asp:TableHeaderCell>N° de refacción</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Cantidad</asp:TableHeaderCell>
                    </asp:TableRow>
                    </asp:Table> 
                            
                    <div id="botonnuevoRef" class="btnnuevoRef">
                        <asp:Button runat="server" Text="Nuevo" BorderStyle="None" style="float:right" ID="btnNuevoR" />
                    </div>
                </div>
            </div>
        </section>
       
     </form>  
   
</body>
</html>
