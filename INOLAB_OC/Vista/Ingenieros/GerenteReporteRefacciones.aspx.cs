using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Packaging;
using INOLAB_OC.Controlador;
using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Modelo.Browser.Interfaces;
using INOLAB_OC.Vista.Ingenieros.Responsabilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Vista.Ingenieros
{
    public partial class GerenteReporteRefaccion : System.Web.UI.Page
    {
        ReporteRefaccionRepository repositorio = new ReporteRefaccionRepository();
        C_Refaccion controlador;
        UsuarioRepository repositoryUsuario = new UsuarioRepository();
        C_Usuario controladorUsuario;
        string idUsuario;
        int areaGerente;
        protected void Page_Load(object sender, EventArgs e)
        {
            controlador = new C_Refaccion(repositorio);
            controladorUsuario = new C_Usuario(repositoryUsuario);
            lbluser.Text = Session["nameUsuario"].ToString();
            idUsuario = Session["idUsuario"].ToString();
            areaGerente = consultarAreaDeIngeniero();
        }

        private int consultarAreaDeIngeniero()
        {
            return int.Parse(controladorUsuario.consultarValorDeCampo("IngArea", idUsuario));
        }

        protected void Servicios_Asignados(object sender, EventArgs e)
        {
            Response.Redirect("ServiciosAsignados.aspx");
        }

        protected void Salir(object sender, EventArgs e)
        {
            Response.Redirect("/Sesion.aspx");
        }

        protected void List_Buscar_Reporte(object sender, EventArgs e)
        {
            if (List_BuscarReporte.Text.Equals("Folio"))
            {
                consultarPorFolios();
            }
            else if (List_BuscarReporte.Text.Equals("Ingeniero"))
            {
                consultarPorIngeniero();
            }
        }

        private void consultarPorFolios()
        {
            Sect_Buscar_Por.Style.Add("display", "block");
            Lbl_Buscar_Por.Text = "Introduce el Folio:";
            Txbox_Buscar_Por.Text = "";
            Btn_Buscar.Visible = true;
            Btn_Buscar_Ingeniero.Visible = false;

            Sect_Gv_Folios.Style.Add("display", "block");
            Sect_Gv_Ingenieros.Style.Add("display", "none");
            consultarTodosLosFolios();
        }

        private void consultarTodosLosFolios()
        {
            Gv_Folios.DataSource = controlador.consultarFoliosPorArea(areaGerente);
            Gv_Folios.DataBind();
        }

        private void consultarPorIngeniero()
        {
            Sect_Buscar_Por.Style.Add("display", "block");
            Lbl_Buscar_Por.Text = "Introduce Usuario:";
            Txbox_Buscar_Por.Text = "";

            Sect_Gv_Folios.Style.Add("display", "none");
            Sect_Gv_Ingenieros.Style.Add("display", "block");
            Btn_Buscar_Ingeniero.Visible = true;
            Btn_Buscar.Visible = false;
            consultarTodosLosIngenieros();
        }

        private void consultarTodosLosIngenieros()
        {
            Gv_Ingenieros.DataSource = controlador.consultarIngenierosPorArea(areaGerente);
            Gv_Ingenieros.DataBind();
        }
        protected void Buscar_Folio(object sender, EventArgs e)
        {
            if (Txbox_Buscar_Por.Text.Equals(""))
            {
                consultarTodosLosFolios();
            }
            else
            {
                buscarFolioEnDataGrid(Txbox_Buscar_Por.Text.ToString());
            }
        }

        private void buscarFolioEnDataGrid(string folioBuscado)
        {
            DataSet datos = controlador.consultarFoliosPorArea(areaGerente);
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                string folio = row["Folio"].ToString();
                if (folio.Equals(folioBuscado))
                {
                    Gv_Folios.DataSource = controlador.consultarFoliosPorAreaYFolio(areaGerente, folio);
                    Gv_Folios.DataBind();
                }
            }
        }

        protected void Buscar_Ingeniero(object sender, EventArgs e)
        {
            if (Txbox_Buscar_Por.Text.Equals(""))
            {
                consultarTodosLosIngenieros();
            }
            else
            {
                buscarIngenieroEnDataGrid(Txbox_Buscar_Por.Text.ToString());
            }
        }

        private void buscarIngenieroEnDataGrid(string nombreIngeniero)
        {
            Gv_Ingenieros.DataSource = controlador.consultarIngenierosPorAreaYNombre(areaGerente, nombreIngeniero);
            Gv_Ingenieros.DataBind();  
        }
        protected void Gv_Ingenieros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Cordenada cordenada = new Cordenada(Gv_Ingenieros);
            if (e.CommandName.Equals("Seleccionar"))
            {
                Session["FolioRefaccion"] = cordenada.consultarValorDeCelda(int.Parse(e.CommandArgument.ToString()), 2);
                Response.Redirect("EdicionReporteRefacciones.aspx");
            }
        }

        protected void Gv_Folios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Cordenada cordenada = new Cordenada(Gv_Folios);
            if (e.CommandName.Equals("Seleccionar"))
            {
                Session["FolioRefaccion"] = cordenada.consultarValorDeCelda(int.Parse(e.CommandArgument.ToString()), 1);
                Response.Redirect("EdicionReporteRefacciones.aspx");
            }
        }
    }
}