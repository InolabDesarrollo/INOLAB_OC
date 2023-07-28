using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using INOLAB_OC.Modelo;
using INOLAB_OC;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Diagnostics;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;
using INOLAB_OC.Controlador.Ingenieros;

public partial class DetalleFSR : Page
{
    int q;
    
    const int FINALIZADO =3;
    const string sinFechaAsignada = "";
    const string sinAccionRegistrada = "";

    static FSR_Repository repositorio = new FSR_Repository();
    C_FSR controladorFSR;
    E_Servicio entidadServicio = new E_Servicio();

    static FSR_AccionRepository repositorioFsrAccion = new FSR_AccionRepository();
    C_FSR_Accion controladorFSRAccion;

    static V_FSR_Repository repositorioV_FSR = new V_FSR_Repository();
    C_V_FSR controlador_V_FSR = new C_V_FSR(repositorioV_FSR);

    static Refaccion_Repository repositorioRefaccion = new Refaccion_Repository();
    C_Refaccion controladorRefaccion = new C_Refaccion(repositorioRefaccion);

    string idUsuario;
    string idFolioServicio;
    protected void Page_Load(object sender, EventArgs e)
    {
       idUsuario = Session["idUsuario"].ToString();
       idFolioServicio = Session["folio_p"].ToString();

       controladorFSR = new C_FSR(repositorio, idUsuario);
       controladorFSRAccion = new C_FSR_Accion(repositorioFsrAccion);

       agregarEncabezadosDePanel();
       definirVisibilidadDeBotonesDependiendoEstatusFolio();
       cargarAccionesDelIngeniero();
       llenarInformacionDeRefaccionesActuales();
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
        GridView1.DataSource =  controladorFSRAccion.consultarDatosDeFSRAccion(idFolioServicio);
        GridView1.DataBind();
    }

    private void llenarInformacionDeRefaccionesActuales()
    {
        try
        {
            DataSet refacciones =  controladorRefaccion.consultarNumeroYCantidadDeRefaccion(idFolioServicio);           
            int numeroDeRefacciones = refacciones.Tables[0].Rows.Count;

            if (numeroDeRefacciones > 0)
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
                //Comparacion de fechas (no puede hacerlo si la fecha es anterior a la fecha de servicio) funcionalidad pendiente
                
                    string fechaNuevaAccion, horasDedicadasEnNuevaAccion, nuevaAccionRealizada;
                    
                    fechaNuevaAccion = Fecha_nueva_accion_realizada.Text;
                    horasDedicadasEnNuevaAccion = txthorasD.Text;
                    nuevaAccionRealizada = txtacciones.Text;

                    if (insertarNuevaAccionRealizada(fechaNuevaAccion, horasDedicadasEnNuevaAccion, nuevaAccionRealizada))
                    {
                        string[] vectorFechaNuevaAccion = fechaNuevaAccion.Split('-');
                        string onemod = vectorFechaNuevaAccion[2] + "-" + vectorFechaNuevaAccion[1] + "-" + vectorFechaNuevaAccion[0];

                    cerrarVentanaAgregarNuevaAccion();
                    cargarAccionesDelIngeniero();
                    }
                    else
                    {
                    cerrarVentanaAgregarNuevaAccion();
                    Response.Redirect("DetalleFSR.aspx");
                    }
            }
        }

    }
    private bool insertarNuevaAccionRealizada(String fechaNuevaAccion, String horasDedicadasEnNuevaAccion, String nuevaAccionRealizada)
    {  
        E_FSRAccion entidadAccion = new E_FSRAccion();
        entidadAccion.FechaAccion = fechaNuevaAccion;
        entidadAccion.HorasAccion = horasDedicadasEnNuevaAccion;
        entidadAccion.AccionR = nuevaAccionRealizada;
        entidadAccion.idFolioFSR = idFolioServicio;
        entidadAccion.idUsuario = Session["idUsuario"].ToString();
        entidadAccion.FechaSistema = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        int filasAfectadasPorUpdate = controladorFSRAccion.agregarAccionFSR(entidadAccion);
        return (filasAfectadasPorUpdate == 1) ? true: false;
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
        try
        {          
            string observacionesDeFolioServicio = controladorFSR.consultarValorDeCampoPorFolioyUsuario(idFolioServicio, "Observaciones");

            if (observacionesDeFolioServicio != null)
            {
                txtobservaciones.Text = observacionesDeFolioServicio;
            }
            Envio_de_notificacion_de_observacion.Checked = controladorFSR.verificarSiSeEnviaEmailAlAsesor(idFolioServicio, "NotAsesor");
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            
        }
        finally
        {
            observaciones.Style.Add("display", "block");
            headerone.Style.Add("filter", "blur(9px)");
            contenone.Style.Add("filter", "blur(9px)");
            footerid.Style.Add("display", "none");
        }
    }
    protected void Btn_Fallas_Encontradas_Click(object sender, EventArgs e)
    {
        try
        {   
            string fallaEncontrada = controladorFSR.consultarValorDeCampoPorFolioyUsuario(idFolioServicio, "FallaEncontrada");

            if (fallaEncontrada != null)
            {
                txtfallaencontrada.Text = fallaEncontrada;
            }          
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            
        }
        finally
        {
            FallaEncontrada.Style.Add("display", "block");
            headerone.Style.Add("filter", "blur(9px)");
            contenone.Style.Add("filter", "blur(9px)");
            footerid.Style.Add("display", "none");
        }
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
        if(txtobservaciones.Text.Length > 0) 
        {
            try
            {
                controladorFSR.actualizarValorDeCampoPorFolioYUsuario(idFolioServicio, "Observaciones", txtobservaciones.Text);
                Session["not_ase"] = controladorFSR.verificarSiEnviaNotificacionDeObservacionesAlUsuario(Envio_de_notificacion_de_observacion.Checked, idFolioServicio);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            finally
            {
                cerrarCampoObservaciones();
            }
        }
    }
  
    protected void Actualizar_fallas_encontradas_Click(object sender, EventArgs e)
    {
        if (txtfallaencontrada.Text.Length > 0)
        {
            try
            {
                controladorFSR.actualizarValorDeCampoPorFolioYUsuario(idFolioServicio, "FallaEncontrada", txtfallaencontrada.Text);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            finally
            {
                cerrarCompoFallasEncontradas();
            }
        }
    }

    protected void Btn_Vista_Previa_Click(object sender, EventArgs e)
    {   
        Response.Redirect("VistaPrevia.aspx");       
    }

    protected void Verificacion_de_estatus_esta_o_no_funcionando_CheckedChanged(object sender, EventArgs e)
    {
        controladorFSR.verificarSiServicioFuncionaCorrectamente(idFolioServicio, CHECKED_ESTA_FUNCIONANDO.Checked);
        consularSiServicioFuncionaCorrectamente();
    }

    private void consularSiServicioFuncionaCorrectamente()
    {
        Funciona_Correctamente.Text= controladorFSR.consultarValorDeCampoPorFolioyUsuario(idFolioServicio, "Funcionando");
    }
   
    protected void Agregar_refaccion_a_base_de_datos_Click(object sender, EventArgs e)
    { 
        string numeroDePartes, descripcionDeRefacion, cantidadDeRefacciones;
        numeroDePartes = txtbox_numero_de_partes.Text;
        descripcionDeRefacion = txtbox_descripcion_refaccion.Text;
        cantidadDeRefacciones = txtbox_cantidad_refaccion.Text;

        if (numeroDePartes.Length > 0)
        {
            if (descripcionDeRefacion.Length > 0)
            {
                if (cantidadDeRefacciones.Length > 0)
                {
                    if (insertarRefaccion(numeroDePartes, cantidadDeRefacciones, descripcionDeRefacion))
                    {
                        agregarDatosDeRefacciones(numeroDePartes, cantidadDeRefacciones);
                        cerrarVentanaDeNuevaRefaccion();
                    }
                }
                else
                    Response.Write("<script>alert('Favor de llenar todos los campos');</script>");
            }
            else
                Response.Write("<script>alert('Favor de llenar todos los campos');</script>");
        }
        else
            Response.Write("<script>alert('Favor de llenar todos los campos');</script>");
    }

    private void agregarDatosDeRefacciones(string numeroDePartes, string numeroDeRefacciones)
    {
        try
        {
            TableRow row = new TableRow();
            TableCell[] cell1 = new TableCell[2];
            cell1[0] = new TableCell();
            cell1[1] = new TableCell();
            cell1[0].Text = numeroDePartes;
            cell1[1].Text = numeroDeRefacciones + " pieza(s)";
            row.Cells.AddRange(cell1);
            row.Style.Add("HorizontalAlign", "Center");
            Table1.Rows.Add(row);
        }
        catch(Exception e)
        {
            Response.Write("<script>alert('Error al agregar a la tabla de datos');</script>");
        }
    }

    protected bool insertarRefaccion(string numeroDePartes, string cantidadDeRefacciones, string descripcionDeRefacion)
    {    
        E_Refaccion refaccion = new E_Refaccion();
        refaccion.numRefaccion = numeroDePartes;
        refaccion.cantidadRefaccion = cantidadDeRefacciones;
        refaccion.descRefaccion = descripcionDeRefacion;
        refaccion.idFSR = idFolioServicio;

        int numeroDeFilasAfectadas = controladorRefaccion.agregarRefaccion(refaccion);
        return numeroDeFilasAfectadas == 1?  true : false;
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
        //Guarda la fecha de proximo servicio que se haya insertado (Si se oprimio sin haber seleccionado una fecha antes aparecera como 1999-01-01)
        controladorFSR.actualizarValorDeCampoPorFolio(idFolioServicio, "Proximo_Servicio", datepicker1.Text);
    }
    public void GridView_de_acciones_realizadas_en_folio_OnRowComand(object sender, GridViewCommandEventArgs e)
    {
        //Al darle clic al folio deseado este se almacena en la sesión y te redirige a la ventana de FSR
        try
        {
            if (e.CommandName == "Borrar")
            {
                //Me trae el indice del datagrid que seleccione 
                e.CommandArgument.ToString();
                q = Convert.ToInt32(e.CommandArgument.ToString());

                string idFolioDeAccion;
                idFolioDeAccion = GridView1.Rows[q].Cells[0].Text.ToString();
                E_FSRAccion entidadFsrAccion;
                entidadFsrAccion = controladorFSRAccion.consultarFSRAccion(Convert.ToInt32(idFolioDeAccion));
         
                string accionEnServicio =  entidadFsrAccion.AccionR;              
                string fechaDeAccion =  entidadFsrAccion.FechaAccion;                
                string horaDeAccion =  entidadFsrAccion.HorasAccion;
                string idFolioServicioAccion =  entidadFsrAccion.idFSRAccion;        
                string idFolioDeServicio = entidadFsrAccion.idFolioFSR;
                string tipoDeServicio = controlador_V_FSR.consultarValorDeCampo("TipoServicio", idFolioDeServicio);
                                
                fol.Text = idFolioDeServicio;
                serv.Text = tipoDeServicio;
                descacci.Text = accionEnServicio;
                fechacci.Text = fechaDeAccion;
                horaacci.Text = horaDeAccion;
                IDAccion.Text = idFolioServicioAccion;
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
        controladorFSRAccion.eliminarAccionFSR(IDAccion.Text);
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

    protected void Verificar_si_se_envio_notificacion_a_usuario_CheckedChanged(object sender, EventArgs e)
    {
        Session["not_ase"]= controladorFSR.verificarSiEnviaNotificacionDeObservacionesAlUsuario(Envio_de_notificacion_de_observacion.Checked, idFolioServicio);
    }

    protected void Ir_a_servicios_asignados_Click(object sender, EventArgs e)
    {
        Response.Redirect("ServiciosAsignados.aspx");
    }
}