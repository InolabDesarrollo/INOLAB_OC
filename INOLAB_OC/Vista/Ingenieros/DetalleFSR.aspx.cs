using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;
using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Vista.Ingenieros;
using INOLAB_OC.Responsabilities;
using System.Web.Services.Description;
using System.IO;

public partial class DetalleFSR : Page
{
    const int FINALIZADO =3;
    const string sinFechaAsignada = "";
    const string sinAccionRegistrada = "";
    E_FSRAccion entidadAccion;
    E_Refaccion entidadRefaccion;
    static FSR_Repository repositorio = new FSR_Repository();
    C_FSR controladorFSR;

    static V_FSR_Repository repositorioV_FSR = new V_FSR_Repository();
    C_V_FSR controlador_V_FSR = new C_V_FSR(repositorioV_FSR);
    Observaciones observacion;
    Fallas falla;

    private string idUsuario;
    private string idFolioServicio;
    protected void Page_Load(object sender, EventArgs e)
    {
       idUsuario = Session["idUsuario"].ToString();
       idFolioServicio = Session["folio_p"].ToString();

       controladorFSR = new C_FSR(repositorio, idUsuario);
       entidadAccion = new E_FSRAccion(idFolioServicio, idUsuario);
       entidadRefaccion = new E_Refaccion(idFolioServicio);
       observacion = new Observaciones(idFolioServicio,idUsuario);
       falla = new Fallas(idUsuario, idFolioServicio);

       agregarEncabezadosDePanel();
       definirVisibilidadDeBotonesDependiendoEstatusFolio();
       cargarAccionesDelIngeniero();
       llenarInformacionDeRefaccionesActuales();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        idUsuario = Session["idUsuario"].ToString();
        idFolioServicio = Session["folio_p"].ToString();
        controladorFSR = new C_FSR(repositorio, idUsuario);
        consularSiServicioFuncionaCorrectamente();
    }

    public void agregarEncabezadosDePanel()
    {
        if (Session["idUsuario"] == null)
        {
            Response.Redirect("/Sesion.aspx");
        }
        else
        {
            titulo.Text = "Detalle de FSR N°. " + idFolioServicio;
            lbluser.Text = Session["nameUsuario"].ToString();
        }
    }
    public void definirVisibilidadDeBotonesDependiendoEstatusFolio()
    {
        int estatusFolioDeServicio = controladorFSR.consultarEstatusDeFolioServicio(idFolioServicio);
        if (estatusFolioDeServicio == FINALIZADO)
        {
            Btn_agregar_refacciones_a_servicio.Visible = false;
            Checked_verificar_funcionamiento.Visible = true;
            Btn_reportar_falla.Visible = false;
            Btn_vista_previa_reportes.Visible = false;
            Btn_ir_a_servicios_asignados.Visible = true;
        }
        else
        {
            Btn_agregar_refacciones_a_servicio.Visible = true;
            Checked_verificar_funcionamiento.Visible = true;
            Btn_reportar_falla.Visible = true;
            Btn_vista_previa_reportes.Visible = true;
            Btn_ir_a_servicios_asignados.Visible = true;
        }
    }

    private void cargarAccionesDelIngeniero()
    {
        GridView1.DataSource =  entidadAccion.consultarAcciones();
        GridView1.DataBind();
    }
    protected void Agregar_nuevas_acciones_Click(object sender, EventArgs e)
    {
        seccion_nuevo_servicio.Style.Add("display", "block");
        headerone.Style.Add("filter", "blur(9px)");
        contenone.Style.Add("filter", "blur(9px)");
        footerid.Style.Add("display", "none");
    }
    protected void Agregar_nueva_actividad_al_reporte_Click(object sender, EventArgs e)
    {      
        lbl_fecha_nuevo_servicio.Text = "Fecha: ";
        if (Fecha_nueva_accion_realizada.Text.Equals(sinFechaAsignada))
        {
            lbl_fecha_nuevo_servicio.Text = "Favor de ingresar fecha";
        }
        else
        {
          if (txtacciones.Text.Equals(sinAccionRegistrada))
            {
                acciones.Text = "Favor de ingresar la acción realizada";
            }
          else
            {
                agregarNuevaAccion();
            }
        }
    }

    private void agregarNuevaAccion()
    {
        entidadAccion = new E_FSRAccion(idFolioServicio, idUsuario);
        entidadAccion.FechaAccion = Fecha_nueva_accion_realizada.Text;
        entidadAccion.HorasAccion = txthorasD.Text;
        entidadAccion.AccionR = txtacciones.Text;

        int filasAfectadasPorUpdate = entidadAccion.agregarAccion();
        if (filasAfectadasPorUpdate == 1)
        {
            cerrarVentanaAgregarNuevaAccion();
            cargarAccionesDelIngeniero();
        }
        else
        {
            cerrarVentanaAgregarNuevaAccion();
            Response.Redirect("DetalleFSR.aspx");
        }
    }
    protected void Cerrar_ventana_agregar_nueva_accion_Click(object sender, ImageClickEventArgs e)
    {
        cerrarVentanaAgregarNuevaAccion();
    }
    public void cerrarVentanaAgregarNuevaAccion()
    {
        Fecha_nueva_accion_realizada.Text = "";
        txthorasD.Text = "";
        txtacciones.Text = "";

        seccion_nuevo_servicio.Style.Add("display", "none");
        contenone.Style.Add("filter", "blur(0)");
        headerone.Style.Add("filter", "blur(0)");
        footerid.Style.Add("display", "flex");
    }
    protected void Buscar_observaciones_folio_servicio_Click(object sender, EventArgs e)
    {
        txtobservaciones.Text= observacion.consultarObservaciones();
        
        observaciones.Style.Add("display", "block");
        headerone.Style.Add("filter", "blur(9px)");
        contenone.Style.Add("filter", "blur(9px)");
        footerid.Style.Add("display", "none");    
    }   
    protected void Btn_Fallas_Encontradas_Click(object sender, EventArgs e)
    {
       txtfallaencontrada.Text= falla.consultarFallaEncontrada();
             
       FallaEncontrada.Style.Add("display", "block");
       headerone.Style.Add("filter", "blur(9px)");
       contenone.Style.Add("filter", "blur(9px)");
       footerid.Style.Add("display", "none");        
    }

    protected void Btn_Comentarios_Ingeniero_Click(object sender, EventArgs e)
    {
        Agregar_Comentarios_Finales.Style.Add("display", "block");
        headerone.Style.Add("filter", "blur(9px)");
        contenone.Style.Add("filter", "blur(9px)");
        footerid.Style.Add("display", "none");

        C_Comentario_Ingeniero controlador = new C_Comentario_Ingeniero();
        TextBox_Comentarios_Finales.Text = controlador.consultarComentario(Convert.ToInt32(idFolioServicio));
    }

    protected void Btn_Gurardar_Comentarios_Ingeniero_Click(object sender, EventArgs e)
    {
        SeguimientoFSR comentario = new SeguimientoFSR();
        comentario.FechaSistema = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
        comentario.ComentarioIngeniero = TextBox_Comentarios_Finales.Text;
        comentario.IdIngeniero = idUsuario;
        comentario.IdFsr = Convert.ToInt32(idFolioServicio);

        C_Comentario_Ingeniero controlador = new C_Comentario_Ingeniero();
        controlador.insertarComentarioIngeniero(comentario);

        cerrarComentariosFinales();
    }

    protected void Cerrar_Comentarios_Finales_Click(object sender, ImageClickEventArgs e)
    {
        cerrarComentariosFinales();
    }

    private void cerrarComentariosFinales()
    {
        Agregar_Comentarios_Finales.Style.Add("display", "none");
        contenone.Style.Add("filter", "blur(0)");
        headerone.Style.Add("filter", "blur(0)");
        footerid.Style.Add("display", "flex");
    }

    protected void Cerrar_campo_observaciones_Click(object sender, ImageClickEventArgs e)
    {
        cerrarCampoObservaciones();
    }

    private void cerrarCampoObservaciones()
    {
        txtobservaciones.Text = "";
        observaciones.Style.Add("display", "none");
        contenone.Style.Add("filter", "blur(0)");
        headerone.Style.Add("filter", "blur(0)");
        footerid.Style.Add("display", "flex");
    }
    protected void Cerrar_campo_fallas_encontradas_Click(object sender, ImageClickEventArgs e)
    {
        cerrarCompoFallasEncontradas();
    }
    private void cerrarCompoFallasEncontradas()
    {
        txtfallaencontrada.Text = "";
        FallaEncontrada.Style.Add("display", "none");
        contenone.Style.Add("filter", "blur(0)");
        headerone.Style.Add("filter", "blur(0)");
        footerid.Style.Add("display", "flex");
    }
    protected void Actualizar_observaciones_Click(object sender, EventArgs e)
    {
        observacion.actualizarObservaciones(txtobservaciones.Text);
        cerrarCampoObservaciones();
    }
  
    protected void Actualizar_fallas_encontradas_Click(object sender, EventArgs e)
    {
        falla.actualizarFallas(txtfallaencontrada.Text);
        cerrarCompoFallasEncontradas();
    }

    protected void Btn_Vista_Previa_Click(object sender, EventArgs e)
    {
        C_Comentario_Ingeniero controlador = new C_Comentario_Ingeniero();
        
        string comentarioIngeniero = controlador.consultarComentario(Convert.ToInt32(idFolioServicio));
        if (comentarioIngeniero.Length > 2)
        {
            Response.Redirect("VistaPrevia.aspx");
        }
        else
        {
            Response.Write("<script>alert('Favor de llenar el campo de comentario ingeniero');</script>");
        }
              
    }
    protected void Verificacion_de_estatusCheckedChanged(object sender, EventArgs e)
    {
        if (Funciona_Correctamente_lista.SelectedValue.Equals("1")){
            controladorFSR.verificarSiServicioFuncionaCorrectamente(idFolioServicio, "Si");
        }else if (Funciona_Correctamente_lista.SelectedValue.Equals("2"))
        {
            controladorFSR.verificarSiServicioFuncionaCorrectamente(idFolioServicio, "No");
        }
        else
        {
            controladorFSR.verificarSiServicioFuncionaCorrectamente(idFolioServicio, "NA");
        }       
    }

    private void consularSiServicioFuncionaCorrectamente()
    {
        var funciona = controladorFSR.consultarValorDeCampoPorFolioyUsuario(idFolioServicio, "Funcionando");
        if(funciona.Equals("Si"))
        {
            Funciona_Correctamente_lista.SelectedValue = "1";
        }
        else if(funciona.Equals("No"))
        {
            Funciona_Correctamente_lista.SelectedValue = "2";
        }
        else
        {
            Funciona_Correctamente_lista.SelectedValue = "3";
        }
    }
   
    protected void Agregar_refaccion_a_base_de_datos_Click(object sender, EventArgs e)
    { 
        string numeroDePartes = txtbox_numero_de_partes.Text;
        string descripcionDeRefacion = txtbox_descripcion_refaccion.Text;
        string cantidadDeRefacciones = txtbox_cantidad_refaccion.Text;

        if (numeroDePartes.Length > 0 && descripcionDeRefacion.Length > 0 && cantidadDeRefacciones.Length > 0)
        {
            if (entidadRefaccion.insertarRefaccion(numeroDePartes, cantidadDeRefacciones, descripcionDeRefacion))
            {
                agregarDatosDeRefacciones(numeroDePartes, cantidadDeRefacciones);
                cerrarVentanaDeNuevaRefaccion();
            }
        }
        else
        {
            Response.Write("<script>alert('Favor de llenar todos los campos');</script>");
        }
    }

    private void agregarDatosDeRefacciones(string numeroDePartes, string numeroDeRefacciones)
    {
        Table1.Rows.Add(entidadRefaccion.crearFilaParaRefacciones(numeroDePartes, numeroDeRefacciones));  
    }

    private void llenarInformacionDeRefaccionesActuales()
    {
        try
        {
            DataSet refacciones = entidadRefaccion.consultarRefacciones();
            if (refacciones.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in refacciones.Tables[0].Rows)
                {
                    agregarDatosDeRefacciones(dataRow["numRefaccion"].ToString(), dataRow["cantidadRefaccion"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }
    }
    protected void Mostrar_ventana_refacciones_Click(object sender, EventArgs e)
    {
        refacciones.Style.Add("display", "block");
        headerone.Style.Add("filter", "blur(9px)");
        contenone.Style.Add("filter", "blur(9px)");
        footerid.Style.Add("display", "none");
    }

    protected void Cerrar_Ventana_Refacciones_Click(object sender, ImageClickEventArgs e)
    {
        txtbox_numero_de_partes.Text = "";
        txtbox_cantidad_refaccion.Text = "";
        txtbox_descripcion_refaccion.Text = "";

        refacciones.Style.Add("display", "none");
        contenone.Style.Add("filter", "blur(0)");
        headerone.Style.Add("filter", "blur(0)");
        footerid.Style.Add("display", "flex");
    }
    protected void Cerrar_ventana_nueva_refaccion_Click(object sender, ImageClickEventArgs e)
    {
        cerrarVentanaDeNuevaRefaccion();
    }
    private void cerrarVentanaDeNuevaRefaccion()
    {
        txtbox_numero_de_partes.Text = "";
        txtbox_cantidad_refaccion.Text = "";
        txtbox_descripcion_refaccion.Text = "";

        SECCION_AGREGAR_REFACCION.Style.Add("display", "none");
        refacciones.Style.Add("display", "block");
    }

    protected void Mostrar_nueva_refaccion_Click(object sender, EventArgs e)
    {       
        refacciones.Style.Add("display", "none");
        SECCION_AGREGAR_REFACCION.Style.Add("display", "block");
    }
    protected void Agrendar_proximo_servicio_Click(object sender, EventArgs e)
    {
        if (datepicker1.Text != "")
        {
            controladorFSR.actualizarValorDeCampoPorFolio(idFolioServicio, "Proximo_Servicio", datepicker1.Text);
        }
        else
        {
            controladorFSR.actualizarValorDeCampoNull(idFolioServicio);
        }   
    }
    
    public void GridView_de_acciones_realizadas_en_folio_OnRowComand(object sender, GridViewCommandEventArgs e)
    {
        int fila;
        //Al darle clic al folio deseado este se almacena en la sesión y te redirige a la ventana de FSR
        try
        {
            if (e.CommandName == "Borrar")
            {
                e.CommandArgument.ToString();
                fila = Convert.ToInt32(e.CommandArgument.ToString());
                string idFolioDeAccion = GridView1.Rows[fila].Cells[0].Text.ToString();

                E_FSRAccion entidadFsrAccion;
                entidadFsrAccion =  entidadAccion.consultarAccion(Convert.ToInt32(idFolioDeAccion));
                                     
                fol.Text = entidadFsrAccion.idFolioFSR;
                serv.Text = controlador_V_FSR.consultarValorDeCampo("TipoServicio", entidadFsrAccion.idFolioFSR);
                descacci.Text = entidadFsrAccion.AccionR;
                fechacci.Text = entidadFsrAccion.FechaAccion;
                horaacci.Text = entidadFsrAccion.HorasAccion;
                IDAccion.Text = entidadFsrAccion.idFSRAccion;

                avisodel.Style.Add("display", "block");
                headerone.Style.Add("display", "none");
                footerid.Style.Add("display", "none");
                contenone.Style.Add("display", "none");
            }
        }
        catch (SqlException ex)
        {
            Console.Write(ex.ToString());
        }
    }

    public void Borrar_accion_realizada_Click(object sender, EventArgs e)
    {
        entidadAccion.eliminarAccion(IDAccion.Text);
        cargarAccionesDelIngeniero();

        avisodel.Style.Add("display", "none");
        headerone.Style.Add("display", "block");
        footerid.Style.Add("display", "flex");
        contenone.Style.Add("display", "block");
    }
    protected void Cancelar_proceso_de_eliminar_accion_Click(object sender, EventArgs e)
    { 
        avisodel.Style.Add("display", "none");
        headerone.Style.Add("display", "block");
        footerid.Style.Add("display", "flex");
        contenone.Style.Add("display", "block");
    }

    protected void Ir_a_servicios_asignados_Click(object sender, EventArgs e)
    {
        Response.Redirect("ServiciosAsignados.aspx");
    }
}