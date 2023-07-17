<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Informacion.aspx.cs" Inherits="INOLAB_OC.Informacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="../../CSS/EstiloVista.css" />
    <link rel="stylesheet" href="../../CSS/EncabezadoInformacion.css" />
    <link rel="stylesheet" href="../../CSS/drop.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    
    <script type="text/javascript">
        function redireccionar() {
            window.locationf = "FSR.aspx";
        }
        setTimeout("redireccionar()", 5000); //tiempo expresado en milisegundos
    </script>

    <style type="text/css">
        .auto-style7 {
            margin-bottom: 0;
        }
        .auto-style9 {
            width: 144px;
        }
        .auto-style11 {
            width: 360px;
        }
        .auto-style12 {
            width: 117px;
        }
        </style>

    </head>
<body>
      <form id="form1" runat="server">
            <header class="header2">
                <div class="wrapper">
                    <div class="logo"><img src="../../Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>         
                    <asp:Label ID="label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White" class="logo" Width="65px"></asp:Label>
                    <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo"></asp:Label>
                    <nav>
                        <div class="dropdown">
                        </div>
                        <div class="dropdown">
                        </div>
                        <div class="dropdown">
                        </div>
                        <div class="dropdown">
                        </div>
                        <div class="dropdown">
                        </div>
                    </nav>                
                </div>
            </header>

            <section class="contenido3" id="sectionreport" runat="server" style="display:none;">
                <div id="reportdiv" runat="server" class="reportclass">
                    <asp:ScriptManager runat="server"></asp:ScriptManager>        
                    
                </div>
            </section>
            <section class="contenido2">
                <div class="etiqueta">
                    <asp:Label ID="lblcontador" runat="server" Font-Size="14pt" Text="Servicios " Font-Bold="True" ></asp:Label>
                </div>
                    <table style="width: 100%; height:35px;">
                        <tr>
                            <td class="auto-style9">Estatus del Servicio</td>
                            <td class="auto-style12"><asp:DropDownList ID="ddlfiltro" runat="server" Width="177px" AutoPostBack="True" OnSelectedIndexChanged="ddlfiltro_SelectedIndexChanged1">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>Asignado</asp:ListItem>
                                <asp:ListItem>En Proceso</asp:ListItem>
                                <asp:ListItem>Finalizado</asp:ListItem>
                                <asp:ListItem>Todos</asp:ListItem>
                                </asp:DropDownList></td>
                            <td class="auto-style11">
                                <strong>
                                <asp:Label ID="Label2" runat="server" Text="Registros:   "></asp:Label></strong><asp:Label ID="contador" runat="server" Text="0"></asp:Label></td>
                        </tr>                    
                    </table>
            </section>

            <section class="contenido wrapper">
                <div style="overflow-x:auto;width:100%; height:500px">
                    <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="100%" Font-Size="9pt"  DataKeyNames="Folio" CellPadding="4"  ForeColor="#333333" GridLines="None" BorderStyle="Ridge" Font-Bold="False" CssClass="auto-style7" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:ButtonField HeaderText="FSR" DataTextField="Folio" SortExpression="Folio" Visible="true" ButtonType="Link" ItemStyle-Width="5%"  CommandName="Select">
                        <ItemStyle  HorizontalAlign="Center"></ItemStyle></asp:ButtonField>

                        <asp:ButtonField HeaderText="Estatus" DataTextField="Estatus" SortExpression="Estatus" Visible="true" ItemStyle-Width="5%" >
                        <ItemStyle  HorizontalAlign="Center"></ItemStyle></asp:ButtonField>

                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" Visible="True"  ItemStyle-Width="10%" >
                        <ItemStyle HorizontalAlign="Center"  /></asp:BoundField>
                
                        <asp:BoundField DataField="TipoServicio"  HeaderText="Tipo de Servicio"  SortExpression="TipoServicio"  ItemStyle-Width="20%">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>

                        <asp:BoundField DataField="FechaServicio" HeaderText="FechaServicio" SortExpression="FechaServicio" DataFormatString="{0:d}" ItemStyle-Width="10%">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Equipo"  HeaderText="Equipo" SortExpression="Equipo" ItemStyle-Width="20%">
                        <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                
                        <asp:BoundField DataField="Marca"  HeaderText="Marca" SortExpression="Marca" ItemStyle-Width="10%">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Ingeniero"  HeaderText="Ingeniero"  SortExpression="Ingeniero"  ItemStyle-Width="10%">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>
                    </Columns>

                    <EditRowStyle BackColor="#7C6F57" HorizontalAlign="Center" VerticalAlign="Middle"  />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="False" />
                    <HeaderStyle BackColor="#1C5E55"  Font-Bold="True" ForeColor="White"  />
                    <PagerSettings Position="TopAndBottom" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" Height="60px"/>
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
               
                    <asp:SqlDataSource ID="DSBrowser" runat="server" ConnectionString="<%$ ConnectionStrings:CSServicio %>" SelectCommand="SELECT * FROM [V_FSR]"></asp:SqlDataSource>
                </div>
            </section> 
      </form>
</body>
</html>
