using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office.Excel;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Vista.Ingenieros.Responsabilidades;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace INOLAB_OC.Vista.Ingenieros
{
    public partial class ReporteRefacciones : System.Web.UI.Page
    {
        string folioServicio;
        ReporteRefaccion refaccion;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbluser.Text = " "+ Session["nameUsuario"].ToString();
            folioServicio = Session["folio_p"].ToString();
            Lbl_Folio.Text = "N° de Folio: " + folioServicio;
            refaccion = new ReporteRefaccion(folioServicio);

            consultarTodasLasRefacciones(folioServicio);
        }

        private void consultarTodasLasRefacciones(string folioServicio)
        {
            refaccion = new ReporteRefaccion(folioServicio);
            Gv_Refacciones.DataSource = refaccion.consultarTodasLasRefacciones();
            Gv_Refacciones.DataBind();
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
          
        protected void Mostrar_Ventana_Nueva_Refaccion(object sender, EventArgs e)
        {
            Sect_Refacciones.Style.Add("display", "none");
            Sect_agregar_refaccion.Style.Add("display", "block");
            Btn_agregar.Text = "-";
        }

        protected void Agregar_nueva_refaccion(object sender, EventArgs e)
        {
            if (txtbox_cantidad_refaccion.Text != "" && txtbox_descripcion_refaccion.Text != "")
            {
                ReporteRefaccion refaccion = new ReporteRefaccion(folioServicio, txtbox_numero_de_partes.Text, txtbox_cantidad_refaccion.Text, 
                    txtbox_descripcion_refaccion.Text);
                refaccion.agregarRefaccion();
                consultarTodasLasRefacciones(folioServicio);
                cerrarVentanaNuevaReaccion();
            }            
            else
            {
                Response.Write("<script>alert('Favor de llenar todos los campos');</script>");
            }
        }

        protected void Cerrar_Ventana_Refacciones(object sender, EventArgs e)
        {
            cerrarVentanaNuevaReaccion();
        }

        private void cerrarVentanaNuevaReaccion()
        {
            txtbox_numero_de_partes.Text = "";
            txtbox_cantidad_refaccion.Text = "";
            txtbox_descripcion_refaccion.Text = "";

            Sect_agregar_refaccion.Style.Add("display", "none");
            Sect_Refacciones.Style.Add("display", "block");
            Btn_agregar.Text = "Agregar";
        }

        protected void Gv_Refacciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Eliminar"))
            {
                int indice = int.Parse(e.CommandArgument.ToString());
                System.Data.DataTable refacciones = refaccion.consultarTodasLasRefacciones();
                int id=int.Parse(refacciones.Rows[indice]["IdReporteRefaccion"].ToString());
                refaccion.eliminar(id);
                consultarTodasLasRefacciones(folioServicio);
            }
        }

    }
}