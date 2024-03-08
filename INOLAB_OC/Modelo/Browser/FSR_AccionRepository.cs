using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Modelo.Browser
{
    public class FSR_AccionRepository : IFSR_AccionRepository
    {
        public void actualizarAccion(E_FSRAccion entidad)
        {
            throw new NotImplementedException();
        }

        public int agregarAccion(E_FSRAccion entidad)
        {
            int filasAfectadasPorUpdate = Conexion.getScalar("Insert into FSRAccion(FechaAccion,HorasAccion,AccionR,idFolioFSR,idUsuario, FechaSistema)" +
                " values(CAST('" + entidad.FechaAccion + " " + DateTime.Now.ToString("HH:mm:ss.fff") + "' AS DATETIME)," + entidad.HorasAccion+ ",'" + entidad.AccionR + "'," +
                entidad.idFolioFSR + "," + entidad.idUsuario + ",CAST('" + entidad.FechaSistema + "' AS DATETIME));");

            return filasAfectadasPorUpdate;
        }

        public DataSet consultarTodosLosDatosDeFSRAccion(string idFolioFSR)
        {
            return Conexion.getDataSet("Select * from  FSRAccion where idFolioFSR= " + idFolioFSR + ";");
        }

        public void eliminarAccion(string idFSRAccion)
        {
            Conexion.executeQuery("delete from FSRAccion where idFSRAccion = "+ idFSRAccion+ ";");
        }

        public E_FSRAccion optenerPorId(int idFolioFSRAccion)
        {
            E_FSRAccion entidad = new E_FSRAccion();
            DataRow datosAccionFSR;
            datosAccionFSR = Conexion.getDataRow("SELECT * FROM FSRAccion WHERE idFSRAccion = " + idFolioFSRAccion + ";");

            entidad.AccionR= datosAccionFSR["AccionR"].ToString();
            entidad.HorasAccion = datosAccionFSR["HorasAccion"].ToString();
            entidad.idFSRAccion = datosAccionFSR["IDFSRAccion"].ToString();
            entidad.idFolioFSR = datosAccionFSR["idFolioFSR"].ToString();
            entidad.FechaAccion = Conexion.getText("select convert(varchar, FechaAccion, 105) as FechaAccion from FSRAccion " +
                "where idFSRAccion= " + idFolioFSRAccion + ";");
            entidad.idUsuario = datosAccionFSR["idUsuario"].ToString();
            entidad.FechaSistema = datosAccionFSR["FechaSistema"].ToString();

            return entidad;
        }
    }
}