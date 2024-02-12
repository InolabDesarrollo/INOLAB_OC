<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRM_4.aspx.cs" Inherits="INOLAB_OC.CRM_4" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Plan de trabajo</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="../../CSS/EstiloVista.css" />
    <link rel="stylesheet" href="../../CSS/EncabezadoCRM_2.css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" />


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

    <script>
        $(function () {
            $("#datepicker").datepicker();
        });
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '< Ant',
            nextText: 'Sig >',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);
        $(function () {
            $("#fecha").datepicker();
        });

        $(function () {
            $("#datepicker2").datepicker();
        });
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '< Ant',
            nextText: 'Sig >',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);
        $(function () {
            $("#fecha2").datepicker();
        });
    </script>

    <style type="text/css">
        .auto-style2 {
            width: 95%;
            max-width: 100%;
            margin: auto;
            height: 75px;
        }
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            text-align: right;
            height: 49px;
        }
        .auto-style5 {
            height: 49px;
        }
        .auto-style6 {
            text-align: right;
            height: 47px;
        }
        .auto-style7 {
            height: 47px;
        }
        </style>

    </head>
<body>
    <form id="form1" runat="server">

     <header class="header2">
        
        <div class="logo" style="height: 70px"><img src="../../Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
            <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo" Width="65px" Height="68px" ></asp:Label>
            <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo" Height="69px"></asp:Label>
            <asp:Label ID="lbliduser" runat="server" Text="id" Font-Bold="True" ForeColor="White" class="logo" Height="69px" Visible="false" ></asp:Label>

            <input type="checkbox" id="check" />
                <label for="check" class="mostrar-menu">
                    &#8801
                </label>

            <nav class="menu">
                <asp:Button ID="btnRegistroFunnel" runat="server" Text="Registro Funnel" class="boton"  visible="False" /> 
                <asp:Button ID="btnInforme_A" runat="server" Text="Estadisticas" class="boton" visible="False"  Target="_blank" />     
                <asp:Button ID="Button1" runat="server" Text="Cotizaciones" class="boton" visible="false"/>
                <asp:Button ID="Btn_VolverMenuPrincipal" runat="server" Text="Volver a menu principal" class="boton" />
            
                <label for="check" class="esconder-menu">
                        &#215
                </label>
            
            </nav>                

    </header>
        <section class="contenido2" >           
                <div class="form-style-2 " >
                <div class="form-style-2-heading">Registro de Autorizaciones</div>

                <table class="auto-style3">
                    <tr>
                      <td><strong>Cliente</strong></td>
                      <td><strong>Clasificacion</strong></td>
                      <td><strong>Tipo Venta</strong></td>
                      <td><strong>Fecha Cierre de Etapa</strong></td>
                    </tr>
 
                    <tr>
                      <td><asp:TextBox ID="txtcliente" class="mercado" runat="server" Width="430px"></asp:TextBox><asp:Label ID="lblresistro" Visible="False" runat="server" Text="Label"></asp:Label></td>
                        
                      <td><asp:DropDownList ID="ddlClas_save" class="mercado" runat="server" AutoPostBack="True" Width="135px">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>Prospecto</asp:ListItem>
                                    <asp:ListItem>Oportunidad</asp:ListItem>
                                    <asp:ListItem>Lead</asp:ListItem>
                                    <asp:ListItem>Proyecto</asp:ListItem>
                                    <asp:ListItem>Forecast</asp:ListItem>
                                    <asp:ListItem>Orden Compra</asp:ListItem>  
                                    <asp:ListItem>No Relacionado</asp:ListItem>
                                    <asp:ListItem>Perdido</asp:ListItem>
                                </asp:DropDownList></td>
                      <td><asp:TextBox ID="txttipoventa" class="mercado" runat="server" Width="176px"></asp:TextBox></td>
                      <td><asp:TextBox ID="datepicker" class="mercado" runat="server" Enabled="False"></asp:TextBox></td>
                    </tr>
 
                    <tr>
                      <td><strong>Equipo</strong></td>
                      <td><strong>Valor</strong></td>
                      <td><strong>Solicitud Fecha Cierre</strong></td>
                      <td><strong>Solicitud Cambio Clasificacion</strong></td>
                      <td></td>
                    </tr>
 
                    <tr>
                      <td><asp:TextBox ID="txtequipo" class="mercado" runat="server" Width="430px"></asp:TextBox><asp:Label ID="lblautorizar" Visible="False" runat="server" Text="Label"></asp:Label></td>
                      <td><asp:TextBox ID="txtvalor" class="mercado" runat="server">0</asp:TextBox></td>
                      <td><asp:TextBox ID="datepicker2" class="mercado" runat="server"></asp:TextBox></td>
                      <td><asp:TextBox ID="txtCambioClasif" class="mercado" runat="server"></asp:TextBox></td>
                      <td></td>
                    </tr>
                    <tr>
                      <td><strong>Estatus</strong></td>
                      <td></td>
                      <td></td>
                    </tr>
                    <tr>
                      <td colspan="4">
                          <asp:TextBox ID="txtestatus" class="mercado" runat="server" TextMode="MultiLine" Height="130px" Width="1100px"></asp:TextBox>
                          <asp:Button ID="btnautorizacion" runat="server" Text="Actualizar Registro" class="boton" Visible="True" CssClass="auto-style5" OnClick="btnautorizacion_Click" />
                      </td>
                    </tr>
                    <tr>
                      <td colspan="4"><strong></strong></td>
                    </tr>
                    <tr>
                      <td colspan="4">
                          <asp:GridView ID="GridView1" AutoPostBack="False" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="9pt" DataKeyNames="NoRegistro" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Bold="False" CssClass="auto-style7" Style="margin-top: 0" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>   
                        <asp:CommandField ButtonType="Button"  ShowSelectButton="True">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                        </asp:CommandField>
                        <asp:BoundField DataField="NoRegistro"  HeaderText="#Registro" SortExpression="Registro" ItemStyle-Width="5%">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                        <asp:BoundField DataField="Clasificacion"  HeaderText="Clasificacion" SortExpression="Clasificacion" ItemStyle-Width="5%">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                        <asp:BoundField DataField="Cliente"  HeaderText="Cliente" SortExpression="Cliente" ItemStyle-Width="5%">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                        <asp:BoundField DataField="Equipo"  HeaderText="Equipo" SortExpression="Equipo" ItemStyle-Width="10%">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                        
                        <asp:BoundField DataField="Valor" HeaderText="Valor USD" SortExpression="valor" ItemStyle-Width="10%">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                        
                        
                        <asp:BoundField DataField="FechaCierre"  HeaderText="Fecha Cierre" SortExpression="FechaCierre" ItemStyle-Width="5%" DataFormatString="{0:d}">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                        
                        <asp:BoundField DataField="C_Clasificacion"  HeaderText="Cambio" SortExpression="Cambio" ItemStyle-Width="10%">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Estatus"  HeaderText="Estatus" SortExpression="Estatus" ItemStyle-Width="30%">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        
                    </Columns>

                    <EditRowStyle BackColor="#999999" HorizontalAlign="Center" VerticalAlign="Middle"  />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerSettings Position="TopAndBottom" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" Height="60px" ForeColor="#333333"/>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                      </td>
                    </tr>
                </table>
            </div>

            </section>

        
        <asp:Label ID="lblrol" runat="server" class="logo" Font-Bold="True" ForeColor="White" Text="rol" Visible="False"></asp:Label>
            <asp:Label ID="lblidarea" runat="server" Text="area" Font-Bold="True" ForeColor="White" Visible="False" class="logo"></asp:Label>
          
        
   <div>
        <asp:SqlDataSource ID="comercial" runat="server" ConnectionString="<%$ ConnectionStrings:ComercialCS %>" SelectCommand="SELECT * FROM [Funnel]"></asp:SqlDataSource>
    </div>
    </form>
 
    
</body>
</html>