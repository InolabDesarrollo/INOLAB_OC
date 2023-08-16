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
            string query = "Insert into ReporteRefaccion(NumeroRefaccion,CantidadRefaccion,Descripcion,idFSR)" +
                " values('" + entidad.NumeroRefaccion + "'," + entidad.CantidadRefaccion + ",'" + entidad.Descripcion + "','" + entidad.idFolioServicio + "');";
            return Conexion.getNumberOfRowsAfected(query);
        }

        public DataSet consultarNumeroYCantidadDeRefaccion(string idFSR)
        {
            throw new NotImplementedException();
        }

        public DataSet consultarTodosLosDatosDeRefaccion(string id)
        {
            throw new NotImplementedException();
        }

        public void eliminarRefaccion(string id)
        {
            throw new NotImplementedException();
        }
    }
}