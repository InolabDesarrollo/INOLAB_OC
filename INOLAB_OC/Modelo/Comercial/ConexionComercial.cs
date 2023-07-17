using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Drawing;
using DocumentFormat.OpenXml.Office.Word;

namespace INOLAB_OC.Modelo
{
    public class ConexionComercial
    {
        private static string source;
        private static string catalog;
        private static string user;
        private static string password;
        private static SqlConnection conexion;
        private bool databaseProduction = false;
        private static bool databaseTest = true;

        private static void initDatabase()
        {
            string conexionString = @"Data Source=INOLABSERVER03;Initial Catalog=Comercial;Persist Security Info=True;User ID=ventas;Password=V3ntas_17";
            conexion = new SqlConnection(conexionString);
        }


        public static bool executeQuery(string query)
        {
            initDatabase();
            try
            {
                SqlCommand comand = new SqlCommand(query, conexion);
                conexion.Open();
                comand.ExecuteNonQuery();
                conexion.Close();
                Trace.WriteLine("PASS: SUCESS");
                return true;
            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS: FAILED");
                return false;
            }

        }

        public static int getScalar(string query)
        {
            initDatabase();
            try
            {
                SqlCommand comand = new SqlCommand(query, conexion);
                conexion.Open();
                int escalar = Convert.ToInt32(comand.ExecuteScalar());
                conexion.Close();
                Trace.WriteLine("PASS: SUCESS");
                return escalar;

            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS: FAILED");
                return -1;
            }

        }

        public static string getText(string query)
        {
            initDatabase();
            try
            {
                DataSet table = new DataSet();
                conexion.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                adapter.Fill(table);
                conexion.Close();
                Trace.WriteLine("PASS SUCES");
                return table.Tables[0].Rows[0][0].ToString();

            }
            catch (SqlException ex)
            {
                Trace.WriteLine("PASS FAILED");
                conexion.Close();
                return null;
            }
            catch (IndexOutOfRangeException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS: FAILED ( " + ex.Message + " )");
                return "FUERA_DE_RANGO";
            }

        }

        public static bool isThereSomeInformation(string query)
        {
            initDatabase();
            try
            {
                DataSet table = new DataSet();
                conexion.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                adapter.Fill(table);
                conexion.Close();
                Trace.WriteLine("PAAS SUCCES");
                return table.Tables[0].Rows.Count > 0 ? true : false;
            }
            catch (SqlException ex)
            {
                Trace.WriteLine("PAS FAILED", ex.Message);
                conexion.Close();
                return false;
            }
        }

        public static DataTable getDataTable(string query)
        {
            initDatabase();
            try
            {
                DataSet table = new DataSet();
                conexion.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                adapter.Fill(table);
                conexion.Close();
                Trace.WriteLine("PASS SUCCES");
                return table.Tables[0];

            }
            catch (SqlException ex)
            {
                Trace.WriteLine("PASS FAILED", ex.Message);
                conexion.Close();
                return null;
            }
        }

        public static DataRow getDataRow(string query)
        {
            initDatabase();
            try
            {

                DataSet tabla = new DataSet();
                conexion.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion); //SqlDataAdapter, actúa como un puente entre un DataSet y SQL Server para recuperar y guardar datos.
                adaptador.Fill(tabla);
                conexion.Close();
                Trace.WriteLine("PASS: SUCESS");
                return tabla.Tables[0].Rows[0];
            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS: FAILED ( " + ex.Message + " )");
                return null;
            }
        }

        public static DataSet getDataSet(string query)
        {
            initDatabase();
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                conexion.Close();
                Trace.WriteLine("PASS SUCCES");
                return dataSet;

            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("CONEXION FAILED " + ex.Message);
                return null;
            }
        }

        public static void executeStoreProcedureStp_Update_Plan(int registro, DateTime datePicker, string textoCliente, string textoComentario, string ddlTipoRegistro, string textoObjetivo)
        {
            initDatabase();
            try
            {
                SqlCommand comando = new SqlCommand("stp_update_plan", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Open();

                comando.Parameters.Add("@registro", SqlDbType.Int);
                comando.Parameters.Add("@fecha", SqlDbType.Date);
                comando.Parameters.Add("@cliente", SqlDbType.VarChar);
                comando.Parameters.Add("@comen", SqlDbType.VarChar);
                comando.Parameters.Add("@tipo", SqlDbType.VarChar);
                comando.Parameters.Add("@objetivo", SqlDbType.VarChar);

                comando.Parameters["@registro"].Value = registro;
                comando.Parameters["@fecha"].Value = datePicker;
                comando.Parameters["@cliente"].Value = textoCliente;
                comando.Parameters["@comen"].Value = textoComentario;
                comando.Parameters["@tipo"].Value = ddlTipoRegistro;
                comando.Parameters["@objetivo"].Value = textoObjetivo;

                
                comando.ExecuteNonQuery();
                conexion.Close();
                Trace.WriteLine("SUCCES STORE PROCEDURE");
            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("STORE PROCEDURE FAILED " + ex.Message);
            }
        }

        public static void executeStoreProcedureStrp_Save_Plan(string ddlTipoRegistro, string textoCliente, DateTime datePicker, string comentario,
            string user,string textoObjetivo, string hora)
        {
            initDatabase();
            try
            {
                SqlCommand comando = new SqlCommand("strp_save_plan", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Open();

                comando.Parameters.Add("@tiporegistro", SqlDbType.VarChar);
                comando.Parameters.Add("@cliente", SqlDbType.VarChar);
                comando.Parameters.Add("@fllamada", SqlDbType.Date);
                comando.Parameters.Add("@comentario", SqlDbType.VarChar);
                comando.Parameters.Add("@asesor", SqlDbType.VarChar);
                comando.Parameters.Add("@objetivo", SqlDbType.VarChar);
                comando.Parameters.Add("@hora", SqlDbType.VarChar);

                comando.Parameters["@tiporegistro"].Value = ddlTipoRegistro;
                comando.Parameters["@cliente"].Value = textoCliente;
                comando.Parameters["@fllamada"].Value = datePicker;
                comando.Parameters["@comentario"].Value = comentario;
                comando.Parameters["@asesor"].Value = user;
                comando.Parameters["@objetivo"].Value = textoObjetivo;
                comando.Parameters["@hora"].Value = hora;

                comando.ExecuteNonQuery();
                conexion.Close();
                Trace.WriteLine("SUCCES STORE PROCEDURE");
            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("STORE PROCEDURE FAILED " + ex.Message);
            }
        }

        public static void executeStp_Update_Funnel(int registro,string cliente,string classSave,DateTime datepicker,string equipo,
            string marca, string modelo, string valor, string status, string user,string contacto, string localidad, string origen, string tipoVenta)
        {
            initDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand("stp_update_funnel", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@registro", SqlDbType.Int);
                cmd.Parameters.Add("@cliente", SqlDbType.VarChar);
                cmd.Parameters.Add("@clasif", SqlDbType.VarChar);
                cmd.Parameters.Add("@f_cierre", SqlDbType.Date);
                cmd.Parameters.Add("@equipo", SqlDbType.VarChar);
                cmd.Parameters.Add("@marca", SqlDbType.VarChar);
                cmd.Parameters.Add("@modelo", SqlDbType.VarChar);
                cmd.Parameters.Add("@valor", SqlDbType.Decimal);
                cmd.Parameters.Add("@estatus", SqlDbType.VarChar);
                cmd.Parameters.Add("@asesor", SqlDbType.VarChar);
                cmd.Parameters.Add("@contacto", SqlDbType.VarChar);
                cmd.Parameters.Add("@localidad", SqlDbType.VarChar);
                cmd.Parameters.Add("@origen", SqlDbType.VarChar);
                cmd.Parameters.Add("@tipo", SqlDbType.VarChar);

                conexion.Open();

                cmd.Parameters["@registro"].Value = registro;
                cmd.Parameters["@cliente"].Value = cliente;
                cmd.Parameters["@clasif"].Value = classSave;
                cmd.Parameters["@f_cierre"].Value = datepicker;
                cmd.Parameters["@equipo"].Value = equipo;
                cmd.Parameters["@marca"].Value = marca;
                cmd.Parameters["@modelo"].Value = modelo;
                cmd.Parameters["@valor"].Value = valor;
                cmd.Parameters["@estatus"].Value = status;
                cmd.Parameters["@asesor"].Value = user;
                cmd.Parameters["@contacto"].Value = contacto;
                cmd.Parameters["@localidad"].Value = localidad;
                cmd.Parameters["@origen"].Value = origen;
                cmd.Parameters["@tipo"].Value = tipoVenta;

                
                cmd.ExecuteNonQuery();
                conexion.Close();
                Trace.WriteLine("SUCCES STORE PROCEDURE");
            }
            catch(SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("STORE PROCEDURE FAILED " + ex.Message);
            }
        }

        public static void executeStp_Save_Funnel(string cliente,string clasificacion, string fechaCierre, string equipo,string marca, string modelo,
            string valor, string estatus, string asesor, string contacto, string localidad, string origen, string tipo, string gte)
        {
            initDatabase();
            try
            {
                SqlCommand cmd = new SqlCommand("stp_save_funnel", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@cliente", SqlDbType.VarChar);
                cmd.Parameters.Add("@clasif", SqlDbType.VarChar);
                cmd.Parameters.Add("@f_cierre", SqlDbType.Date);
                cmd.Parameters.Add("@equipo", SqlDbType.VarChar);
                cmd.Parameters.Add("@marca", SqlDbType.VarChar);
                cmd.Parameters.Add("@modelo", SqlDbType.VarChar);
                cmd.Parameters.Add("@valor", SqlDbType.Decimal);
                cmd.Parameters.Add("@estatus", SqlDbType.VarChar);
                cmd.Parameters.Add("@asesor", SqlDbType.VarChar);
                cmd.Parameters.Add("@contacto", SqlDbType.VarChar);
                cmd.Parameters.Add("@localidad", SqlDbType.VarChar);
                cmd.Parameters.Add("@origen", SqlDbType.VarChar);
                cmd.Parameters.Add("@tipo", SqlDbType.VarChar);
                cmd.Parameters.Add("@gte", SqlDbType.VarChar);

                conexion.Open();

                cmd.Parameters["@cliente"].Value = cliente;
                cmd.Parameters["@clasif"].Value = clasificacion;
                cmd.Parameters["@f_cierre"].Value = fechaCierre;
                cmd.Parameters["@equipo"].Value = equipo;
                cmd.Parameters["@marca"].Value = marca;
                cmd.Parameters["@modelo"].Value = modelo;
                cmd.Parameters["@valor"].Value = valor;
                cmd.Parameters["@estatus"].Value = estatus;
                cmd.Parameters["@asesor"].Value = asesor;
                cmd.Parameters["@contacto"].Value = contacto;
                cmd.Parameters["@localidad"].Value = localidad;
                cmd.Parameters["@origen"].Value = origen;
                cmd.Parameters["@tipo"].Value = tipo;
                cmd.Parameters["@gte"].Value = gte;

                cmd.ExecuteNonQuery();
                conexion.Close();
                Trace.WriteLine("SUCCES STORE PROCEDURE");

            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("FAIL STORE PROCEDURE"+ ex.Message);
            }
        }

    }
}