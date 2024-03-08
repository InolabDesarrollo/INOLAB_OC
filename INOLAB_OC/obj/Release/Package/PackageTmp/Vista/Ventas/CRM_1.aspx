<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRM_1.aspx.cs" Inherits="INOLAB_OC.CRM_1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Ventas</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="../../CSS/EstiloVista.css" />
    <link rel="stylesheet" href="../../CSS/EncabezadoCRM_1.css" />
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
        .auto-style5 {
            left: 0px;
            top: 0px;
        }
        </style>

    </head>
<body>
    <form id="form1" runat="server">

     <header class="header2">
        
        <div class="logo" style="height: 70px"><img src="../../Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
           <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo" Width="65px" Height="68px" ></asp:Label>
           <asp:Label ID="Lbl_nombre_usuario" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo" Height="69px"></asp:Label>
           <asp:Label ID="Lbl_id_usuario" runat="server" Text="id" Font-Bold="True" ForeColor="White" class="logo" Height="69px" Visible="false"></asp:Label>                
            
            <input type="checkbox" id="check" />
                <label for="check" class="mostrar-menu">
                    &#8801
                </label>

            <nap class="menu">
                <asp:Button ID="Btn_plan_de_trabajo" runat="server" Text="Plan de Trabajo" class="boton"  visible="True" OnClick="Btn_Plan_De_Trabajo_Click" width="200px" Height="25px"/> 
                <asp:Button ID="Btn_registro_funnel_ventas" runat="server" Text="Registro Funnel" class="boton" visible="True"  Target="_blank" OnClick="Btn_Registro_Funnel_Click" width="200px" Height="25px"/>     
                <asp:Button ID="Btn_reporte_cotizaciones" runat="server" Text="Cotizaciones" class="boton" OnClick="Btn_Reporte_Cotizaciones_Click"  width="200px" Height="25px"/>
                
                <asp:Button ID="Btn_Salir" runat="server" Text="Salir" class="boton"   OnClick="Btn_Salir_Click" Height="25px"/>

                <label for="check" class="esconder-menu">
                        &#215
                </label>
            </nap>                
       
    </header>
        <section class="contenido2" >           
           <div class="form-style-2 " align="center" >
             <center style="height: 56px; width: 1363px">
                <!--Script manager control-->
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" class="auto-style3" ZoomMode="FullPage"  Width="100%" Height="100%"  SizeToReportContent="true" RightToCenter="YES" ></rsweb:ReportViewer >            
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
             </center>                   
           </div>
            
       </section>
        
        <asp:Label ID="lblrol" runat="server" class="logo" Font-Bold="True" ForeColor="White" Text="rol" Visible="False"></asp:Label>
        <asp:Label ID="lblidarea" runat="server" Text="area" Font-Bold="True" ForeColor="White" Visible="False" class="logo"></asp:Label>
          
        
        <p>
                <asp:Button ID="btnFunnelAsesores" runat="server" Text="Funnel General" class="boton"  visible="False"/> 
                </p>
    </form>
 
    
</body>
</html>
