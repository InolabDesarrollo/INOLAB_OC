<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalles Registro Equipo1.aspx.cs" Inherits="INOLAB_OC.Detalles_Registro_Equipo1" %>

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
    <%--<script src="Scripts/jquery-1.7.1.js"></script>
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
            </script>--%>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 105px;
        }
        .auto-style3 {
            width: 471px;
        }
        .auto-style4 {
            width: 125px;
        }
        .auto-style5 {
            width: 105px;
            height: 26px;
        }
        .auto-style6 {
            width: 471px;
            height: 26px;
        }
        .auto-style7 {
            width: 125px;
            height: 26px;
        }
        .auto-style8 {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <header class="header2">
        <div class="wrapper">
        <div class="logo"><img src="Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>

            <nav> 
            </nav>                
            
        </div>
    </header>
          <section class="contenido2">
            <div class="etiqueta">
                <asp:Label ID="lblcontador" runat="server" Font-Size="14pt" Text="Detalles del Registro" Font-Bold="True" ></asp:Label>
               
            </div>
    </section>
    <section class="contenido wrapper">
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Cliente:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtCliente" class="mercado" runat="server" ReadOnly="True" Width="451px"></asp:TextBox>
                </td>
                <td class="auto-style4">Fecha Recepcion:</td>
                <td>
                    <asp:TextBox ID="txtFecha_R" class="mercado" ReadOnly="True" DataFormatString="{0:d}" runat="server" Width="171px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Equipo:</td>
                <td class="auto-style6">
                    <asp:TextBox ID="txtServicio" TextMode="MultiLine" class="nota" ReadOnly="True" runat="server" Height="54px" Width="366px"></asp:TextBox>
                </td>
                <td class="auto-style7">Fecha OC Cliente:</td>
                <td class="auto-style8">
                    <asp:TextBox ID="txtFecha_OC" class="mercado" ReadOnly="True" runat="server" Width="167px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Importe:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtImporte"  class="mercado" ReadOnly="True" runat="server" Width="82px"></asp:TextBox>
                    <asp:TextBox ID="txtMoneda" class="mercado" ReadOnly="True" runat="server" Width="62px"></asp:TextBox>
                </td>
                <td class="auto-style4">Anexo 1:</td>
                <td>
                    <asp:LinkButton ID="adjunto1" runat="server"  Font-Size="Small" OnClick="adjunto1_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style4">Anexo 2:</td>
                <td>
                    <asp:LinkButton ID="adjunto2" runat="server"  Font-Size="Small" OnClick="adjunto2_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">OC:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtOC" class="mercado" ReadOnly="True" Width="451px" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style4">Anexo 3:</td>
                <td>
                    <asp:LinkButton ID="adjunto3" runat="server"  Font-Size="Small" OnClick="adjunto3_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style4">Anexo 4:</td>
                <td>
                    <asp:LinkButton ID="adjunto4" runat="server"  Font-Size="Small" OnClick="adjunto4_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Registrado por:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtAsesor" class="mercado" ReadOnly="True" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style4">Anexo 5:</td>
                <td>
                    <asp:LinkButton ID="adjunto5" runat="server"  Font-Size="Small" OnClick="adjunto5_Click"></asp:LinkButton>
                    <asp:Label ID="lblID" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Nota:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtNota" class="nota" TextMode="MultiLine" ReadOnly="True" Rows="4" runat="server" Width="453px" Height="122px"></asp:TextBox>
                </td>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Fecha del Registro:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtFecha_Creacion" class="mercado" ReadOnly="True" runat="server" Width="181px"></asp:TextBox>
                </td>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </section>
    </form>
</body>
</html>