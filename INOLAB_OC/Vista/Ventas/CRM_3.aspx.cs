using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Security.Principal;
using System.Configuration;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Collections.Generic;
using SpreadsheetLight;
using System.Windows;
using INOLAB_OC.Modelo;
using System.IO.Packaging;
using System.Drawing;
using INOLAB_OC.Controlador.Ventas;
using INOLAB_OC.Entidades.Ventas;
using DocumentFormat.OpenXml.Presentation;

namespace INOLAB_OC
{
    public partial class CRM_3 : System.Web.UI.Page
    {
        //variable para saber quien es su gerente
        string gte;
        C_Funnel controladorFunnel = new C_Funnel();

        //FECHA VENCIMIENTO PROSPECTO  DIAS
        DateTime Fecha_Vencimiento_Prospecto = DateTime.Now.AddDays(365);

        //FECHA VENCIMIENTO OPORTUNIDAD DE 30 DIAS
        DateTime Fecha_Vencimiento_Oportunidad = DateTime.Now.AddDays(30);

        //FECHA VENCIMIENTO LEAD DE 60 DIAS
        DateTime Fecha_Vencimiento_Lead = DateTime.Now.AddDays(60);

        //FECHA VENCIMIENTO PROYECTO DE 120 DIAS
        DateTime Fecha_Vencimiento_Proyecto = DateTime.Now.AddDays(120);

        //FECHA VENCIMIENTO FORCAST DE 90 DIAS
        DateTime Fecha_Vencimiento_Forecast = DateTime.Now.AddDays(90);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("/Sesion.aspx");
            }

            lbluser.Text = Session["nameUsuario"].ToString();
            lbliduser.Text = Session["idUsuario"].ToString();

            //Comercial - Servicio
            if ((Session["idUsuario"].ToString() == "2") || (Session["idUsuario"].ToString() == "44") || (Session["idUsuario"].ToString() == "3") || (Session["idUsuario"].ToString() == "15" || (Session["idUsuario"].ToString() == "119") || (Session["idUsuario"].ToString() == "16")))
            {
                gte = "Paola";
            }
            //Comercial - Equipo
            if ((Session["idUsuario"].ToString() == "1") || (Session["idUsuario"].ToString() == "13") || (Session["idUsuario"].ToString() == "123") || (Session["idUsuario"].ToString() == "124") || (Session["idUsuario"].ToString() == "84") || (Session["idUsuario"].ToString() == "98") 
                || (Session["idUsuario"].ToString() == "126") || (Session["idUsuario"].ToString() == "131") || (Session["idUsuario"].ToString() == "139") || (Session["idUsuario"].ToString() == "146") || (Session["idUsuario"].ToString() == "147") || (Session["idUsuario"].ToString() == "148"))
            {
                gte = "Abel";
            }
            //Comercial - Guadalajara
            if ((Session["idUsuario"].ToString() == "122") || (Session["idUsuario"].ToString() == "6"))
            {
                gte = "Rodolfo";
            }
            //Comercial Equipos - Karla Ivette
            if ((Session["idUsuario"].ToString() == "149"))
            {
                gte = "Karla";
            }
            // Direccion 
            if ((Session["idUsuario"].ToString() == "7"))
            {
                gte = "Artemio";
            }
            // Consumibles y usuario admin
            if ((Session["idUsuario"].ToString() == "1") || (Session["idUsuario"].ToString() == "28"))
            {
                gte = "";
            }
            if (Session["idUsuario"].ToString().Equals("140"))
            {
                gte = "Artemio";
            }

            // Vizualizacion de Botones
            if (lbliduser.Text == "7" || lbliduser.Text == "98" || lbliduser.Text == "2" || lbliduser.Text == "6") // usuario ARTEMIO
            {
                btnPlan.Visible = false;
                BtnMenuPrincipal.Visible = true;
                Button1.Visible = false;
                btnAturizaciones.Visible = true;



            }
            else // Asesores
            {
                btnPlan.Visible = true;
                BtnMenuPrincipal.Visible = true;
                Button1.Visible = true;
                btnAturizaciones.Visible = false;
                
            }

            //// Boton para Asignar
            //if ((Session["idUsuario"].ToString() == "7") || (Session["idUsuario"].ToString() == "74") || (Session["idUsuario"].ToString() == "1"))
            //{
            //    lblasesorA.Visible = true;
            //    ddAsesorA.Enabled = true;
            //}
            //else
            //{
            //    //lblasesorA.Visible = false;
            //    ddAsesorA.Enabled = false;
            //}

            ValidaGTE();

            OcultaFiltrosfecha();




 
        }
        private void ValidaGTE()
        {
            if((Session["idUsuario"].ToString() == "7") || (Session["idUsuario"].ToString() == "13") || (Session["idUsuario"].ToString() == "2") || (Session["idUsuario"].ToString() == "6") || (Session["idUsuario"].ToString() == "98"))
            {
                ddAsesorA.Enabled = true;
                ddlF_Asesor.Visible = true;
                Lista();
                ddlGTeClasif.Visible= true;
                ddlClasificacion.Visible = false;
            }
            else
            {
                ddlF_Asesor.Visible = false;
                ddAsesorA.Enabled = false;
                ddlClasificacion.Visible = true;
                ddlGTeClasif.Visible = false;


            }
            
        }

        

        // definine la clasificacion para la consulta sql
        string clasificacionDeRegistro;
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MuestraFiltrosfecha();

            if ((Session["idUsuario"].ToString() == "7") || (Session["idUsuario"].ToString() == "13") || (Session["idUsuario"].ToString() == "2") || (Session["idUsuario"].ToString() == "6"))
            {
                Gte_Datos_Asesor();
            }
            else
            {
                if (ddlClasificacion.Text != "Todo")
                {
                    cargarDatosDelAsesor(ddlClasificacion.Text);
                }
                else
                {
                    traerTodosLosDatosDelFunnel();
                }


            }

            lblcontador.Text = GridView1.Rows.Count.ToString();
            ddlbuscar.Text = null;
            CalcularSuma();

        }

        //private void traerDatosDelFunnelDependiendoElTipoDeRegistro()
        //{
        //    txtfecha1.Text = null;
        //    txtfecha2.Text = null;
        //    clasificacionDeRegistro = ddlClasificacion.Text;
        //    cargarDatosDelAsesor(clasificacionDeRegistro);
        //}

        private void Gte_Datos_Asesor()
        {
            //string query = "Select* from  funnel where clasificacion = '" + clasificacionDeRegistro + "' and asesor='" + ddlF_Asesor.Text + "'"; 
            GridView1.DataSource = controladorFunnel.consultarDatosPorAsesorYClasificacion(ddlF_Asesor.Text, ddlClasificacion.Text);
            GridView1.DataBind();

        }

        private void cargarDatosDelAsesor(string clasificacionDeRegistro)
        {
            GridView1.DataSource = controladorFunnel.consultarDatosPorAsesorYClasificacion(lbluser.Text, clasificacionDeRegistro);
            GridView1.DataBind();
        }

        // GERENTE CONSULTA DEL FUNEL DEL ASESOR LEAD PROYECTO FORECAST
        private void Gte_Funnel_Asesor()
        {
            GridView1.DataSource =  controladorFunnel.consultarDatosFunnelPorAsesor(ddlF_Asesor.SelectedValue);
            GridView1.DataBind();
        }

        private void Gte_Funnel_Asesor_Clasificacion()
        {
            GridView1.DataSource = controladorFunnel.consultaGteAsesorClasificacion(ddlF_Asesor.SelectedValue,ddlGTeClasif.SelectedValue);
            GridView1.DataBind();
        }


        private void traerTodosLosDatosDelFunnel()
        {
            txtfecha1.Text = "2024-01-01";
            txtfecha2.Text = "2090-12-31";
            cargarTodosLosRegistrosDelAsesor();
            txtfecha1.Text = null;
            txtfecha2.Text = null;
        }
        private void cargarTodosLosRegistrosDelAsesor()
        {
            string query = "Select* from  funnel where asesor='" + lbluser.Text + "' and clasificacion in ('Lead','Proyecto','Forecast') and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'";
            //string query = "Select* from  funnel where asesor='" + lbluser.Text + "' and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();
        }
        protected void Guardar_nuevo_registro_Click(object sender, EventArgs e)
        {
            //verificarQueNoHallaCeldasVacias();
           
            guardarDatosDeNuevoRegistro();
        }

        // no esta validando la funcion
        private void verificarQueNoHallaCeldasVacias()
        {
            if (txtcliente.Text == "")
            {
                Response.Write("<script>alert('Captura el Cliente.');</script>");
                return;
            }
            if (ddlClas_save.Text == "")
            {
                Response.Write("<script>alert('Selecciona la clasificación del Registro.');</script>");
                return;
            }
            if (datepicker.Text == "")
            {
                Response.Write("<script>alert('Captura la Fecha de Cierre del registro.');</script>");
                return;
            }
            if (ddLocalidad.Text == "")
            {
                Response.Write("<script>alert('Captura la Localidad del registro.');</script>");
                return;
            }
            if (ddTipoVenta.Text == "")
            {
                Response.Write("<script>alert('Captura el Tipo de Venta.');</script>");
                return;
            }
        }
        
        private void guardarDatosDeNuevoRegistro()
        {
            if (txtcliente.Text == "")
            {
                Response.Write("<script>alert('Captura el Cliente.');</script>");
                return;
            }
            if (ddlClas_save.Text == "")
            {
                Response.Write("<script>alert('Selecciona la clasificación del Registro.');</script>");
                return;
            }
            if (datepicker.Text == "")
            {
                Response.Write("<script>alert('Captura la Fecha de Cierre del registro.');</script>");
                return;
            }
            if (ddLocalidad.Text == "")
            {
                Response.Write("<script>alert('Captura la Localidad del registro.');</script>");
                return;
            }
            if (ddTipoVenta.Text == "")
            {
                Response.Write("<script>alert('Captura el Tipo de Venta.');</script>");
                return;
            }
            if (ddTipoVenta.Text == "")
            {
                Response.Write("<script>alert('Captura el Origen del Registro.');</script>");
                return;
            }
            string cliente = txtcliente.Text;
            string clasifiacion = ddlClas_save.Text;
            string fechaCierre = Convert.ToDateTime(datepicker.Text).ToString("dd/MM/yyyy");
            string equipo = txtequipo.Text;
            string marca = txtmarca.Text;
            string modelo = txtmodelo.Text;
            string valor = txtvalor.Text;
            string estatus = txtestatus.Text;
            string asesor = lbluser.Text;
            string contacto = TXTcONTACTO.Text;
            string localidad = ddLocalidad.Text;
            string origen = ddOrigen.Text;
            string tipo = ddTipoVenta.Text;
            string get_ = gte;

            ConexionComercial.executeStp_Save_Funnel(cliente, clasifiacion, fechaCierre, equipo, marca, modelo, valor, estatus, asesor, contacto, localidad, origen,
                tipo, gte);

            Response.Write("<script language=javascript>if(confirm('Registro Guardado Exitosamente')==true){ location.href='CRM_3.aspx'} else {location.href='CRM_3.aspx'}</script>");
        }
        // parametro para la consulta en BD 
        int numeroDeRegistro;
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numeroDeRegistro = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
            Gte_Registros_Asesor(numeroDeRegistro);
            //traerRegistrosDelFunnel(numeroDeRegistro);
            btnGuardar.Visible = false;
            //btnactualiza.Visible = true;
            lblmensaje_clas.Visible=false;
            ddlAutorizado.Enabled = true;
        }
        //TRAE REGISTROS DEL GRID
        private void Gte_Registros_Asesor (int numeroDeRegistro)
        {
            DataRow datosFunel = controladorFunnel.consultarDatosFunnelPorNoRegistro(numeroDeRegistro);

            DateTime fechagridcierre=Convert.ToDateTime(datosFunel["FechaCierre"].ToString());           
            //FORMATO  DE FECHA EN DATEPICKER
            string CierreEtapa=fechagridcierre.ToString("dd/MM/yyyy");

            txtcliente.Text = datosFunel["Cliente"].ToString();
            ddlClas_save.Text = datosFunel["Clasificacion"].ToString();
            datepicker.Text = CierreEtapa;
            txtequipo.Text = datosFunel["Equipo"].ToString();
            txtmarca.Text = datosFunel["Marca"].ToString();
            txtmodelo.Text = datosFunel["Modelo"].ToString();
            txtvalor.Text = datosFunel["Valor"].ToString();
            txtestatus.Text = datosFunel["Estatus"].ToString();
            txtf_actualiza.Text = datosFunel["FechaActualizacion"].ToString();
            lblresistro.Text = datosFunel["NoRegistro"].ToString();
            TXTcONTACTO.Text = datosFunel["Contacto"].ToString();
            ddLocalidad.Text = datosFunel["Localidad"].ToString();
            ddOrigen.Text = datosFunel["Origen"].ToString();
            ddTipoVenta.Text = datosFunel["TipoVenta"].ToString();
            ddAsesorA.Text = datosFunel["Asesor"].ToString();
            datepicker2.Text = datosFunel["C_FechaCierre"].ToString();
           
            //CAMBIA COLOR PARA EVITAR FECHA EN PROSPECTO
            if (ddlClas_save.Text=="Prospecto" || ddlClas_save.Text == "Perdido" || ddlClas_save.Text == "No Relacionado")
            {
                datepicker.ForeColor = Color.White;
            }
            if (ddlClas_save.Text != "Prospecto")
            {
                datepicker.ForeColor = Color.Black;
            }
            //En estas clasificaciones ya no se permite realizar alguna edicion
            if (ddlClas_save.Text == "Perdido" || ddlClas_save.Text == "No Relacionado" || ddlClas_save.Text == "Orden Compra")
            {
                txtcliente.Enabled = false;
                datepicker.Enabled = false;
                txtequipo.Enabled = false;
                txtmarca.Enabled = false;
                txtmodelo.Enabled = false;
                txtvalor.Enabled = false;
                txtestatus.Enabled = false;
                TXTcONTACTO.Enabled = false;
                ddLocalidad.Enabled = false;
                ddOrigen.Enabled = false;
                ddTipoVenta.Enabled = false;
                ddlClas_save.Enabled = false;
                btnactualiza.Visible = false;
            }

            else
            {
                txtcliente.Enabled = true;
                //datepicker.Enabled = true;
                txtequipo.Enabled = true;
                txtmarca.Enabled = true;
                txtmodelo.Enabled = true;
                txtvalor.Enabled = true;
                txtestatus.Enabled = true;
                TXTcONTACTO.Enabled = true;
                ddLocalidad.Enabled = true;
                ddOrigen.Enabled = true;
                ddTipoVenta.Enabled = true;
                ddlClas_save.Enabled = true;
                btnactualiza.Visible = true;
            }
            if(fechagridcierre < DateTime.Now)
            {
                btnactualiza.Enabled = false;
                ddlAutorizado.Enabled = true;
                ddlClas_save.Enabled = false;
            }
            else
            { 
                btnactualiza.Enabled = true;
                ddlAutorizado.Enabled = false;
                ddlClas_save.Enabled = true;
            }
            btnautorizacion.Visible = false;

            datepicker.Enabled = false;

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.PageIndex = e.NewPageIndex;
        }
        // BOTON LIMPIAR REGISTROS
        protected void Limpiar_datos_Click(object sender, EventArgs e)
        {
            limpiarValoresDeDatos();
        }
        //FUNCION - LIMPA LOS REGISTROS
        public void limpiarValoresDeDatos()
        {
            txtcliente.Text = null;
            ddlClas_save.Text = null;
            datepicker.Text = null;
            txtequipo.Text = null;
            txtmarca.Text = null;
            txtmodelo.Text = null;
            txtvalor.Text = "0";
            txtestatus.Text = null;
            txtf_actualiza.Text = null;
            TXTcONTACTO.Text = null;
            ddLocalidad.Text = null;
            ddOrigen.Text = null;
            ddTipoVenta.Text = null;

            ddlClasificacion.Text = null;
            btnGuardar.Visible = true;
            btnactualiza.Visible = false;
            lblmensaje_clas.Visible = false;

            txtcliente.Enabled = true;
            datepicker.Enabled = true;
            txtequipo.Enabled = true;
            txtmarca.Enabled = true;
            txtmodelo.Enabled = true;
            txtvalor.Enabled = true;
            txtestatus.Enabled = true;
            TXTcONTACTO.Enabled = true;
            ddLocalidad.Enabled = true;
            ddOrigen.Enabled = true;
            ddTipoVenta.Enabled = true;
            ddlClas_save.Enabled = true;
            lblFechaOC.Visible = false;
            datepicker2.Visible= false;
            ddlAutorizado.Enabled = false;
            OcultaFiltrosfecha();
        }
        // FUNCION PARA ACTUALIZAR REGISTROS
        protected void Actualizar_registro_Click(object sender, EventArgs e)
        {
            verificarQueNoHallaCeldasVacias();
            actualizarNuevosValores();
        }


        // ACTUALIZA NUEVOS VALORES
        private void actualizarNuevosValores()
        {
            E_Funnel entidadFunnel = new E_Funnel();
            entidadFunnel.NoRegistro = Convert.ToInt32(lblresistro.Text);
            entidadFunnel.Cliente = txtcliente.Text;
            entidadFunnel.Clasificacion = ddlClas_save.Text;
            entidadFunnel.FechaActualizacion= Convert.ToDateTime(datepicker.Text); 
            entidadFunnel.Equipo = txtequipo.Text;
            entidadFunnel.Marca = txtmarca.Text;
            entidadFunnel.Modelo = txtmodelo.Text;
            entidadFunnel.Valor = txtvalor.Text;
            entidadFunnel.Estatus = txtestatus.Text;
            entidadFunnel.Asesor = lbluser.Text;
            entidadFunnel.Contacto = TXTcONTACTO.Text;
            entidadFunnel.Localidad = ddLocalidad.Text;
            entidadFunnel.Origen= ddOrigen.Text;
            entidadFunnel.TipoVenta = ddTipoVenta.Text;

            controladorFunnel.actualizarDatosFunel(entidadFunnel);

            Response.Write("<script language=javascript>if(confirm('Registro Actualizado Exitosamente')==true){ location.href='CRM_3.aspx'} else {location.href='CRM_3.aspx'}</script>");
            limpiarValoresDeDatos();
        }
       // MENU PLAN DE TRABAJO
        protected void Volver_a_plan_de_trabajo_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRM_2.aspx");
        }
    
        protected void Ir_a_cotizaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://inolabserver01/Reportes_Inolab/Pages/ReportViewer.aspx?%2fComercial%2fCOTIZACION-EQUIPO&rs:Command=Render");
        }

        // MENU PRINCIPAL
        protected void Btn_MenuPrincipal_Click(object sender, EventArgs e)
        {
            Response.Redirect("./CRM_1.aspx");
        }
        protected void Filtrar_registros_Click(object sender, EventArgs e)
        {

            if(ddlClasificacion.Text=="Todo")
            {
                filtrarPorFechaDeCierre();
            }
            else
            {
                filtrarPorClasificacion();
            }

            if(gte == "Paola" || gte == "Rodolfo" || gte == "karla Ivvete")
            {

            }



            lblcontador.Text = GridView1.Rows.Count.ToString();
            CalcularSuma();
        }

        public void filtrarPorFechaDeCierre()
        {
            string query = "Select * from  funnel where asesor='" + lbluser.Text + "'and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "' and NoRegistro not in (select NoRegistro from funnel where Clasificacion ='orden compra' and YEAR(FechaCierre)=2023)";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();

            //if(gte=="Paola" || gte == "Rodolfo" || gte == "karla Ivvete")
            //{
            //    string query = "Select * from  funnel where asesor='" + lbluser.Text + "'and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'";
            //}

        }

        // no trae registros de oc 2023
        public void filtrarPorClasificacion()
        {
            // string query = "Select * from  funnel where asesor='" + lbluser.Text + "'and clasificacion='" + ddlClasificacion.Text + "' and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'  and NoRegistro not in (select NoRegistro from funnel where Clasificacion ='orden compra' and YEAR(FechaCierre)=2023)";
            string query = "Select * from  funnel where asesor='" + lbluser.Text + "'and clasificacion='" + ddlClasificacion.Text + "' and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "' and NoRegistro not in (select NoRegistro from funnel where Clasificacion ='orden compra' and YEAR(FechaCierre)=2023)";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();
        }

        // CAMBIO DE ASESOR EN EL REGISTRO INMEDIATAMENTE
        protected void ddAsesorA_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Guarda el asesor para su seguimiento to esta con artemio
            ConexionComercial.executeQuery("Update Funnel set Asesor='" + ddAsesorA.Text + "' where NoRegistro=" + Convert.ToInt32(lblresistro.Text));

            Response.Write("<script language=javascript>if(confirm('Se asigno Asesor Correctamente')==true){ location.href='CRM_3.aspx'} else {location.href='CRM_3.aspx'}</script>");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //verificarQueNoHallaCeldasVacias();
            guardarDatosDeNuevoRegistro();
        }

        protected void btnactualiza_Click(object sender, EventArgs e)
        {
            verificarQueNoHallaCeldasVacias();

            //VALIDACION AL RETROCEDER
            if (ddlClas_save.Text == "Oportunidad")
            {
                if (GridView1.SelectedRow.Cells[2].Text == "Proyecto" || GridView1.SelectedRow.Cells[2].Text == "Lead" || GridView1.SelectedRow.Cells[2].Text == "Forecast")
                {
                    Response.Write("<script>alert('Solo puedes Retroceder a estatus PROSPECTO o PERDIDO..');</script>");
                    return;

                }
            }

            if (ddlClas_save.Text == "Lead")
            {

                if (GridView1.SelectedRow.Cells[2].Text == "Forecast" || GridView1.SelectedRow.Cells[2].Text == "Proyecto")
                {
                    Response.Write("<script>alert('Solo puedes Retroceder a estatus PROSPECTO o PERDIDO.');</script>");
                    return;
                }
            }

            if (ddlClas_save.Text == "Proyecto")
            {
                if (GridView1.SelectedRow.Cells[2].Text == "Forecast")
                {
                    Response.Write("<script>alert('Solo puedes Retroceder a estatus PROSPECTO o PERDIDO.');</script>");
                    return;
                }

            }
            if (ddlClas_save.Text == "Forecast")
            {
                if (GridView1.SelectedRow.Cells[2].Text == "Lead")
                {
                    Response.Write("<script>alert('Solo puedes Retroceder a estatus PROSPECTO o PERDIDO.');</script>");
                    return;
                }

            }

            if(ddlClas_save.Text=="Orden Compra")
            {
                if (datepicker2.Text == "")
                {
                    Response.Write("<script>alert('Ingresa la Fecha de Orden de Compra.');</script>");
                    return;
                }
                GuardaOC();
            }
            actualizarNuevosValores();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarValoresDeDatos();
        }

        protected void btnfiltrar_Click(object sender, EventArgs e)
        {
            if ((Session["idUsuario"].ToString() == "7") || (Session["idUsuario"].ToString() == "13") || (Session["idUsuario"].ToString() == "2") || (Session["idUsuario"].ToString() == "6") || (Session["idUsuario"].ToString() == "98"))
            {
                if(ddlGTeClasif.Text=="")
                {
                    string query = "Select * from  funnel where asesor='" + ddlF_Asesor.Text + "'and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'"; // and NoRegistro not in (select NoRegistro from funnel where Clasificacion ='orden compra' and YEAR(FechaCierre)=2023)";
                    GridView1.DataSource = ConexionComercial.getDataSet(query);
                    GridView1.DataBind();
                }
                else
                {
                    string query = "Select * from  funnel where asesor='" + ddlF_Asesor.Text + "'and clasificacion='" + ddlGTeClasif.Text + "' and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'"; // and NoRegistro not in (select NoRegistro from funnel where Clasificacion ='orden compra' and YEAR(FechaCierre)=2023)";
                    GridView1.DataSource = ConexionComercial.getDataSet(query);
                    GridView1.DataBind();
                }
                
            }
            else
            {
                if (ddlClasificacion.Text == "Todo")
                {
                    filtrarPorFechaDeCierre();
                }
                else
                {
                    //filtro asesor de la clasificacion seleccionada
                    filtrarPorClasificacion();
                }

            }

            lblcontador.Text = GridView1.Rows.Count.ToString();
         CalcularSuma();
        }

        private void filtrarGTE()
        {
            if ((Session["idUsuario"].ToString() == "7") || (Session["idUsuario"].ToString() == "13") || (Session["idUsuario"].ToString() == "2") || (Session["idUsuario"].ToString() == "6"))
            {

            }
                if (ddlF_Asesor.Text=="Todos")
            {
                string query = "Select * from  funnel where gte='" + lbluser.Text + "'' and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "' and clasificacion in ('lead','Proyecto','Forecast') and NoRegistro not in (select NoRegistro from funnel where Clasificacion ='orden compra' and YEAR(FechaCierre)=2023)";
                GridView1.DataSource = ConexionComercial.getDataSet(query);
                GridView1.DataBind();
            }
        }
        public void gte_Fitro_Fecha()
        {
            string query = "Select * from  funnel where asesor='" + ddlF_Asesor.Text + "'and clasificacion='" + ddlClasificacion.Text + "' and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "' and NoRegistro not in (select NoRegistro from funnel where Clasificacion ='orden compra' and YEAR(FechaCierre)=2023)";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();
        }
        protected void btnFunnelAsesor_Click(object sender, EventArgs e)
        {
            
        }

        // Solo gerentes de ventas filtro para sus asesores
        protected void ddlF_Asesor_SelectedIndexChanged(object sender, EventArgs e)
        {
            MuestraFiltrosfecha(); 
            Gte_Funnel_Asesor();
            lblcontador.Text = GridView1.Rows.Count.ToString();
            ddlClasificacion.Text = null;
            
            CalcularSuma();
            ddlGTeClasif.Text = null;
        }
         
        // Carga la lista de asesores por area comercial
        private void Lista()
        {
            if((Session["idUsuario"].ToString() == "13") || (Session["idUsuario"].ToString() == "98"))
            {
                equipos();                
            }
            if ((Session["idUsuario"].ToString() == "2"))
            {
                servicio();
            }
            if ((Session["idUsuario"].ToString() == "6"))
            {
                gdl();
            }

        }
        //Asesores Equipo
        private void equipos()
        {
            if (!IsPostBack)
            {
                ListItem i1 = new ListItem("Abel", "Abel");
                ListItem i2 = new ListItem("Adrian", "Adrian");
                ListItem i3 = new ListItem("Amairani", "Amairani");
                ListItem i4 = new ListItem("Aranzha", "Aranzha");
                ListItem i5 = new ListItem("Artemio", "Artemio");
                ListItem i6 = new ListItem("Karla Ivette", "Karla Ivette");
                ListItem i7 = new ListItem("Karla Mariana", "Karla Mariana");
                ListItem i8 = new ListItem("Gustavo", "Gustavo");
                ListItem i9 = new ListItem("Sebastian", "Sebastian");
                ListItem i10 = new ListItem("Anaid", "Anaid");
                ListItem i11 = new ListItem("Silvia", "Silvia");
                ListItem i12 = new ListItem("Janatan", "Janatan");
                ListItem i13 = new ListItem("Gabriel", "Gabriel");

                ddlF_Asesor.Items.Add(i1);
                ddlF_Asesor.Items.Add(i2);
                ddlF_Asesor.Items.Add(i3);
                ddlF_Asesor.Items.Add(i4);
                ddlF_Asesor.Items.Add(i5);
                ddlF_Asesor.Items.Add(i6);
                ddlF_Asesor.Items.Add(i7);
                ddlF_Asesor.Items.Add(i8);
                ddlF_Asesor.Items.Add(i9);
                ddlF_Asesor.Items.Add(i10);
                ddlF_Asesor.Items.Add(i11);
                ddlF_Asesor.Items.Add(i12);
                ddlF_Asesor.Items.Add(i13);

            //    ddlF_Asesor.Items.Add(silvia);
            //    ddlF_Asesor.Items.Add(janathan);
            }         
        }
        //Asesores Servicio
        private void servicio()
        {
            if (!IsPostBack)
            {

                ListItem i1 = new ListItem("Dinorath", "Dinorath");
                ListItem i2 = new ListItem("Evelyn", "Evelyn");
                ListItem i3 = new ListItem("Karina", "Karina");
                ListItem i4 = new ListItem("Lidia", "Lidia");
                ListItem i5 = new ListItem("Paola", "Paola");
                ListItem i6 = new ListItem("Yuliet", "Yuliet");
                //ListItem i7 = new ListItem("Todos", "Todos");

                ddlF_Asesor.Items.Add(i1);
                ddlF_Asesor.Items.Add(i2);
                ddlF_Asesor.Items.Add(i3);
                ddlF_Asesor.Items.Add(i4);
                ddlF_Asesor.Items.Add(i5);
                ddlF_Asesor.Items.Add(i6);
                //ddlF_Asesor.Items.Add(i7);
            }
        }
        // Aesores Guadalajara
        private void gdl()
        {
            if (!IsPostBack)
            {

                ListItem i1 = new ListItem("Daniel", "Daniel");
                ListItem i2 = new ListItem("Perla", "Perla");
                ListItem i3 = new ListItem("Rodolfo", "Rodolfo");
                //ListItem i4 = new ListItem("Todos", "Todos");

                ddlF_Asesor.Items.Add(i1);
                ddlF_Asesor.Items.Add(i2);
                ddlF_Asesor.Items.Add(i3);
                //ddlF_Asesor.Items.Add(i4);
            }
        }
        //  FILTRADO DE BUSQUEDA PARA CLIENTE Y MARCA EN GENERAL
        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            if (ddlbuscar.Text=="Cliente")
            {
                BusquedaCliente();
            }
            else
            {
                BusquedaMarca();
            }          
        }
        private void BusquedaCliente()
        {
            string query = "Select* from  funnel where asesor='" + lbluser.Text + "' and cliente like '%" + txtbuscar.Text + "%' ";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();
        }
        private void BusquedaMarca()
        {
            string query = "Select* from  funnel where asesor='" + lbluser.Text + "' and marca like '%" + txtbuscar.Text + "%' ";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();
        }

        // SOMBREADO DE GRID
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Encuentra el índice de la columna que contiene las fechas
                int fechaColumnaIndex = GetColumnIndexByName(e.Row, "FechaCierre");
                int clasificacion = GetColumnIndexByName(e.Row, "Clasificacion");

                // Verifica si la fecha cierre es mayor a la fecha de hoy
                DateTime fecha = Convert.ToDateTime(e.Row.Cells[fechaColumnaIndex].Text); // variable para fecha cierre
                string tipo = Convert.ToString(e.Row.Cells[clasificacion].Text);


                // Verifica si la fecha está 15 días antes de hoy
                if (fecha.AddDays(-15) <= DateTime.Now && tipo != "Perdido" && tipo != "Orden Compra" && tipo != "Prospecto" && tipo != "No Relacionado")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow; // Establece el color de fondo en amarillo
                }
                // Verifica si la fecha cierre ya expiro
                if (fecha < DateTime.Now && tipo != "Perdido" && tipo != "Orden Compra" && tipo != "Prospecto" && tipo != "No Relacionado")
                {
                    e.Row.BackColor = System.Drawing.Color.Red; // Establece el color de fondo en rojo
                }

                string formato = e.Row.Cells[2].Text;
                if(formato=="Prospecto")
                {
                    //e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;         ---cambia el color
                    e.Row.Cells[10].Text = "";
                }


                //if (fecha == DateTime.Now.AddDays(-20))
                //{
                //    e.Row.BackColor = System.Drawing.Color.Yellow; // Establece el color de fondo en amarillo
                //}
            }
        }
        // Método para obtener el índice de la columna por su nombre
        private int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                {
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                    {
                        break;
                    }
                }
                columnIndex++;
            }
            return columnIndex;
        }

        private void CalcularSuma()
        {
            decimal suma = 0;

            foreach (GridViewRow row in GridView1.Rows)
            {
                // valor de la celda 
                decimal valorCelda;
                if (decimal.TryParse(row.Cells[7].Text, out valorCelda))
                {
                    suma += valorCelda;
                }
            }

            lbltotalvalor.Text = "       Importe USD: " + suma.ToString("C");
        }

        string autoriza = "Ninguno";
        //AUTORIZACION
        public void Autorizacion()
        {

            if (datepicker.Text == "")
            {
                return;
            }
            else
            {
                DateTime fechacierre = Convert.ToDateTime(datepicker.Text);

                if (ddlClas_save.Text == "Oportunidad")
                {
                    if (fechacierre > Fecha_Vencimiento_Oportunidad)
                    {

                        Response.Write("<script>alert('La fecha de Cierre es mayor a 30 dias, este registro necesitara autorizacion para poder continuar con su edición');</script>");
                        autoriza = "En Espera";
                        return;
                    }
                }

                if (ddlClas_save.Text == "Lead")
                {
                    if (fechacierre > Fecha_Vencimiento_Lead)
                    {

                        Response.Write("<script>alert('La fecha de Cierre es mayor a 60 dias, este registro necesitara autorizacion para poder continuar con su edición');</script>");
                        autoriza = "En Espera";
                        return;
                    }
                }
                if (ddlClas_save.Text == "Proyecto")
                {
                    if (fechacierre > Fecha_Vencimiento_Proyecto)
                    {

                        Response.Write("<script>alert('La fecha de Cierre es mayor a 120 dias, este registro necesitara autorizacion para poder continuar con su edición');</script>");
                        autoriza = "En Espera";
                        return;
                    }
                }
                if (ddlClas_save.Text == "Forecast")
                {
                    if (fechacierre > Fecha_Vencimiento_Forecast)
                    {

                        Response.Write("<script>alert('La fecha de Cierre es mayor a 90 dias, este registro necesitara autorizacion para poder continuar con su edición');</script>");
                        autoriza = "En Espera";
                        return;
                    }
                }
            }



        }

        protected void ddlAutorizado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlAutorizado.Text=="Fecha Cierre")
            {
                txtfechacierre_aut.Visible = true;
                ddlclasif_aut.Visible = false;
                
            }
            if(ddlAutorizado.Text=="Clasificacion")
            {
                txtfechacierre_aut.Visible = false;
                ddlclasif_aut.Visible = true;

            }
            btnautorizacion.Visible = true;
        }

        protected void ddlClas_save_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlClas_save.Text == "Oportunidad")
            //{
            //    if (GridView1.SelectedRow.Cells[2].Text == "Proyecto" || GridView1.SelectedRow.Cells[2].Text == "Lead" || GridView1.SelectedRow.Cells[2].Text == "Forecast")
            //    {

            //        lblmensaje_clas.Visible = true; // MENSAJE AUTORIZACION POR RETROCEDER DE CLASIFICACION

            //        ddlAutorizado.Visible = true;     // SE HABILITA MENU PARA SELECCIONAR TIPO DE AUTORIZACION
            //        ddlClas_save.Text = GridView1.SelectedRow.Cells[2].Text;
            //        datepicker.Text = GridView1.SelectedRow.Cells[10].Text;
            //    }
            //}

            //if (ddlClas_save.Text == "Lead")
            //{

            //    if (GridView1.SelectedRow.Cells[2].Text == "Forecast" || GridView1.SelectedRow.Cells[2].Text == "Proyecto")
            //    {
            //        lblmensaje_clas.Visible = true; // MENSAJE AUTORIZACION POR RETROCEDER DE CLASIFICACION

            //        ddlAutorizado.Visible = true;     // SE HABILITA MENU PARA SELECCIONAR TIPO DE AUTORIZACION
            //        ddlClas_save.Text = GridView1.SelectedRow.Cells[2].Text;
            //        datepicker.Text = GridView1.SelectedRow.Cells[10].Text;
            //    }
            //}
            //if (ddlClas_save.Text == "Proyecto")
            //{
            //    if (GridView1.SelectedRow.Cells[2].Text == "Forecast")
            //    {
            //        lblmensaje_clas.Visible = true; // MENSAJE AUTORIZACION POR RETROCEDER DE CLASIFICACION

            //        ddlAutorizado.Visible = true;     // SE HABILITA MENU PARA SELECCIONAR TIPO DE AUTORIZACION
            //        ddlClas_save.Text = GridView1.SelectedRow.Cells[2].Text;
            //        datepicker.Text = GridView1.SelectedRow.Cells[10].Text;
            //    }

            //}
            //if (ddlClas_save.Text == "Forecast")
            //{
            //    if (GridView1.SelectedRow.Cells[2].Text == "Lead")
            //    {
            //        lblmensaje_clas.Visible = true; // MENSAJE AUTORIZACION POR RETROCEDER DE CLASIFICACION

            //        ddlAutorizado.Visible = true;     // SE HABILITA MENU PARA SELECCIONAR TIPO DE AUTORIZACION
            //        ddlClas_save.Text = GridView1.SelectedRow.Cells[2].Text;
            //        datepicker.Text = GridView1.SelectedRow.Cells[10].Text;
            //    }

            //}
            if (ddlClas_save.Text == "Prospecto")
            {
                datepicker.Text = Fecha_Vencimiento_Prospecto.ToString("dd/MM/yyyy");
                datepicker.ForeColor = System.Drawing.Color.White;
                datepicker.Enabled = false;
                lblFechaOC.Visible = false;
                datepicker2.Visible = false;
            }
            if (ddlClas_save.Text == "Oportunidad")
            {
                datepicker.Text = Fecha_Vencimiento_Oportunidad.ToString("dd/MM/yyyy");
                datepicker.ForeColor = System.Drawing.Color.Black;
                datepicker.Enabled = false;
                lblFechaOC.Visible = false;
                datepicker2.Visible = false;
            }
            if (ddlClas_save.Text == "Lead")
            {
                datepicker.Text = Fecha_Vencimiento_Lead.ToString("dd/MM/yyyy");
                datepicker.ForeColor = System.Drawing.Color.Black;
                datepicker.Enabled = false;
                lblFechaOC.Visible = false;
                datepicker2.Visible = false;
            }
            if (ddlClas_save.Text == "Proyecto")
            {
                datepicker.Text = Fecha_Vencimiento_Proyecto.ToString("dd/MM/yyyy");
                datepicker.ForeColor = System.Drawing.Color.Black;
                datepicker.Enabled = false;
                lblFechaOC.Visible = false;
                datepicker2.Visible = false;
            }
            if (ddlClas_save.Text == "Forecast")
            {
                datepicker.Text = Fecha_Vencimiento_Forecast.ToString("dd/MM/yyyy");
                datepicker.ForeColor = System.Drawing.Color.Black;
                datepicker.Enabled = false;
                lblFechaOC.Visible = false;
                datepicker2.Visible = false;
            }
            if (ddlClas_save.Text == "Orden Compra")
            {
                datepicker.Enabled = true;
                lblFechaOC.Visible = true;
                datepicker2.Visible = true;
                
            }

        }

        //GUARDA EL PROCESO DE AUTORIZACION
        protected void btnautorizacion_Click(object sender, EventArgs e)
        {
            if(ddlAutorizado.Text=="Fecha Cierre")
            {
                if(txtfechacierre_aut.Text=="")
                {
                    Response.Write("<script>alert('Ingresa la Fecha de Cierre para Autorizar');</script>");
                    return;
                }
                ConexionComercial.executeQuery("Update Funnel set Autoriza='Fecha', C_Clasificacion=null,C_Fechacierre='" + txtfechacierre_aut.Text + "' where NoRegistro=" + Convert.ToInt32(lblresistro.Text));
                Response.Write("<script language=javascript>if(confirm('Solicitud Enviada Correctamente, para cambio Fecha Cierre de Etapa')==true){ location.href='CRM_3.aspx'} else {location.href='CRM_3.aspx'}</script>");
            }
            if (ddlAutorizado.Text == "Clasificacion")
            {
                if (ddlclasif_aut.Text == "")
                {
                    Response.Write("<script>alert('Ingresa la Clasificacion para Autorizar');</script>");
                    return;
                }
                ConexionComercial.executeQuery("Update Funnel set Autoriza='Clasificacion', C_FechaCierre=null, C_Clasificacion='" + ddlclasif_aut.Text + "' where NoRegistro=" + Convert.ToInt32(lblresistro.Text));
                Response.Write("<script language=javascript>if(confirm('Solicitud Enviada Correctamente, para cambio de Clasificación')==true){ location.href='CRM_3.aspx'} else {location.href='CRM_3.aspx'}</script>");
            }
            
            

        }

        //GUARDA ORDEN DE COMPRA
        protected void GuardaOC ()
        {
                ConexionComercial.executeQuery("Update funnel set FechaOC='"+ Convert.ToDateTime(datepicker.Text)+"' where NoRegistro=" + Convert.ToInt32(lblresistro.Text));
           
        }

        protected void ddlGTeClasif_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gte_Funnel_Asesor_Clasificacion();
            lblcontador.Text = GridView1.Rows.Count.ToString();
            //ddlClasificacion.Text = null;
            CalcularSuma();
            MuestraFiltrosfecha();
        }
        //MENU AUTORIZACIONES
        protected void btnAturizaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRM_4.aspx");
        }

        //OCULTA FILTROS FECHA

        protected void OcultaFiltrosfecha()
        {
            lblfecha1.Visible = false;
            lblfecha2.Visible= false;
            txtfecha1.Visible = false;
            txtfecha2.Visible = false;
            btnfiltrar.Visible=false;

        }
        protected void MuestraFiltrosfecha()
        {
            lblfecha1.Visible = true;
            lblfecha2.Visible = true;
            txtfecha1.Visible = true;
            txtfecha2.Visible = true;
            btnfiltrar.Visible = true;  
        }






    }
}