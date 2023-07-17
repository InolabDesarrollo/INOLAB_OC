using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Diagnostics;
using System.IO.Packaging;
using System.Data;
using System.Drawing;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office.Word;
using System.Security.Cryptography;
using DocumentFormat.OpenXml.Bibliography;
using System.Runtime.Remoting.Messaging;

namespace INOLAB_OC.Modelo
{
    public class Conexion
    {
        
        private static string source;
        private static string catalog;
        private static string user;
        private static string password;
        private static SqlConnection conexion;
        private static bool databaseProduction = false;
        private static bool databaseTest = true;

        private Conexion()
        {
            if (databaseTest)
            {
                source = "INOLABSERVER03";
                catalog = "BrowserPruebas";
                user = "ventas";
                password = "V3ntas_17";
            }
            else
            {
                source = "INOLABSERVER03";
                catalog = "Browser";
                user = "ventas";
                password = "V3ntas_17";
            }
            string conexionString = @"Data Source=" + source + ";Initial Catalog=" + catalog + ";Persist Security Info=True;User ID=" + user + ";Password= " + password + "";
            conexion = new SqlConnection(conexionString);
        }

        private static void iniciarBaseDeDatos()
        {
            if (databaseTest)
            {
                source = "INOLABSERVER03";
                catalog = "BrowserPruebas";
                user = "ventas";
                password = "V3ntas_17";
            }
            else
            {
                source = "INOLABSERVER03";
                catalog = "Browser";
                user = "ventas";
                password = "V3ntas_17";
            }
            string conexionString = @"Data Source=" + source + ";Initial Catalog=" + catalog + ";Persist Security Info=True;User ID=" + user + ";Password= " + password + "";
            conexion = new SqlConnection(conexionString);
        }

        public static SqlConnection crearConexionABrowser()
        {
            iniciarBaseDeDatos();
            string conexionString = @"Data Source=" + source + ";Initial Catalog=" + catalog + ";Persist Security Info=True;User ID=" + user + ";Password= " + password + "";
            conexion = new SqlConnection(conexionString);
            return conexion;
        }



        public static bool executeQuery(string query)
        {
            iniciarBaseDeDatos();
            try
            {
                SqlCommand comand = new SqlCommand(query, conexion);
                conexion.Open();
                comand.ExecuteNonQuery();
                conexion.Close();
                Trace.WriteLine("PASS: SUCESS executeQuery()");
                return true;
            }catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS: FAILED executeQuery()");
                return false;
            }
            
        }

        public static int getScalar(string query)
        {
            iniciarBaseDeDatos();
            try
            {
                SqlCommand comand = new SqlCommand(query,conexion);
                conexion.Open();
                int escalar = Convert.ToInt32(comand.ExecuteScalar());
                conexion.Close();
                Trace.WriteLine("PASS: SUCESS getScalar()");
                return escalar;

            }catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS: FAILED getScalar()");
                return -1;
            }
           
        }

        public static string getText(string query)
        {
            iniciarBaseDeDatos();
            try
            {
                DataSet table = new DataSet();
                conexion.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                adapter.Fill(table);
                conexion.Close();
                Trace.WriteLine("PASS SUCES");
                return table.Tables[0].Rows[0][0].ToString();

            }catch (SqlException ex)
            {
                Trace.WriteLine("PASS FAILED getText ", ex.Message.ToString());
                conexion.Close();
                return null;
            }
            catch (IndexOutOfRangeException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS: FAILED ( " + ex.Message + " ) getText No encontro texto en esa posicion y devuelve '' ");
                return "";
            }

        }

        public static object getObject(string query)
        {
            iniciarBaseDeDatos();
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(query, conexion);
                object objeto = comando.ExecuteScalar();
                conexion.Close();
                Trace.WriteLine("PASS SUCCES getObject");
                return objeto;
            }
            catch (SqlException ex)
            {
                Trace.WriteLine("PASS FAILED getObject ", ex.Message);
                conexion.Close();
                return null;
            }
            

        }

        public static bool isThereSomeInformation(string query)
        {
            iniciarBaseDeDatos();
            try
            {
                DataSet table = new DataSet();
                conexion.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                adapter.Fill(table);
                conexion.Close();
                Trace.WriteLine("PAAS SUCCES");
                return table.Tables[0].Rows.Count > 0 ? true : false;
            }catch (SqlException ex)
            {
                Trace.WriteLine("PAS FAILED",ex.Message);
                conexion.Close();
                return false;
            }
        }

        public static DataTable getDataTable(string query)
        {
            iniciarBaseDeDatos();
            try
            {
                DataSet table = new DataSet();
                conexion.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query,conexion);
                adapter.Fill(table);
                conexion.Close();
                Trace.WriteLine("PASS SUCCES");
                return table.Tables[0];

            }catch (SqlException ex)
            {
                Trace.WriteLine("PASS FAILED", ex.Message);
                conexion.Close();
                return null;
            }
        }

        public static DataRow getDataRow(string query)
        {
            iniciarBaseDeDatos();
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
            iniciarBaseDeDatos();
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                conexion.Close();
                Trace.WriteLine("PASS SUCCES getDataSet");
                return dataSet;

            }
            catch(SqlException ex) 
            {
                conexion.Close();
                Trace.WriteLine("CONEXION FAILED getDataSet" + ex.Message);
                return null;
            }
        }

        public static DateTime getDateTime(string query)
        {
            iniciarBaseDeDatos();
            DateTime error = DateTime.Now;
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(query, conexion);
                object date = comando.ExecuteScalar();
                DateTime dateTime = (DateTime)date;
                conexion.Close();
                Trace.WriteLine("PASS SUCCES getDateTime");
                return dateTime;
            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS FAILED getDateTime", ex.Message);
                return error;
            }


        }


        public static int getNumberOfRowsAfected(string query)
        {
            iniciarBaseDeDatos();
            try
            {
                conexion.Open();
                SqlCommand insertREF = new SqlCommand(query, conexion);
                int filasAfectadas = insertREF.ExecuteNonQuery();
                conexion.Close();
                Trace.WriteLine("PASS SUCES getNumberOfRowsAfected");
                return filasAfectadas;
            }
            catch (SqlException ex)
            {
                Trace.WriteLine("PASS FAILED getNumberOfRowsAfected", ex.Message);
                conexion.Close();
                return 0;
            }

        }

       public static int insertarFirmaImagen(string nombreDeImagen,string tipoDeImagen,string imagen)
        {
            iniciarBaseDeDatos();
            try
            {
                conexion.Open();
                SqlCommand firma = new SqlCommand("Insert into FirmaImg(ImageName,MimeType,ImageBits)" +
                    " values(@nombre,@mime,@image);" +
                    "SELECT CAST(scope_identity() AS int)", conexion);

                firma.Parameters.Add("@nombre", SqlDbType.VarChar);
                firma.Parameters.Add("@mime", SqlDbType.VarChar);
                firma.Parameters.Add("@image", SqlDbType.VarBinary);
                firma.Parameters["@nombre"].Value = nombreDeImagen;
                firma.Parameters["@mime"].Value = tipoDeImagen;
                firma.Parameters["@image"].Value = Convert.FromBase64String(imagen);
                int escalar = (int)firma.ExecuteScalar();

                return escalar;
            }
            catch(SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS FAILED"+ex.Message);
                return -1;
            }
        }


      public static SqlDataReader getSqlDataReader(string query)
         {
            iniciarBaseDeDatos();
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader sqlDataReader = comando.ExecuteReader();
                Trace.WriteLine("PASS SUCCES getSqlDataReader()");
                return sqlDataReader;
            }
            catch(SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS FAILED getSqlDataReader()");
                return null;
            }
         }
 
       public static void cerrarConexion()
        {
            conexion.Close();
        }  

      public static void updateHorasDeServicio(object folio, object idUsuario)
      {
            iniciarBaseDeDatos();
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(" UPDATE F SET F.IdHorasServicio=A.TotalHoras, " +
                    "F.Fin_Servicio=CAST('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' AS DATETIME) FROM FSR as F INNER JOIN(" +
                "select idFolioFSR, idUsuario, SUM(HorasAccion) as TotalHoras from FSRAccion GROUP BY idFolioFSR, idUsuario) A" +
                 " ON A.idFolioFSR = F.Folio and A.idUsuario = F.Id_Ingeniero WHERE F.Folio = @folio and F.Id_Ingeniero = @usuario;", conexion);

                comando.Parameters.Add("@folio", SqlDbType.Int);
                comando.Parameters.Add("@usuario", SqlDbType.Int);
                comando.Parameters["@folio"].Value = folio;
                comando.Parameters["@usuario"].Value = idUsuario;
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (SqlException ex)
            {
                conexion.Close();
            }
      }

    }
}