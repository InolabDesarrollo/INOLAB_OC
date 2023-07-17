using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Modelo.Browser
{
    public class Refaccion_Repository : IRefaccion
    {
        public void actualizarRefaccion(E_Refaccion entidad)
        {
            throw new NotImplementedException();
        }

        public int agregarRefaccion(E_Refaccion entidad)
        {
            string query = "Insert into Refaccion(numRefaccion,cantidadRefaccion,descRefaccion,idFSR)" +
                " values('" + entidad.numRefaccion + "'," + entidad.cantidadRefaccion + ",'" + entidad.descRefaccion + "'," + entidad.idFSR + ");";
            return Conexion.getNumberOfRowsAfected(query);
        }

        public DataSet consultarNumeroYCantidadDeRefaccion(string idFSR)
        {
            return Conexion.getDataSet("select numRefaccion,cantidadRefaccion from " +
                "Refaccion where idFSR= " + idFSR + ";");
        }

        public DataSet consultarTodosLosDatosDeRefaccion(string id)
        {
            throw new NotImplementedException();
        }

        public void eliminarRefaccion(string id)
        {
            throw new NotImplementedException();
        }

        public E_Refaccion optenerPorId(int id)
        {
            E_Refaccion entidad = new E_Refaccion();
            DataRow datosEntidadRefaccion;
            

            throw new NotImplementedException();
        }
    }
}