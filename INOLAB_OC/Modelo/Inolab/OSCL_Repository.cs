using INOLAB_OC.Modelo.Inolab.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Modelo.Inolab
{
    public class OSCL_Repository : IOSCL_Repository
    {
        public void cambiarValorDeEstatusFolioAFinalizado(int callId)
        {
            ConexionInolab.executeQuery("Update OSCL set status = -1 where callID= " +callId);
        }
    }
}