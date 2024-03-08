using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Diagnostics;
namespace INOLAB_OC.Modelo.Browser
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public DataRow OptenerDatosDeUsuario(E_Usuario objetoUsuario)
        {
            DataRow datosDelUsuario = Conexion.getDataRow("select  Usuario, Password_,Nombre, Apellidos,idarea,idrol,IdUsuario from " +
                "Usuarios where usuario='" + objetoUsuario.Nombre + "' and password_='" + objetoUsuario.Contraseña + "'");

            return datosDelUsuario;
        }

        public void executeStoreProcedureLogWeb(E_Usuario usuario)
        {
            SqlConnection conexion = Conexion.crearConexionABrowser();
            try
            {
                SqlCommand comando = new SqlCommand("LogWeb", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@usuario", SqlDbType.VarChar);
                comando.Parameters.Add("@ip", SqlDbType.VarChar);

                comando.Parameters["@usuario"].Value = usuario.NombreDeUsuario;
                comando.Parameters["@ip"].Value = usuario.IP;

                conexion.Open();
                comando.ExecuteNonQuery();
                Trace.WriteLine("Conexion Correcta executeStoreProcedureLogWeb");
                conexion.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("PASS: FAILED " + ex.Message);
            }

        }

        public string consultarValorDeCampo(string campo, string idUsuario)
        {
            string query = "Select "+campo+ " from usuarios where idUsuario = " + idUsuario;
            return Conexion.getText(query);

        }
    }
}