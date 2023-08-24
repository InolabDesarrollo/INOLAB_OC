using INOLAB_OC.Vista.Ingenieros.Responsabilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Vista.Ingenieros
{
    public partial class EdicionReporteRefacciones : System.Web.UI.Page
    {
        ReporteRefaccion refaccion;
        string idFolioFsr;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbluser.Text = Session["Usuario"].ToString();
            Txbox_Folio.Text = "Folio : "+ Session["FolioRefaccion"].ToString();
            Txbox_Folio.Enabled = false;

            
            refaccion = new ReporteRefaccion(Session["FolioRefaccion"].ToString());
            consultarRefaccionesDeFolio();
        }

        protected void Servicios_Asignados(object sender, EventArgs e)
        {
            Response.Redirect("ServiciosAsignados.aspx");
        }

        protected void Atras(object sender, EventArgs e)
        {
            Response.Redirect("GerenteReporteRefacciones.aspx");
        }

        protected void Salir(object sender, EventArgs e)
        {
            Response.Redirect("/Sesion.aspx");
        }

        private void consultarRefaccionesDeFolio()
        {
            Gv_Refacciones.DataSource = refaccion.consultarTodasLasRefacciones();
            Gv_Refacciones.DataBind();
        }

        protected void Gv_Refacciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                Sect_Editar_Refaccion.Style.Add("display", "block");
                Sect_Refacciones.Style.Add("display", "none");

                int fila = int.Parse(e.CommandArgument.ToString());
                string idReporteRefaccion = refaccion.consultarIdReporteRefaccion(fila);

                ReporteRefaccion refaccionCompleta = new ReporteRefaccion(idFolioFsr, txtbox_numero_de_partes.Text, txtbox_cantidad_refaccion.Text
                    ,txtbox_descripcion_refaccion.Text, Txtbox_Comentario_Gerente.Text);
            }
        }

        protected void Cerrar_Ventana_Editar_Refaccion(object sender, EventArgs e)
        {
            Sect_Editar_Refaccion.Style.Add("display", "none");
            Sect_Refacciones.Style.Add("display", "block");
        }

    }
}