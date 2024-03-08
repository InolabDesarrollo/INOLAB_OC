using INOLAB_OC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOLAB_OC.Modelo.Browser.Interfaces
{
    public interface IFSR_AccionRepository
    {
        void eliminarAccion(string id);

        void actualizarAccion(E_FSRAccion entidad);

        int agregarAccion(E_FSRAccion entidad);
        E_FSRAccion optenerPorId(int id);

        DataSet consultarTodosLosDatosDeFSRAccion(string id);

    }
}
