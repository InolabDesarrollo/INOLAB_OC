<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cotizacion.aspx.cs" Inherits="INOLAB_OC.Cotizacion" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


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
        .auto-style2 {
            width: 95%;
            max-width: 100%;
            margin: auto;
            height: 75px;
        }
        .auto-style4 {
            position: fixed;
            height: 75px;
            z-index: 1;
            background: #0264d6;
            left: 0px;
            top: 0px;
        }
        </style>

    </head>
<body>
    <form id="form1" runat="server">

     <header class="auto-style4">
        <div class="auto-style2">
        <div class="logo" style="height: 70px"><img src="Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
            <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo" Width="65px" Height="68px" ></asp:Label>
            <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo" Height="69px"></asp:Label>
            
            <nav>
                <asp:Button ID="btnRegistroFunnel" runat="server" Text="Funnel" class="boton" visible="True"  Target="_blank" OnClick="btnInforme_A_Click" />     
            </nav>                

        </div>
        

    </header>
        <section class="contenido2" >           
                <div class="form-style-2 " >
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="95%" Height="650px" ZoomMode="PageWidth" ShowPageNavigationControls="True" ShowZoomControl="True"></rsweb:ReportViewer>
                     
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>   
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
        
    </div>
    </form>
 
    
</body>
</html>
