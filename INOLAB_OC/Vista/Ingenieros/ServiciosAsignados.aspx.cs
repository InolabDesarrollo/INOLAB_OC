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

public partial class ServiciosAsignados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Session["idUsuario"] == null) { 
            Response.Redirect("./Sesion.aspx");
        }
        else {
            lbluser.Text = Session["nameUsuario"].ToString();
            if (validarSiUsuarioEsGefeDeArea())
            {
                Btn_Calendario.Visible = true;
            }
            else
            {
                Btn_Calendario.Visible = false;
            }
        }

        btninformacion.Visible = false;
    }

    public bool validarSiUsuarioEsGefeDeArea()
    {
        if (Session["idUsuario"].ToString() == "54" || Session["idUsuario"].ToString() == "60" ||
                Session["idUsuario"].ToString() == "30")
        {
            return true;
        }
        else
        {
            return false;
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
            serverReport.ReportPath = "/Servicio/Calendario-Servicio-Ing";

            // Create the sales order number report parameter
            ReportParameter salesOrderNumber = new ReportParameter();
            salesOrderNumber.Name = "ing";
            salesOrderNumber.Values.Add(Session["idUsuario"].ToString());

            // Set the report parameters for the report
            ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { salesOrderNumber });
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

            // Not using form credentials
            return false;
        }
    }

  
    protected void Gridview_datos_de_servicio_OnRowComand(object sender, GridViewCommandEventArgs e)
    {
        //Al darle clic al folio deseado este se almacena en la sesión y te redirige a la ventana de FSR
        try
        {

            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow filasDelDataGridView = Gridview_datos_de_servicio.Rows[index];
            string numeroDeFolioDeServicio = "";
            string estatusDelServicio = "";

            if (e.CommandName == "Select")
            {
                numeroDeFolioDeServicio = ((LinkButton)filasDelDataGridView.Cells[0].Controls[0]).Text;
                estatusDelServicio = ((LinkButton)filasDelDataGridView.Cells[1].Controls[0]).Text;
                Session["folio_p"] = numeroDeFolioDeServicio;
                Session["not_ase"] = "";
                if (estatusDelServicio.Equals("Asignado"))
                {
                    verificarSiServicioTieneFechaActual(filasDelDataGridView);
                }
                else
                {
                    Response.Redirect("FSR.aspx", true);
                }
            }
            if (e.CommandName == "Select2")
            {
                //Para el modo offline, genera un archivo con toda la informacion que ya hay del folio para que se llene en el aplicativo accediendo a esta 
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

    private void verificarSiServicioTieneFechaActual(GridViewRow filasDelDataGridView)
    {
        string fechaDelServicio = "";
        string fechaDelDiaActual = "";
        fechaDelServicio = filasDelDataGridView.Cells[4].Text;
        fechaDelDiaActual = DateTime.Now.ToString("dd/MM/yyyy");

        if (fechaDelDiaActual.Equals(fechaDelServicio))
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
        SLDocument documentoSl = new SLDocument();
        System.Data.DataTable reporteServicio = new System.Data.DataTable();

        string[] columnasParaReporteServicio = { "IdFSR", "Folio", "Cliente", "Departamento", "Direccion", "Telefono", "Localidad",
                        "N_Reportado","N_Responsable","Mail","TipoContrato","TipoProblema","TipoServicio","servicio","Ingeniero",
                    "mailIng","F_SolicitudServicio","FechaServicio","Equipo","Marca","Modelo","NoSerie","IdEquipo_C","Estatusid","Estatus","Observaciones",
                    "NoLlamada","Inicio_Servicio","Fin_Servicio","Dia","FallaReportada","HoraServicio","Confirmacion","Propuesta","Actividad","S_Confirmacion",
                    "Asesor1","Correoasesor1","CooreoIng","Proximo_Servicio","idcontrato","idservicio","idproblema","IdResp","Responsable","IdDocumenta","Documentador",
                    "Refaccion","Ingeniero_A1","IdIng_A1","mailIng_A1","Ingeniero_A2","IdIng_A2","mailIng_A2","F_InicioServicio","F_FinServicio","IdT_Servicio","OC","ArchivoAdjunto",
                    "DiaInicioServ","DiaFinServ", "DiasServ","NotAsesor","Funcionando","FallaEncontrada","FechaFirmCliente","NombreCliente"};

        foreach (string columna in columnasParaReporteServicio)
        {
            reporteServicio.Columns.Add(columna, typeof(string));
        }

        List<string> valoresParaReporteServicio = new List<string>();
        DataRow informacionServicios = C_ServiciosAsignados.InformacionDeFolioParaReporteServicios(Session["folio_p"].ToString());


        for (int i = 0; i == 67; i++)
        {
            string valorColumna = columnasParaReporteServicio[i];
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

   

    protected void Btn_Salir_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("../Sesion.aspx");
    }

    protected void Btn_Descrgar_Calendario_De_Servicios_Click(object sender, EventArgs e)
    {
        ServerReport serverReport = ReportViewer1.ServerReport;

        // Set the report server URL and report path
        serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
        serverReport.ReportPath = "/Servicio/Calendario-Servicio-Ing";

        // Create the sales order number report parameter
        ReportParameter salesOrderNumber = new ReportParameter();
        salesOrderNumber.Name = "ing";
        salesOrderNumber.Values.Add(Session["idUsuario"].ToString());

        // Set the report parameters for the report
        ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { salesOrderNumber });
        ReportViewer1.ShowParameterPrompts = false;

        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string day = DateTime.Now.Day.ToString();
        string nombre = "Calendario_" + day + "-" + month + "-" + year + ".pdf";
        CrearArchivoPDF(nombre);
    }

    protected void recrearPDFParaFolioDeServicioFinalizado(string folio)
    {

        ServerReport serverReport = ReportViewer1.ServerReport;
        // Set the report server URL and report path
        serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
        serverReport.ReportPath = "/OC/FSR Servicio";

        // Create the sales order number report parameter
        ReportParameter salesOrderNumber = new ReportParameter();
        salesOrderNumber.Name = "folio";
        salesOrderNumber.Values.Add(folio);

        // Set the report parameters for the report
        ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { salesOrderNumber });
        ReportViewer1.ShowParameterPrompts = false;

        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string nombre = "Folio:" + folio + "_" + year + ".pdf";
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

        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.  
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = mimeType;
        Response.AddHeader("content-disposition", "attachment; filename="+ nombre);
        Response.BinaryWrite(bytes); // create the file  
        Response.Flush(); // send it to the client to download  
    }

    
    protected void Tipo_De_Estatus_De_Servicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        string query = "";
        if(Estatus_de_servicio.Text=="Asignado")
        {
            query = "select *from V_FSR where Estatus='Asignado' and IdIngeniero=" + Session["idusuario"] +" order by folio desc";
            consultarFoliosDeServicio(query);
        }
        if (Estatus_de_servicio.Text == "En Proceso")
        {
            query = "select *from V_FSR where Estatus='En Proceso' and IdIngeniero=" + Session["idusuario"]+" order by folio desc";
            consultarFoliosDeServicio(query);
        }
        if (Estatus_de_servicio.Text == "Finalizado")
        {
            query = "select *from v_fsr where estatus='Finalizado' and idingeniero=" + Session["idusuario"]+ " order by folio desc";
            consultarFoliosDeServicio(query);
        }
        if (Estatus_de_servicio.Text == "Todos")
        {
            query = "Select DISTINCT* from  v_fsr where idingeniero = " + Session["Idusuario"] + " order by folio desc";
            consultarFoliosDeServicio(query);
        }
    }
   

    public void consultarFoliosDeServicio(string query)
    {
        Gridview_datos_de_servicio.DataSource = Conexion.getDataSet(query);
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