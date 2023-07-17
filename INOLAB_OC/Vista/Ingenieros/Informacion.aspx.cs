using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Security.Principal;
using System.Configuration;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Collections.Generic;
using SpreadsheetLight;
using System.Windows;
using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;

namespace INOLAB_OC
{
   
    public partial class Informacion : System.Web.UI.Page
    {
        const string todosLosFolios = "Todos";

        static  V_FSR_Repository repositorioVFSR = new V_FSR_Repository();
        C_V_FSR controladorVFSR;
        string idUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            controladorVFSR = new C_V_FSR(repositorioVFSR);
            idUsuario = Session["idUsuario"].ToString();
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("./Sesion.aspx");
            }
            else
            {
                verificarSiUsuarioEsJefeDeSuArea();
                lbluser.Text = Session["nameUsuario"].ToString();
            }
        }
      

        public void verificarSiUsuarioEsJefeDeSuArea()
        {
            
            if (Session["idUsuario"].ToString() == "54") //Gustavo
            {
                lblcontador.Text = "Servicios Area Temperatura";
            }
            if (Session["idUsuario"].ToString() == "60") //Sergio
            {
                lblcontador.Text = "Servicios Area Fisicoquímicos";
            }
            if (Session["idUsuario"].ToString() == "30") //Armando
            {
                lblcontador.Text = "Servicios Area Analítica";
            }
        }

        // VALIDACION DE AREA PARA MOSTRAR FOLIOS DEPENDIENDO DEL AREA
        public void datosAnalitica()
        {
                //Carga los folios del ingeniero
                string query = "Select DISTINCT * from  v_fsr where areaservicio='Analitica' AND estatus='" + ddlfiltro.Text + "' order by folio desc";
                GridView1.DataSource = Conexion.getDataSet(query);
               
                GridView1.DataBind();
                contador.Text = GridView1.Rows.Count.ToString(); 
        }
        public void datosTemperatura()
        {
                //Carga los folios del ingeniero
                string query = "Select DISTINCT * from  v_fsr where areaservicio='Temperatura' order by folio desc";
                GridView1.DataSource = Conexion.getDataSet(query);
                
                GridView1.DataBind();
                contador.Text = GridView1.Rows.Count.ToString();
        }
        public void datosFisicoquimicos()
        {
                //Carga los folios del ingeniero
                string query = "Select DISTINCT * from  v_fsr where areaservicio='Fisicoquimico' order by folio desc";
                GridView1.DataSource = Conexion.getDataSet(query);
                
                GridView1.DataBind();
                contador.Text = GridView1.Rows.Count.ToString();
           
        }

        protected void ddlfiltro_SelectedIndexChanged1(object sender, EventArgs e)
        {
            
            if (ddlfiltro.Text.Equals(todosLosFolios))
            {
                GridView1.DataSource = controladorVFSR.consultarTodosLosFoliosDeIngeniero(idUsuario);
            }
            else
            {
                E_V_FSR entidad_VistaFsr = new E_V_FSR();
                entidad_VistaFsr.Estatus = ddlfiltro.Text;
                GridView1.DataSource = controladorVFSR.consultarFolioServicioPorEstatus(entidad_VistaFsr, idUsuario);
            }

            GridView1.DataBind();
            contador.Text = GridView1.Rows.Count.ToString();
        }
        
    }
}