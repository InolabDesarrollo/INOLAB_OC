<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActualizarAccion.aspx.cs" Inherits="INOLAB_OC.Vista.Ingenieros.ActualizarAccion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Actualizar Acción</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <link rel="stylesheet" href="../../CSS/EncabezadoFirmaFolio.css" />
    <link rel="stylesheet" href="../../CSS/ActualizarAccion.css" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" />

    <script src="https://cdn.jsdelivr.net/npm/signature_pad@2.3.2/dist/signature_pad.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

    <script type="text/javascript">
        function go(clicked)
        {            
            if (clicked == "salir")
            {
                window.location.href = "/Sesion.aspx"; 
                return false;
            }   
            if (clicked == "atras") {
                window.location.href = "./VistaPrevia.aspx";
                return false;
            }
        }
    </script>

     <script>    
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
             dateFormat: 'yy-mm-dd',

             firstDay: 1,
             isRTL: false,
             showMonthAfterYear: false,
             yearSuffix: ''
         };

         $.datepicker.setDefaults($.datepicker.regional['es']);

         $(function () {
             $("#TxtBox_actualizar_fecha_accion").datepicker();
         });
     </script>
</head>

<body style="overflow:auto;" onload="window.history.forward();">

    <header id="Contenedor_logo">
        <div>
             <img id="logo" src="../../Imagenes/LOGO_Blanco_Lineas.png" alt="Logo de mi empresa"/>
        </div>         
   </header>

    <form id="form_actualizar_accion" runat="server">
    <div>
 <section   runat="server" >
    <div   id="section_actualizar_accion">             
        <table class="auto-style5" id="tabla_actualizar_accion">
            <tr>
                <td colspan="2">
                    <div class="container">
                        <asp:Label ID="Lbl_actualizar_accion" runat="server" Text="Editar acción realizada" font-size="25px"  ></asp:Label>
                    </div>
                    
                </td>
            </tr>
            <tr>
                <td  colspan="2">
                    <asp:Label ID="lbl_fecha_actualizar_accion" runat="server" Text="Fecha:" ></asp:Label>
                    <asp:TextBox   ID="TxtBox_actualizar_fecha_accion" runat="server" autocomplete="off" AutoCompleteType="Disabled" ></asp:TextBox>
                </td>                           
            </tr>
            <tr>
                <td  colspan="2">
                    <asp:Label ID="Lbl_actualizar_horas_dedicadas" runat="server" Text="Horas Dedicadas:" ></asp:Label>
                    <asp:TextBox   ID="TxtBox_actualizar_horas_dedicadas" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>                  
                </td>
            </tr>
            <tr>
                <td  colspan="2">
                    <asp:Label ID="Lbl_Actualizar_Nueva_Accion" runat="server" Text="Acciones Realizadas:" CssClass="lbl-nueva-accion"></asp:Label>
                    <asp:TextBox CssClass ="txtbox_actualizar_accion" ID="TxtBox_Actualizar_Accion" runat="server" Columns="2" MaxLength="240" AutoCompleteType="Disabled"></asp:TextBox>
                </td>    
            </tr>
            <tr>
                <td  colspan="2">
                    <asp:Button runat="server" Text="Cancelar" BorderStyle="None" style="float:right;" ID="Btn_Cancelar" CssClass ="botones" OnClick="Cancelar_Click" />
                    <asp:Button runat="server" Text="Actualizar" BorderStyle="None" style="float:right;" ID="Btn_Actualizar_Accion" CssClass ="botones"  OnClick="Actualizar_Click"/>                        
                </td>    
            </tr>
        </table>
        </div>
  </section> 
    </div>
</form>

</body>
</html>