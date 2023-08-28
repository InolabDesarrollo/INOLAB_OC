using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Security.Principal;
using System.Configuration;
using System.Web.UI;
using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Browser.Interfaces;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Controlador;
using INOLAB_OC.Vista;

namespace INOLAB_OC
{
    public partial class DescargaFolio : System.Web.UI.Page
    {
        static V_FSR_Repository repositorioVFSR = new V_FSR_Repository();
        C_V_FSR controladorVFSR = new C_V_FSR(repositorioVFSR);
        static FSR_Repository repositorioFSR = new FSR_Repository();
        C_FSR controladorFsr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("/Sesion.aspx");
            }
            else
            {
                lbluser.Text = Session["nameUsuario"].ToString();
            }
            cargarDatosDeFoliosConEstatusFinalizado();
            controladorFsr = new C_FSR(repositorioFSR, Session["idUsuario"].ToString());
        }

        protected void Page_Init(object sender, EventArgs e)
        {           
            if (!Page.IsPostBack)
            {       
                ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
                serverReport.ReportPath = "/Servicio/Calendario-Servicio-Ing";

                ReportParameter salesOrderNumber = new ReportParameter();
                salesOrderNumber.Name = "ing";
                salesOrderNumber.Values.Add(Session["idUsuario"].ToString());
                ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { salesOrderNumber });
                ReportViewer1.ShowParameterPrompts = false;
                ReportViewer1.ServerReport.Refresh();
            }      
        } 
        
        [Serializable]
        public sealed class MyReportServerCredentials :
            IReportServerCredentials
        {
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
                    string userName =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerUser"];

                    if (string.IsNullOrEmpty(userName))
                        throw new Exception(
                            "Missing user name from web.config file");

                    // Password
                    string password =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerPassword"];

                    if (string.IsNullOrEmpty(password))
                        throw new Exception(
                            "Missing password from web.config file");

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
                return false;
            }
        }
        private void cargarDatosDeFoliosConEstatusFinalizado()
        {      
            GridViewServicios_Finalizados.DataSource =  controladorVFSR.consultarFoliosConEstatusFinalizado(Session["Idusuario"].ToString());
            GridViewServicios_Finalizados.DataBind(); 
        }

        protected void Servicios_Finalizados_OnRowComand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                String folio = "";
                String estatusFolio = "";
                if (e.CommandName == "Select")
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow fila = GridViewServicios_Finalizados.Rows[index];
                    folio = ((LinkButton)fila.Cells[0].Controls[0]).Text;
                    estatusFolio = fila.Cells[1].Text;
                    actualizarNombreCliente(folio);
                }
                Session["folio_p"] = folio;
                if (estatusFolio.Equals("Finalizado"))
                {
                    recrearReporteDeServicioFinalizado(Session["folio_p"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private void actualizarNombreCliente(string folio)
        {
            string nombre = controladorFsr.consultarValorDeCampoPorFolioyUsuario(folio, "NombreCliente");
            controladorFsr.actualizarValorDeCampoPorFolioYUsuario(folio, "NombreCliente", nombre);
            ReportViewer1.ServerReport.Refresh();
        }

        protected void recrearReporteDeServicioFinalizado(string folio)
        {
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
            serverReport.ReportPath = "/OC/FSR Servicio";

            ReportParameter salesOrderNumber = new ReportParameter();
            salesOrderNumber.Name = "folio";
            salesOrderNumber.Values.Add(folio);

            ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { salesOrderNumber });
            ReportViewer1.ShowParameterPrompts = false;

            string año = DateTime.Now.Year.ToString();
            string nombre = "Folio:" + folio + "_" + año + ".pdf";

            ReportViewer1.ServerReport.Refresh();
            crearReportePDF(nombre);
        }

        private void  crearReportePDF(string nombre)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + nombre);
            Response.BinaryWrite(bytes); 
            Response.Flush(); 
        }
    }
}
