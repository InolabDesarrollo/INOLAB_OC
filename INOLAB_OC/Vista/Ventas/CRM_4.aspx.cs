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
using System.IO.Packaging;
using System.Drawing;
using INOLAB_OC.Controlador.Ventas;
using INOLAB_OC.Entidades.Ventas;
using DocumentFormat.OpenXml.Presentation;

namespace INOLAB_OC
{
    public partial class CRM_4 : System.Web.UI.Page
    {

        //variable para saber quien es su gerente
        string gte;
        C_Funnel controladorFunnel = new C_Funnel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("/Sesion.aspx");
            }

            lbluser.Text = Session["nameUsuario"].ToString();
            lbliduser.Text = Session["idUsuario"].ToString();

            //Datos();

            cargarDatosDelAsesor(gte);


        }
        private void Datos()
        {
            GridView1.DataSource = controladorFunnel.consultarDatosFunnelAutorizar();
            GridView1.DataBind();
        }

        protected void Salir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/Sesion.aspx");
        }

        protected void btnAturizaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("Vista/Ventas/CRM_3.aspx");
        }

        protected void ddlF_Asesor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gte_Funnel_Asesor();

        }
        private void Gte_Funnel_Asesor()
        {
        }

        protected void TextBox11_TextChanged(object sender, EventArgs e)
        {

        }
        string autorizacion;
        private void Gte_Registros_Asesor(int numeroDeRegistro)
        {
            DataRow datosFunel = controladorFunnel.consultarDatosFunnelPorNoRegistro(numeroDeRegistro);

            DateTime fechagridcierre = Convert.ToDateTime(datosFunel["FechaCierre"].ToString());
            //FORMATO  DE FECHA EN DATEPICKER
            string CierreEtapa = fechagridcierre.ToString("dd/MM/yyyy");

            txtcliente.Text = datosFunel["Cliente"].ToString();
            ddlClas_save.Text = datosFunel["Clasificacion"].ToString();
            datepicker.Text = CierreEtapa;
            txtequipo.Text = datosFunel["Equipo"].ToString();
            txtvalor.Text = datosFunel["Valor"].ToString();
            txtestatus.Text = datosFunel["Estatus"].ToString();
            txttipoventa.Text = datosFunel["TipoVenta"].ToString();
            txtCambioClasif.Text = datosFunel["C_Clasificacion"].ToString();
            datepicker2.Text = datosFunel["C_FechaCierre"].ToString();
            lblresistro.Text= datosFunel["NoRegistro"].ToString();
            lblautorizar.Text= datosFunel["Autoriza"].ToString();


        }

        // parametro para la consulta en BD 
        int numeroDeRegistro;
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numeroDeRegistro = Convert.ToInt32(GridView1.SelectedRow.Cells[1].Text);
            Gte_Registros_Asesor(numeroDeRegistro);
            //traerRegistrosDelFunnel(numeroDeRegistro);
            //btnGuardar.Visible = false;
            //btnactualiza.Visible = true;
            //lblmensaje_clas.Visible = false;
        }

        private void cargarDatosDelAsesor(string gte)
        {
            GridView1.DataSource = controladorFunnel.consultarDatosFunnelAutorizarGTE(lbluser.Text);
            GridView1.DataBind();
        }

        protected void btnautorizacion_Click(object sender, EventArgs e)
        {
            if(lblautorizar.Text=="Clasificacion")
            {
                //ConexionComercial.executeQuery("Update Funnel set Autoriza=null, fechacierre='" + Convert.ToDateTime(datepicker2.Text).ToString("yyyy-MM-dd") + "',Clasificacion='" + txtCambioClasif.Text + "',c_fechacierre=null,c_clasificacion=null where noregistro=" + Convert.ToInt32(lblresistro.Text));
                ConexionComercial.executeQuery("Update Funnel set Autoriza=null,Clasificacion='" + txtCambioClasif.Text + "',c_fechacierre=null,c_clasificacion=null where noregistro=" + Convert.ToInt32(lblresistro.Text));
                Response.Write("<script language=javascript>if(confirm('Registro Autorizado Correctamente para Cambio de Clasificacion')==true){ location.href='CRM_4.aspx'} else {location.href='CRM_4.aspx'}</script>");
            }
            if (lblautorizar.Text == "Fecha")
            {
                if(string.IsNullOrWhiteSpace(datepicker2.Text))
                {
                    Response.Write("<script>alert('Selecciona la Fecha Cierre de Etapa para Actualizar el cambio de Clasificacion.');</script>");
                    return;

                }
                ConexionComercial.executeQuery("Update Funnel set Autoriza=null, fechacierre='" + Convert.ToDateTime(datepicker2.Text).ToString("yyyy-MM-dd") + "',c_fechacierre=null,c_clasificacion=null where noregistro=" + Convert.ToInt32(lblresistro.Text));
                Response.Write("<script language=javascript>if(confirm('Registro Autorizado Correctamente para Cambio de Fecha Cierre')==true){ location.href='CRM_4.aspx'} else {location.href='CRM_4.aspx'}</script>");
            }
        }

        protected void Btn_VolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            Response.Redirect("./CRM_3.aspx");
        }
    }
}