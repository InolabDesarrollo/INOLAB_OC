﻿using System;
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
public partial class FSR : Page
{
    const string FINALIZADO = "3";
    const string ASIGNADO = "1";
    const string PROCESO = "2";
    const string SIN_SERVICIO_INICIADO ="";
    E_Servicio folioServicioFSR = new E_Servicio();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        llenarDatosDeServicioEnElPanel();
        definirVisibilidadYTextoDeBotonesPrincipales();
    }
    public void llenarDatosDeServicioEnElPanel()
    {
        if (Session["idUsuario"] == null)
        {
            Response.Redirect("/Vista/Sesion.aspx");
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
           DataRow informacionServicio =C_FSR.consultarInformacionFolioServicio(Session["Idusuario"].ToString(), 
               Session["folio_p"].ToString());
            
            //Inserta los datos de la vista coorepondientemente al campo que se le es asignado dentro del .aspx
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
            cmding.Text = informacionServicio["IdIngeniero"].ToString();
            DropDownList8.Text = informacionServicio["Estatusid"].ToString();
            Estatus_de_folio_servicio.Text= informacionServicio["Estatusid"].ToString();

           verificarSiContinuaOIniciaServicio();

    }

    public void verificarSiContinuaOIniciaServicio()
    {
        string inicioServicio = C_FSR.seleccionarInicioServicio(Session["folio_p"].ToString());
        if (inicioServicio != SIN_SERVICIO_INICIADO)
        {
            Btn_Estatus_Servicio.Text = "Continuar Servicio";
        }
        else if (inicioServicio.Equals(SIN_SERVICIO_INICIADO) || inicioServicio == null)
        {
            Btn_Estatus_Servicio.Text = "Iniciar Servicio";
        }
    }
    public void definirVisibilidadYTextoDeBotonesPrincipales()
    {
        if (Estatus_de_folio_servicio.Text.Equals(FINALIZADO))
        {
            Btn_Estatus_Servicio.Text = "Guardar Cambios";
            Btn_actualizar_fechas.Visible = true;
            Btn_agregar_acciones.Visible = true;
        }
        else
        {
            Btn_actualizar_fechas.Visible = false;
            Btn_agregar_acciones.Visible = false;
        }
        if (idservicio.SelectedValue.ToString() == "4" || idservicio.SelectedValue.ToString() == "8" ||
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
            btnadjuntar.Visible = true;
        }
        else
        {
            btnadjuntar.Visible = false;
        }
    }
    
    protected void Actualizar_Datos_Servicio_Click(object sender, EventArgs e)
    {
          folioServicioFSR.Folio = int.Parse(Session["folio_p"].ToString());
          folioServicioFSR.Marca = marcaHF.Value;
          folioServicioFSR.Modelo  = modeloHF.Value;
          folioServicioFSR.NoSerie = noserieHF.Value;
          folioServicioFSR.DescripcionEquipo = descripcionHF.Value;

          folioServicioFSR.IdEquipo = idHF.Value;
          folioServicioFSR.IdContrato =int.Parse(tcontratoHF.Value);
          folioServicioFSR.IdProblema = int.Parse(tproblemaHF.Value);
          folioServicioFSR.idServicio = int.Parse(tservicioHF.Value);

          folioServicioFSR.Direccion = direccionHF.Value;
          folioServicioFSR.Cliente = clienteHF.Value;
          folioServicioFSR.Departamento = deptoHF.Value;
          folioServicioFSR.Localidad = localidadHF.Value;

          folioServicioFSR.Telefono = TelefonoHF.Value;
          folioServicioFSR.N_Responsable = responsableHF.Value;
          folioServicioFSR.N_Reportado = reportadoHF.Value;
          folioServicioFSR.Email  = emailHF.Value;

          C_FSR.actualizarDatosDeServicio(folioServicioFSR, Session["idUsuario"].ToString());
          verificarEstatusDeFolio(Estatus_de_folio_servicio.Text);
       
    }

    private void verificarEstatusDeFolio(string estatusDeFolio)
    {
        switch (estatusDeFolio)
        {
            case ASIGNADO:
                //Se abre ventana emergente para colocar 
                floatsection.Style.Add("display", "block");
                headerone.Style.Add("filter", "blur(9px)");
                cuerpo.Style.Add("display", "none");
                reportdiv.Style.Add("display", "none");
                break;

            case PROCESO:
                //Si esta en proceso puede ir a ver la parte de acciones realizadas (modificada)
                recreatePdfParaServicioFinalizado(Session["folio_p"].ToString());
                Response.Redirect("./DetalleFSR.aspx");
                break;
            case FINALIZADO:
                recreatePdfParaServicioFinalizado(Session["folio_p"].ToString());
                Response.Redirect("./FSR.aspx");
                break;

        }
            
    }
    protected void btndescarga_Click(object sender, EventArgs e)
    {
        //recreatePDF(Session["folio_p"].ToString());
    }

    protected void recreatePdfParaServicioFinalizado(string folio)
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

        crearReportePdf(nombre);
    }

    private void crearReportePdf(string nombre)
    {
     // Variables  
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;

        //Setup the report viewer object and get the array of bytes
        byte[] bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

        if (Estatus_de_folio_servicio.Text.Equals(FINALIZADO))
        {
            string nombre_archivo = Session["folio_p"].ToString();
            string fecha_archivo = DateTime.Now.ToString("dd-MM-yyyy HH_mm");

            //El nombre del archivo al ser una actualizacion, llevara la fecha y hora a al que se subio la actualizacion
            string filepath = HttpRuntime.AppDomainAppPath + "Docs\\" + nombre_archivo + " " + fecha_archivo + ".pdf";

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
            //Pendiente funcionalidad para verificar si la fecha Inicio de servicio es despues de la fecha de inicio real
            C_FSR.iniciarFolioServicio(fechaYhoraDeInicioDeServicio, Session["folio_p"].ToString(), 
                Session["idUsuario"].ToString());
            
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

        C_FSR.actualizarFechayHoraFinDeServicio(folioServicio, Session["idUsuario"].ToString());

        Actfechas.Style.Add("display", "none");
        headerone.Style.Add("filter", "blur(0px)");
        cuerpo.Style.Add("display", "block");
        reportdiv.Style.Add("display", "block");
    }


    private DateTime traerFechaYHoraDeInicioDeFolio()
    { 
       DateTime fechaYhoraDeInicioDeFolioServicio = C_FSR.traerFechaYhoraDeInicioDeFolio(Session["folio_p"].ToString(), Session["idUsuario"].ToString());
       lbl_fechaYhora_inicio_servicio.Text = fechaYhoraDeInicioDeFolioServicio.ToString();
       return fechaYhoraDeInicioDeFolioServicio;
    }

    private DateTime traerFechaYHoraDeCierreDeFolio()
    {
      DateTime fechaYHoraFinDeFolioDeServicio = C_FSR.traerFechaYhoraDeFinDeFolio(Session["folio_p"].ToString(), Session["idUsuario"].ToString());
      Lbl_fin_de_servicio.Text = fechaYHoraFinDeFolioDeServicio.ToString();
      return fechaYHoraFinDeFolioDeServicio;
    }

    protected void Actualizar_Acciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("./DetalleFSR.aspx");
    }

    protected void Adjuntar_Archivos_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Adjuntar.aspx");
    }

    
}