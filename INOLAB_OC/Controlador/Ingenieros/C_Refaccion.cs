using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser.Interfaces;
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

        public DataSet consultarNumeroYCantidadDeRefaccion(string idFSR)
        {
            return repository.consultarNumeroYCantidadDeRefaccion(idFSR);
        }

        public int agregarRefaccion(E_Refaccion entidad)
        {
            return repository.agregarRefaccion(entidad);
        }
    }
}