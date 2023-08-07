
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.IO;
using INOLAB_OC.Modelo;
using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Vista.Ingenieros;

namespace INOLAB_OC
{
    public partial class FirmarFolio : System.Web.UI.Page
    {
        static FSR_Repository repositorioFSR = new FSR_Repository();
        C_FSR controladorFSR;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("/Sesion.aspx"); 
            }
            else
            {
                lbluser.Text = Session["nameUsuario"].ToString();
            }
            controladorFSR = new C_FSR(repositorioFSR, Session["idUsuario"].ToString());
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                firma.Style.Add("display", "block");
                headerid.Style.Add("display", "none");
                sectionreport.Style.Add("display", "none");
                footerid.Style.Add("display", "none");
                string script = "startFirma();";
                ClientScript.RegisterStartupScript(this.GetType(), "Star", script, true);
            }
        }

        protected void Mostrar_pantalla_para_firmar_documento_Click(object sender, EventArgs e)
        {
            Response.Redirect("VistaPrevia.aspx");
        }
        protected void Almacenar_la_firma_Click(object sender, EventArgs e)
        {
            string imagenFirma = hidValue.Value;
            string nombreDelCliente = textboxnombre.Text;
            Firma firmaReporte = new Firma(Session["idUsuario"].ToString(), Session["folio_p"].ToString());

            firma.Style.Add("display", "none");
            headerid.Style.Add("display", "block");
            sectionreport.Style.Add("display", "block");
            footerid.Style.Add("display", "flex");

            if (nombreDelCliente.Length < 1)
            {
                nombreDelCliente = "N/A";
            }
            if (firmaReporte.actualizarFirmaIngeniero(nombreDelCliente, imagenFirma))
            {
                ReportViewer1.ServerReport.Refresh();
            }
        }


        protected void firmaing_Click(object sender, EventArgs e)
        {
            //Muestra la pantalla para firmar el documento que le es requerido
            Response.Redirect("VistaPrevia.aspx");
        }
    }
}