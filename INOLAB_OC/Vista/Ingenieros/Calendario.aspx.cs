using System;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Security.Principal;
using System.Configuration;
using System.Web.UI;
using System.Globalization;
using INOLAB_OC.Modelo;
using System.Diagnostics;
using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo.Browser;

public partial class Calendario : Page
{
    static UsuarioRepository repositorioUsuario = new UsuarioRepository();
    C_Usuario controladorUsuario = new C_Usuario(repositorioUsuario);
    string idAreaIngeniero = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["idUsuario"] == null)
        {
            Response.Redirect("/Vista/Sesion.aspx");
        }
        else
        {
            lbluser.Text = Session["nameUsuario"].ToString();
            ReportViewer1.Visible = true;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["fecha1"].ToString().Equals(""))
            {
                asignarFechasAlCalendario();
            }
            generarCalendarioDeServicios();
        }
    }

    public void asignarFechasAlCalendario()
    {
        DateTime fechaDelDiaActual = DateTime.Now;
        DateTime fechaLunesSemanaActual = DateTime.MinValue;

        fechaLunesSemanaActual = fechaDelDiaActual.AddDays(1 - Convert.ToDouble(fechaDelDiaActual.DayOfWeek));
        DateTime fechaDesdeSemana = fechaLunesSemanaActual.Date;
        string fechaDomingoSemanaActual = fechaDesdeSemana.AddDays(6).ToString();

        Session["fecha1"] = fechaLunesSemanaActual.Date.ToString("dd/MM/yyyy");
        Session["fecha2"] = fechaDomingoSemanaActual;
    }

    public void generarCalendarioDeServicios()
    {
        idAreaIngeniero = controladorUsuario.consultarValorDeCampo("IngArea", Session["idUsuario"].ToString());

        ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
        ReportViewer1.ProcessingMode = ProcessingMode.Remote;
        ServerReport serverReport = ReportViewer1.ServerReport;

        // Set the report server URL and report path
        serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
        //Ruta de reportViewer hacia el reporte por area
        serverReport.ReportPath = "/Servicio/Calendario-Servicio_x_Area";

      
        ReportParameter[] salesOrderNumber = new ReportParameter[3];
       
        salesOrderNumber[0] = new ReportParameter("fecha1", Session["fecha1"].ToString());
        salesOrderNumber[1] = new ReportParameter("fecha2", Session["fecha2"].ToString());
        salesOrderNumber[2] = new ReportParameter("Area", idAreaIngeniero);

        ReportViewer1.ServerReport.SetParameters(salesOrderNumber);
        ReportViewer1.ShowParameterPrompts = false;
    }

    [Serializable]

    public sealed class MyReportServerCredentials :
        IReportServerCredentials
    {//Inicializa el reporteador con las credenciales almacenadas en la configuración
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

    protected void Cerrar_sesion_Click(object sender, EventArgs e)
    {
        Session["fecha1"] = "";
        Session["fecha2"] = "";
        Response.Redirect("./ServiciosAsignados.aspx");
    }

    protected void Verificar_semana_antes_de_fecha_consulta_Click(object sender, EventArgs e)
    {
        Session["fecha1"] = (Convert.ToDateTime(Session["fecha1"].ToString()).AddDays(-7)).ToString();
        Session["fecha2"] = (Convert.ToDateTime(Session["fecha2"].ToString()).AddDays(-7)).ToString();

        Response.Redirect("./Calendario.aspx");
    }

    protected void Verificar_semana_despues_de_fecha_consulta_Click(object sender, EventArgs e)
    {
        Session["fecha1"] = (Convert.ToDateTime(Session["fecha1"].ToString()).AddDays(7)).ToString();
        Session["fecha2"] = (Convert.ToDateTime(Session["fecha2"].ToString()).AddDays(7)).ToString();
        Response.Redirect("./Calendario.aspx");
    }

    protected void mes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //En caso de que se quiera seleccionar algun mes y mostrar todos los servicios registrados en el
        //En caso de ser año bisiesto, se debera de cambiar el 28 de febrero a 29  -----------------------------------------
        if (mes.Text == "Enero")
        {
            Session["fecha1"] = "01/01/2023";
            Session["fecha2"] = "30/01/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Febrero")
        {
            Session["fecha1"] = "01/02/2023";
            Session["fecha2"] = "28/02/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Marzo")
        {
            Session["fecha1"] = "01/03/2023";
            Session["fecha2"] = "31/03/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Abril")
        {
            Session["fecha1"] = "01/04/2023";
            Session["fecha2"] = "30/04/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Mayo")
        {
            Session["fecha1"] = "01/05/2023";
            Session["fecha2"] = "31/01/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Junio")
        {
            Session["fecha1"] = "01/06/2023";
            Session["fecha2"] = "30/06/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Julio")
        {
            Session["fecha1"] = "01/07/2023";
            Session["fecha2"] = "31/07/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Agosto")
        {
            Session["fecha1"] = "01/08/2023";
            Session["fecha2"] = "31/08/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Septiembre")
        {
            Session["fecha1"] = "01/09/2023";
            Session["fecha2"] = "30/09/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Octubre")
        {
            Session["fecha1"] = "01/10/2023";
            Session["fecha2"] = "31/10/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Noviembre")
        {
            Session["fecha1"] = "01/11/2023";
            Session["fecha2"] = "30/11/2023";
            Response.Redirect("./Calendario.aspx");
        }
        if (mes.Text == "Diciembre")
        {
            Session["fecha1"] = "01/12/2023";
            Session["fecha2"] = "31/12/2023";
            Response.Redirect("./Calendario.aspx");
        }
    }

   
}