using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.IO;
using System.Diagnostics;
using INOLAB_OC.Modelo;
using INOLAB_OC.Entidades.Ventas;
using INOLAB_OC.Controlador.Ventas;

namespace INOLAB_OC
{
    
    public partial class CRM_2 : System.Web.UI.Page
    {
        C_Llamada_Vista controladorLlamada = new C_Llamada_Vista();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("/Sesion.aspx");
            }

            lbluser.Text = Session["nameUsuario"].ToString();
            lbliduser.Text = Session["idUsuario"].ToString();
            ReportViewer1.ServerReport.Refresh();
        }

        //REPORTEADOR
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                // Set the processing mode for the ReportViewer to Remote
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;

                ServerReport serverReport = ReportViewer1.ServerReport;

                // Set the report server URL and report path
                serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
                serverReport.ReportPath = "/Comercial/Calendario-LlamadasXAsesor";

                // Create the sales order number report parameter
                ReportParameter asesor_v = new ReportParameter();
                asesor_v.Name = "asesor";
                asesor_v.Values.Add(Session["nameUsuario"].ToString());
                //asesor_v.Values.Add(Session["folio_p"].ToString());

                // Set the report parameters for the report
                ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { asesor_v });
                ReportViewer1.ShowParameterPrompts = false;
            }
        }
        [Serializable]
        public sealed class MyReportServerCredentials :
       IReportServerCredentials
        {//Inicializa el Reporteador
            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    // Use the default Windows user.  Credentials will be
                    // provided by the NetworkCredentials property.
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    /* Read the user information from the Web.config file.  
                     By reading the information on demand instead of 
                     storing it, the credentials will not be stored in 
                     session, reducing the vulnerable surface area to the
                     Web.config file, which can be secured with an ACL.

                     User name */
                    string userName =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerUser"];

                    if (string.IsNullOrEmpty(userName))
                        throw new Exception("Missing user name from web.config file");

                    // Password
                    string password =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerPassword"];

                    if (string.IsNullOrEmpty(password))
                        throw new Exception("Missing password from web.config file");

                    // Domain
                    string domain =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerDomain"];

                    return new NetworkCredential(userName, password, domain);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie,
                        out string userName, out string password,
                        out string authority)
            {
                authCookie = null;
                userName = null;
                password = null;
                authority = null;

                // Not using form credentials
                return false;
            }
        }

        // definine la clasificacion para la consulta sql

        protected void Seleccionar_tipo_de_registro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoRegistro.Text == "Llamada")
            {
                lblFecha.Text = "Fecha de Llamada";
                datepicker.Enabled = true;
            }
            else
            {
                lblFecha.Text = "Fecha de Visita";
                datepicker.Enabled = true;

            }
        }

        protected void Guardar_nuevo_registro_Click(object sender, EventArgs e)
        {
            GuardarNuevoRegistro();
        }

        public void GuardarNuevoRegistro()
        {
            if (ddlTipoRegistro.Text == "")
            {
                Response.Write("<script>alert('Selecciona si el Registro es Llamada o Visita.');</script>");
                return;
            }
            if (datepicker.Text == "")
            {
                Response.Write("<script>alert('El Campo Fecha llamada/Visita no puede estar vacío.');</script>");
                return;
            }
           
            validarQueFechaDeRegistroNoSeaAnteriorAlDiaDeHoy();
            E_Llamada_Vista entidadLlamada = new E_Llamada_Vista();
            entidadLlamada.Tipo = ddlTipoRegistro.Text;
            entidadLlamada.Cliente = txtcliente.Text;
            entidadLlamada.FechaLlamada = Convert.ToDateTime(datepicker.Text);
            entidadLlamada.Comentario = txtcomentario.Text;
            entidadLlamada.Asesor= lbluser.Text;
            entidadLlamada.Objetivo = txtobjetivo.Text;
            entidadLlamada.Horario = ddlhora.Text;

            controladorLlamada.actualizarLLamada(entidadLlamada);

            Response.Write("<script language=javascript>if(confirm('Registro Guardado Exitosamente')==true){ location.href='CRM_2.aspx'} else {location.href='CRM_2.aspx'}</script>");

            //llamada Vista
        }

        public void validarQueFechaDeRegistroNoSeaAnteriorAlDiaDeHoy()
        {
            DateTime fechaDeLRegistro = DateTime.Parse(datepicker.Text);
            DateTime fechaDelDiaDeHoy = DateTime.Today;

            if (fechaDeLRegistro < fechaDelDiaDeHoy)
            {
                Response.Write("<script>alert('El Campo Fecha Llamada/Visita no puede ser menor al día de Hoy.');</script>");
                return;
            }
        }


        protected void Ir_a_registro_funnel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRM_3.1.aspx");
        }
        protected void Ir_a_grafica_funnel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CRM_1.aspx");
        }
        protected void Ir_a_reporte_cotizaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://inolabserver01/Reportes_Inolab/Pages/ReportViewer.aspx?%2fComercial%2fCOTIZACION-EQUIPO&rs:Command=Render");
        }

        protected void Btn_MenuPrincipal(object sender, EventArgs e)
        {
            Response.Redirect("CRM_1.aspx");
        }

        int idLlamada;
        protected void Seleccionar_registro_SelectedIndexChanged(object sender, EventArgs e)
        {
            idLlamada = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
            btnUpdate.Visible = true;
            btnGuardar.Visible = false;
            traerTodosLosRegistrosDeVendedor();
        }

        // Pendiente de modificar
        public void traerTodosLosRegistrosDeVendedor()
        {
            DataRow datosLLamada =  controladorLlamada.mostrarTodosLosDatosPorIdYAsesor(idLlamada, lbluser.Text);

            txtcliente.Text = datosLLamada["Cliente"].ToString();
            ddlTipoRegistro.Text = datosLLamada["Tipo"].ToString();
            datepicker.Text = datosLLamada["Fechallamada"].ToString();
            txtobjetivo.Text = datosLLamada["Objetivo"].ToString();
            txtcomentario.Text = datosLLamada["Comentario"].ToString();
            lblREGISTRO.Text = datosLLamada["IdLlamada"].ToString();

        }

        protected void ddlTipofiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipofiltro.Text == "Llamada")
            {
                cargardatosDeAcuerdoAlTipoDeRegistro(ddlTipofiltro.Text, lbluser.Text);
            }
            if (ddlTipofiltro.Text == "Visita")
            {
                cargardatosDeAcuerdoAlTipoDeRegistro(ddlTipofiltro.Text, lbluser.Text);
            }
            if (ddlTipofiltro.Text == "Todo")
            {
                mostrarTodosLosDatosDelAsesor(lbluser.Text);
            }
            lblcontador.Text = GridView1.Rows.Count.ToString();
        }

        private void cargardatosDeAcuerdoAlTipoDeRegistro(string tipoDeRegistro, string usuario)
        {
            GridView1.DataSource = controladorLlamada.cargarDatosDependiendoTipoDeRegistro(tipoDeRegistro, usuario);
            GridView1.DataBind();
        }

        private void mostrarTodosLosDatosDelAsesor(string asesor)
        {
            GridView1.DataSource = controladorLlamada.mostrarTodosLosDatosDelAsesor(asesor);
            GridView1.DataBind();
        }



        protected void Actualizar_datos_del_registro_Click(object sender, EventArgs e)
        {
            actualizarDatosDelRegistroLlamadaOVisita();
        }

        public void actualizarDatosDelRegistroLlamadaOVisita()
        {
            if (ddlTipoRegistro.Text == "")
            {
                Response.Write("<script>alert('Selecciona si el Registro es Llamada o Visita.');</script>");
                return;
            }
            if (datepicker.Text == "")
            {
                Response.Write("<script>alert('El Campo Fecha llamada/Visita no puede estar vacío.');</script>");
                return;
            }

            validarQueFechaDeRegistroNoSeaAnteriorAlDiaDeHoy();
            actualizarNuevosDatosDeRegistroEnBBD();
        }

        private void actualizarNuevosDatosDeRegistroEnBBD()
        {
            E_Llamada_Vista entidadLlamada = new E_Llamada_Vista();

            entidadLlamada.Registro = Convert.ToInt32(lblREGISTRO.Text);
            entidadLlamada.FechaLlamada= Convert.ToDateTime(datepicker.Text);
            entidadLlamada.Cliente= txtcliente.Text;
            entidadLlamada.Comentario = txtcomentario.Text;
            entidadLlamada.Tipo = ddlTipoRegistro.Text;
            entidadLlamada.Objetivo = txtobjetivo.Text;

            controladorLlamada.actualizarDatosDeRegistro(entidadLlamada);
            Response.Write("<script language=javascript>if(confirm('Registro Actualizado Exitosamente')==true){ location.href='CRM_2.aspx'} else {location.href='CRM_2.aspx'}</script>");
        }

        protected void Limpiar_datos_del_registro_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            limpiarLosDatosDelRegistro();
        }
        public void limpiarLosDatosDelRegistro()
        {
            ddlTipoRegistro.Text = "";
            txtcliente.Text = null;
            datepicker.Text = null;
            txtcomentario.Text = null;
            btnUpdate.Visible = false;
            btnGuardar.Visible = true;
            txtobjetivo.Text = "";
        }

    }
}