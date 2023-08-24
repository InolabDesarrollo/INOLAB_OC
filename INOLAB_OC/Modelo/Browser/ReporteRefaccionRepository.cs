using INOLAB_OC.Modelo.Browser.Interfaces;
using INOLAB_OC.Vista.Ingenieros.Responsabilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Modelo.Browser
{
    public class ReporteRefaccionRepository : IRefaccion
    {
        public void actualizarRefaccion(Refaccion entidad)
        {
            throw new NotImplementedException();
        }

        public int agregarRefaccion(Refaccion entidad)
        {
            DateTime fechaActual = DateTime.Now;
            string query = "Insert into ReporteRefaccion(NumeroRefaccion,CantidadRefaccion,Descripcion,FechaRegistro,idFSR)" +
                " values('" + entidad.NumeroRefaccion + "'," + entidad.CantidadRefaccion + ",'" + entidad.Descripcion + "','"+fechaActual.ToString("yyyy-MM-dd") +"', '" + entidad.idFolioServicio + "');";
            return Conexion.getNumberOfRowsAfected(query);
        }

        public DataSet consultarRefacciones(string idFSR)
        {
            return Conexion.getDataSet("select * from " +
                "ReporteRefaccion where idFSR= " + idFSR + ";");
        }

        public DataSet consultarTodosLosDatosDeRefaccion(string id)
        {
            throw new NotImplementedException();
        }

        public void eliminarRefaccion(int id)
        {
            Conexion.executeQuery(" DELETE  FROM ReporteRefaccion WHERE IdReporteRefaccion ="+id+";");
        }

        public DataSet consultarFoliosPorArea(int areaIngeniero)
        {
            return Conexion.getDataSet(" SELECT DISTINCT FSR.Folio,FSR.Id_Ingeniero, CONCAT( Usr.Nombre,' ', Usr.Apellidos) As 'Nombre', Fsr.Cliente, Rr.FechaRegistro \r\n" +
                " FROM ReporteRefaccion Rr\r\n INNER JOIN FSR \r\nON Rr.idFSR = FSR.Folio\r\nINNER JOIN Usuarios Usr\r\nON Usr.idUsuario = FSR.Id_Ingeniero " +
                "\r\n WHERE Usr.IngArea = " + areaIngeniero + ";");
        }

        public DataSet consultarFoliosPorAreaYFolio(int areaIngeniero,string folio)
        {
            return Conexion.getDataSet(" SELECT DISTINCT FSR.Folio,FSR.Id_Ingeniero, CONCAT( Usr.Nombre,' ', Usr.Apellidos) As 'Nombre', Fsr.Cliente, Rr.FechaRegistro \r\n" +
                " FROM ReporteRefaccion Rr\r\n INNER JOIN FSR \r\nON Rr.idFSR = FSR.Folio\r\nINNER JOIN Usuarios Usr\r\nON Usr.idUsuario = FSR.Id_Ingeniero " +
                "\r\n WHERE Usr.IngArea = " + areaIngeniero +" AND Folio = "+folio+ ";");
        }

        public DataSet consultarIngenierosPorArea(int areaGerente)
        {
            return Conexion.getDataSet(" SELECT DISTINCT CONCAT( Usr.Nombre,' ',Usr.Apellidos) As 'Nombre',  FSR.Folio, Fsr.Cliente\r\n " +
                " FROM ReporteRefaccion Rr\r\nINNER JOIN FSR \r\nON Rr.idFSR = FSR.Folio\r\n" +
                " INNER JOIN Usuarios Usr\r\nON Usr.idUsuario = FSR.Id_Ingeniero\r\n" +
                " WHERE Usr.IngArea = " + areaGerente + ";");
        }

        public DataSet consultarIngenierosPorAreaYNombre(int areaGerente, string nombreIngeniero)
        {

            return Conexion.getDataSet(" SELECT DISTINCT CONCAT( Usr.Nombre,' ',Usr.Apellidos) As 'Nombre',  FSR.Folio, Fsr.Cliente\r\n " +
                " FROM ReporteRefaccion Rr\r\nINNER JOIN FSR \r\nON Rr.idFSR = FSR.Folio\r\n" +
                " INNER JOIN Usuarios Usr\r\nON Usr.idUsuario = FSR.Id_Ingeniero\r\n " +
                " WHERE Usr.IngArea = " + areaGerente + " AND Nombre LIKE '%"+nombreIngeniero+ "%'"+ ";");

        }

        public DataSet consultarDatosPorIdReporteRefaccion(string idReporteRefaccion)
        {
            return Conexion.getDataSet("SELECT * FROM ReporteRefaccion WHERE IdReporteRefaccion = " + idReporteRefaccion + ";");
        }

        public void actualizarRegistroRefaccion(Refaccion refaccion, string idReporteRefaccion)
        {
            Conexion.executeQuery("UPDATE ReporteRefaccion SET NumeroRefaccion = "+refaccion.NumeroRefaccion+", CantidadRefaccion = "+refaccion.CantidadRefaccion+", Descripcion = "+refaccion.Descripcion+", " +
                "\r\n RevisoGerente ='1', ComentarioGerente = "+refaccion.ComentarioGerente +" WHERE IdReporteRefaccion = "+ idReporteRefaccion + ";");
        }
    }
}