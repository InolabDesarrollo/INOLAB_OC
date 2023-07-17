using System;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo;
using INOLAB_OC.Entidades;

namespace INOLAB_OC
{   //COMENTARIO DE RAMA OMAR_DEVELOP
    public partial class Sesion : System.Web.UI.Page
    {
        const string areaVentas = "2";
        const string areaServiciosIngenieria = "6";
        const string ceoArtemio = "7";
        const string usuarioElizabethHuazo = "8";
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
                //Fechas para el calendario
                Session["fecha1"] = "";
                Session["fecha2"] = "";

                registrarInicioDeSesionDeUsuario();

                DataRow  dataUser = C_Sesion.optenerDatosDeUsuario(txtUsuario.Text, txtPass.Text);
                
                if ((txtUsuario.Text == dataUser["Usuario"].ToString()) || (txtPass.Text == dataUser["Password_"].ToString()))
                {
                    //Response.Write("<script>alert('Ingreso de " + dataUser["Nombre"].ToString() + " " + dataUser["Apellidos"].ToString() + "');</script>");
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
                    Response.Redirect("../Vista/Ingenieros/ServiciosAsignados.aspx");
                    
                }
                else
                {
                    Session["idUsuario"] = dataUser["idUsuario"].ToString();
                    Session["nameUsuario"] = dataUser["Nombre"].ToString();

                    if (Session["idUsuario"].ToString().Equals(usuarioElizabethHuazo))
                    {
                        Response.Redirect("../Vista/Ingenieros/CalSel.aspx");
                    }
                    if (idArea ==areaVentas)
                    {

                        Response.Redirect("../Vista/Ventas/CRM_1.aspx");
                    }

                }
            }
            catch
            {
                Response.Write("<script>alert('Usuario y/o Contraseña Incorrectos');</script>");
            }
        }

        private void registrarInicioDeSesionDeUsuario()
        {
            E_Usuario usuario = new E_Usuario();
            usuario.NombreDeUsuario = txtUsuario.Text;
            usuario.IP = lblip.Text;

            C_Sesion.loggearUsuario(usuario);
        }

    }
}