<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sesion.aspx.cs" Inherits="INOLAB_OC.Sesion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="CSS/stilos.css" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" id="bootstrap-css" />
    <!------ Include the above in your HEAD tag ---------->
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css" id="Link1" />
    <link href="../CSS/stilos.css" rel="stylesheet" />

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
                        <img src="../Imagenes/LOGO_Blanco_.png" class="auto-style1"/><img src="../Imagenes/slogan.png"  class="auto-style2"/>
                        <br/><br/>
                        <fieldset class="clearfix">
                            <p><span class="fa fa-user" style="margin-right:0;"> </span>
                                <span style="width: 100%; margin-left:0">
                                    <asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuario" required></asp:TextBox> <!-- JS because of IE support; better: placeholder="Username" -->
                                </span>
                            </p>
                            <p><span class="fa fa-lock" style="margin-right:0;"></span>
                                <span style="width: 100%; margin-left:0">
                                <asp:TextBox ID="txtPass" runat="server" type="password" placeholder="Contraseña" required></asp:TextBox> <!-- JS because of IE support; better: placeholder="Password" -->
                                </span>
                            </p>
                            <div>
                                <span style="width:100%"; display: block; margin:auto;" >
                                <asp:Button class="boton" ID="btnSesion" runat="server" Text="Inicio de Sesión" OnClick="Inicio_De_Sesion_Click"   ></asp:Button></span>
                                <asp:Label runat="server" Text="Label" ID="lblip" Visible="False"></asp:Label>
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