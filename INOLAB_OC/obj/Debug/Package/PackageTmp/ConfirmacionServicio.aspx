<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmacionServicio.aspx.cs" Inherits="INOLAB_OC.ConfirmacionServicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="CSS/EstiloVistaConfirmacion.css" />
    <link rel="stylesheet" href="CSS/EncabezadoConfirmacion.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" />

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
    </script>

    <style type="text/css">
        .auto-style2 {
            width: 238px;
        }
        .auto-style5 {
            height: 17px;
        }
        .auto-style7 {
            width: 238px;
            height: 53px;
        }
        .auto-style14 {
            width: 130px;
        }
        .auto-style17 {
            width: 34px;
        }
        .auto-style18 {
            width: 130px;
            height: 18px;
        }
        .auto-style21 {
            width: 34px;
            height: 18px;
        }
        .auto-style22 {
            height: 18px;
        }
        .auto-style23 {
            width: 130px;
            height: 17px;
        }
        .auto-style26 {
            width: 34px;
            height: 17px;
        }
        .auto-style27 {
            width: 89px;
        }
        .auto-style28 {
            height: 18px;
            width: 89px;
        }
        .auto-style29 {
            height: 17px;
            width: 89px;
        }
        .auto-style30 {
            width: 27px;
        }
        .auto-style31 {
            height: 18px;
            width: 27px;
        }
        .auto-style32 {
            height: 17px;
            width: 27px;
        }
        </style>
         
</head>
<body>
    <form id="form1" runat="server">
     <header class="header2">
        <div class="wrapper">
        <div class="logo"><img src="Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
            <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo" Width="65px" Visible="False" ></asp:Label>
            <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo" Visible="False"></asp:Label>
            <nav> </nav>                
        </div>
        
    </header>
        <section class="contenido2" >             
                <div class="form-style-2" />
                <div class="form-style-2-heading">Confirmación de Servicio</div>
                    <table style="width: 100%;">
                        <tr>
                            <td class="auto-style14">
                                <asp:Label ID="lblFechaservicio" runat="server" Text="Fecha de Servicio"></asp:Label>
                            </td>
                            <td class="auto-style30">
                                <asp:TextBox ID="txtfechafsr" runat="server"></asp:TextBox>
                            </td>
                            <td class="auto-style27">
                                <asp:Button ID="btnconfirmar" runat="server" Text="Confirmar Fecha de Servicio" OnClick="btnconfirmar_Click" />
                            </td>
                            <td class="auto-style17">&nbsp;</td>    
                            <td class="auto-style2">
                                <asp:Button ID="btbproponer" runat="server" OnClick="btbproponer_Click"  Text="Proponer Fecha de Servicio"  />
                            </td>
                        </tr>                       
                        <tr>
                            <td class="auto-style18"></td>
                            <td class="auto-style31"></td>
                            <td class="auto-style28">&nbsp;</td>
                            <td class="auto-style21">&nbsp;</td>
                            <td class="auto-style22">
                                <asp:Label ID="lblnuevafecha" runat="server" Text="Fecha:" Visible="False"></asp:Label>
                                <asp:TextBox ID="datepicker" runat="server" Visible="False" Wrap="False"></asp:TextBox>
                                <br/>
                                <asp:Label ID="lblhora" runat="server" Text="Hora:" Visible="False"></asp:Label>
                                <asp:DropDownList ID="dplhora" runat="server" Visible="False">
                                    <asp:ListItem>8:00</asp:ListItem>
                                    <asp:ListItem>9:00</asp:ListItem>
                                    <asp:ListItem>10:00</asp:ListItem>
                                    <asp:ListItem>11:00</asp:ListItem>
                                    <asp:ListItem>12:00</asp:ListItem>
                                    <asp:ListItem>13:00</asp:ListItem>
                                    <asp:ListItem>14:00</asp:ListItem>
                                    <asp:ListItem>15:00</asp:ListItem>
                                    <asp:ListItem>16:00</asp:ListItem>
                                    <asp:ListItem>17:00</asp:ListItem>
                                    <asp:ListItem>18:00</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td ></td>
                            <td ></td>
                            <td ></td>
                            <td ></td>
                            <td >
                                <asp:Button ID="btnEnviarRespuesta" runat="server" Text="Enviar fecha de Servicio" Visible="False" OnClick="btnEnviarRespuesta_Click" />
                            </td>
                        </tr>
                    </table>
        </section>

        <section class="contenido wrapper">
             <div style="overflow-x:auto;width:100%; height:500px">
            <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="100%" Font-Size="10pt" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Bold="False" CssClass="auto-style7">
                    <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Equipo" HeaderText="Equipo" SortExpression="Equipo" Visible="true" ItemStyle-Width="20%">
                <ItemStyle HorizontalAlign="Justify"/>
                </asp:BoundField>
                <asp:BoundField DataField="Marca"  HeaderText="Marca" SortExpression="Marca"  ItemStyle-Width="16%">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Modelo"  HeaderText="Modelo" SortExpression="Modelo" ItemStyle-Width="16%">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                </asp:BoundField>
                <asp:BoundField DataField="NoSerie"  HeaderText="No de Serie" SortExpression="NoSerie" ItemStyle-Width="16%">
                <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="IdEquipo_C"  HeaderText="ID Equipo" SortExpression="IdEquipo_C" ItemStyle-Width="16%">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TipoServicio" HeaderText="Tipo de Servicio" SortExpression="TipoServicio" ItemStyle-Width="16%">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:BoundField>
            </Columns>

            <EditRowStyle BackColor="#2461BF" HorizontalAlign="Center" VerticalAlign="Middle"  />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#000099"  Font-Bold="True" ForeColor="White"  />
            <PagerSettings Position="TopAndBottom" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" Height="60px"/>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>          
        <asp:SqlDataSource ID="DSBrowser" runat="server" ConnectionString="<%$ ConnectionStrings:BrowserCS %>" SelectCommand="SELECT * FROM [V_FSR]"></asp:SqlDataSource>
            </div>
    </section>

        <asp:Label ID="lblrol" runat="server" class="logo" Font-Bold="True" ForeColor="White" Text="rol" Visible="False"></asp:Label>
        <asp:Label ID="lblidarea" runat="server" Text="area" Font-Bold="True" ForeColor="White" Visible="False" class="logo"></asp:Label>
    </form>   
</body>
</html>