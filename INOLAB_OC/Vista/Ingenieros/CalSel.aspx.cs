using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace INOLAB_OC
{
    public partial class CalSel : System.Web.UI.Page
    {
        private const string idAreaAnalitica = "1";
        private const string idAreaFisicoquimicos = "2";
        private const string idAreaTemperatura = "3";
        protected void Page_Load(object sender, EventArgs e)
        {
            verificarCorrectoInicioDeSesion();
        }

        private void verificarCorrectoInicioDeSesion()
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("./Sesion.aspx");
            }
        }
        protected void Seleccionar_el_area_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlfiltro.SelectedIndex == 1)
            {
                Session["Area"] = idAreaAnalitica; 
            }
            if (ddlfiltro.SelectedIndex == 2)
            {
                Session["Area"] = idAreaFisicoquimicos;  
            }
            if (ddlfiltro.SelectedIndex == 3)
            {
                Session["Area"] = idAreaTemperatura; 
            }
        }
        protected void Iniciar_sesion_en_calendario_area_Click(object sender, EventArgs e)
        {
            if (ddlfiltro.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Por favor, seleccionar un Area');</script>");
            }
            else
            {
                Response.Redirect("Calendario_Areas.aspx");
            }
        }

        protected void  Volder_a_inicio_de_sesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Vista/Sesion.aspx");
        }
    }
}