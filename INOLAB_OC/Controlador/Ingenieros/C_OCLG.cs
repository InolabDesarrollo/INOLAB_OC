using INOLAB_OC.Modelo.Inolab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ingenieros
{
    public class C_OCLG
    {
        OCLG_Repository repositorio = new OCLG_Repository();

        public C_OCLG() { }
        public void actualizarEstatusFolio(string folioFSR)
        {
            repositorio.actualizarEstatusFolio(folioFSR);
        }

        public void concatenacionDeFolioYEstatus(string folioFSR, string ClgCode)
        {
            repositorio.updateDeConcatenacionDeFolioYEstatus(folioFSR, ClgCode);
        }
    }
}