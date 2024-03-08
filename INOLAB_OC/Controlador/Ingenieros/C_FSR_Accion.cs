using DocumentFormat.OpenXml.Bibliography;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador
{
    public class C_FSR_Accion
    {
        private readonly IFSR_AccionRepository repositorio;

        public C_FSR_Accion(IFSR_AccionRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        public void eliminarAccionFSR(string idFSRAccion)
        {
            repositorio.eliminarAccion(idFSRAccion);
        }

        public E_FSRAccion consultarFSRAccion(int idFSRAccion)
        {
            return repositorio.optenerPorId(idFSRAccion);
        }

        public DataSet consultarDatosDeFSRAccion(string idFolioFSR)
        {
            return repositorio.consultarTodosLosDatosDeFSRAccion(idFolioFSR);
        }

        public int agregarAccionFSR(E_FSRAccion entidad)
        {
            return repositorio.agregarAccion(entidad);
        }

        

    }
}