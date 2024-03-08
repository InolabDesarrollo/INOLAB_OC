<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRM_2.aspx.cs" Inherits="INOLAB_OC.CRM_2" %>


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
                <asp:Button ID="btnRegistroFunnel" runat="server" Text="Registro Funnel" class="boton"  visible="True" OnClick="Ir_a_registro_funnel_Click"  /> 
                <asp:Button ID="btnInforme_A" runat="server" Text="Estadisticas" class="boton" visible="True"  Target="_blank" OnClick="Ir_a_grafica_funnel_Click"  />     
                <asp:Button ID="Button1" runat="server" Text="Cotizaciones" class="boton" OnClick="Ir_a_reporte_cotizaciones_Click"  />
                <asp:Button ID="Btn_VolverMenuPrincipal" runat="server" Text="Volver a menu principal" class="boton" OnClick="Btn_MenuPrincipal"   />
            
                <label for="check" class="esconder-menu">
                        &#215
                </label>
            
            </nav>                

    </header>
        <section class="contenido2" >           
                <div class="form-style-2 " >
                <div class="form-style-2-heading">Registro de Plan de Trabajo</div>
                <table class="auto-style3">
                    <tr>
                        <td class="chosen-rtl">
                            <label for="field1"><span>Tipo de Registro</span></label></td>
                        <td>
                            <label for="field1"><asp:DropDownList ID="ddlTipoRegistro" class="mercado" runat="server" OnSelectedIndexChanged="Seleccionar_tipo_de_registro_SelectedIndexChanged" AutoPostBack="True" style="height: 20px">
                                <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem>Llamada</asp:ListItem>
                                    <asp:ListItem>Visita</asp:ListItem>     
                            </asp:DropDownList>
                            <strong>
                            <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                            <asp:TextBox ID="datepicker" runat="server" tooltip="Fecha cuando se recibe la OC" class="mercado" ></asp:TextBox>
                            <asp:Label ID="lblREGISTRO" runat="server" Visible="false" Text="Fecha"></asp:Label>
                            </strong>
                            </label>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td >
                            <label for="field1">
                            <strong>
                            <asp:Label ID="lblHora" runat="server" Text="Hora"></asp:Label>
                            <asp:DropDownList ID="ddlhora" class="mercado" runat="server" OnSelectedIndexChanged="Seleccionar_tipo_de_registro_SelectedIndexChanged" AutoPostBack="True" style="height: 20px">
                                <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem>08:00</asp:ListItem>
                                    <asp:ListItem>08:30</asp:ListItem>
                                    <asp:ListItem>09:00</asp:ListItem>
                                    <asp:ListItem>09:30</asp:ListItem>
                                    <asp:ListItem>10:00</asp:ListItem>
                                    <asp:ListItem>10:30</asp:ListItem>
                                    <asp:ListItem>11:00</asp:ListItem>
                                    <asp:ListItem>11:30</asp:ListItem>
                                    <asp:ListItem>12:00</asp:ListItem>
                                    <asp:ListItem>12:30</asp:ListItem>
                                    <asp:ListItem>13:00</asp:ListItem>
                                    <asp:ListItem>13:30</asp:ListItem>
                                    <asp:ListItem>14:00</asp:ListItem>
                                    <asp:ListItem>14:30</asp:ListItem>
                                    <asp:ListItem>15:00</asp:ListItem>
                                    <asp:ListItem>15:30</asp:ListItem>
                                    <asp:ListItem>16:00</asp:ListItem>
                                    <asp:ListItem>16:30</asp:ListItem>
                                    <asp:ListItem>17:00</asp:ListItem>
                                    <asp:ListItem>17:30</asp:ListItem>
                                    <asp:ListItem>18:00</asp:ListItem>
                            </asp:DropDownList>
                            </strong>
                            </label>
                        </td>
                        <td rowspan="5">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <rsweb:ReportViewer ID="ReportViewer1" Width="130%" runat="server" ShowExportControls="False" ShowFindControls="False" ProcessingMode="Remote" ShowToolBar="False">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl"><label for="field1"><span>Cliente</span></label></td>
                        <td><label for="field1">
                            <asp:TextBox ID="txtcliente" class="mercado" runat="server" Width="435px"></asp:TextBox>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <strong>
                            <asp:Label ID="lblFecha1" runat="server" Text="Objetivo"></asp:Label>
                            </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtobjetivo" CssClass="mercado" runat="server" Height="32px" TextMode="MultiLine" Width="434px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="chosen-rtl">
                            <label for="field1"><span>Comentario</span></label></td>
                        <td>
                            <asp:TextBox ID="txtcomentario" CssClass="mercado" runat="server" Height="86px" TextMode="MultiLine" Width="435px"></asp:TextBox>
                        </td>
                        </tr>
                        <tr>
                            <td class="chosen-rtl">
             <asp:Button ID="btnUpdate" runat="server" Text="Actualizar" class="otroboton" Visible="false" OnClick="Actualizar_datos_del_registro_Click" />         
        
             <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="otroboton" OnClick="Guardar_nuevo_registro_Click" />         
        
                            </td>
                            <td>
             <asp:Button ID="btnClean" runat="server" Text="Limpiar Registros" class="otroboton" OnClick="Limpiar_datos_del_registro_Click"/>         
        
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                            <label for="field1">
                            <strong>
                            <asp:Label ID="lblFecha0" runat="server" Text="Filtro"></asp:Label>
                            </strong>
                                <asp:DropDownList ID="ddlTipofiltro" class="mercado" runat="server"  AutoPostBack="True" style="height: 20px" OnSelectedIndexChanged="ddlTipofiltro_SelectedIndexChanged">
                                <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem>Llamada</asp:ListItem>
                                    <asp:ListItem>Visita</asp:ListItem>     
                                    <asp:ListItem>Todo</asp:ListItem>    
                            </asp:DropDownList>
                            </label>
                            </td>
                            
                            <td class="auto-style7">
                                <asp:Label ID="Label12" runat="server" Text="Total de Registros:  " Font-Bold="True"></asp:Label>
                                <asp:Label ID="lblcontador" runat="server" Text="0" Font-Bold="True"></asp:Label>
                            </td>
                            
                            <td class="auto-style7">&nbsp;</td>
                            
                        </tr>
                        <tr>
                            <td  colspan="3">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoPostBack="False" CellPadding="4" CssClass="auto-style7" DataKeyNames="IdLlamada" Font-Bold="False" Font-Size="9pt" ForeColor="#333333" GridLines="None"  style="margin-top: 0" Width="100%" OnSelectedIndexChanged="Seleccionar_registro_SelectedIndexChanged">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Button" ShowSelectButton="True">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                                        </asp:CommandField>
                                        <asp:BoundField DataField="IdLlamada" HeaderText="#Registro" ItemStyle-Width="10%" SortExpression="IdLlamada">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fechallamada" DataFormatString="{0:d}" HeaderText="Fecha" ItemStyle-Width="10%" SortExpression="Fecha">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" ItemStyle-Width="10%" SortExpression="Cliente">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Objetivo" HeaderText="Objetivo" ItemStyle-Width="10%" SortExpression="Objetivo">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Comentario" HeaderText="Comentario" ItemStyle-Width="10%" SortExpression="Comentario">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" ItemStyle-Width="10%" SortExpression="Tipo">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerSettings Position="TopAndBottom" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="60px" />
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

     <%--       <script>
                $('#<%=cmbEmpresa.ClientID%>').chosen();
            </script>--%>
            </section>
        <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
        
        <asp:Label ID="lblrol" runat="server" class="logo" Font-Bold="True" ForeColor="White" Text="rol" Visible="False"></asp:Label>
            <asp:Label ID="lblidarea" runat="server" Text="area" Font-Bold="True" ForeColor="White" Visible="False" class="logo"></asp:Label>
          
        
   <div>
        <asp:SqlDataSource ID="comercial" runat="server" ConnectionString="<%$ ConnectionStrings:ComercialCS %>" SelectCommand="SELECT * FROM [Llamada_Vista]"></asp:SqlDataSource>
    </div>
    </form>
 
    
</body>
</html>
