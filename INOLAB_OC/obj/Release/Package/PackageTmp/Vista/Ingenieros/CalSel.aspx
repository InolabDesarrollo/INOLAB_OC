<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalSel.aspx.cs" Inherits="INOLAB_OC.CalSel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="../../CSS/stilos.css" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" id="bootstrap-css" />
    
    <!------ Include the above in your HEAD tag ---------->
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" id="Link1" />

    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <!-- Include the above in your HEAD tag -->
    <style type="text/css">
        .auto-style1 {
            display:block;
            margin:auto;
            height:150px;
            width: 450px;
        }
        .auto-style2 {
            width: 450px;
            display:block;
            margin:auto;
            height:80px;
        }
    </style>
</head>

<body onload="window.history.forward();">
    <div class="main" >   
        <div class="container">
            <div class="middle">
                <div id="login">
                    <form id="Form1" runat="server">
                        <img src="../../Imagenes/LOGO_Blanco_.png" class="auto-style1"/><img src="../../Imagenes/slogan.png"  class="auto-style2"/>
                        <br/><br/>
                        <fieldset class="clearfix">
                            <div style="margin-left:200px">
                                <!-- Aqui va el combobox para eleccion de area-->
                                <asp:DropDownList ID="ddlfiltro" runat="server" Width="375px" Height="40px" OnSelectedIndexChanged="Seleccionar_el_area_SelectedIndexChanged">
                                <asp:ListItem>Seleccion de Area</asp:ListItem>
                                <asp:ListItem>Analítica</asp:ListItem>
                                <asp:ListItem>Fisicoquímico</asp:ListItem>
                                <asp:ListItem>Temperatura</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button class="boton" ID="btnSesion" runat="server" Width="300px" Text="Ir a Calendario" OnClick="Iniciar_sesion_en_calendario_area_Click" ></asp:Button><br />
                                <asp:Button  runat="server" ID="Btn_volver_a_inicio_sesion" Text="Volver a Inicio de Sesion " Width="300px" OnClick="Volder_a_inicio_de_sesion_Click" />
                            </div>
                        </fieldset>
                        <div class="clearfix">
                        </div>
                    </form>
                <div class="clearfix"></div>
                </div> <!-- end login -->
            </div>
        </div>
    </div>
</body>
</html>