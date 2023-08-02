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

public partial class VistaPrevia : Page
{
    static string idUsuario;
    static SCL5Repository repositorioSCL5 = new SCL5Repository();
    C_SCL5 controladorSCL5;

    protected void Page_Load(object sender, EventArgs e)
    {
        controladorSCL5 = new C_SCL5(repositorioSCL5);
        idUsuario = Session["idUsuario"].ToString();
        cargarDatosInicialesDeUsuario();
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
            // Set the processing mode for the ReportViewer to Remote
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;

            ServerReport serverReport = ReportViewer1.ServerReport;

            // Set the report server URL and report path
            serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
            serverReport.ReportPath = "/OC/FSR Servicio v2";

            // Create the sales order number report parameter
            ReportParameter salesOrderNumber = new ReportParameter();
            salesOrderNumber.Name = "folio";
            salesOrderNumber.Values.Add(Session["folio_p"].ToString());

            // Set the report parameters for the report
            ReportViewer1.ServerReport.SetParameters( new ReportParameter[] { salesOrderNumber });
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

    protected void Mostrar_pantalla_para_firma_de_documento_Click(object sender, EventArgs e)
    {
        avisopriv.Style.Add("display", "block");
        headerid.Style.Add("display", "none");
        sectionreport.Style.Add("display", "none");
        footerid.Style.Add("display", "none");
    }

    protected void Btn_Aceptar_aviso_de_privacidad(object sender, EventArgs e)
    {
        avisopriv.Style.Add("display", "none");
        ulfol.Text = Session["folio_p"].ToString();
        ul_advert.Style.Add("display", "block");
    }

    protected void Mostrar_ventana_de_firma_de_usuario(object sender, EventArgs e)
    {
        ul_advert.Style.Add("display", "none");
        firma.Style.Add("display", "block");
        string script = "startFirma();";
        ClientScript.RegisterStartupScript(GetType(), "Star", script, true);
    }

    protected void Btn_guardar_firmar_Click(object sender, EventArgs e)
    {
        string image = hidValue.Value;
        string nombre = textboxnombre.Text;
        firma.Style.Add("display", "none");
        headerid.Style.Add("display", "block");
        sectionreport.Style.Add("display", "block");
        footerid.Style.Add("display", "flex");
        if (nombre.Length < 1)
        {
            nombre = "N/A";
        }
        if(insertarFirma(nombre, image))
        {
            actualizarNombreDeClienteYFechaEnQueFirma(nombre);
            ReportViewer1.ServerReport.Refresh();
        }
    }

    protected void actualizarNombreDeClienteYFechaEnQueFirma(string nombre)
    { 
        controladorFSR.actualizarValorDeCampoPorFolioYUsuario(Session["folio_p"].ToString(), "NombreCliente", nombre);
        controladorFSR.actualizarValorDeCampoPorFolioYUsuario(Session["folio_p"].ToString(), "FechaFirmaCliente",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
    }

    protected bool insertarFirma(string nombreDeImagen, string imagen)
    {
        try
        {
            string[] images = imagen.Split(',');
            string pattern = @"[^:\s*]\w+\/[\w-+\d.]+(?=[;| ])";
            string tipoDeImagen = "";
            string img1 = images[0];
            string img2 = images[1];
            Regex rx = new Regex(pattern);
            Match m = rx.Match(img1);
            if (m.Success)
                tipoDeImagen = m.Value;
           
            int idFirmaImagen = Conexion.insertarFirmaImagen(nombreDeImagen, tipoDeImagen, img2);
            if (idFirmaImagen != 0)
            {
                controladorFSR.actualizarValorDeCampoPorFolio(Session["folio_p"].ToString(), "IdFirmaImg", Convert.ToString(idFirmaImagen));
                return true;
            }
            else
            {       
                return false;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error al cargar la información');</script>");
            Console.Write(ex.ToString());
            return false;
        }
    }

    protected void Finalizar_reporte_Click(object sender, EventArgs e)
    {
        int firmaCliente = 0;
        int firmaIngeniero = 0;
        firmaCliente = verificarSiSeAgregoFirmaDeCliente();
        firmaIngeniero = verificarSiSeAgregoFirmaDeIngeniero();

        if (firmaCliente <= 0 )
        {    
            Response.Write("<script>alert('Falta firma de cliente');</script>");
            
        }else if (firmaIngeniero <= 0)
        {
            Response.Write("<script>alert('Falta firma de Ingeniero');</script>");
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

    private int verificarSiSeAgregoFirmaDeCliente()
    {
        int firmaCliente = -1;
        try
        {
            firmaCliente = Convert.ToInt32(controladorFSR.consultarValorDeCampoPorFolioyUsuario(Session["folio_p"].ToString(), "IdFirmaImg"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return firmaCliente;
    }

    private int verificarSiSeAgregoFirmaDeIngeniero()
    {
        int firmaIngeniero =-1;
        try
        {
            firmaIngeniero = Convert.ToInt32(controladorFSR.consultarValorDeCampoPorFolioyUsuario(Session["folio_p"].ToString(), "IDFirmaIng"));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return firmaIngeniero;
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
        catch (Exception et)
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
   
}