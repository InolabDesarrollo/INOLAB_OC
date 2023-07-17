<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registros1.aspx.cs" Inherits="INOLAB_OC.Registros1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="CSS/EstiloVista.css" />
    <link rel="stylesheet" href="CSS/EncabezadoComun.css" />
    <link rel="stylesheet" href="http://localhost:50455/code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <link rel='stylesheet' type='text/css' href='Styles/StaticHeader.css' />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="FormatoFecha.js"></script>
    <script src="Scripts/jquery-1.7.1.js"></script>
            <script language="javascript" >
                $(document).ready(function () {
                    var gridHeader = $('#<%=GridView1.ClientID%>').clone(true); // Here Clone Copy of Gridview with style
                    $(gridHeader).find("tr:gt(0)").remove(); // Here remove all rows except first row (header row)
                    $('#<%=GridView1.ClientID%> tr th').each(function (i) {
                        // Here Set Width of each th from gridview to new table(clone table) th 
                        $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
                    });
                    $("#GHead").append(gridHeader);
                    $('#GHead').css('position', 'absolute');
                    $('#GHead').css('top', $('#<%=GridView1.ClientID%>').offset().top);

                });
            </script>

    <style type="text/css">
        .auto-style1 {
            height: 21px;
        }
        .auto-style2 {
            width: 100%;
        }
        .auto-style3 {
            height: 21px;
            width: 232px;
        }
        .auto-style4 {
            width: 232px;
        }
        .auto-style5 {
            height: 21px;
            width: 523px;
        }
        .auto-style6 {
            width: 523px;
        }
        .auto-style7 {
            margin-bottom: 0;
        }
        </style>

    </head>
<body>
      <form id="form1" runat="server">
          <header class="header2">
        <div class="wrapper">
        <div class="logo"><img src="Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
            <asp:Label ID="label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White" class="logo" Width="65px"></asp:Label>
            <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo"></asp:Label>
            <nav>           
                <!--<asp:Button ID="btnNuevoOC_Equipo" runat="server" Text="Nueva OC Equipo" class="boton"   Visible="True" OnClick="Button3_Click" UseSubmitBehavior="False"/>
                <asp:Button ID="btnNuevoOC_Servicio" runat="server" Text="Nueva OC Servicio" class="boton"   Visible="True" OnClick="Button4_Click" UseSubmitBehavior="False"/>
                -->
                <asp:Button ID="btnOC_Equipo" runat="server" Text="Registros OC Equipo" class="boton" OnClick="btnOC_Equipo_Click"  Visible="True" UseSubmitBehavior="False" />
                <asp:Button ID="Button1" runat="server" Text="Salir" class="boton" OnClick="Button1_Click" UseSubmitBehavior="False" />
            </nav>                

        </div>
    </header>
     
    <section class="contenido2">
            <div class="etiqueta">
                <asp:Label ID="lblcontador" runat="server" Font-Size="14pt" Text="Registros OC - Servicio" Font-Bold="True" ></asp:Label>
               
            </div>
    </section>
          

    <section class="contenido wrapper">
        <table class="auto-style2">
            <tr>
                <td class="auto-style3">
         <asp:Button ID="btnvautorizar" runat="server"  Text="Autorizar Seleccionados" class="otroboton" OnClick="btnvautorizar_Click" UseSubmitBehavior="False" />
                </td>
                <td class="auto-style5">&nbsp;<strong> Filtros de Búsqueda:</strong> <asp:DropDownList ID="cmbFiltro" runat="server" class="mercado" OnSelectedIndexChanged="cmbFiltro_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Todos</asp:ListItem>
                    <asp:ListItem>Autorizados</asp:ListItem>
                    <asp:ListItem>Sin Autorizar</asp:ListItem>
                    <asp:ListItem>Cliente</asp:ListItem>
                    <asp:ListItem>OC</asp:ListItem>
                    <asp:ListItem>Asesor Comercial</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtbusqueda" runat="server" class="mercado" Visible="False"  Width="105px" Enabled="true"></asp:TextBox>
                    <asp:Button ID="btnbuscar" runat="server" OnClick="btnbuscar_Click" Text="Buscar" Visible="False" />
                </td>
                <td class="auto-style1">
                    <asp:Label ID="mensaje" runat="server" Font-Bold="True" Text="Registros: "></asp:Label>
                    <asp:Label ID="lbln_registros" runat="server" Font-Bold="True" Text="0"></asp:Label>
                </td>
            </tr>            
            <tr>
                <td class="auto-style4">
            <asp:Label ID="lblrol" runat="server" Text="rol" Font-Bold="True" ForeColor="White" Visible="False" class="logo"></asp:Label>
            <asp:Label ID="lblidarea" runat="server" Text="area" Font-Bold="True" ForeColor="White" Visible="False" class="logo"></asp:Label>
                </td>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
                
<%--                        <div style="width:100%;">
                    <div id="GHead" style="width:100%;""></div> --%>                    <%--<div style="height:500px; width:100%; overflow-x:auto">--%>
             <div style="overflow-x:auto;width:100%; height:500px">
       
            <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="100%" Font-Size="9pt"  DataKeyNames="IdOC_S" CellPadding="4" OnRowDataBound="GridView1_RowDataBound" ForeColor="#333333" GridLines="None" BorderStyle="Ridge" Font-Bold="False" CssClass="auto-style7">
                    <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:TemplateField HeaderText="Admon" ItemStyle-Width="5%" >
                     <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Enabled="False" Checked='<%# Convert.ToBoolean(Eval("Admon_V")) %>' Font-Bold="True" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comercial" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox3" runat="server" Enabled="False" Checked='<%# Convert.ToBoolean(Eval("Comercial_V")) %>'/>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Servicio" ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox2" runat="server" Enabled="False" Checked='<%# Convert.ToBoolean(Eval("Serv_V")) %>'/>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="IdOC_S"  ItemStyle-Width="4%" HeaderText="No." InsertVisible="False" ReadOnly="True" SortExpression="IdOC_S" Visible="True" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="IdOC_S" DataNavigateUrlFormatString="~/Detalle Registro1.aspx?valor={0}" visible="false" DataTextField="IdOC_S" HeaderText="No." Target="_blank" SortExpression="IdOC_S" >
                
                <ItemStyle Width="6%" HorizontalAlign="Center" Font-Bold="True"  />
                </asp:HyperLinkField>
                
                
                <asp:HyperLinkField DataNavigateUrlFields="IdOC_S" DataNavigateUrlFormatString="~/Detalle Registro1.aspx?valor={0}" ItemStyle-Width="19%" DataTextField="Cliente" HeaderText="Cliente" SortExpression="Cliente" Target="_blank" >
<ItemStyle Width="19%"></ItemStyle>
                </asp:HyperLinkField>
                
                
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" Visible="false" ItemStyle-Width="19%">
                <ItemStyle HorizontalAlign="Justify"  />
                </asp:BoundField>
                <asp:BoundField DataField="Importe"  HeaderText="Importe" DataFormatString="{0:C}" SortExpression="Importe"  ItemStyle-Width="8%">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Moneda"  HeaderText="Moneda" SortExpression="Moneda" ItemStyle-Width="4%">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                </asp:BoundField>
                <asp:BoundField DataField="OC"  HeaderText="OC" SortExpression="OC" ItemStyle-Width="8%">
<ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Servicio_T"  HeaderText="Servicio" SortExpression="Servicio_T" ItemStyle-Width="5%">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FechaRecepcion" HeaderText="Fecha Recepcion" SortExpression="FechaRecepcion" DataFormatString="{0:d}" ItemStyle-Width="8%">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:BoundField>
               <%-- <asp:BoundField DataField="FechaOC_C"  HeaderText="Fecha OC Cliente" SortExpression="FechaOC_C" DataFormatString="{0:d}" ItemStyle-Width="7%" Visible="False">

                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                
<ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:BoundField>--%>

                
                <asp:BoundField DataField="Nota"  HeaderText="Nota" SortExpression="Nota" ItemStyle-Width="20%">
<ItemStyle  HorizontalAlign="Justify"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Asesor"  HeaderText="Usuario" SortExpression="Asesor" ItemStyle-Width="7%" Visible="True">
<ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                </asp:BoundField>
               <%-- <asp:HyperLinkField DataNavigateUrlFields="Adjunto1"  DataNavigateUrlFormatString="~/Adjuntos/{0}" DataTextField="Adjunto1" HeaderText="Archivo1" Target="_blank" ItemStyle-Width="2%" Visible="False">
                              
                              
<ItemStyle Width="2%"></ItemStyle>
                              
                              
                              </asp:HyperLinkField>

                
                <asp:HyperLinkField DataNavigateUrlFields="Adjunto2"  DataNavigateUrlFormatString="~/Adjuntos/{0}" DataTextField="Adjunto2" HeaderText="Archivo2" Target="_blank" ItemStyle-Width="2%" Visible="False">
                    
<ItemStyle Width="4%"></ItemStyle>
                    
                </asp:HyperLinkField>
                <asp:HyperLinkField DataNavigateUrlFields="Adjunto3" DataNavigateUrlFormatString="~/Adjuntos/{0}" DataTextField="Adjunto3" HeaderText="Archivo3" Target="_blank" ItemStyle-Width="2%" Visible="False">
                    
<ItemStyle Width="4%"></ItemStyle>
                    
                </asp:HyperLinkField>

                
                <asp:HyperLinkField DataNavigateUrlFields="Adjunto4" DataNavigateUrlFormatString="~/Adjuntos/{0}" DataTextField="Adjunto4" HeaderText="Archivo4" Target="_blank" ItemStyle-Width="2%" Visible="False">
                    
<ItemStyle Width="4%"></ItemStyle>
                    
                </asp:HyperLinkField>
                <asp:HyperLinkField DataNavigateUrlFields="Adjunto5" DataNavigateUrlFormatString="~/Adjuntos/{0}" DataTextField="Adjunto5" HeaderText="Archivo5" Target="_blank" ItemStyle-Width="2%" NavigateUrl="~/Adjuntos/{0}" Visible="False">
                    
<ItemStyle Width="4%"></ItemStyle>
                    
                </asp:HyperLinkField>--%>
                
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
               
        <asp:SqlDataSource ID="DSBrowser" runat="server" ConnectionString="<%$ ConnectionStrings:BrowserCS %>" SelectCommand="SELECT * FROM [ServicioOC]"></asp:SqlDataSource>
            </div>
        <%-- </div>--%>
    </section>
          
      </form>
</body>
</html>

