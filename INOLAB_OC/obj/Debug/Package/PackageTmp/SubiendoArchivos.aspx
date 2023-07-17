<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubiendoArchivos.aspx.cs" Inherits="INOLAB_OC.SubiendoArchivos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title></title>
    
    <link rel="stylesheet" href="CSS/EstiloVista.css" />
    <link rel="stylesheet" href="CSS/EncabezadoComun.css" />
    <link rel="stylesheet" href="CSS/drop.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" />
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css"/>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>

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
    </style>
</head>

<body onload="window.history.forward();">
    <form id="form1" runat="server" autocomplete="off">
     <header class="header2">
        <div class="auto-style1">
            <div class="logo" style="height: 67px"><img src="Imagenes/LOGO_Blanco_Lineas.png" class="logo"/></div>
                <asp:Label ID="Label1" runat="server" Text="Usuario: " Font-Bold="True" ForeColor="White"  class="logo"  ></asp:Label>
                <asp:Label ID="lbluser" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" class="logo" ></asp:Label>
                <asp:Label ID="labelestado" runat="server" Text="usuario" Font-Bold="True" ForeColor="White" Visible="false" class="logo" ></asp:Label>        
                                         <nav>
                        <div class="dropdown">
                             <asp:Button ID="cg" runat="server" Text="C G" class="dropbtn" Width="70px" UseSubmitBehavior="False" OnClick="pruebaclic"/>
                        </div>
                                 </nav>
        </div>

    </header>
            <section class="contenido2">
                <div class="etiqueta">
                    <asp:Label ID="lblcontador" runat="server" Font-Size="14pt" Text="Cargando datos, espere un minuto porfavor..." Font-Bold="True" ></asp:Label>
                </div>
            </section>
        <section class="contenido wrapper">
                <div style="overflow-x:auto;width:100%; height:500px">
                    <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" Width="100%" Font-Size="9pt" CellPadding="4"  ForeColor="#333333" GridLines="None" BorderStyle="Ridge" Font-Bold="False" CssClass="auto-style7">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="IdFSR" HeaderText="IdFSR" SortExpression="IdFSR">
                        <ItemStyle HorizontalAlign="Center"  /></asp:BoundField>
                
                        <asp:BoundField DataField="Folio"  HeaderText="Folio"  SortExpression="Folio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" /></asp:BoundField>

                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente">
                        <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Departamento"  HeaderText="Departamento" SortExpression="Departamento">
                        <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                
                        <asp:BoundField DataField="Direccion"  HeaderText="Direccion" SortExpression="Direccion">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Telefono"  HeaderText="Telefono" SortExpression="Telefono">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                    
                        <asp:BoundField DataField="Localidad"  HeaderText="Localidad" SortExpression="Localidad">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="N_Reportado"  HeaderText="N_Reportado" SortExpression="N_Reportado">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="N_Responsable"  HeaderText="N_Responsable" SortExpression="N_Responsable">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Mail"  HeaderText="Mail" SortExpression="Mail">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="TipoContrato"  HeaderText="TipoContrato" SortExpression="TipoContrato">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="TipoProblema"  HeaderText="TipoProblema" SortExpression="TipoProblema">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="TipoServicio"  HeaderText="TipoServicio" SortExpression="TipoServicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="servicio"  HeaderText="servicio" SortExpression="servicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Ingeniero"  HeaderText="Ingeniero" SortExpression="Ingeniero">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="IdIngeniero"  HeaderText="IdIngeniero" SortExpression="IdIngeniero">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="mailIng"  HeaderText="mailIng" SortExpression="mailIng">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="F_SolicitudServicio"  HeaderText="F_SolicitudServicio" SortExpression="F_SolicitudServicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="FechaServicio"  HeaderText="FechaServicio" SortExpression="FechaServicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Equipo"  HeaderText="Equipo" SortExpression="Equipo">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Marca"  HeaderText="Marca" SortExpression="Marca">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Modelo"  HeaderText="Modelo" SortExpression="Modelo">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="NoSerie"  HeaderText="NoSerie" SortExpression="NoSerie">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="IdEquipo_C"  HeaderText="IdEquipo_C" SortExpression="IdEquipo_C">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Estatusid"  HeaderText="Estatusid" SortExpression="Estatusid">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Estatus"  HeaderText="Estatus" SortExpression="Estatus">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Observaciones"  HeaderText="Observaciones" SortExpression="Observaciones">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="NoLlamada"  HeaderText="NoLlamada" SortExpression="NoLlamada">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Inicio_Servicio"  HeaderText="Inicio_Servicio" SortExpression="Inicio_Servicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Fin_Servicio"  HeaderText="Fin_Servicio" SortExpression="Fin_Servicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Dia"  HeaderText="Dia" SortExpression="Dia">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="FallaReportada"  HeaderText="FallaReportada" SortExpression="FallaReportada">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="HoraServicio"  HeaderText="HoraServicio" SortExpression="HoraServicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Confirmacion"  HeaderText="Confirmacion" SortExpression="Confirmacion">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Propuesta"  HeaderText="Propuesta" SortExpression="Propuesta">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Actividad"  HeaderText="Actividad" SortExpression="Actividad">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="S_Confirmacion"  HeaderText="S_Confirmacion" SortExpression="S_Confirmacion">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Asesor1"  HeaderText="Asesor1" SortExpression="Asesor1">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Correoasesor1"  HeaderText="Correoasesor1" SortExpression="Correoasesor1">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="CooreoIng"  HeaderText="CooreoIng" SortExpression="CooreoIng">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Proximo_Servicio"  HeaderText="Proximo_Servicio" SortExpression="Proximo_Servicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="idcontrato"  HeaderText="idcontrato" SortExpression="idcontrato">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="idservicio"  HeaderText="idservicio" SortExpression="idservicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="idproblema"  HeaderText="idproblema" SortExpression="idproblema">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="IdResp"  HeaderText="IdResp" SortExpression="IdResp">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Responsable"  HeaderText="Responsable" SortExpression="Responsable">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="IdDocumenta"  HeaderText="IdDocumenta" SortExpression="IdDocumenta">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Documentador"  HeaderText="Documentador" SortExpression="Documentador">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Refaccion"  HeaderText="Refaccion" SortExpression="Refaccion">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Ingeniero_A1"  HeaderText="Ingeniero_A1" SortExpression="Ingeniero_A1">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="IdIng_A1"  HeaderText="IdIng_A1" SortExpression="IdIng_A1">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="mailIng_A1"  HeaderText="mailIng_A1" SortExpression="mailIng_A1">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Ingeniero_A2"  HeaderText="Ingeniero_A2" SortExpression="Ingeniero_A2">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="IdIng_A2"  HeaderText="IdIng_A2" SortExpression="IdIng_A2">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="mailIng_A2"  HeaderText="mailIng_A2" SortExpression="mailIng_A2">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="F_InicioServicio"  HeaderText="F_InicioServicio" SortExpression="F_InicioServicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="F_FinServicio"  HeaderText="F_FinServicio" SortExpression="F_FinServicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="IdT_Servicio"  HeaderText="IdT_Servicio" SortExpression="IdT_Servicio">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="OC"  HeaderText="OC" SortExpression="OC">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="ArchivoAdjunto"  HeaderText="ArchivoAdjunto" SortExpression="ArchivoAdjunto">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="DiaInicioServ"  HeaderText="DiaInicioServ" SortExpression="DiaInicioServ">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="DiaFinServ"  HeaderText="DiaFinServ" SortExpression="DiaFinServ">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="DiasServ"  HeaderText="DiasServ" SortExpression="DiasServ">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="NotAsesor"  HeaderText="NotASesor" SortExpression="NotAsesor">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="Funcionando"  HeaderText="Funcionando" SortExpression="Funcionando">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="FallaEncontrada"  HeaderText="FallaEncontrada" SortExpression="FallaEncontrada">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="FechaFirmaCliente"  HeaderText="FechaFirmaCliente" SortExpression="FechaFirmaCliente">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>

                        <asp:BoundField DataField="NombreCliente"  HeaderText="NombreCliente" SortExpression="NombreCliente">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle></asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" HorizontalAlign="Center" VerticalAlign="Middle"  />
                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="False" />
                    <HeaderStyle BackColor="#1C5E55"  Font-Bold="True" ForeColor="White"  />
                    <PagerSettings Position="TopAndBottom" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" Height="60px"/>
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
               
                    <asp:SqlDataSource ID="DSBrowser" runat="server" ConnectionString="<%$ ConnectionStrings:CSServicio %>" SelectCommand="SELECT * FROM [V_FSR]"></asp:SqlDataSource>
                </div>
            </section>
    </form>
</body>
</html>
