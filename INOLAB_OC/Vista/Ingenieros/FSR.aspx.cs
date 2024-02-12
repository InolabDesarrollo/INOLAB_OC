using System;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Web.UI;
using System.Web;
using System.IO;
using static INOLAB_OC.DescargaFolio;
using INOLAB_OC.Modelo;
using System.Data;
using System.Linq.Expressions;
using DocumentFormat.OpenXml.Bibliography;
using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Vista.Ingenieros;
using System.Net.Mail;
using INOLAB_OC.Vista;

public partial class FSR : Page
{
    const string FINALIZADO = "3";
    const string ASIGNADO = "1";
    const string PROCESO = "2";
    E_Servicio folioServicioFSR = new E_Servicio();
    static FSR_Repository repositorio = new FSR_Repository();
    static string _idUsuario;
     
    C_FSR controladorFSR = new C_FSR(repositorio, _idUsuario);

    protected void Page_Init(object sender, EventArgs e)
    {
        _idUsuario = Session["idUsuario"].ToString();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        llenarDatosDeServicioEnElPanel();
        definirVisibilidadYTextoDeBotonesPrincipales();
    }
    public void llenarDatosDeServicioEnElPanel()
    {
        if (Session["idUsuario"] == null)
        {
            Response.Redirect("/Sesion.aspx");
        }
        else
        {
            //Codigo para que aparezca el titulo, sin error de repeticion del numero del folio
            titulo.Text = "Información de Servicio con numero de folio N°. " + Session["folio_p"].ToString();
            Nombre_de_usuario.Text = Session["nameUsuario"].ToString();
        }
        consultaDatosFolioServicio();
    }
    
    public void consultaDatosFolioServicio()
    {
        DataRow informacionServicio =controladorFSR.consultarInformacionFolioServicioPorFolioYUsuario( Session["idUsuario"].ToString(),Session["folio_p"].ToString());

        txtfolio.Text= informacionServicio["Cliente"].ToString();
        txttelfax.Text= informacionServicio["Telefono"].ToString();
        txtdireccion.Text= informacionServicio["Direccion"].ToString();
        txtlocalidad.Text= informacionServicio["Localidad"].ToString();
        txtdepto.Text= informacionServicio["Departamento"].ToString();
        txtnresponsable.Text= informacionServicio["N_Responsable"].ToString();
        txtreportadopor.Text= informacionServicio["N_Reportado"].ToString();
        txtemail.Text = informacionServicio["Mail"].ToString();
        txtdescripcion.Text= informacionServicio["Equipo"].ToString();
        txtmarca.Text= informacionServicio["Marca"].ToString();
        txtmodelo.Text= informacionServicio["Modelo"].ToString();
        txtnoserie.Text= informacionServicio["NoSerie"].ToString();
        txtid.Text= informacionServicio["IdEquipo_C"].ToString();
            
        idservicio.SelectedValue = informacionServicio["idservicio"].ToString();
        idcontrato.SelectedValue = informacionServicio["idcontrato"].ToString();
        idproblema.SelectedValue = informacionServicio["idproblema"].ToString();

        datepicker.Text = informacionServicio["FechaServicio"].ToString();
        DropDownList7.SelectedValue = informacionServicio["HoraServicio"].ToString();
        string test = informacionServicio["IdIngeniero"].ToString();

        try{
            cmding.Text = test;
           }
        catch(Exception ex) { 
            Console.WriteLine(ex.Message);
        }
        DropDownList8.Text = informacionServicio["Estatusid"].ToString();
        Estatus_de_folio_servicio.Text= informacionServicio["Estatusid"].ToString();

        Btn_Estatus_Servicio.Text = controladorFSR.verificarSiIniciaOContinuaServicio(Session["folio_p"].ToString());
    }

    public void definirVisibilidadYTextoDeBotonesPrincipales()
    {
        string status = Estatus_de_folio_servicio.Text;
        if (status.Equals(FINALIZADO))
        {
            Btn_Estatus_Servicio.Text = "Guardar Cambios";
            Btn_actualizar_fechas.Visible = true;
            Btn_agregar_acciones.Visible = true;
            Btn_Reenviar_Correo.Visible = true; //cambio de prueba
        }
        else if (status.Equals(PROCESO))
        {
            Btn_Estatus_Servicio.Text = "Continuar Servicio";
            Btn_actualizar_fechas.Visible = false;
            Btn_agregar_acciones.Visible = false;
        }
        else if(status == ASIGNADO)
        {
            Btn_Estatus_Servicio.Text = "Iniciar Servicio";
            Btn_Reenviar_Correo.Visible = false;
        }
        else if (idservicio.SelectedValue.ToString() == "4" || idservicio.SelectedValue.ToString() == "8" ||
            idservicio.SelectedValue.ToString() == "9")
        {
            //En ciertos tipos de servicio, no se podra hacer el cambio de su estado debido a que no deben de poder cambiarlo libremente los ingenieros
            idservicio.Enabled = false;
        }
        else
        {
            idservicio.Enabled = true;
        }
        int usuario;
        usuario = Convert.ToInt32(Session["idUsuario"]);

        if (usuario == 71)
        {
            btnadjuntar.Visible = false;
        }
        else
        {
            btnadjuntar.Visible = false;
        }
    }
    
    protected void Actualizar_Datos_Servicio_Click(object sender, EventArgs e)
    {
        actualizarDatosDelFolioServicio();
    }
    private void actualizarDatosDelFolioServicio()
    {
        folioServicioFSR.Folio = int.Parse(Session["folio_p"].ToString());
        folioServicioFSR.Marca = marcaHF.Value;
        folioServicioFSR.Modelo = modeloHF.Value;
        folioServicioFSR.NoSerie = noserieHF.Value;
        folioServicioFSR.DescripcionEquipo = descripcionHF.Value;

        folioServicioFSR.IdEquipo = idHF.Value;
        string folio = tcontratoHF.Value;

        folioServicioFSR.IdContrato = int.Parse(tcontratoHF.Value);
        folioServicioFSR.IdProblema = int.Parse(tproblemaHF.Value);
        folioServicioFSR.idServicio = int.Parse(tservicioHF.Value);

        folioServicioFSR.Direccion = direccionHF.Value;
        folioServicioFSR.Cliente = clienteHF.Value;
        folioServicioFSR.Departamento = deptoHF.Value;
        folioServicioFSR.Localidad = localidadHF.Value;

        folioServicioFSR.Telefono = TelefonoHF.Value;
        folioServicioFSR.N_Responsable = responsableHF.Value;
        folioServicioFSR.N_Reportado = reportadoHF.Value;
        folioServicioFSR.Email = emailHF.Value;
        controladorFSR.actualizarDatosDeServicio(folioServicioFSR);
        verificarEstatusDeFolio(Estatus_de_folio_servicio.Text);
    }

    private void verificarEstatusDeFolio(string estatusDeFolio)
    {
        switch (estatusDeFolio)
        {
            case ASIGNADO:
                floatsection.Style.Add("display", "block");
                headerone.Style.Add("filter", "blur(9px)");
                cuerpo.Style.Add("display", "none");
                reportdiv.Style.Add("display", "none");
                break;

            case PROCESO:
                recreatePdfParaServicioFinalizado(Session["folio_p"].ToString());
                Response.Redirect("./DetalleFSR.aspx");
                break;
            case FINALIZADO:
                recreatePdfParaServicioFinalizado(Session["folio_p"].ToString());
                Response.Redirect("./FSR.aspx");
                break;
        }       
    }

    protected void recreatePdfParaServicioFinalizado(string folio)
    {
        ServerReport serverReport = ReportViewer1.ServerReport;
        serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
        serverReport.ReportPath = "/OC/FSR Servicio";

        ReportParameter salesOrderNumber = new ReportParameter();
        salesOrderNumber.Name = "folio";
        salesOrderNumber.Values.Add(folio);

        ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { salesOrderNumber });
        ReportViewer1.ShowParameterPrompts = false;

        string month = DateTime.Now.Month.ToString();
        string year = DateTime.Now.Year.ToString();
        string nombre = "Folio:" + folio + "_" + year + ".pdf";
        crearReportePdf(nombre);
    }

    private void crearReportePdf(string nombre)
    { 
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;

        byte[] bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

        if (Estatus_de_folio_servicio.Text.Equals(FINALIZADO))
        {
            string nombre_archivo = Session["folio_p"].ToString();
            string fechaDeActualizacion = DateTime.Now.ToString("dd-MM-yyyy HH_mm");

            string filepath = HttpRuntime.AppDomainAppPath + "Docs\\" + nombre_archivo + " " + fechaDeActualizacion + ".pdf";
            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                Console.Write(fs.Name);
                fs.Dispose();
            }
        }
    }

    protected void Iniciar_Folio_De_Servicio_Click(object sender, EventArgs e)
    {
        if (Fecha_inicio_servicio.Text.ToString().Equals(""))
        {
            Response.Write("<script>alert('Favor de seleccionar alguna fecha para la el inicio del folio');</script>");
        }
        else
        { 
            DateTime fechaYhoraDeInicioDeServicio = generarFechaYHoraDeInicioDeServicio();
            controladorFSR.iniciarFolioServicio(fechaYhoraDeInicioDeServicio, 
                Session["folio_p"].ToString());            
            actualizarFolioActividadSap();
        }
    }

    public void actualizarFolioActividadSap()
    {
       C_FSR.actualizarFolioSap(Session["folio_p"].ToString());
       Response.Redirect("./DetalleFSR.aspx");
    }
    
    public DateTime generarFechaYHoraDeInicioDeServicio()
    {
        string fechaInicioServicio = Convert.ToDateTime(Fecha_inicio_servicio.Text.ToString()).ToString("yyyy-MM-dd");
        int horaInicioServicio = Convert.ToInt32(horainicial.SelectedItem.ToString());
        int minutoInicioServicio = Convert.ToInt32(mininicial.SelectedItem.ToString());

        string fechaYhoraInicioServicio = fechaInicioServicio + " " + horaInicioServicio.ToString() + ":" + minutoInicioServicio.ToString();
        DateTime inicioDeServicio = DateTime.Parse(fechaYhoraInicioServicio);
        return inicioDeServicio;
    }

    protected void Actualizar_fechaYhora_Servicio_Click(object sender, EventArgs e)
    {
        DateTime fechaYhoraInicioDeFolio = traerFechaYHoraDeInicioDeFolio();
        DateTime fechaYhoraCierreFolio = traerFechaYHoraDeCierreDeFolio();

        datepicker2.Text = fechaYhoraInicioDeFolio.ToString("yyyy-MM-dd");
        datepicker3.Text = fechaYhoraCierreFolio.ToString("yyyy-MM-dd");

        Hora_inicio_folio.SelectedValue = fechaYhoraInicioDeFolio.ToString("HH");
        Minuto_inicio_folio.SelectedValue = fechaYhoraInicioDeFolio.ToString("mm");

        Hora_fin_folio.SelectedValue = fechaYhoraCierreFolio.ToString("HH");
        Minuto_fin_folio.SelectedValue = fechaYhoraCierreFolio.ToString("mm");

        //Se abre ventana emergente para colocar 
        Actfechas.Style.Add("display", "block");
        headerone.Style.Add("filter", "blur(9px)");
        cuerpo.Style.Add("display", "none");
        reportdiv.Style.Add("display", "none");
    }

    protected void Finalizar_Click(object sender, EventArgs e)
    {   
        string fechaYhoraInicio = datepicker2.Text.ToString() + " " + Hora_inicio_folio.SelectedValue.ToString() + ":" + Minuto_inicio_folio.SelectedValue.ToString();
        DateTime fechaYhoraInicioFolioServicio = DateTime.Parse(fechaYhoraInicio);
        string fechaYhoraFin = datepicker3.Text.ToString() + " " + Hora_fin_folio.SelectedValue.ToString() + ":" + Minuto_fin_folio.SelectedValue.ToString();
        DateTime fechaYhoraFinFolioServicio = DateTime.Parse(fechaYhoraFin);

        E_Servicio folioServicio = new E_Servicio();
        folioServicio.FechaInicio = fechaYhoraInicioFolioServicio.ToString("yyyy - MM - dd HH: mm:ss.fff");
        folioServicio.FechaFin = fechaYhoraFinFolioServicio.ToString("yyyy - MM - dd HH: mm:ss.fff");
        folioServicio.Folio = int.Parse(Session["folio_p"].ToString());

        controladorFSR.actualizarFechayHoraFinDeServicio(folioServicio);

        Actfechas.Style.Add("display", "none");
        headerone.Style.Add("filter", "blur(0px)");
        cuerpo.Style.Add("display", "block");
        reportdiv.Style.Add("display", "block");
    }

    private DateTime traerFechaYHoraDeInicioDeFolio()
    { 
       DateTime fechaYhoraDeInicioDeFolioServicio = controladorFSR.traerFechaYhoraDeInicioDeFolio(Session["folio_p"].ToString());
       lbl_fechaYhora_inicio_servicio.Text = fechaYhoraDeInicioDeFolioServicio.ToString();
       return fechaYhoraDeInicioDeFolioServicio;
    }

    private DateTime traerFechaYHoraDeCierreDeFolio()
    {
      DateTime fechaYHoraFinDeFolioDeServicio = controladorFSR.traerFechaYhoraDeFinDeFolio(Session["folio_p"].ToString());
      Lbl_fin_de_servicio.Text = fechaYHoraFinDeFolioDeServicio.ToString();
      return fechaYHoraFinDeFolioDeServicio;
    }

    protected void Actualizar_Acciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("./DetalleFSR.aspx");
    }

    protected void Reenviar_Correo_Click(object sender, EventArgs e)
    {
        string folioFSR = Session["folio_p"].ToString();
        string ubicacionArchivo = crearPDF(folioFSR);
        enviarCorreoACliente(folioFSR, ubicacionArchivo);
    }
    private string crearPDF(string folioFSR)
    {
        DocumentoPDF pdf = new DocumentoPDF(folioFSR);
        return pdf.crearReporteFinalFSR();
    }
    private void enviarCorreoACliente(string folioFSR,string ubicacionArchivo)
    {
        string asunto = "FSR folio " + folioFSR;
        string correoElectronicoEmisor = "notificaciones@inolab.com";
        string correoCliente =  txtemail.Text;
        string contraseña = "Notificaciones2021*";
        try
        {
            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress(correoElectronicoEmisor);
            mensaje.To.Add(correoCliente);
            mensaje.Subject = asunto;
            mensaje.Body = cuerpoDelCorreoElectronicoParaCliente(folioFSR,clienteHF.Value);
            mensaje.IsBodyHtml = true;            
            Attachment attach = new Attachment(ubicacionArchivo);
            mensaje.Attachments.Add(attach);

            CorreoElectronico correoElectronico = new CorreoElectronico(correoElectronicoEmisor, contraseña);
            correoElectronico.enviar(mensaje);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private string cuerpoDelCorreoElectronicoParaCliente(string folioDeServicio, string cliente)
    {
        string cuerpoDelCorreo = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("/HTML/index2.html")))
        {
            cuerpoDelCorreo = reader.ReadToEnd();
            reader.Dispose();
        }
        cuerpoDelCorreo = cuerpoDelCorreo.Replace("{folio}", folioDeServicio);
        cuerpoDelCorreo = cuerpoDelCorreo.Replace("{cliente}", cliente);
        cuerpoDelCorreo = cuerpoDelCorreo.Replace("{slogan}", "data:image/png;base64," + convertirImagenAStringBase64(Server.MapPath("/Imagenes/slogan.png")));
        return cuerpoDelCorreo;
    }

    protected static string convertirImagenAStringBase64(string imgPath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
        string base64String = Convert.ToBase64String(imageBytes);
        return base64String;
    }

    protected void Adjuntar_Archivos_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Adjuntar.aspx");
    }

    
}