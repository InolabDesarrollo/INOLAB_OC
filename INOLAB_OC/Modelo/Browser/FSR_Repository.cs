using INOLAB_OC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace INOLAB_OC.Modelo.Browser
{
    public class FSR_Repository : IFSR_Repository
    {
       
        public DataSet consultarFolioServicioPorEstatus(E_Servicio servicio, string idUsuario)
        {
            string query = "select * from V_FSR where Estatus = '"+servicio.Estatus+"' and " +
                " IdIngeniero= '" + idUsuario + "' order by folio desc ";

            DataSet foliosDeServicio = Conexion.getDataSet(query);
            return foliosDeServicio;
        }

        public DataRow consultarInformacionDeFolioPorFolio(string folio)
        {
            string query = " select * from  v_fsr where Folio = " + folio;
            DataRow informacionDeFolio = Conexion.getDataRow(query);
            return informacionDeFolio;
        }

        public DataRow consultarInformacionDeFolioPorFolioYUsuario(string usuario, string folio)
        {
            string query = "select * from v_fsr where idingeniero = '" + usuario + "' and Folio = '" + folio + "'; ";
            DataRow informacionFolio = Conexion.getDataRow(query);
            return informacionFolio;
        }

        public DataSet consultarTodosLosFoliosDeServicio(string idUsuario)
        {
           string query = "Select DISTINCT * from  v_fsr where " +
                "idingeniero = " + idUsuario + " order by folio desc";

            DataSet todosLosFoliosDelIngeniero = Conexion.getDataSet(query);
            return todosLosFoliosDelIngeniero;
        }

        public string consultarInicioDeServicio(string folio)
        {
            string inicioDeServicio = Conexion.getText("select top 1 Inicio_Servicio from FSR where Folio = " + folio + " and Inicio_Servicio is not null;");
            return inicioDeServicio;
        }

        public void actualizarDatosDeServicio(E_Servicio folioServicioFSR, string idUsuario)
        {
            Conexion.executeQuery(" UPDATE FSR SET Marca='" + folioServicioFSR.Marca + "', Modelo='" + folioServicioFSR.Modelo + "', NoSerie='" + folioServicioFSR.NoSerie +
                     "', Equipo='" + folioServicioFSR.DescripcionEquipo + "', IdEquipo_C='" + folioServicioFSR.IdEquipo + "',Cliente='" + folioServicioFSR.Cliente + "',Telefono='" + folioServicioFSR.Telefono +
                     "',N_Responsable='" + folioServicioFSR.N_Responsable + "',N_Reportado='" + folioServicioFSR.N_Reportado + "',Mail='" + folioServicioFSR.Email + "',Direccion='" + folioServicioFSR.Direccion +
                     "',Localidad='" + folioServicioFSR.Localidad + "',Depto='" + folioServicioFSR.Departamento + "',IdT_Problema='" + folioServicioFSR.IdProblema + "',IdT_Contrato='" + folioServicioFSR.IdContrato +
                     "', IdT_Servicio='" + folioServicioFSR.idServicio + "'" + " where Folio=" + folioServicioFSR.Folio + " and Id_Ingeniero ='" +
                     idUsuario + "';");
        }

        public void iniciarFolioServicio(DateTime fechaYHoraDeInicioDeServicio, string folio, string idIngeniero)
        {
            Conexion.executeQuery(" UPDATE FSR SET idStatus = '2' ,Inicio_Servicio='" +
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', WebFechaIni='" + fechaYHoraDeInicioDeServicio.ToString("yyyy - MM - dd HH: mm:ss.fff")
            + "' where Folio=" + folio + " and Id_Ingeniero =" + idIngeniero + ";");
        }

        public void actualizarFechayHoraFinDeServicio(E_Servicio servicio, string idIngeniero)
        {
            Conexion.executeQuery(" UPDATE FSR SET WebFechaIni ='" + servicio.FechaInicio +
          "' ,WebFechaFin='" + servicio.FechaFin + "' where Folio= " + servicio.Folio +
          " and Id_Ingeniero ='" + idIngeniero + "';");
        }

        public DateTime consultarFechaInicioDeFolio(string folio, string idIngeniero, string campoDondeSeConsulta)
        {
            string query;
            if (campoDondeSeConsulta.Equals("WebFechaIni"))
            {
                query = "select WebFechaIni from FSR where " +
                "Folio=" + folio + " and Id_Ingeniero ='" + idIngeniero + "';";
            }
            else
            {
                query = "select Inicio_Servicio from FSR where " +
               "Folio=" + folio + " and Id_Ingeniero ='" + idIngeniero + "';";
            }
            DateTime fechaInicioDeFolio = Conexion.getDateTime(query);
            return fechaInicioDeFolio;
        }
    

        public DateTime consultarFechaFinDeFolio(string folio, string idIngeniero, string campoDondeSeConsulta)
        {
            string query;
            if (campoDondeSeConsulta.Equals("Fin_Servicio"))
            {
                query = "select  Fin_Servicio from FSR " +
                "where Folio= '" + folio + "' and Id_Ingeniero ='" + idIngeniero + "';";
            }
            else
            {
                query = "select  WebFechaFin from FSR " +
                "where Folio= '" + folio + "' and Id_Ingeniero ='" + idIngeniero + "';";
            }
            DateTime fechaInicioDeFolio  = Conexion.getDateTime(query);
            return fechaInicioDeFolio;
        }

        public string consultarValorDeCampo(string folio, string idUsuario, string campo)
        {
            string _campo = campo;
            string query = "SELECT "+ _campo + " From FSR WHERE Folio = '"+folio+ "' AND Id_Ingeniero = '" + idUsuario + "';";
            return Conexion.getText(query);

        }

        public string consultarValorDeCampo(string folio, string campo)
        {
            string query = "SELECT " + campo + " From FSR WHERE Folio = '" +folio +"';";
            return Conexion.getText(query);
        }

        public void actualizarValorDeCampo(string folio,  string campo, string valorDelCampo)
        {
            string query = "UPDATE FSR SET "+ campo +" = '"+ valorDelCampo + "' where Folio = '"+ folio +"';";
            Conexion.executeQuery(query);
        }

        
        public void actualizarValorDeCampo(string folio, string campo, string valorDelCampo, string idUsuario)
        {
            string query = "UPDATE FSR SET " + campo + " = '" + valorDelCampo + "' where Folio = '" + folio + "' AND Id_Ingeniero = "+ idUsuario + ";";
            Conexion.executeQuery(query);
        }

        public void consultarEstatusDeFolioServicio()
        {

        }

        public int consultarEstatusDeFolioServicio(string folio, string idUsuario)
        {
            string consulta = "SELECT Top 1 IdStatus FROM FSR where Folio= " + folio + " and Id_Ingeniero =" + idUsuario + ";";
            int estatusFolioDeServicio = Conexion.getScalar(consulta);
            return estatusFolioDeServicio;
        }

        public string consultarValorDeCampoPorTop(string folio, string campo)
        {
            string consulta = "Select top (1) "+ campo + " FROM FSR where Folio= " + folio+";";
            return Conexion.getText(consulta);           
        }

        public void actualizarHorasDeServicio(string folioFSR, string idUsuario)
        {
            Conexion.updateHorasDeServicio(folioFSR, idUsuario);
        }

        public string consultarMailDeFolioServicio(string folioFSR)
        {     
            return Conexion.getText("select Mail from FSR where Folio = " + folioFSR + " and IdFirmaImg is not null;");
        }
    }
}