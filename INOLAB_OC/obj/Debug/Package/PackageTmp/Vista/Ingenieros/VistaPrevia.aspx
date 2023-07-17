<%@ Page Language="C#" AutoEventWireup="true" Inherits="VistaPrevia" Codebehind="VistaPrevia.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>VISTA PREVIA</title>
    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link rel="stylesheet" href="../../CSS/EstiloVista.css" />
    <link rel="stylesheet" href="../../CSS/EncabezadoFirmaFolio.css" />
    <link rel="stylesheet" href="../../CSS/drop.css" />
   

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css"/>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css"/>

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
                window.location.href = "./DetalleFSR.aspx";
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
                document.getElementById("firma").style.display = "none";
                document.getElementById("headerid").style.display = "block";
                document.getElementById("sectionreport").style.display = "block";
                document.getElementById("footerid").style.display = "flex";
                event.returnValue = false;
            }, false);            
        }      
    </script>
   
    <style type="text/css">
        .auto-style1 {
            width: 98%;
            max-width: 10000px;
            margin: auto;
            height: 69px;
        }
        .auto-style5 {
            width: 100%;
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
        <header class="header2" style="position:relative" runat="server" id="headerid">
            <div id="headerone" class="auto-style1" runat="server">
                <div class="logo" style="height: 67px"><img src="../../Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
                <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo" ></asp:Label>
                <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo"></asp:Label>
                <nav>
                   <button type="reset" class="dropbtn" id="Btn_Atras" onclick="go('atras')">Atras</button>
                    
                                      
                   <button type="reset" class="dropbtn" id="Btn_Salir" onclick="go('salir')">Salir</button>
                </nav>
            </div>
        </header>

        <section class="auto-style7" id="sectionreport" runat="server">
            <div id="reportdiv" runat="server" class="reportclass">
                <asp:ScriptManager runat="server"></asp:ScriptManager>        
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote" Width="100%" ShowExportControls="False" ShowFindControls="False"></rsweb:ReportViewer>
            </div>
        </section>
        <!-- Seccion de aviso de privacidad-->
        <section class="centrar2"  id="avisopriv" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;" id="sectionf1">
                <table class="auto-style5">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" Text="Aviso de Privacidad" CssClass="auto-style4"></asp:Label>
                        </td>
                    </tr>                
                    <tr>
                        <td class="auto-style6" colspan="2" style="text-align:left">
                            <!-- Aqui ira el texto del aviso de Privacidad-->
                            <asp:Label ID="Label3" runat="server">De acuerdo a lo Previsto en la 
                                'Ley Federal de Protección de Datos Personales', declara: INOLAB 
                                Especialistas de Servicio S.A de C.V, ser una empresa legalmente constituida de conformidad con 
                                las leyes mexicanas, con domicilio en Aniceto Ortega 1341 Col. Del Valle, Delegación Benito Juárez 
                                C.P. 03100 en México, D.F. y como responsable del tratamiento de sus datos personales, hace de su 
                                conocimiento que la información de nuestros clientes es tratada de forma estrictamente 
                                confidencial.  <br/>
                                Estos serán utilizados única y exclusivamente para los siguientes fines:<br/>
                                1. Información y Prestación de Servicios<br/>
                                2. Actualización de la base de datos<br/>
                                3. Cualquier finalidad análoga o compatible con las anteriores<br/>
                                Para más información sobre nuestro Aviso de Privacidad Integral acceda a la siguiente dirección 
                                electrónica</asp:Label>
                                <a href =" https://www.inolab.com/aviso-de-privacidad.html" target="_blank">https://www.inolab.com/aviso-de-privacidad.html</a> 
                        </td>    
                    </tr>            
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Button runat="server" Text="Estoy de acuerdo" BorderStyle="None" style="float:right;" ID="btnguardar" OnClick="Btn_Aceptar_aviso_de_privacidad"/>
                        </td>
                    </tr>
                </table>
            </div>
        </section>
        <!-- Seccion de Ultima Advertencia-->
        <section class="centrar2"  id="ul_advert" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;" id="sectionf2">
                <table class="auto-style5">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label4" runat="server" Text="Aviso de cambios de la Información" CssClass="auto-style4"></asp:Label>
                        </td>
                    </tr>                
                    <tr>
                        <td class="auto-style6" colspan="2" style="text-align:left">
                            <asp:Label ID="Label5" runat="server">Se hace la mención que después de realizada la firma, 
                                ya NO se poda realizar ningún cambio a la información contenida de este folio número: 
                                <asp:label ID="ulfol" runat="server">a</asp:label> por motivos de seguridad, protección 
                                y veracidad de la información.
                            </asp:Label>
                        </td>    
                    </tr>            
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Button runat="server" Text="Estoy de acuerdo" BorderStyle="None" style="float:right;" ID="Button1" OnClick="Mostrar_ventana_de_firma_de_usuario"/>
                        </td>
                    </tr>
                </table>
            </div>
        </section>
        <section id="firma" runat="server" style="display:none">
            <div id="divfirma" runat="server">
                <div id="areafirma" runat="server">
                    <canvas id="signature-pad" class="signature-pad" width="100%"></canvas>            
                </div>
                <div id="buttonsfirma" runat="server" style="text-align:center">
                    <div id="nombrecli">
                        <table><tr><td class="auto-style6" colspan="2">
                            <asp:Label ID="labelcli" Text="Nombre:" runat="server"></asp:Label>
                            <asp:TextBox Id="textboxnombre" runat="server"></asp:TextBox>
                        </td></tr></table>
                    </div>
                    
                        <button id="salir" runat="server" class="dropbtn">Salir</button> 
                    
                   
                        <button  id="clear" runat="server"  class="dropbtn">Limpiar</button> 
                    
                    
                        <button  id="save" runat="server"  class="dropbtn">Guardar</button> 
                    
                </div>
           </div>
            <div style="display: none;">
                <input id="hidValue" type="hidden" runat="server" />
                <asp:Button runat="server" id="hidebutton" OnClick="Btn_guardar_firmar_Click" />
            </div>
        </section>

        <!-- Seccion de formulario de fechas que iran al folio-->
        <section class="centrar2"  id="floatsection" runat="server" style="display: none;">
            <div class="drop2" style="background-color: RGBA(255,255,255,1); padding:30px;"  id="sectionf">
                <table class="auto-style5">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Titulo" runat="server" Text="Fecha en la que se finaliza el folio:" CssClass="auto-style4"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:TextBox ID="datepicker" runat="server" autocomplete="off" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>                           
                    </tr>
                    <tr>
                        <td class="auto-style6" colspan="2">
                            <asp:Label ID="horasfin" runat="server" Text="Hora en la que se finaliza el folio:"></asp:Label>
                            <asp:DropDownList ID="horafinal" runat="server" Enabled="True" Width="100px">
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
                            <asp:Label ID="Label6" runat="server" Text=":"></asp:Label>
                            <asp:DropDownList ID="minfinal" runat="server" Enabled="True" Width="100px">
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
                            <asp:Button runat="server" Text="Finalizar" BorderStyle="None" style="float:right;" ID="Addbutton" OnClick="Finalizar_folio_Click" />
                        </td>    
                    </tr>
                </table>

                </div>
        </section>

        <footer runat="server" id="footerid" class="footercl">
            <div runat="server" id="firmabutton" class="footerbtn" >
                 <asp:Button runat="server" Text="Firma Usuario" BorderStyle="None" style="float:unset;" ID="firmarbtn" OnClick="Mostrar_pantalla_para_firma_de_documento_Click"  />
            </div>
            <div runat="server" id="firmaingbtn" class="footerbtn" >
                 <asp:Button runat="server" Text="Firma Ingeniero" BorderStyle="None" style="float:unset;" ID="firmaing" OnClick="firmaing_Click" />
            </div>
            <div runat="server" id="finbuttonid" class="footerbtn" >
                 <asp:Button runat="server" Text="Finalizar" BorderStyle="None" style="float:unset;" ID="finalizarbtn" OnClick="Finalizar_reporte_Click"  />
            </div>
        </footer>
    </form>
</body>
</html>