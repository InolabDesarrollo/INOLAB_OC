using System;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Security.Principal;
using System.Configuration;
using System.Web.UI;

namespace INOLAB_OC
{
    public partial class Calendario_Areas : System.Web.UI.Page
    {
        string fecha1;
        string fecha2;
        string Area = "";
        protected void Page_Load(object sender, EventArgs e)
        {
         verificarCorrectoInicioDeSession();
        }

        private void verificarCorrectoInicioDeSession()
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("./Sesion.aspx");
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
                if (Session["fecha1"].ToString() == "")
                {
                    //Se obtiene la fecha actual
                    DateTime dt = DateTime.Now;
                    DateTime wkStDt = DateTime.MinValue;
                    
                    //Se obtiene el lunes de la semana actual
                    wkStDt = dt.AddDays(1 - Convert.ToDouble(dt.DayOfWeek));
                    DateTime fechadesdesemana = wkStDt.Date;
                    fecha1 = fechadesdesemana.ToString("dd/MM/yyyy");

                    //Se obtiene el domingo de la semana actual
                    fecha2 = fechadesdesemana.AddDays(6).ToString();

                    //Se registran las fechas del lunes y el domingo de la semana actual 
                    Session["fecha1"] = fecha1;
                    Session["fecha2"] = fecha2;
                }
                Area = Session["Area"].ToString();

                ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                // Set the processing mode for the ReportViewer to Remote
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;

                ServerReport serverReport = ReportViewer1.ServerReport;

                // Set the report server URL and report path
                serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
                //Acceso a la ruta del reporteador del calendario de servicio por area
                serverReport.ReportPath = "/Servicio/Calendario-Servicio_x_Area";

                // Create the sales order number report parameter
                //este reporte requiere de 3 parametros (Fecha de inicio de la consulta, fecha de fin de la consulta y el id del area)
                ReportParameter[] salesOrderNumber = new ReportParameter[3];
                salesOrderNumber[0] = new ReportParameter("fecha1", Session["fecha1"].ToString());
                salesOrderNumber[1] = new ReportParameter("fecha2", Session["fecha2"].ToString());
                salesOrderNumber[2] = new ReportParameter("Area", Area);


                ReportViewer1.ServerReport.SetParameters(salesOrderNumber);
                ReportViewer1.ShowParameterPrompts = false;
            }
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

        protected void Button1_Click(object sender, EventArgs e)
        {//Se regresa a la ventana de seleccion de area (Vista actualmente solo para Liz)
            Session["fecha1"] = "";
            Session["fecha2"] = "";
            Response.Redirect("CalSel.aspx");
        }

        protected void mes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se hace la busqueda por mes de los servicios resgistrados depende al mes seleccionado
            if (mes.Text == "Enero")
            {
                Session["fecha1"] = "01/01/2022";
                Session["fecha2"] = "30/01/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Febrero")
            {
                Session["fecha1"] = "01/02/2022";
                Session["fecha2"] = "28/02/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Marzo")
            {
                Session["fecha1"] = "01/03/2022";
                Session["fecha2"] = "31/03/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Abril")
            {
                Session["fecha1"] = "01/04/2022";
                Session["fecha2"] = "30/04/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Mayo")
            {
                Session["fecha1"] = "01/05/2022";
                Session["fecha2"] = "31/01/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Junio")
            {
                Session["fecha1"] = "01/06/2022";
                Session["fecha2"] = "30/06/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Julio")
            {
                Session["fecha1"] = "01/07/2022";
                Session["fecha2"] = "31/07/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Agosto")
            {
                Session["fecha1"] = "01/08/2022";
                Session["fecha2"] = "31/08/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Septiembre")
            {
                Session["fecha1"] = "01/09/2022";
                Session["fecha2"] = "30/09/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Octubre")
            {
                Session["fecha1"] = "01/10/2022";
                Session["fecha2"] = "31/10/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Noviembre")
            {
                Session["fecha1"] = "01/11/2022";
                Session["fecha2"] = "30/11/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
            if (mes.Text == "Diciembre")
            {
                Session["fecha1"] = "01/12/2022";
                Session["fecha2"] = "31/12/2022";
                Response.Redirect("./Calendario_Areas.aspx");
            }
        }
        protected void Antes_Click(object sender, EventArgs e)
        {//Se hace una consulta, una semana antes de la actual
            Session["fecha1"] = (Convert.ToDateTime(Session["fecha1"].ToString()).AddDays(-7)).ToString();
            Session["fecha2"] = (Convert.ToDateTime(Session["fecha2"].ToString()).AddDays(-7)).ToString();

            Response.Redirect("./Calendario_Areas.aspx");
        }

        protected void Despues_Click(object sender, EventArgs e)
        {//S hace una consulta, una semana despues de la actual
            Session["fecha1"] = (Convert.ToDateTime(Session["fecha1"].ToString()).AddDays(7)).ToString();
            Session["fecha2"] = (Convert.ToDateTime(Session["fecha2"].ToString()).AddDays(7)).ToString();

            Response.Redirect("./Calendario_Areas.aspx");
        }
    }
}