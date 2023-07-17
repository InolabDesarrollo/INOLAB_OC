using INOLAB_OC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOLAB_OC.Modelo.Browser.Interfaces
{
    public interface IRefaccion
    {
        void eliminarRefaccion(string id);

        void actualizarRefaccion(E_Refaccion entidad);

        int agregarRefaccion(E_Refaccion entidad);
        E_Refaccion optenerPorId(int id);

        DataSet consultarTodosLosDatosDeRefaccion(string id);

        DataSet consultarNumeroYCantidadDeRefaccion(string idFSR);

        
    }
}
