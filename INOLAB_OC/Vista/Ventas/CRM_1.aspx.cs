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

namespace INOLAB_OC
{
    public partial class CRM_1 : System.Web.UI.Page
    {

        const string usuarioArtemio = "7";
        string ligaReporte;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("./Sesion.aspx");
            }

            Lbl_nombre_usuario.Text = Session["nameUsuario"].ToString();
            Lbl_id_usuario.Text = Session["idUsuario"].ToString();
            ReportViewer1.ServerReport.Refresh();

            if(Lbl_id_usuario.Text.Equals(usuarioArtemio)) 
            {
                Btn_plan_de_trabajo.Visible = false;
                Btn_registro_funnel_ventas.Visible = true; //habilita boton Registro Funnel
                Btn_reporte_cotizaciones.Visible = false;
                ligaReporte = "/Comercial/Funnel-Direccion";
            }
            else
            {
                Btn_plan_de_trabajo.Visible = true;
                Btn_registro_funnel_ventas.Visible = true;
                Btn_reporte_cotizaciones.Visible = true;
                ligaReporte = "/Comercial/FunnelxAsesor";
            }                      


        }

        

        // COMIENZO DE REPORTEADOR
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
                serverReport.ReportPath = "/Comercial/FunnelxAsesor";
                //serverReport.ReportPath = Reporte;

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

        
        protected void Btn_Plan_De_Trabajo_Click(object sender, EventArgs e)
        {
            Response.Redirect("./CRM_2.aspx");
        }

       
        protected void Btn_Registro_Funnel_Click(object sender, EventArgs e)
        {
            Response.Redirect("./CRM_3.aspx");
        }

        
        protected void Btn_Reporte_Cotizaciones_Click(object sender, EventArgs e)
        {
           // Response.Redirect("http://inolabserver01/Reportes_Inolab/Pages/ReportViewer.aspx?%2fComercial%2fCOTIZACION-EQUIPO&rs:Command=Render");

            string abrirReporteCotizaciones = "window.open('http://inolabserver01/Reportes_Inolab/Pages/ReportViewer.aspx?%2fComercial%2fCOTIZACION-EQUIPO&rs:Command=Render', '_newtab');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), abrirReporteCotizaciones, true);
        }

        protected void Btn_Salir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../Sesion.aspx");
        }
    }
}