using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Vista.Ingenieros.Responsabilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Vista.Ingenieros
{
    public partial class MonitorReporteRefacciones : System.Web.UI.Page
    {
        string idIngeniero;
        ReporteRefaccion refaccion;
        protected void Page_Load(object sender, EventArgs e)
        {
            idIngeniero = Session["idUsuario"].ToString();
            lbluser.Text = Session["nameUsuario"].ToString();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            refaccion = new ReporteRefaccion();
            consultarReportesRefacciones();
        }

        protected void Servicios_Asignados(object sender, EventArgs e)
        {
            Response.Redirect("ServiciosAsignados.aspx");
        }

        protected void Salir(object sender, EventArgs e)
        {
            Response.Redirect("/Sesion.aspx");
        }

        private void consultarReportesRefacciones()
        {
            Txt_Folio_Monitor.Visible = false;
            Buscar_Folio_Monitor.Visible = false;
            Sect_Gv_Monitor_Folios.Style.Add("display", "block");

            Gv_Folios.DataSource = refaccion.consultarReporteRefaccionPorIdIngeniero(idIngeniero);
            Gv_Folios.DataBind();
        }

        protected void List_Buscar_Refacciones(object sender, EventArgs e)
        {
            bool revisado;
            if (List_Refacciones.Text.Equals("Revisados"))
            {
                ocultalBotonesBuscarPorFolio();
                revisado = true;
                consultarReportesRefacciones(revisado);
            }
            else if (List_Refacciones.Text.Equals("Sin Revisar"))
            {
                ocultalBotonesBuscarPorFolio();
                revisado = false;
                consultarReportesRefacciones(revisado);
            }
            else if (List_Refacciones.Text.Equals("Todos"))
            {
                ocultalBotonesBuscarPorFolio();
                consultarReportesRefacciones();

            }else if (List_Refacciones.Text.Equals("Por Folio"))
            {
                habilitarBusquedaPorFolio();
            }         
        }

        private void ocultalBotonesBuscarPorFolio()
        {
            Txt_Folio_Monitor.Visible = false;
            Buscar_Folio_Monitor.Visible = false;
        }

        private void consultarReportesRefacciones(bool revisado)
        {
            Gv_Folios.DataSource = refaccion.consultarReporteRefaccionPorIdIngeniero(idIngeniero,revisado);
            Gv_Folios.DataBind();
        }

        private void habilitarBusquedaPorFolio()
        {
            Txt_Folio_Monitor.Visible = true;
            Buscar_Folio_Monitor.Visible = true;
            Gv_Folios.DataSource = refaccion.consultarReporteRefaccionPorIdIngeniero(idIngeniero);
            Gv_Folios.DataBind();
        }

        protected void Buscar_Folio(object sender, EventArgs e)
        {   
            Gv_Folios.DataSource = refaccion.consultarTodasLasRefacciones(Txt_Folio_Monitor.Text);
            Gv_Folios.DataBind();
        }


    }
}