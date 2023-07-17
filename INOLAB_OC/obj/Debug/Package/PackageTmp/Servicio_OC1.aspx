<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Servicio_OC1.aspx.cs" Inherits="INOLAB_OC.Servicio_OC1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="CSS/EstiloVista.css" />
    <link rel="stylesheet" href="CSS/EncabezadoComun.css" />
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
        .auto-style1 {
            width: 664px;
        }
        .auto-style2 {
            position: fixed;
            height: 75px;
            z-index: 1;
            background: #0264d6;
            left: 176px;
            top: 15px;
        }
    </style>

    </head>
<body>
    <form id="form1" runat="server">

     <header class="auto-style2">
        <div class="wrapper">
        <div class="logo"><img src="Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
            <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo" Width="65px" ></asp:Label>
            <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo"></asp:Label>
            
            <nav>
                <!--<asp:Button ID="btnNuevoOC_Equipo" runat="server" Text="Nueva OC Equipo" class="boton" visible="True" OnClick="Button5_Click" />
                -->
                <asp:Button ID="btnOC_Servicio" runat="server" Text="Registros OC Servicio" class="boton" OnClick="Button2_Click" visible="True" />
                <asp:Button ID="btnOC_Equipo" runat="server" Text="Registros OC Equipo" class="boton"  visible="True"  OnClick="Button4_Click1" /> 
                <asp:Button ID="btnInforme_A" runat="server" Text="Estadisticas" class="boton" visible="True" OnClick="btnInforme_A_Click" Target="_blank" />     
                <asp:Button ID="Button1" runat="server" Text="Salir" class="boton" OnClick="Button1_Click" />
            </nav>                

        </div>
        

    </header>
        <section class="contenido2" >           
                <div class="form-style-2 " >
                <div class="form-style-2-heading">Registro Orden de Compra - Servicio</div>
                <table style="table-layout: fixed; width: 1100px">
                    <tr>
                        <td class="auto-style1">
                            <label><span>Empresa</span><asp:DropDownList ID="cmbEmpresa" runat="server" class="mercado" DataSourceID="SqlDataSource1" DataTextField="Empresa" DataValueField="idCliente" Height="29px" Width="372px" OnSelectedIndexChanged="cmbEmpresa_SelectedIndexChanged" AutoPostBack="True"> </asp:DropDownList>
                                <asp:Button ID="btnNuevaEmpresa" runat="server" Text="Nuevo"  class="otroboton" OnClick="btnNuevaEmpresa_Click" />         
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BrowserCS %>" SelectCommand="SELECT * FROM [Clientes] ORDER BY [Empresa]"></asp:SqlDataSource>                                     
                                <asp:Label ID="lblempresa" runat="server" Text="Label" Visible="False"></asp:Label>                                     
                            </label>
                        </td>
                        <td><label for="field6"><span>Nota</span><asp:TextBox ID="txtNota" runat="server" class="nota" TextMode="MultiLine" Rows="4"  Width="400px" OnTextChanged="txtNota_TextChanged2"></asp:TextBox></label></td>
                    </tr>
                    <tr>
                        <td class="auto-style1"><label for="field1"><span>Fecha Recepción</span><asp:TextBox ID="datepicker" runat="server" tooltip="Fecha cuando se recibe la OC" class="mercado" OnTextChanged="datepicker_TextChanged"></asp:TextBox></label></td>
                        <td><asp:CheckBox ID="chValidar" runat="server" Text="Autorizar Información" Font-Bold="True" OnCheckedChanged="chValidar_CheckedChanged" /></td>
                    </tr>
                    <tr>
                        <td class="auto-style1"><label for="field3"><span>OC</span><asp:TextBox ID="txtOC" runat="server" class="input-field"></asp:TextBox></label></td>
                        <td><asp:FileUpload ID="FileUpload1" runat="server" class="input-field" AutoPostBack="True" ></asp:FileUpload><asp:Label ID="adjunto1" runat="server" Text="0" Visible="False"></asp:Label><asp:Label ID="lblcontador" runat="server" Text="1" Visible="false"></asp:Label><br /></td>
                    </tr>
                    <tr>
                        <td class="auto-style1"><label for="field4"><span>Importe</span><asp:TextBox ID="txtImporte" runat="server" class="mercado" Text="0" OnTextChanged="txtImporte_TextChanged" ></asp:TextBox></label></td>
                        <td><asp:FileUpload ID="FileUpload2" runat="server" class="input-field" Visible="True"/><asp:Label ID="adjunto2" runat="server" Text="0" Visible="False"></asp:Label><br /></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <label><span>Moneda</span>
                                <asp:DropDownList ID="cmbMoneda" runat="server" class="mercado" OnSelectedIndexChanged="cmbMoneda_SelectedIndexChanged">
                                    <asp:ListItem Value=""></asp:ListItem>
                                    <asp:ListItem>MXN</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>                    
                                </asp:DropDownList>
                            </label>
                        </td>
                        <td><asp:FileUpload ID="FileUpload3" runat="server" class="input-field" Visible="True" /><asp:Label ID="adjunto3" runat="server" Text="0" Visible="False"></asp:Label><br /></td>
                        </tr>
                        <tr>
                            <td class="auto-style1"><label for="field5"><span>Fecha OC Cliente</span><asp:TextBox ID="datepicker2" runat="server" tooltip="Fecha de la OC" class="mercado"></asp:TextBox></label></td>
                            <td><asp:FileUpload ID="FileUpload4" runat="server" class="input-field" Visible="True" /><asp:Label ID="adjunto4" runat="server" Text="0" Visible="False"></asp:Label><br /></td>
                        </tr>
                        <tr>
                            <td class="auto-style1"><asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <label><span>Tipo Servicio</span><asp:DropDownList ID="cmbservicio" runat="server" class="mercado" AutoPostBack="True" OnSelectedIndexChanged="cmbservicio_SelectedIndexChanged">
                                            <asp:ListItem Value=""></asp:ListItem>
                                                <asp:ListItem>Analisis</asp:ListItem>
                                                <asp:ListItem>Contrato</asp:ListItem>                    
                                                <asp:ListItem>Servicio</asp:ListItem>     
                                                <asp:ListItem>Refaccion</asp:ListItem>     
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbtipo" runat="server" class="mercado" AutoPostBack="False" Visible="False">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem>Advanced</asp:ListItem>
                    <asp:ListItem>Classic</asp:ListItem>                    
                    <asp:ListItem>Plus</asp:ListItem>     
                    <asp:ListItem>Senior</asp:ListItem>     
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbTiempo" runat="server" class="mercado" AutoPostBack="False" Visible="False">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem>12 Meses</asp:ListItem>
                    <asp:ListItem>24 Meses</asp:ListItem>                    
                    <asp:ListItem>36 Meses</asp:ListItem>     
                    <asp:ListItem>48 Meses</asp:ListItem>     
                    </asp:DropDownList>
                     </ContentTemplate>
                    </asp:UpdatePanel>
                    </label></td>
                            
                            <td><asp:FileUpload ID="FileUpload5" runat="server" class="input-field" Visible="True" /><asp:Label ID="adjunto5" runat="server" Text="0" Visible="False"></asp:Label><br /><br /></td>
                           
                        </tr>
                    </table>
            </div>
            <script>
                $('#<%=cmbEmpresa.ClientID%>').chosen();
            </script>
            </section>
        <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
             <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" class="otroboton" />         
        <asp:Label ID="idregistro" runat="server" Text="registro" Visible="False"></asp:Label>
        
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button"  visible="false"/>
        
        <asp:Label ID="lblrol" runat="server" class="logo" Font-Bold="True" ForeColor="White" Text="rol" Visible="False"></asp:Label>
            <asp:Label ID="lblidarea" runat="server" Text="area" Font-Bold="True" ForeColor="White" Visible="False" class="logo"></asp:Label>
            
    </form>
    
</body>
</html>
