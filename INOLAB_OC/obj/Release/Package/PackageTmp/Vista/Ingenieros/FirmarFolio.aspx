<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirmarFolio.aspx.cs" Inherits="INOLAB_OC.FirmarFolio" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link rel="stylesheet" href="../../CSS/EstiloVista.css" />
    <link rel="stylesheet" href="../../CSS/EncabezadoFirmaFolio.css" />
    <link rel="stylesheet" href="../../CSS/drop.css"  />

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
                window.location.href = "/Vista/Sesion.aspx";
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
            $("#datepicker").datepicker();
        });
    </script>

    <script type="text/javascript">
        function startFirma() {
            var canvas = document.getElementById("signature-pad");
            canvas.width  = window.innerWidth;
            canvas.height = window.innerHeight;
            canvas.height = canvas.height - 170;
            
            var signaturePad = new SignaturePad(canvas, {
                minWidth: 3,
                maxWidth: 6,
              backgroundColor: 'rgba(255, 255, 255, 0)',
              penColor: 'rgb(35, 0, 255)'
            });

            var saveButton = document.getElementById('save');
            var cancelButton = document.getElementById('clear');
            var salirButton = document.getElementById('salir');

            saveButton.addEventListener('click', function (event) {
                var sourceImage = new Image();
                sourceImage.src = canvas.toDataURL();
                var canvas1 = document.createElement("canvas");
                canvas1.width = 106;
                canvas1.height = 19;
                var ctx = canvas1.getContext('2d');
                ctx.drawImage(sourceImage, 0, 0, 106, 19);
                
                var data = canvas.toDataURL();
                var button = document.getElementById("hidebutton");
                var label = document.getElementById("hidValue");
                label.value = data;
                event.returnValue = false;
                button.click();                
            },false);

            cancelButton.addEventListener('click', function (event) {
                signaturePad.clear();
                event.returnValue = false;
            }, false);

            salirButton.addEventListener('click', function (event) {
                signaturePad.clear();
                event.returnValue = false;
            }, false);
        }

        function go(clicked) {            
            if (clicked == "regresar") {
                window.location.href = "./VistaPrevia.aspx";
                return false;
            }   
        }
    </script>
   
    <style type="text/css">
        .auto-style1 {
            width: 98%;
            max-width: 10000px;
            margin: auto;
            height: 69px;
        }
        .auto-style6 {
            height: 48px;
        }
        .auto-style7 {
            padding-top: 10px;
            left: 0px;
            top: 0px;
        }
    </style>
</head>

<body style="overflow:auto;" onload="window.history.forward();">
    <form id="form1" runat="server">    
     <header class="header2" runat="server" id="headerid">
        <div id="headerone" class="auto-style1" runat="server">
            <div class="logo" style="height: 67px"><img src="../../Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
                <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo" ></asp:Label>
                <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo"></asp:Label>
                <nav>
                                     
                        <button id="Btn_Atras" type="reset" class="dropbtn" onclick="go('atras')">Atras</button>
                    
                                     
                        <button id="Btn_Salir" type="reset" class="dropbtn" onclick="go('salir')">Salir</button>
                    
                </nav>                              
        </div>
    </header>

    <section class="auto-style7" id="sectionreport" runat="server">
        <div id="reportdiv" runat="server" class="reportclass">
            <asp:ScriptManager runat="server"></asp:ScriptManager>        
            <rsweb:ReportViewer Visible="false" ID="ReportViewer1" runat="server" ProcessingMode="Remote" Width="100%"></rsweb:ReportViewer>
        </div>
    </section>

    <section id="firma" runat="server" style="display:none">
        <div id="divfirma" runat="server">
            <div id="areafirma" runat="server">
                <canvas id="signature-pad" class="signature-pad" width="100%"></canvas>            
            </div>
            <div id="buttonsfirma" runat="server" style="text-align:center">
                <div id="nombrecli">    
                    <table>
                        <tr>
                            <td class="auto-style6" colspan="2">
                                <asp:Label ID="labelcli" Text="Nombre:" Visible="false" runat="server"></asp:Label>
                                <asp:TextBox Id="textboxnombre" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>

               
                    <button id="salir" runat="server" class="dropbtn" onclick="go('regresar')">Salir</button> 
                
                
                    <button  id="clear" runat="server"  class="dropbtn">Limpiar</button> 
                
                
                    <button  id="save" runat="server"  class="dropbtn">Guardar</button> 
                
            </div>
        </div>    
        <div style="display: none;">
            <input id="hidValue" type="hidden" runat="server" />
               <asp:Button runat="server" id="hidebutton" OnClick="Almacenar_la_firma_Click" />
        </div>
    </section>
            
        <footer runat="server" id="footerid" class="footercl">
            <div runat="server" id="firmabutton" class="footerbtn" >
                <h3 style="text-align:center; color:white;">Datos Guardados correctamente!</h3>
                <asp:Button runat="server" Text="Continuar..." BorderStyle="None" style="float:unset; text-decoration:underline;" ID="firmarbtn" OnClick="Mostrar_pantalla_para_firmar_documento_Click"  />
            </div>
        </footer>
    </form>
</body>
</html>