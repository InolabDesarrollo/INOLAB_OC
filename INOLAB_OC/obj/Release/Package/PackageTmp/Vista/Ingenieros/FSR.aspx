<%@ Page Language="C#" AutoEventWireup="true" Inherits="FSR" Codebehind="FSR.aspx.cs" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>FSR</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="../../CSS/EstiloVista.css" />
    <link rel="stylesheet" href="../../CSS/EncabezadoFSR.css" />
    <link rel="stylesheet" href="../../CSS/drop.css" /> 
    

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css"/>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    
    <script>
        function go(clicked)
        {            
            if (clicked == "salir")
            {
                window.location.href = "/Vista/Sesion.aspx";
            return false;
            }   
            if (clicked == "atras") {
                window.location.href = "./ServiciosAsignados.aspx";
                return false;
            }
            if (clicked == "next") {
                window.location.href = "./DetalleFSR.aspx";
                return false;
            }
        }
    </script>

    <script>
        function SubirDatos()
        {            
            var marca = document.getElementById("txtmarca").value;
            var modelo = document.getElementById("txtmodelo").value;
            var noserie = document.getElementById("txtnoserie").value;
            var descripcion = document.getElementById("txtdescripcion").value;
            var id = document.getElementById("txtid").value;


            var t1problema = document.getElementById("idproblema");
            var t1servicio = document.getElementById("idservicio");
            var t1contrato = document.getElementById("idcontrato");

            var tproblema = t1problema.options[t1problema.selectedIndex].value;
            var tservicio = t1servicio.options[t1servicio.selectedIndex].value;
            var tcontrato = t1contrato.options[t1contrato.selectedIndex].value;

            var cliente1 = document.getElementById("txtfolio").value;
            var telefono1 = document.getElementById("txttelfax").value;
            var direccion1 = document.getElementById("txtdireccion").value;
            var localidad1 = document.getElementById("txtlocalidad").value;
            var depto1 = document.getElementById("txtdepto").value;
            var responsable1 = document.getElementById("txtnresponsable").value;
            var reportado1 = document.getElementById("txtreportadopor").value;
            var email1 = document.getElementById("txtemail").value;

            document.getElementById("marcaHF").value = marca;
            document.getElementById("modeloHF").value = modelo;
            document.getElementById("noserieHF").value = noserie;
            document.getElementById("descripcionHF").value = descripcion;
            document.getElementById("idHF").value = id;

            document.getElementById("tproblemaHF").value = tproblema;
            document.getElementById("tservicioHF").value = tservicio;
            document.getElementById("tcontratoHF").value = tcontrato;

            document.getElementById("clienteHF").value = cliente1;
            document.getElementById("TelefonoHF").value = telefono1;
            document.getElementById("direccionHF").value = direccion1;
            document.getElementById("localidadHF").value = localidad1;
            document.getElementById("deptoHF").value =depto1;
            document.getElementById("responsableHF").value = responsable1;
            document.getElementById("reportadoHF").value = reportado1;
            document.getElementById("emailHF").value = email1;
        }
    </script>

    <style>.hidden { display: none; }</style>

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
            dateFormat: 'yy-mm-dd',
            
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
            $("#Fecha_inicio_servicio").datepicker();
        });
        $(function () {
            $("#datepicker2").datepicker();
        });
        $(function () {
            $("#datepicker3").datepicker();
        });
    </script>

    <style type="text/css">
        .auto-style1 {
            width: 98%;
            max-width: 10000px;
            margin: auto;
            height: 69px;
        }
        .auto-style4 {
            text-decoration: underline;
           font-weight:bold;
        }
        .auto-style5 {
            width: 100%;
        }
        .auto-style6 {
            height: 48px;
        }
        .auto-style7 {
           font-weight:bold;
           float:left;
        }
        .auto-style8 {
           font-weight:bold;
           float:left;
           color:red;
        }
    </style>
</head>

<body onload="window.history.forward();">
    <form id="form1" runat="server" autocomplete="off">
     <header class="header2">
        <div id="headerone" class="auto-style1" runat="server">
            <div class="logo" style="height: 67px"><img src="../../Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
                <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo"  ></asp:Label>
                <asp:Label ID="Nombre_de_usuario" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo" ></asp:Label>
                <asp:Label ID="Estatus_de_folio_servicio" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" Visible="false" class="logo" ></asp:Label>
                <nav>
                                        
                        <button type="reset" class="dropbtn" id="Btn_Atras" onclick="go('atras')">Atras</button>
                    
                                      
                        <button type="reset" class="dropbtn" id="Btn_Salir" onclick="go('salir')">Salir</button>
                    
                </nav>                              
         </div>
    </header>

                <section class="contenido3" id="sectionreport" runat="server" style="display:none;">
            <div id="reportdiv" runat="server" class="reportclass">
                <asp:ScriptManager runat="server"></asp:ScriptManager>        
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" Width="100%"></rsweb:ReportViewer>
            </div>
        </section>

    <section  class="contenido2" >       
        <div class="drop" id="cuerpo" runat="server">            
            <div class="form-style-2-heading">
                <asp:Label ID="titulo" runat="server">Información de FSR</asp:Label>
            </div>
                    
            <table class="auto-style5">
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label18" runat="server" Text="Datos del Cliente" CssClass="auto-style4"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6">
                        <asp:Label ID="Label2" runat="server" Text="Cliente/Empresa:" ></asp:Label> <br />
                        <asp:TextBox ID="txtfolio" runat="server"  CssClass="Txt_Datos_Folio" ></asp:TextBox>
                    </td>
                    <td class="auto-style6">
                        <asp:Label ID="Label6" runat="server" Text="Tel. Ext.:"></asp:Label> <br /> 
                        <asp:TextBox ID="txttelfax" runat="server"  CssClass="Txt_Datos_Folio" ></asp:TextBox>
                    </td>
                    <td class="auto-style6">
                        <asp:Label ID="Label7" runat="server" Text="Dirección:"></asp:Label> <br />
                        <asp:TextBox ID="txtdireccion" runat="server"  CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Localidad:"></asp:Label> <br />
                        <asp:TextBox ID="txtlocalidad" runat="server" CssClass="Txt_Datos_Folio" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Departamento:"></asp:Label> <br />
                        <asp:TextBox ID="txtdepto" runat="server" CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Nombre de Responsable:"></asp:Label> <br />
                        <asp:TextBox ID="txtnresponsable" runat="server" CssClass="Txt_Datos_Folio" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Reportado por:"></asp:Label> <br />
                        <asp:TextBox ID="txtreportadopor" runat="server" CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="E-mail:"></asp:Label> <br />
                        <asp:TextBox ID="txtemail" runat="server" CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>    
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" >
                        <asp:Label ID="Label19" runat="server" Text="Datos del Equipo" CssClass="auto-style4"></asp:Label>            
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Descripción"></asp:Label><br />
                        <asp:TextBox ID="txtdescripcion" runat="server" CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Marca:"></asp:Label> <br />
                        <asp:TextBox ID="txtmarca" runat="server" CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Modelo:"></asp:Label> <br />
                        <asp:TextBox ID="txtmodelo" runat="server" CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="No. de Serie"></asp:Label><br />
                        <asp:TextBox ID="txtnoserie" runat="server" CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="ID:"></asp:Label> <br />
                        <asp:TextBox ID="txtid" runat="server" CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="Tipo de Problema:"></asp:Label> <br />
                        <asp:DropDownList ID="idproblema" runat="server" Enabled="True" CssClass="Txt_Datos_Folio">
                                            <asp:ListItem Value="1">Neumático</asp:ListItem>
                                            <asp:ListItem Value="2">Electrónico</asp:ListItem>
                                            <asp:ListItem Value="3">Periféricos</asp:ListItem>
                                            <asp:ListItem Value="4">Operación</asp:ListItem>
                                            <asp:ListItem Value="5">Computadora</asp:ListItem>
                                            <asp:ListItem Value="6">Mecánico</asp:ListItem>
                                            <asp:ListItem Value="7">N/A</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="Tipo de Servicio:"></asp:Label> <br />
                        <asp:DropDownList ID="idservicio" runat="server" Enabled="True" CssClass="Txt_Datos_Folio">
                                            <asp:ListItem Value="1">Desinstalación</asp:ListItem>
                                            <asp:ListItem Value="2">Instalacíon</asp:ListItem>
                                            <asp:ListItem Value="3">Mant. Prev.</asp:ListItem>
                                            <asp:ListItem Value="4">Mant. Correctivo</asp:ListItem>
                                            <asp:ListItem Value="5">Calif. de Instalación</asp:ListItem>
                                            <asp:ListItem Value="6">Calif. de Operación</asp:ListItem>
                                            <asp:ListItem Value="7">Calif. de Funcionamiento</asp:ListItem>
                                            <asp:ListItem Value="8">Revisión</asp:ListItem>
                                            <asp:ListItem Value="9">Levantamiento</asp:ListItem>
                                            <asp:ListItem Value="10">Capacitación</asp:ListItem>
                                            <asp:ListIteM Value="11">Cotización / S.V.</asp:ListIteM>
                                            <asp:ListItem Value="12">SOP. Aplicaciones</asp:ListItem>
                                            <asp:ListItem Value="13">Calibración</asp:ListItem>
                                            <asp:ListItem Value="14">OTRO</asp:ListItem>
                                            <asp:ListItem Value="15">Eval. Periódica de desempeño.</asp:ListItem>
                                            <asp:ListItem Value="16">Verificación</asp:ListItem>
                                            <asp:ListItem Value="17">Calif. de Diseño</asp:ListItem>
                                            <asp:ListItem Value="18">Certificación OQ3</asp:ListItem>
                        </asp:DropDownList>
                     </td>
                     <td>
                        <asp:Label ID="Label22" runat="server" Text="Tipo de Contrato:" Width="180px"></asp:Label><br />
                        <asp:DropDownList ID="idcontrato" runat="server" Enabled="true" CssClass="Txt_Datos_Folio">
                                            <asp:ListItem Value="1">Equipo en Garantía</asp:ListItem>
                                            <asp:ListItem Value="2">Contrato Classic</asp:ListItem>
                                            <asp:ListItem Value="3">Contrato Plus</asp:ListItem>
                                            <asp:ListItem Value="4">Contrato Advanced</asp:ListItem>
                                            <asp:ListItem Value="5">Contrato Senior</asp:ListItem>
                                            <asp:ListItem Value="6">Garantia de Serv.</asp:ListItem>
                                            <asp:ListItem Value="7">Servicio Puntual</asp:ListItem>
                                            <asp:ListItem Value="8">Otro</asp:ListItem>
                        </asp:DropDownList>                    
                     </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label26" runat="server" Text="Detalle del Servicio" CssClass="auto-style4"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Fecha de servicio:"></asp:Label><br />
                        <asp:TextBox ID="datepicker" runat="server" Enabled="False" ReadOnly="True"  CssClass="Txt_Datos_Folio"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label25" runat="server" Text="Hora:"></asp:Label><br/>
                        <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" Enabled="False">
                                    <asp:ListItem>07:00</asp:ListItem>
                                    <asp:ListItem>07:30</asp:ListItem>
                                    <asp:ListItem>08:00</asp:ListItem>
                                    <asp:ListItem>08:30</asp:ListItem>
                                    <asp:ListItem>09:00</asp:ListItem>
                                    <asp:ListItem>09:30</asp:ListItem>
                                    <asp:ListItem>10:00</asp:ListItem>
                                    <asp:ListItem>10:30</asp:ListItem>
                                    <asp:ListItem>11:00</asp:ListItem>
                                    <asp:ListItem>11:30</asp:ListItem>
                                    <asp:ListItem>12:00</asp:ListItem>
                                    <asp:ListItem>12:30</asp:ListItem>
                                    <asp:ListItem>13:00</asp:ListItem>
                                    <asp:ListItem>13:30</asp:ListItem>
                                    <asp:ListItem>14:00</asp:ListItem>
                                    <asp:ListItem>14:30</asp:ListItem>
                                    <asp:ListItem>15:00</asp:ListItem>
                                    <asp:ListItem>15:30</asp:ListItem>
                                    <asp:ListItem>16:00</asp:ListItem>
                                    <asp:ListItem>16:30</asp:ListItem>
                                    <asp:ListItem>17:00</asp:ListItem>
                                    <asp:ListItem>17:30</asp:ListItem>
                                    <asp:ListItem>18:00</asp:ListItem>
                                    <asp:ListItem>18:30</asp:ListItem>
                                    <asp:ListItem>19:00</asp:ListItem>
                                    <asp:ListItem>19:30</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label24" runat="server" Text="Ingeniero de Servicio:"></asp:Label> <br />
                        <asp:DropDownList ID="cmding" runat="server" AutoPostBack="True" DataSourceID="Ingeniero" DataTextField="NombreI" DataValueField="IdUsuario" Enabled="False" CssClass="Txt_Datos_Folio"></asp:DropDownList>
                        <asp:SqlDataSource ID="Ingeniero" runat="server" ConnectionString="<%$ ConnectionStrings:CSServicio %>" SelectCommand="SELECT IdUsuario, Nombre + ' ' + Apellidos AS NombreI FROM Usuarios WHERE (IdArea = 6) ORDER BY Nombre"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label27" runat="server" Text="Status:"></asp:Label> <br />
                        <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="True" DataSourceID="Estatusfolio" DataTextField="Descripcion" DataValueField="IdStatus" Enabled="False"></asp:DropDownList>
                        <asp:SqlDataSource ID="Estatusfolio" runat="server" ConnectionString="<%$ ConnectionStrings:CSServicio %>" SelectCommand="SELECT * FROM [F_Status]"></asp:SqlDataSource>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="Falla Reportada:"></asp:Label>
                        <asp:TextBox ID="txtfalla" runat="server" TextMode="MultiLine" Rows="4" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button runat="server" Text="Actualizar Fechas" BorderStyle="None" style="float:unset; background-color:teal;" Visible="false" ID="Btn_actualizar_fechas" OnClick="Actualizar_fechaYhora_Servicio_Click" />
                    </td>
                    <td>
                        <asp:Button runat="server" Text="Actualizar Acciones" BorderStyle="None" style="float:unset; background-color:teal;" Visible="false" ID="Btn_agregar_acciones" OnClick="Actualizar_Acciones_Click" />
                    </td>
                    <td>
                        <asp:Button runat="server" Text="Descargar Folio" BorderStyle="None" style="float:unset;" Visible="false" ID="btndescarga" OnClick="btndescarga_Click" color="b"/>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>                                
                                <asp:HiddenField id="marcaHF" runat="server" />
                                <asp:HiddenField id="modeloHF" runat="server" />
                                <asp:HiddenField id="noserieHF" runat="server" />
                                <asp:HiddenField id="descripcionHF" runat="server" />
                                <asp:HiddenField id="idHF" runat="server" />
                                                                              
                                <asp:HiddenField id="tproblemaHF" runat="server" />
                                <asp:HiddenField id="tservicioHF" runat="server" />
                                <asp:HiddenField id="tcontratoHF" runat="server" />

                                <asp:HiddenField id="clienteHF" runat="server" />
                                <asp:HiddenField id="TelefonoHF" runat="server" />
                                <asp:HiddenField id="emailHF" runat="server" />
                                <asp:HiddenField id="reportadoHF" runat="server" />
                                <asp:HiddenField id="direccionHF" runat="server" />
                                <asp:HiddenField id="localidadHF" runat="server" />
                                <asp:HiddenField id="deptoHF" runat="server" />
                                <asp:HiddenField id="responsableHF" runat="server" />
                                
                                <div runat="server" id="Button1id" class="footerbtn" >
                                    <asp:Button runat="server" Text="Realizar servicio" BorderStyle="None" style="float:unset;" ID="Btn_Estatus_Servicio" OnClientClick="SubirDatos();" OnClick="Actualizar_Datos_Servicio_Click"  />
                                    <asp:Button ID="btnadjuntar" runat="server" OnClick="Adjuntar_Archivos_Click" Text="Adjuntar Archivos" />
                                </div>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </section>

        <!-- Seccion de formulario de fechas que iran al folio-->
        <section class="centrar2"  id="floatsection" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;"  id="sectionf">
                <table class="auto-style5">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label16" runat="server" Text="Fechas de inicio de folio" CssClass="auto-style4"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Label ID="fechaini" runat="server" Text="Fecha en la que se inicia el folio:"></asp:Label>
                            <asp:TextBox ID="Fecha_inicio_servicio" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>                           
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Label ID="horasini" runat="server" Text="Hora en la que se inicia el folio:"></asp:Label>
                            <asp:DropDownList ID="horainicial" runat="server" Enabled="True" Width="100px">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label17" runat="server" Text=":"></asp:Label>
                            <asp:DropDownList ID="mininicial" runat="server" Enabled="True" Width="100px">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                            </asp:DropDownList>                        
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Button runat="server" Text="Iniciar Folio" BorderStyle="None" style="float:right;" ID="Addbutton" OnClick="Iniciar_Folio_De_Servicio_Click" />
                        </td>    
                    </tr>
                </table>

                </div>
        </section>

                <!-- Seccion de formulario de fechas que iran al folio-->
        <section class="centrar2"  id="Actfechas" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;"  id="sectionf2">
                <table class="auto-style5">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label33" runat="server" Text="Fecha de inicio de servicio (Actual):" CssClass="auto-style7"></asp:Label>
                            <asp:Label ID="lbl_fechaYhora_inicio_servicio" runat="server" Text="Ejemplo 1" CssClass="auto-style8"></asp:Label>
                        </td>
                    </tr>                    
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label34" runat="server" Text="Fecha de fin de servicio (Actual):" CssClass="auto-style7"></asp:Label>
                            <asp:Label ID="Lbl_fin_de_servicio" runat="server" Text="Ejemplo 2" CssClass="auto-style8"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label28" runat="server" Text="Fecha de Inicio de Servicio:" CssClass="auto-style4"></asp:Label>                        
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:TextBox ID="datepicker2" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>                           
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Label ID="horasfin" runat="server" Text="Hora de Inicio de Servicio:"></asp:Label>
                            <asp:DropDownList ID="Hora_inicio_folio" runat="server" Enabled="True" Width="100px">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>            
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label29" runat="server" Text=":"></asp:Label>
                            <asp:DropDownList ID="Minuto_inicio_folio" runat="server" Enabled="True" Width="100px">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                            </asp:DropDownList>                        
                        </td>
                    </tr>
                                        <tr>
                        <td colspan="2">
                            <asp:Label ID="Label30" runat="server" Text="Fecha de Fin de Servicio:" CssClass="auto-style4"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:TextBox ID="datepicker3" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>                           
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Label ID="Label31" runat="server" Text="Hora de Fin de Servicio:"></asp:Label>
                            <asp:DropDownList ID="Hora_fin_folio" runat="server" Enabled="True" Width="100px">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>            
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label32" runat="server" Text=":"></asp:Label>
                            <asp:DropDownList ID="Minuto_fin_folio" runat="server" Enabled="True" Width="100px">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                            </asp:DropDownList>                        
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Button runat="server" Text="Finalizar" BorderStyle="None" style="float:right;" ID="Button2" OnClick="Finalizar_Click" />
                        </td>    
                    </tr>
                </table>

                </div>
        </section>

        <asp:Label ID="lblrol" runat="server" class="logo" Font-Bold="True" ForeColor="White" Text="rol" Visible="False"></asp:Label>
        <asp:Label ID="lblidarea" runat="server" Text="area" Font-Bold="True" ForeColor="White" Visible="False" class="logo"></asp:Label>
    </form>
</body>
</html>
