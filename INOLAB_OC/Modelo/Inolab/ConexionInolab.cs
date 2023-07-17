using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Diagnostics;
using DocumentFormat.OpenXml.Drawing;

namespace INOLAB_OC.Modelo
{
    public class ConexionInolab
    {
        private static string source;
        private static string catalog;
        private static string user;
        private static string password;
        private static SqlConnection conexion;
        private static bool databaseProduction = false;
        private static bool databaseTest = true;

        
        private static void initDatabase()
        {
            if (databaseTest)
            {
                source = "INOLABSERVER03";
                catalog = "Inolab_Test";
                user = "ventas";
                password = "V3ntas_17";
            }
            else
            {
                source = "INOLABSERVER03";
                catalog = "Inolab";
                user = "ventas";
                password = "V3ntas_17";
            }
            string conexionString = @"Data Source=" + source + ";Initial Catalog=" + catalog + ";Persist Security Info=True;User ID=" + user + ";Password= " + password + "";
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
                Trace.WriteLine("PASS: SUCESS executeQuery");
                return true;
            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS: FAILED" +ex.Message);
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
                Trace.WriteLine("PASS SUCES getText");
                return table.Tables[0].Rows[0][0].ToString();

            }
            catch (SqlException ex)
            {
                Trace.WriteLine("PASS FAILED getText "+ex.Message);
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

        public static object getObject(string query)
        {
            initDatabase();
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(query, conexion);
                object objeto = comando.ExecuteScalar();
                conexion.Close();
                Trace.WriteLine("PASS SUCCES");
                return objeto;
            }
            catch (SqlException ex)
            {
                Trace.WriteLine("PASS FAILED", ex.Message);
                conexion.Close();
                return null;
            }

        }

        public static DateTime getDateTime(string query)
        {
            initDatabase();
            DateTime error = DateTime.Now;
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(query, conexion);
                object date = comando.ExecuteScalar();
                DateTime dateTime = (DateTime)date;
                conexion.Close();
                Trace.WriteLine("PASS SUCCES");
                return dateTime;
            }
            catch (SqlException ex)
            {
                conexion.Close();
                Trace.WriteLine("PASS FAILED", ex.Message);
                return error;
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

    }
}