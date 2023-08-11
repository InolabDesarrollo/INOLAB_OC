using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Vista.Ingenieros
{
    public partial class ReporteRefacciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Servicios_Asignados(object sender, EventArgs e)
        {
            Response.Redirect("ServiciosAsignados.aspx");
        }
        protected void Atras(object sender, EventArgs e)
        {
            Response.Redirect("VistaPrevia.aspx");
        }

        protected void Salir(object sender, EventArgs e)
        {
            Response.Redirect("/Sesion.aspx");
        }
          
        protected void Agregar_Refaccion(object sender, EventArgs e)
        {
            Sect_refacciones.Style.Add("display","none");
            Sect_agregar_refaccion.Style.Add("display", "block");
        }

        protected void Cerrar_Refacciones(object sender, EventArgs e)
        {
            Sect_agregar_refaccion.Style.Add("display", "none");
            Sect_refacciones.Style.Add("display", "block");
        }

    }
}