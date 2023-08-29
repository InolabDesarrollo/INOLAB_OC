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
            consultarReportesRefacciones();
            lbluser.Text = Session["nameUsuario"].ToString();
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
            refaccion = new ReporteRefaccion();
            Gv_Folios.DataSource = refaccion.consultarReporteRefaccionPorIdIngeniero(idIngeniero);
            Gv_Folios.DataBind();
        }

        protected void List_Buscar_Refacciones(object sender, EventArgs e)
        {
            bool revisado;
            if (List_Refacciones.Text.Equals("Revisados"))
            {
                revisado = true;
                consultarReportesRefacciones(revisado);
            }
            else if (List_Refacciones.Text.Equals("Sin Revisar"))
            {
                revisado = false;
                consultarReportesRefacciones(revisado);
            }
            else if (List_Refacciones.Text.Equals("Todos"))
            {
                consultarReportesRefacciones();
            }
            
        }

        private void consultarReportesRefacciones(bool revisado)
        {
            refaccion = new ReporteRefaccion();
            Gv_Folios.DataSource = refaccion.consultarReporteRefaccionPorIdIngeniero(idIngeniero,revisado);
            Gv_Folios.DataBind();
        }

    }
}