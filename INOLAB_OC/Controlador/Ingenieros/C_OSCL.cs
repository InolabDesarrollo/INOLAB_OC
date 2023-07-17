using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Inolab;
using INOLAB_OC.Modelo.Inolab.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ingenieros
{
    public class C_OSCL
    {
        private readonly IOSCL_Repository repositorio;
        private readonly int callId;

        public C_OSCL(IOSCL_Repository repositorio) {
            this.repositorio = repositorio;
        }
        public C_OSCL(IOSCL_Repository repositorio, int callId)
        {
            this.repositorio = repositorio;
            this.callId = callId;
        }

        public void verificarSiSeCierraLaLLamada(string numeroDeValoresEnEstatus, bool HayFoliosConEstatusDiferenteDeFinalizado)
        {
            if (numeroDeValoresEnEstatus == "1" && HayFoliosConEstatusDiferenteDeFinalizado == false)
            {
                repositorio.cambiarValorDeEstatusFolioAFinalizado(callId);
            }
        }

    }
}