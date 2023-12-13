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

namespace INOLAB_OC
{
    public partial class CRM_3 : System.Web.UI.Page
    {
        //variable para saber quien es su gerente
        string gte;
        C_Funnel controladorFunnel = new C_Funnel();

        //comentario test
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("/Sesion.aspx");
            }

            lbluser.Text = Session["nameUsuario"].ToString();
            lbliduser.Text = Session["idUsuario"].ToString();

            //Comercial - Servicio
            if ((Session["idUsuario"].ToString() == "2") || (Session["idUsuario"].ToString() == "44") || (Session["idUsuario"].ToString() == "3") || (Session["idUsuario"].ToString() == "15" || (Session["idUsuario"].ToString() == "16")))
            {
                gte = "Paola";
            }
            //Comercial - Equipo
            if ((Session["idUsuario"].ToString() == "1") || (Session["idUsuario"].ToString() == "13") || (Session["idUsuario"].ToString() == "123") || (Session["idUsuario"].ToString() == "124") || (Session["idUsuario"].ToString() == "84") || (Session["idUsuario"].ToString() == "98") || (Session["idUsuario"].ToString() == "126") || (Session["idUsuario"].ToString() == "131") || (Session["idUsuario"].ToString() == "139") || (Session["idUsuario"].ToString() == "146"))
            {
                gte = "Karla";
            }
            //Comercial - Guadalajara
            if ((Session["idUsuario"].ToString() == "119") || (Session["idUsuario"].ToString() == "122") || (Session["idUsuario"].ToString() == "6"))
            {
                gte = "Rodolfo";
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
            if (lbliduser.Text == "7" || lbliduser.Text == "98" ) // usuario ARTEMIO
            {
                btnPlan.Visible = false;
                BtnMenuPrincipal.Visible = true;
                Button1.Visible = false;
                

            }
            else // Asesores
            {
                btnPlan.Visible = true;
                BtnMenuPrincipal.Visible = true;
                Button1.Visible = true;
                
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
        }
        private void ValidaGTE()
        {
            if((Session["idUsuario"].ToString() == "7") || (Session["idUsuario"].ToString() == "13") || (Session["idUsuario"].ToString() == "2") || (Session["idUsuario"].ToString() == "6") || (Session["idUsuario"].ToString() == "98"))
            {
                ddAsesorA.Enabled = true;
                ddlF_Asesor.Visible = true;
                Lista();
            }
            else
            {
                ddlF_Asesor.Visible = false;
                ddAsesorA.Enabled = false;

            }
            
        }

        

        // definine la clasificacion para la consulta sql
        string clasificacionDeRegistro;
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnfiltrar.Visible = true;
            lblfecha1.Visible = true;
            lblfecha2.Visible = true;
            btnfiltrar.Visible = true;
            txtfecha1.Visible = true;
            txtfecha2.Visible = true;

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

            //if(ddlClasificacion.Text != "Todo")
            //{
            //    traerDatosDelFunnelDependiendoElTipoDeRegistro();
            //}else if (ddlClasificacion.Text.Equals("Todo"))
            //{
            //    traerTodosLosDatosDelFunnel();
            //}
            lblcontador.Text = GridView1.Rows.Count.ToString();
   
        }

        private void traerDatosDelFunnelDependiendoElTipoDeRegistro()
        {
            txtfecha1.Text = null;
            txtfecha2.Text = null;
            clasificacionDeRegistro = ddlClasificacion.Text;
            cargarDatosDelAsesor(clasificacionDeRegistro);
        }

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

        private void Gte_Funnel_Asesor()
        {
            GridView1.DataSource =  controladorFunnel.consultarDatosFunnelPorAsesor(ddlF_Asesor.SelectedValue);
            GridView1.DataBind();
        }

        private void traerTodosLosDatosDelFunnel()
        {
            txtfecha1.Text = "2023-01-01";
            txtfecha2.Text = "2090-12-31";
            cargarTodosLosRegistrosDelAsesor();
            txtfecha1.Text = null;
            txtfecha2.Text = null;
        }

        private void cargarTodosLosRegistrosDelAsesor()
        {
            string query = "Select* from  funnel where asesor='" + lbluser.Text + "' and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();
        }
        protected void Guardar_nuevo_registro_Click(object sender, EventArgs e)
        {
            verificarQueNoHallaCeldasVacias();
            guardarDatosDeNuevoRegistro();
        }


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
            btnactualiza.Visible = true;
        }
        private void Gte_Registros_Asesor (int numeroDeRegistro)
        {
            DataRow datosFunel = controladorFunnel.consultarDatosFunnelPorNoRegistro(numeroDeRegistro);

            txtcliente.Text = datosFunel["Cliente"].ToString();
            ddlClas_save.Text = datosFunel["Clasificacion"].ToString();
            datepicker.Text = datosFunel["FechaCierre"].ToString();
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

            //En estas clasificaciones ya no se permite realizar alguna edicion
            if (ddlClas_save.Text == "Perdido" || ddlClas_save.Text == "No Relacionado")
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
            }

            else
            {
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
            }
        }

    
        public void traerRegistrosDelFunnel(int numeroDeRegistro)
        {
            DataRow datosFunel =  controladorFunnel.consultarDatosFunnelPorNoRegistroYUsuario(numeroDeRegistro, lbluser.Text);

            txtcliente.Text = datosFunel["Cliente"].ToString();
            ddlClas_save.Text = datosFunel["Clasificacion"].ToString();
            datepicker.Text = datosFunel["FechaCierre"].ToString();
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
            
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.PageIndex = e.NewPageIndex;
        }

        protected void Limpiar_datos_Click(object sender, EventArgs e)
        {
            limpiarValoresDeDatos();
        }
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


            btnGuardar.Visible = true;
            btnactualiza.Visible = false;
        }

        protected void Actualizar_registro_Click(object sender, EventArgs e)
        {
            verificarQueNoHallaCeldasVacias();
            actualizarNuevosValores();
        }

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
       
        protected void Volver_a_plan_de_trabajo_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRM_2.aspx");
        }

      
        protected void Ir_a_cotizaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://inolabserver01/Reportes_Inolab/Pages/ReportViewer.aspx?%2fComercial%2fCOTIZACION-EQUIPO&rs:Command=Render");
        }

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
            
            lblcontador.Text = GridView1.Rows.Count.ToString();
        }

        public void filtrarPorFechaDeCierre()
        {
            string query = "Select * from  funnel where asesor='" + lbluser.Text + "'and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();
        }


        public void filtrarPorClasificacion()
        {
            string query = "Select * from  funnel where asesor='" + lbluser.Text + "'and clasificacion='" + ddlClasificacion.Text + "' and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();
        }

        protected void ddAsesorA_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Guarda el asesor para su seguimiento to esta con artemio
            ConexionComercial.executeQuery("Update Funnel set Asesor='" + ddAsesorA.Text + "' where NoRegistro=" + Convert.ToInt32(lblresistro.Text));

            Response.Write("<script language=javascript>if(confirm('Se asigno Asesor Correctamente')==true){ location.href='CRM_3.aspx'} else {location.href='CRM_3.aspx'}</script>");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            verificarQueNoHallaCeldasVacias();
            guardarDatosDeNuevoRegistro();
        }

        protected void btnactualiza_Click(object sender, EventArgs e)
        {
            verificarQueNoHallaCeldasVacias();
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
                gte_Fitro_Fecha();
            }
            else
            {
                if (ddlClasificacion.Text == "Todo")
                {
                    filtrarPorFechaDeCierre();
                }
                else
                {
                    filtrarPorClasificacion();
                }
            }


            //if (ddlClasificacion.Text == "Todo")
            //{
            //    filtrarPorClasificacion();
            //}
            //else
            //{
            //    filtrarPorClasificacion();
            //}

            lblcontador.Text = GridView1.Rows.Count.ToString();
        }
        public void gte_Fitro_Fecha()
        {
            string query = "Select * from  funnel where asesor='" + ddlF_Asesor.Text + "'and clasificacion='" + ddlClasificacion.Text + "' and fechacierre between '" + txtfecha1.Text + "' and '" + txtfecha2.Text + "'";
            GridView1.DataSource = ConexionComercial.getDataSet(query);
            GridView1.DataBind();
        }
        protected void btnFunnelAsesor_Click(object sender, EventArgs e)
        {
            
        }

        // Solo gerentes de ventas filtro para sus asesores
        protected void ddlF_Asesor_SelectedIndexChanged(object sender, EventArgs e)
        {

            Gte_Funnel_Asesor();
            lblcontador.Text = GridView1.Rows.Count.ToString();
            ddlClasificacion.Text = null;
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
                
                ddlF_Asesor.Items.Add(i1);
                ddlF_Asesor.Items.Add(i2);
                ddlF_Asesor.Items.Add(i3);
                ddlF_Asesor.Items.Add(i4);
                ddlF_Asesor.Items.Add(i5);
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
                ListItem i4 = new ListItem("Yuliet", "Yuliet");
                
                ddlF_Asesor.Items.Add(i1);
                ddlF_Asesor.Items.Add(i2);
                ddlF_Asesor.Items.Add(i3);
                ddlF_Asesor.Items.Add(i4);
            }
        }

    }
}