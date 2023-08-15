using INOLAB_OC.Entidades;
using INOLAB_OC.Vista.Ingenieros.Responsabilidades;
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

        void actualizarRefaccion(Refaccion entidad);

        int agregarRefaccion(Refaccion entidad);

        DataSet consultarTodosLosDatosDeRefaccion(string id);

        DataSet consultarNumeroYCantidadDeRefaccion(string idFSR);

        
    }
}
