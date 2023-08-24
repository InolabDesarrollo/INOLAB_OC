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
using DocumentFormat.OpenXml.Drawing.Charts;
using INOLAB_OC;
using INOLAB_OC.Modelo;
using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Entidades;
using System.Web.Services.Description;
using INOLAB_OC.Modelo.Browser.Interfaces;

public partial class ServiciosAsignados : System.Web.UI.Page
{
    static V_FSR_Repository repositorio = new V_FSR_Repository();
    C_V_FSR controlador_Vista_ServiciosAsignados = new C_V_FSR(repositorio);
    E_V_FSR entidad_VistaFsr = new E_V_FSR();

    static UsuarioRepository repositorioUsuario = new UsuarioRepository();
    C_Usuario controladorUsuario = new C_Usuario(repositorioUsuario);

    string idUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
        idUsuario = Session["idUsuario"].ToString();
        if (Session["idUsuario"] == null) {
            Response.Redirect("/Sesion.aspx");
        }
        else {
            lbluser.Text = Session["nameUsuario"].ToString();
            if (controladorUsuario.validarSiUsuarioEsGefeDeArea(idUsuario))
            {
                Btn_Calendario.Visible = true;
                Btn_ReporteRefacciones.Visible = true;
            }
            else
            {
                Btn_ReporteRefacciones.Visible = false;
                Btn_Calendario.Visible = false;
            }
        }
        btninformacion.Visible = false;
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
                    throw new Exception(
                        "Missing user name from web.config file");

                string password =
                    ConfigurationManager.AppSettings
                        ["MyReportViewerPassword"];

                if (string.IsNullOrEmpty(password))
                    throw new Exception(
                        "Missing password from web.config file");


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

  
    protected void Gridview_datos_de_servicio_OnRowComand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow filasDelDataGridView = Gridview_datos_de_servicio.Rows[index];
            string numeroDeFolioDeServicio = "";
            string estatusDelServicio = "";
            entidad_VistaFsr.FechaEnQueSeAgendoServicio = filasDelDataGridView.Cells[4].Text;

            if (e.CommandName == "Select")
            {
                numeroDeFolioDeServicio = ((LinkButton)filasDelDataGridView.Cells[0].Controls[0]).Text;
                estatusDelServicio = ((LinkButton)filasDelDataGridView.Cells[1].Controls[0]).Text;
                Session["folio_p"] = numeroDeFolioDeServicio;
                Session["not_ase"] = "";
               
                if (estatusDelServicio.Equals("Asignado"))
                {
                    verificarSiServicioTieneFechaActual(entidad_VistaFsr);
                }
                else
                {
                    Response.Redirect("FSR.aspx", true);
                }
            }
            if (e.CommandName == "Select2")
            {
                numeroDeFolioDeServicio = ((LinkButton)filasDelDataGridView.Cells[0].Controls[0]).Text;
                estatusDelServicio = ((LinkButton)filasDelDataGridView.Cells[1].Controls[0]).Text;
                Session["folio_p"] = numeroDeFolioDeServicio;

                if (estatusDelServicio.Equals("Asignado"))
                {
                    generarDocumentoParaReporteServicio();
                    try
                    {
                        //Parte del codigo para pasar el archivo del servidor al equipo del ingeniero
                        using (WebClient client = new WebClient())
                        {
                            //Creacion del archivo de el folio para llenarlo modo offline
                            client.DownloadFile("http://23.102.185.149:8020/Docs/" + Session["folio_p"].ToString() + ".xlsx", 
                                @"C:\INOLAB\Datos Folio\" + Session["Usuario"].ToString() + "_" + Session["folio_p"].ToString() + ".xlsx");
                        }
                    }
                    catch (Exception es)
                    {
                        Console.Write(es.ToString());
                        Response.Write("<script>alert('Error al pasar el archivo al equipo del usuario');</script>");
                    }
                    Response.Write("<script>alert('Información cargada para modo Offline');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Solo se pueden descargar folios con estatus de -Asignado-');</script>");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }
    }

    private void verificarSiServicioTieneFechaActual(E_V_FSR entidad_V_FSR)
    {
        bool fechaDeServicioEsIgualAFechaDeHoy = controlador_Vista_ServiciosAsignados.verificarSiServicioTieneLaFechaDeHoy(entidad_V_FSR);
        if (fechaDeServicioEsIgualAFechaDeHoy)
        {
            Response.Redirect("FSR.aspx", true);
        }
        else
        {
            Response.Write("<script>alert('Error: Fecha de servicio no coincide con el dia de hoy');</script>");
        }
    }

    private void generarDocumentoParaReporteServicio()
    {
        SLDocument documentoReporteServicio = new SLDocument();
        System.Data.DataTable reporteServicio = generarReporteServiciosModoOffLine();

        documentoReporteServicio.ImportDataTable(1, 1, reporteServicio, true);
        string rutaReporteServicio = HttpRuntime.AppDomainAppPath + "Docs\\" + Session["folio_p"].ToString() + ".xlsx";
        documentoReporteServicio.SaveAs(rutaReporteServicio);
    }

    private System.Data.DataTable generarReporteServiciosModoOffLine()
    {
        System.Data.DataTable reporteServicio = new System.Data.DataTable();
        reporteServicio = controlador_Vista_ServiciosAsignados.definirColumnasParaReporteServicio(); 
        List<string> valoresParaReporteServicio = new List<string>();
        DataRow informacionServicios = controlador_Vista_ServiciosAsignados.consultarInformacionDeFolioServicio(Session["folio_p"].ToString());

        for (int i = 0; i == 67; i++)
        {
            string valorColumna = reporteServicio.Columns[i].ColumnName;
            
            if (valorColumna.Equals("F_SolicitudServicio"))
            {
                valoresParaReporteServicio.Insert(i, Convert.ToDateTime(informacionServicios[valorColumna].ToString()).ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
            else if (valorColumna.Equals("FechaServicio"))
            {
                valoresParaReporteServicio.Insert(i, Convert.ToDateTime(informacionServicios[valorColumna].ToString()).ToString("yyyy-MM-dd"));
            }
            else if (i == 28 || i == 29 || i == 40 || i == 66)
            {
                valoresParaReporteServicio.Insert(i, "");
            }
            else
            {
                valoresParaReporteServicio.Insert(i, informacionServicios[valorColumna].ToString());
                reporteServicio.Rows.Add(valoresParaReporteServicio[i]);
            }
        }
        return reporteServicio;
    }

    protected void Reporte_Refacciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("GerenteReporteRefacciones.aspx");
    }

    protected void Btn_Salir_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("/Sesion.aspx");
    }

    protected void Btn_Descrgar_Calendario_De_Servicios_Click(object sender, EventArgs e)
    {
        ServerReport serverReport = ReportViewer1.ServerReport;

        serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
        serverReport.ReportPath = "/Servicio/Calendario-Servicio-Ing";

        ReportParameter salesOrderNumber = new ReportParameter();
        salesOrderNumber.Name = "ing";
        salesOrderNumber.Values.Add(Session["idUsuario"].ToString());

        ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { salesOrderNumber });
        ReportViewer1.ShowParameterPrompts = false;

        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string day = DateTime.Now.Day.ToString();
        string nombre = "Calendario_" + day + "-" + month + "-" + year + ".pdf";
        CrearArchivoPDF(nombre);
    }

    private void CrearArchivoPDF(string nombre)
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
        Response.AddHeader("content-disposition", "attachment; filename="+ nombre);
        Response.BinaryWrite(bytes);  
        Response.Flush(); 
    }

    protected void Tipo_De_Estatus_De_Servicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        entidad_VistaFsr.Estatus= Estatus_de_servicio.Text;
        if (Estatus_de_servicio.Text.Equals("Todos"))
        {
            consultarTodosLosFoliosDeServicio(idUsuario);
        }
        else
        {
            consultarFoliosDeServicioPorEstatus(entidad_VistaFsr, idUsuario);    
        }
    }
   
    public void consultarFoliosDeServicioPorEstatus(E_V_FSR entidadServicio, string idUsuario)
    {
        Gridview_datos_de_servicio.DataSource = controlador_Vista_ServiciosAsignados.consultarFolioServicioPorEstatus(entidadServicio, idUsuario);
        Gridview_datos_de_servicio.DataBind();
        contador.Text = Gridview_datos_de_servicio.Rows.Count.ToString();
    }

    private void consultarTodosLosFoliosDeServicio(string idUsuario)
    {
        Gridview_datos_de_servicio.DataSource = controlador_Vista_ServiciosAsignados.consultarTodosLosFoliosDeIngeniero(idUsuario);
        Gridview_datos_de_servicio.DataBind();
        contador.Text = Gridview_datos_de_servicio.Rows.Count.ToString();
    }

    protected void Btn_Descarga_Folio_De_Servicio_Finalizado_Click(object sender, EventArgs e)
    {
        string _open = "window.open('DescargaFolio.aspx', '_newtab');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
    }

    protected void Btn_Ir_A_Calendario_Click(object sender, EventArgs e)
    {
        Response.Redirect("Calendario.aspx");
    }

    protected void Btn_Manual_De_Usuario_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"\Docs\Manual de Usuario SWF v3.pdf");
    }

    protected void Btn_Ir_A_Seguimiento_De_Serviciod_Click(object sender, EventArgs e)
    {
        string _open = "window.open('Informacion.aspx', '_newtab');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), _open, true);
    }

}