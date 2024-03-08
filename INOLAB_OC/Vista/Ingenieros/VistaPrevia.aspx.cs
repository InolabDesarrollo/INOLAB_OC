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
using INOLAB_OC.Modelo;
using System.Web.Services.Description;
using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo.Browser;
using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Modelo.Inolab;
using INOLAB_OC.Vista.Ingenieros;
using INOLAB_OC;
using DocumentFormat.OpenXml.Wordprocessing;

public partial class VistaPrevia : Page
{
    private static string idUsuario;
    private static string folioServicio;
    private static SCL5Repository repositorioSCL5 = new SCL5Repository();
    private C_SCL5 controladorSCL5;
    private Firma firmaReporte;
    protected void Page_Load(object sender, EventArgs e)
    {
        controladorSCL5 = new C_SCL5(repositorioSCL5);
        idUsuario = Session["idUsuario"].ToString();
        folioServicio = Session["folio_p"].ToString();
        cargarDatosInicialesDeUsuario();
        firmaReporte = new Firma(idUsuario, folioServicio);
    }
    static FSR_Repository reposiorioFSR = new FSR_Repository();
    C_FSR controladorFSR = new C_FSR(reposiorioFSR, idUsuario);

    private void cargarDatosInicialesDeUsuario()
    {
        if (Session["idUsuario"] == null)
        {
            Response.Redirect("/Sesion.aspx"); 
        }
        else
        {
            lbluser.Text = Session["nameUsuario"].ToString();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {       
        if (!Page.IsPostBack)
        {
            ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;

            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
            serverReport.ReportPath = "/OC/FSR Servicio v2";

            ReportParameter salesOrderNumber = new ReportParameter();
            salesOrderNumber.Name = "folio";
            salesOrderNumber.Values.Add(Session["folio_p"].ToString());

            ReportViewer1.ServerReport.SetParameters( new ReportParameter[] { salesOrderNumber });
            ReportViewer1.ShowParameterPrompts = false;
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
                    throw new Exception("Missing user name from web.config file");

                string password =
                    ConfigurationManager.AppSettings
                        ["MyReportViewerPassword"];

                if (string.IsNullOrEmpty(password))
                    throw new Exception("Missing password from web.config file");

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

    protected void Mostrar_pantalla_para_firma_de_documento_Click(object sender, EventArgs e)
    {
        avisopriv.Style.Add("display", "block");
        headerid.Style.Add("display", "none");
        sectionreport.Style.Add("display", "none");
        footerid.Style.Add("display", "none");
        Sec_Nombre_Cliente.Style.Add("display", "none");
        firma.Style.Add("display", "none");
    }

    protected void Btn_Aceptar_aviso_de_privacidad(object sender, EventArgs e)
    {
        avisopriv.Style.Add("display", "none");
        ulfol.Text = Session["folio_p"].ToString();
        ul_advert.Style.Add("display", "block");
        firma.Style.Add("display", "none");
    }

    protected void Mostrar_ventana_de_firma_de_usuario(object sender, EventArgs e)
    {
        ul_advert.Style.Add("display", "none");
        Sec_Nombre_Cliente.Style.Add("display", "none");
        firma.Style.Add("display", "block");
        string script = "startFirma();";
        ClientScript.RegisterStartupScript(GetType(), "Star", script, true);
    }

    protected void Btn_guardar_datos_de_cliente_Click(object sender, EventArgs e)
    {
        string firmaDelCliente = hidValue.Value;
        this.mostrarPanelDeFirma();
 
         bool firmaEsValida = firmaReporte.verficarQueFirmaEsValida(firmaDelCliente);
         if (firmaEsValida)
         {
            this.guardarDatosCliente(firmaDelCliente);
         }
         else
         {
             Response.Write("<script>alert('No puedes dejar la firma del cliente vacia');</script>");
         }      
    }

    private void mostrarPanelDeFirma()
    {
        firma.Style.Add("display", "none");
        headerid.Style.Add("display", "block");
        sectionreport.Style.Add("display", "block");
        footerid.Style.Add("display", "flex");
        Sec_Nombre_Cliente.Style.Add("display", "none");
    }

    private void guardarDatosCliente(string firmaDelCliente)
    {
        bool seActualizoFirmaDeCliente = firmaReporte.actualizarFirmaCliente("firma", firmaDelCliente);
        if (seActualizoFirmaDeCliente)
        {
            controladorFSR.actualizarValorDeCampoPorFolioYUsuario(folioServicio, "FechaFirmaCliente", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            ReportViewer1.ServerReport.Refresh();
        }
    }

    protected void Finalizar_reporte_Click(object sender, EventArgs e)
    {
        int firmaCliente =  firmaReporte.verificarSiSeAgregoFirmaDeCliente();
        int firmaIngeniero = firmaReporte.verificarSiSeAgregoFirmaDeIngeniero();
        bool seGuardoNombreDeCliente = firmaReporte.verificarQueSeGuardoNombreDeCliente(folioServicio);
        Sec_Nombre_Cliente.Style.Add("display", "none");

        if (firmaCliente <= 0 )
        {    
            Response.Write("<script>alert('Falta firma de cliente');</script>");            
        }
        if (firmaIngeniero <= 0)
        {
            Response.Write("<script>alert('Falta firma de Ingeniero');</script>");
        }
        if (!seGuardoNombreDeCliente)
        {
            Response.Write("<script>alert('Falta registrar nombre del cliente');</script>");
        }
        else
        {
            floatsection.Style.Add("display", "block");
            headerone.Style.Add("filter", "blur(9px)");
            sectionreport.Style.Add("display", "none");
            reportdiv.Style.Add("display", "none");
            footerid.Style.Add("display", "none");
        }
    }
 
    protected void firmaing_Click(object sender, EventArgs e)
    {
        Response.Redirect("FirmarFolio.aspx");
    }

    protected void Finalizar_folio_Click(object sender, EventArgs e)
    {
        if (datepicker.Text.ToString() == "")
        {
            Response.Write("<script>alert('Favor de seleccionar alguna fecha para la finalización del folio');</script>");
        }
        else
        {
            DateTime fechaYhoraFinDeFolio = getFechaYhoraDeFinDeFolio();
            DateTime fechaYhoraInicioServicio = getFechaYhoraInicioDeServicio();
            verificarQueFechaInicioDeFolioSeaMenorAfechaFinDeServicio(fechaYhoraInicioServicio, fechaYhoraFinDeFolio);
        }
    }

    public DateTime getFechaYhoraDeFinDeFolio()
    {
        string fechaDeFolio = Convert.ToDateTime(datepicker.Text.ToString()).ToString("yyyy-MM-dd");
        int hora = Convert.ToInt32(horafinal.SelectedItem.ToString());
        int minuto = Convert.ToInt32(minfinal.SelectedItem.ToString());
        string fechaCompletaYhoraDeCierrDeFolio = fechaDeFolio + " " + hora.ToString() + ":" + minuto.ToString();  
        return  DateTime.Parse(fechaCompletaYhoraDeCierrDeFolio);
    }
    public DateTime getFechaYhoraInicioDeServicio()
    {
        DateTime fechaInicioDeServicio = consultarFechaInicioFolioServicio();
        return  DateTime.Parse(fechaInicioDeServicio.ToString("yyyy-MM-dd HH:mm:ss.fff"));
    }

    private DateTime consultarFechaInicioFolioServicio()
    {
        try
        {
            DateTime fechaSolucion = Convert.ToDateTime(controladorFSR.consultarValorDeCampoPorFolioyUsuario(Session["folio_p"].ToString(), "WebFechaIni"));
            return fechaSolucion;
        }
        catch
        {
            DateTime fecha_sol = Convert.ToDateTime(controladorFSR.consultarValorDeCampoPorFolioyUsuario("Inicio_Servicio", Session["folio_p"].ToString()));
            controladorFSR.actualizarValorDeCampoPorFolioYUsuario(Session["folio_p"].ToString(), "WebFechaIni", fecha_sol.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            return fecha_sol;
        }
    }
   
    public void verificarQueFechaInicioDeFolioSeaMenorAfechaFinDeServicio(DateTime fechaInicioServicio, DateTime fechaFinServicio)
    {
        int resultadoComparacion = DateTime.Compare(fechaInicioServicio, fechaFinServicio);
        if (resultadoComparacion > 0)
        {
            Response.Write("<script>alert('La fecha de fin de folio no puede ser anterior a la fecha de inicio del folio');</script>");
        }
        else
        {
            controladorFSR.actualizarValorDeCampoPorFolioYUsuario(Session["folio_p"].ToString(), "WebFechaFin", 
                fechaFinServicio.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            Response.Redirect("CargaFin.aspx");
        }
    }

    protected void Reporte_Refacciones(object sender, EventArgs e)
    {
        Response.Redirect("ReporteRefacciones.aspx");
    }

    protected void Cerrar_campo_nombre_cliente_Click(object sender, EventArgs e)
    {
        Sec_Nombre_Cliente.Style.Add("display", "none");
        sectionreport.Style.Add("display", "block");
        headerid.Style.Add("display", "block");
        footerid.Style.Add("display", "flex");
        ReportViewer1.ServerReport.Refresh();
    }

    protected void Btn_Agregar_Nombre_Cliente_Click(object sender, EventArgs e)
    {
        Sec_Nombre_Cliente.Style.Add("display", "block");
        sectionreport.Style.Add("display", "none");
        firma.Style.Add("display", "none");
    }

    protected void Guardar_nombre_cliente_Click(object sender, EventArgs e)
    {
        if (TxtBox_Nombre_Cliente.Text != "")
        {

        }    
    }

}