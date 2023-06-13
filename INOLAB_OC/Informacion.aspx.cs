﻿using System;
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

namespace INOLAB_OC
{
   
    public partial class Informacion : System.Web.UI.Page
    {
        string area;
        const string estatusDeFolioAsignado = "Asignado";
        const string estatusDeFolioEnProceso = "En Proceso";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("./Sesion.aspx");
            }
            else
            {
                //En caso de que sean los jefes de area de los ingenieros tendran acceso al boton de seguimiento por el area a la que representan
                lbluser.Text = Session["nameUsuario"].ToString();

                if (Session["idUsuario"].ToString() == "54") //Gustavo
                {
                    // cg.Visible = true;
                    lblcontador.Text = "Servicios Area Temperatura";
                }
                if (Session["idUsuario"].ToString() == "60") //Sergio
                {
                    // cg.Visible = true;
                    lblcontador.Text = "Servicios Area Fisicoquímicos";
                }
                if (Session["idUsuario"].ToString() == "30") //Armando
                {
                    // cg.Visible = true;
                    lblcontador.Text = "Servicios Area Analítica";
                }
            }
        }
        public void CargaDatos()
        {
            if (Session["idUsuario"].ToString() == "54")
            {
                datosAnalitica();
            }
            if (Session["idUsuario"].ToString() == "60")
            {
                datosFisicoquimicos();
            }
            if (Session["idUsuario"].ToString() == "30")
            {
                datosTemperatura();
            }
        }

        // VALIDACION DE AREA PARA MOSTRAR FSR
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

      
        string consulta = "";

        // filtro de estatus de los FSR
        protected void ddlfiltro_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //Filtro de folios dependiendo a su estado de FSR Estatus 
            if (ddlfiltro.Text.Equals(estatusDeFolioAsignado))
            {
                datosAnalitica();

                consulta = "select *from V_FSR where Estatus='Asignado' and IdIngeniero=" + Session["idusuario"] + " order by folio desc";
                llenarDataGridView();
            }
            if (ddlfiltro.Text.Equals(estatusDeFolioEnProceso))
            {
                //comando = "select *from V_FSR where Estatus='En Proceso' and IdIngeniero=" + Session["idusuario"] + " order by folio desc";
                //sentencia();
            }
            if (ddlfiltro.Text == "Finalizado")
            {
                //comando = "select *from v_fsr where estatus='Finalizado' and idingeniero=" + Session["idusuario"] + " order by folio desc";
                //sentencia();
            }
            if (ddlfiltro.Text == "Todos")
            {
                // cargardatos();
            }
        }
        public void llenarDataGridView()
        {
            GridView1.DataSource = Conexion.getDataSet(consulta);
            GridView1.DataBind();
            contador.Text = GridView1.Rows.Count.ToString();
        }
    }
}