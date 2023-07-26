using System;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser.Interfaces;
using INOLAB_OC.Modelo.Browser;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net.Mail;
using System.Net;
using System.IO;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace INOLAB_OC
{  
    public partial class Sesion : System.Web.UI.Page
    {
        const string areaVentas = "2";
        const string areaServiciosIngenieria = "6";
        const string ceoArtemio = "7";
        const string usuarioElizabethHuazo = "8";

        static UsuarioRepository  repository = new UsuarioRepository();
        C_Usuario controladorUsuario = new C_Usuario(repository);

        protected void Page_Load(object sender, EventArgs e)
        {
          string IP = Request.ServerVariables["REMOTE_ADDR"];
          lblip.Text = IP.ToString();
        }

        protected void Inicio_De_Sesion_Click(object sender, EventArgs e)
        {
            ingresarAlAreaCorrespondiente();         
        }
        public void ingresarAlAreaCorrespondiente()
        {
            try
            {
                Session["fecha1"] = "";
                Session["fecha2"] = "";

                registrarInicioDeSesionDeUsuario();
                E_Usuario objetoUsuario = new E_Usuario();
                objetoUsuario.Nombre = txtUsuario.Text;
                objetoUsuario.Contraseña = txtPass.Text;

                DataRow  dataUser = controladorUsuario.optenerDatosDeUsuario(objetoUsuario);
                
                if ((txtUsuario.Text == dataUser["Usuario"].ToString()) || (txtPass.Text == dataUser["Password_"].ToString()))
                {
                    Response.Write("<script>alert('Ingreso de " + dataUser["Nombre"].ToString() + " " + dataUser["Apellidos"].ToString() + "');</script>");
                }
                string nombreDeUsuario = dataUser["Nombre"].ToString(), idArea = dataUser["IdArea"].ToString(), idRollUser = dataUser["IdRol"].ToString(), nombreDeUsuarioAbreviado = dataUser["Usuario"].ToString();
                
                Session["valor"] = nombreDeUsuario;
                Session["idar"] = idArea;
                Session["idr"] = idRollUser;
                Session["Usuario"] = nombreDeUsuarioAbreviado;


                if (idArea.Equals(areaServiciosIngenieria))
                {
                    Session["idUsuario"] = dataUser["idUsuario"].ToString();
                    Session["nameUsuario"] = dataUser["Nombre"].ToString();
                    Response.Redirect("Vista/Ingenieros/ServiciosAsignados.aspx"); //INOLAB_OC

                }
                else
                {
                    Session["idUsuario"] = dataUser["idUsuario"].ToString();
                    Session["nameUsuario"] = dataUser["Nombre"].ToString();

                    if (Session["idUsuario"].ToString().Equals(usuarioElizabethHuazo))
                    {
                        Response.Redirect("Vista/Ingenieros/CalSel.aspx");
                    }else if (idArea == areaVentas)
                    {
                        if(Session["idUsuario"].ToString() == ceoArtemio)
                        {
                            Response.Redirect("Vista/Ventas/CRM_4.aspx");
                        }
                        else
                        {
                            Response.Redirect("Vista/Ventas/CRM_1.aspx");
                        }
                    }

                }
            }
            catch(Exception ex) 
            {
                Console.Write(ex.Message.ToString());
                Trace.Write(ex.Message.ToString());
                Response.Write("<script>alert('Usuario y/o Contraseña Incorrectos ');</script>");
            }
        }

        private void registrarInicioDeSesionDeUsuario()
        {
            E_Usuario usuario = new E_Usuario();
            usuario.NombreDeUsuario = txtUsuario.Text;
            usuario.IP = lblip.Text;
            controladorUsuario.loggearUsuario(usuario);
            
        }

    }
}