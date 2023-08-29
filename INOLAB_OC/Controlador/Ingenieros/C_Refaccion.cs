using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser.Interfaces;
using INOLAB_OC.Vista.Ingenieros.Responsabilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ingenieros
{
    public class C_Refaccion
    {
        private readonly IRefaccion repository;

        public C_Refaccion(IRefaccion repository) {
            this.repository = repository;
        }

        public DataSet consultarRefacciones(string idFSR)
        {
            return repository.consultarRefacciones(idFSR);
        }

        public int agregarRefaccion(Refaccion entidad)
        {
            return repository.agregarRefaccion(entidad);
        }

        public void eliminarRefaccion(int id)
        {
            repository.eliminarRefaccion(id);
        }

        public DataSet consultarFoliosPorArea(int areaIngeniero)
        {
            return repository.consultarFoliosPorArea(areaIngeniero);
        } 

        public DataSet consultarFoliosPorAreaYFolio(int areaGerente,string Folio)
        {
            return repository.consultarFoliosPorAreaYFolio(areaGerente, Folio);
        }

        public DataSet consultarIngenierosPorArea(int areaGerente)
        {
            return repository.consultarIngenierosPorArea(areaGerente);
        }

        public DataSet consultarIngenierosPorAreaYNombre(int areaGerente)
        {
            return repository.consultarIngenierosPorArea(areaGerente);
        }

        public DataSet consultarIngenierosPorAreaYNombre(int areaGerente,string nombre)
        {
            return repository.consultarIngenierosPorAreaYNombre(areaGerente, nombre);
        }

        public DataSet consultarReporteRefaccionPorIdIngeniero(string idIngeniero)
        {
            return repository.consultarRegistrosPorIdIngeniero(idIngeniero);
        }

        public DataSet consultarReporteRefaccionPorIdIngeniero(string idIngeniero, bool revisado)
        {
            return repository.consultarRegistrosPorIdIngeniero(idIngeniero, revisado);
        }

        public DataSet consultarTodosLosDatosDeRefacciones(string idFsr)
        {
            return repository.consultarTodosLosDatosDeRefaccion(idFsr);
        }
    }
}