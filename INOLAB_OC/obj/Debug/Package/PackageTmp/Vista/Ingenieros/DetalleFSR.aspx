<%@ Page Language="C#" AutoEventWireup="true" Inherits="DetalleFSR" EnableEventValidation="false" Codebehind="DetalleFSR.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="../../CSS/EstiloVista.css" />
    <link rel="stylesheet" href="../../CSS/EncabezadoDetalleFSR.css" />
    <link rel="stylesheet" href="../../CSS/drop.css" />
    
    
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
                window.location.href = "/Vista/Sesion.aspx";
                return false;
            }   
            if (clicked == "atras") {
                window.location.href = "./FSR.aspx";
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
            $("#Fecha_nueva_accion_realizada").datepicker();
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
            $("#datepicker1").datepicker();
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
            margin-left: 0;
        }
        .auto-style8 {
            width: 222px;
        }
        </style>
         

</head>

<body style="overflow:auto;" onload="window.history.forward();">
    <form id="form1" runat="server" autocomplete="off">

        <header class="header2">
            <div id="headerone" class="auto-style1" runat="server">
            <div class="logo" style="height: 67px"><img src="../../Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
                <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo" ></asp:Label>
                <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo"></asp:Label>
                
                <input type="checkbox" id="check" />
                <label for="check" class="mostrar-menu">
                    &#8801
                </label>
                
                <nav class="menu">
                        <asp:Button class="Btn_Header" runat="server" Text="Ir a Servicios Asignados" BorderStyle="None"   ID="Btn_ir_a_servicios_asignados" OnClick="Ir_a_servicios_asignados_Click"  />          
                        <button type="reset" class="Btn_Header" id="Btn_Atras" onclick="go('atras')">Atras</button>
                        <button type="reset" class="Btn_Header" id="Btn_Salir" onclick="go('salir')">Salir</button>
                        
                        <label for="check" class="esconder-menu">
                        &#215
                        </label>
                </nav>                              
            </div>
        </header>
        
        <section class="contenido2">
            <div class="drop" runat="server" id="contenone" >
                <div class="form-style-2-heading">
                    <asp:Label ID="titulo" runat="server">Detalle de FSR</asp:Label>
                </div>
                <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="100%" Font-Size="9pt" DataKeyNames="idFSRAccion" CellPadding="4"  ForeColor="#333333" GridLines="None" BorderStyle="Ridge" Font-Bold="False" CssClass="auto-style7" OnRowCommand="GridView_de_acciones_realizadas_en_folio_OnRowComand" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField HeaderText="id" DataField="idFSRAccion" SortExpression="idFSRAccion" Visible="true">
                        <ItemStyle Width="1%" ForeColor="#808080"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="FechaAccion"  HeaderText="Fecha de la Acción" SortExpression="FechaAccion" DataFormatString="{0:d}">
                        <ItemStyle  HorizontalAlign="Center" Width="12%"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="HorasAccion" HeaderText="Horas de la Acción" SortExpression="HorasAccion" Visible="True">
                        <ItemStyle HorizontalAlign="Center"  Width="12%"/></asp:BoundField>
                
                        <asp:BoundField DataField="AccionR"  HeaderText="Acción Realizada"  SortExpression="AccionR" >
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>

                        <asp:ButtonField HeaderText="Eliminar"  SortExpression="Eliminar" ButtonType="Link" CommandName="Borrar" Text="Eliminar" >
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%"/></asp:ButtonField>
                    </Columns>

                    <EditRowStyle BackColor="#7C6F57" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#464a49" ForeColor="White" Font-Bold="False" />
                    <HeaderStyle BackColor="#000000"  Font-Bold="True" ForeColor="White"  />
                    <PagerSettings Position="TopAndBottom" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" Height="20px"/>
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                
                <div id="botonnuevo" class="btnnuevo" >
                    <asp:Button runat="server" Text="Nueva Accion" BorderStyle="None" style="float:right" ID="Button4" OnClick="Agregar_nuevas_acciones_Click" />
                    
                </div>
                <div id="botonsa" class="btnnuevo" >
                    
                </div>
                    <table style="width: 100%;">
                        <tr>
                            <td class="auto-style8">
                                <asp:Label ID="proxs" runat="server" >Próximo Servicio</asp:Label>
                                <asp:TextBox ID="datepicker1" runat="server" CssClass="auto-style7"></asp:TextBox> <br /> <br />
                                <asp:Button ID="btnProxServicio" runat="server" Text="Agregar" Width="92px" AutoPostBack="True" OnClick="Agrendar_proximo_servicio_Click" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
        </section>
        
        <!--seccion para ingresar una nueva accion realizada -->
        <section class="seccion-nuevo-servicio"  id="seccion_nuevo_servicio" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;" id="sectionf">
                
                    <asp:ImageButton Visible="true" ID="closeimg" runat="server" ImageAlign="Right" ImageUrl="../../Imagenes/closeimg.png" Width="30px" Height="30px" OnClick="Cerrar_ventana_agregar_nueva_accion_Click" />
               

                <table class="auto-style5" id="tabla_nuevo_servicio">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lbl_nueva_accion_realizada" runat="server" Text="Nueva acción realizada" CssClass="auto-style4"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Label ID="lbl_fecha_nuevo_servicio" runat="server" Text="Fecha:" CssClass="lbl-nueva-accion"></asp:Label>
                            <asp:TextBox ID="Fecha_nueva_accion_realizada" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>                           
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Label ID="horasD" runat="server" Text="Horas Dedicadas:" CssClass="lbl-nueva-accion"></asp:Label>
                            <asp:TextBox ID="txthorasD" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                ControlToValidate="txthorasD" runat="server"
                                ErrorMessage="Solo se permiten números"
                                ValidationExpression="\d+">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Label ID="acciones" runat="server" Text="Acciones Realizadas:" CssClass="lbl-nueva-accion"></asp:Label>
                            <asp:TextBox ID="txtacciones" runat="server" Columns="2" MaxLength="240" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>    
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Button runat="server" Text="Agregar" BorderStyle="None" style="float:right;" ID="Addbutton" OnClick="Agregar_nueva_actividad_al_reporte_Click" />
                        </td>    
                    </tr>
                </table>

                </div>
        </section>

        <section class="centrar2"  id="observaciones" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;" id="sectionf1">
                <div class="buton" id="closebtndiv">
                    <asp:ImageButton Visible="true" ID="closebtn1" runat="server" ImageAlign="Right" ImageUrl="../../Imagenes/closeimg.png" Width="30px" Height="30px" OnClick="Cerrar_campo_observaciones_Click"  />
                </div>

                <table class="auto-style5">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" Text="Observaciones" CssClass="auto-style4"></asp:Label>
                        </td>
                    </tr>                
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:TextBox ID="txtobservaciones" runat="server" TextMode="MultiLine" Rows="7" MaxLength="240" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>    
                    </tr>            
                    <tr>
                        <td class="auto-style6" colspan="2">                            
                            <asp:CheckBox id="Envio_de_notificacion_de_eliminacion_de_accion" runat="server" Text=" Notificar al Asesor" style="float:left;" OnCheckedChanged="Verificar_si_se_envio_notificacion_a_usuario_CheckedChanged" />
                            <asp:Button runat="server" Text="Guardar" BorderStyle="None" style="float:right;" ID="btnguardar" OnClick="Actualizar_observaciones_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </section>

        <section class="centrar2"  id="FallaEncontrada" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;" id="sectionf4">
                <div class="buton" id="closebtndiv1">
                    <asp:ImageButton Visible="true" ID="ImageButton3" runat="server" ImageAlign="Right" ImageUrl="../../Imagenes/closeimg.png" Width="30px" Height="30px" OnClick="Cerrar_campo_fallas_encontradas_Click"  />
                </div>

                <table  class="auto-style5">
                    <tr>
                        <td colspan="2">
                                <asp:Label ID="Label3" runat="server" Text="Fallas Encontradas" CssClass="auto-style4"></asp:Label>
                        </td>
                    </tr>                   
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:TextBox ID="txtfallaencontrada" runat="server" TextMode="MultiLine" Rows="7" MaxLength="100" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>    
                    </tr>                
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Button runat="server" Text="Guardar" BorderStyle="None" style="float:right;" ID="Button2" OnClick="Actualizar_fallas_encontradas_Click"/>
                        </td>    
                    </tr>
                </table>
            </div>
        </section>
        <!-- Se implementa el formulario para la solicitud de refacciones-->  
        <section class="centrar2"  id="refacciones" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;" id="sectionf2">
                <div class="buton" id="closebtnref2">
                    <asp:ImageButton Visible="true" ID="ImageButton2" runat="server" ImageAlign="Right" ImageUrl="../../Imagenes/closeimg.png" Width="30px" Height="30px" OnClick="Cerrar_Ventana_Refacciones_Click" />
                </div>
                <div>
                <!--Aquí va la lista de refacciones que ya se han agregado con anterioridad -->
                    <div class="form-style-2-heading">
                        <asp:Label ID="Label4" runat="server">Detalle de Refacciones</asp:Label>
                    </div>
                        
                    <asp:Table ID="Table1" runat="server" Width="100%" GridLines="Vertical" BorderColor="#252932"> 
                    <asp:TableRow BackColor="#252932" ForeColor="White" HorizontalAlign="Center">
                    <asp:TableHeaderCell>N° de refacción</asp:TableHeaderCell>
                    <asp:TableHeaderCell>Cantidad</asp:TableHeaderCell>
                    </asp:TableRow>
                    </asp:Table> 
                            
                    <div id="botonnuevoRef" class="btnnuevoRef">
                        <asp:Button runat="server" Text="Nuevo" BorderStyle="None" style="float:right" ID="btnNuevoR" OnClick="Mostrar_nueva_refaccion_Click"/>
                    </div>
                </div>
            </div>
        </section>

        <section class="seccion-agregar-refaccion"  id="SECCION_AGREGAR_REFACCION" runat="server" style="display: none;">
            
                <div class="buton" id="closebtnref1">
                    <asp:ImageButton Visible="true" ID="ImageButton1" runat="server" ImageAlign="Right" ImageUrl="../../Imagenes/closeimg.png" Width="30px" Height="30px" OnClick="Cerrar_ventana_nueva_refaccion_Click" />
                </div>

                    <table class="tabla-agregar-refaccion">
                        <tr>
                            <td>
                                <asp:Label ID="AGREGAR_REFACCION_TITULO" runat="server" Text="Agregar refacción" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tabla-agregar-refaccion-celda">
                                <asp:Label ID="LBL_NUM_PARTES_REFACCION" runat="server" Text="N° de partes" ></asp:Label>
                                <asp:TextBox ID="txtbox_numero_de_partes" runat="server" autocomplete="off" AutoCompleteType="Disabled" ></asp:TextBox>
                            </td>
                        </tr>                
                        <tr>

                            <td class="tabla-agregar-refaccion-celda">
                                <asp:Label ID="LBL_CANTIDAD_REFACCION" runat="server" Text="Cantidad"   ></asp:Label>
                                <asp:TextBox ID="txtbox_cantidad_refaccion" runat="server" Columns="2"  autocomplete="off" AutoCompleteType="Disabled" CssClass="txbox-agregar-refaccion"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                    ControlToValidate="txtbox_cantidad_refaccion" runat="server"
                                    ErrorMessage="Solo se permiten números"
                                    ValidationExpression="\d+">
                                </asp:RegularExpressionValidator>
                            </td>

                        </tr>
                        <tr>

                            <td class="tabla-agregar-refaccion-celda">
                                <asp:Label ID="LBL_DESCRIPCION_REFACCION" runat="server" Text="Descripción" class="lbl-agregar-refaccion" ></asp:Label><br />
                                <asp:TextBox ID="txtbox_descripcion_refaccion" runat="server" TextMode="MultiLine" Rows="5" MaxLength="240" autocomplete="off" AutoCompleteType="Disabled" CssClass="txbox-agregar-refaccion"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>

                            <td class="tabla-agregar-refaccion-celda">
                                <asp:Button runat="server" Text="Agregar Refaccion" BorderStyle="None" style="float:right;" ID="BTN_AGREGAR_REFACCION" OnClick="Agregar_refaccion_a_base_de_datos_Click" />
                            </td>

                        </tr>
                    </table>
                 
           
        </section>


        <section class="centrar2"  id="avisodel" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;" id="sectionf12">
                <table class="auto-style5">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label9" runat="server" Text="Aviso de Eliminación de Acción Realizada" CssClass="auto-style4"></asp:Label>
                        </td>
                    </tr>                
                    <tr>
                        <td class="auto-style6" colspan="2" style="text-align:left">
                            <asp:Label ID="Label10" runat="server">
                                La acción "<asp:label ID="descacci" runat="server"> Descripcion de la accion</asp:label>" con fecha "<asp:label ID="fechacci" runat="server"> 
                                fecha de la accion</asp:label>" y "<asp:label ID="horaacci" runat="server"> hora de la accion</asp:label>" horas de acción será eliminada 
                                permanentemente del folio <asp:label ID="fol" runat="server"/> con servicio de <asp:label ID="serv" runat="server"/>. <br /><br />
                                Si la información anterior es correcta, dar click en "Borrar Acción". <asp:label ID="IDAccion" runat="server" Visible ="false"/>
                            </asp:Label></td></tr><tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Button runat="server" Text="Cancelar" BorderStyle="None" style="float:right; margin-left:5px" ID="Button5" OnClick="Cancelar_proceso_de_eliminar_accion_Click"/>
                            <asp:Button runat="server" Text="Borrar Acción" BorderStyle="None" style="float:right; margin-left:5px" ID="Button3" OnClick="Borrar_accion_realizada_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
        </section>
        <footer runat="server" id="footerid" class="footercl">
            <div runat="server" id="obsbutton" class="footerbtn" >
                <asp:Button runat="server" Text="Observaciones" BorderStyle="None" style="float:unset;" ID="observacionesbtn" OnClick="Buscar_observaciones_folio_servicio_Click"  />
            </div>
            <div runat="server" id="Btn_agregar_refacciones_a_servicio" class="footerbtn" >
                <asp:Button runat="server" Text="Refacciones" BorderStyle="None" style="float:unset;" ID="addrefbtn" OnClick="Mostrar_ventana_refacciones_Click"  />
            </div>
            <div runat="server" id="Checked_verificar_funcionamiento" class="footerbtn">
                <asp:Label runat="server" ID="funciona" Text="¿Funciona al 100%? "/>
                <label class="switch"style="color:azure">
                    <asp:CheckBox ID="CHECKED_ESTA_FUNCIONANDO" runat="server" OnCheckedChanged="Verificacion_de_estatus_esta_o_no_funcionando_CheckedChanged" AutoPostBack="true" />
                    <span class="slider round"></span>
                </label>
            </div>
            <div runat="server" id="Btn_reportar_falla" class="footerbtn" >
                 <asp:Button runat="server" Text="Reportar Fallas" BorderStyle="None" style="float:unset;" ID="addfallabtn" OnClick="Btn_Fallas_Encontradas_Click"  />
            </div>
            <div runat="server" id="Btn_vista_previa_reportes" class="footerbtn" >    
                 <asp:Button runat="server" Text="Vista Previa" BorderStyle="None" style="float:unset;" ID="vpbutton"  OnClick="Btn_Vista_Previa_Click" />
            </div>
        </footer>
    </form>
</body>
</html>
