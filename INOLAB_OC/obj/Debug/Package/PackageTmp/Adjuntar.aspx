<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Adjuntar.aspx.cs" Inherits="INOLAB_OC.Adjuntar" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="CSS/EstiloVista.css" />
    <link rel="stylesheet" href="CSS/EncabezadoComun.css" />
    <link rel="stylesheet" href="CSS/drop.css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

     <script type="text/javascript">
        function go(clicked)
        {            
            if (clicked == "salir")
            {
                window.location.href = "./Sesion.aspx";
                return false;
            }   
            if (clicked == "atras") {
                window.location.href = "./FSR.aspx";
                return false;
            }
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            color: #009933;
            font-size: large;
        }
        .auto-style2 {
            margin-top: 0;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <header class="header2">
        <div class="wrapper">
        <div class="logo"><img src="Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
            <nav>
                    <div class="dropdown">                    
                        <button type="reset" class="dropbtn" onclick="go('atras')">Atras</button>
                    </div>
                                    
                    <div class="dropdown">                    
                        <button type="reset" class="dropbtn" onclick="go('salir')">Salir</button>
                    </div>
                    
                </nav>                 
            
        </div>
    </header>
          <section class="contenido2">
            <div class="etiqueta">
                <asp:Label ID="lblcontador" runat="server" Font-Size="14pt" Text="Detalles del Registro" Font-Bold="True" ></asp:Label>
               
            </div>
    </section>
    <section class="contenido wrapper">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" CssClass="auto-style2" Width="324px"/>
            <asp:Button ID="btnguardar" runat="server" Text="Adjuntar Archivos" OnClick="btnguardar_Click" />
            <strong>
            <asp:Label ID="lblmensaje" runat="server" Text="Label" Visible="false" CssClass="auto-style1"></asp:Label>
            </strong>
              
        </div>
    </section>
    </form>
</body>
</html>
