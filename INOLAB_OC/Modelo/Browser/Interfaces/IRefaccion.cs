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
        void eliminarRefaccion(int id);

        void actualizarRefaccion(Refaccion entidad);

        int agregarRefaccion(Refaccion entidad);

        DataSet consultarTodosLosDatosDeRefaccion(string id);

        DataSet consultarRefacciones(string idFSR);

        DataSet consultarFoliosPorArea(int areaIngeniero);

        DataSet consultarFoliosPorAreaYFolio(int areaGerente, string folio);

        DataSet consultarIngenierosPorArea(int areaIngeniero);

        DataSet consultarIngenierosPorAreaYNombre(int areaGerente, string nombre);

        void actualizarRegistroRefaccion(Refaccion refaccion, string idReporteRefaccion);

        DataSet consultarRegistrosPorIdIngeniero(string idIngeniero);

        DataSet consultarRegistrosPorIdIngeniero(string idIngeniero, bool revisado);
    }
}
