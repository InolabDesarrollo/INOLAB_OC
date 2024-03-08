using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Modelo.Inolab
{
    public class OCLG_Repository
    {
        public void actualizarEstatusFolio(string folioFSR)
        {
            string actualizarEstatusOCLG = "Update OCLG set OCLG.status = -3, OCLG.Closed = 'Y', OCLG.CloseDate =CAST('" +
                   DateTime.Now.ToString("yyyy-MM-dd") + "' AS DATETIME) from OCLG INNER JOIN SCL5 ON OCLG.ClgCode=SCL5.ClgID where SCL5.U_FSR ='" + folioFSR + "'";
            ConexionInolab.executeQuery(actualizarEstatusOCLG);
        }

        public void updateDeConcatenacionDeFolioYEstatus(string folioFSR, string ClgCode)
        {
            //Se hace el update de la concatenacion de Folio y Estatus
            ConexionInolab.executeQuery(" UPDATE OCLG SET tel = '" + folioFSR + " Finalizado' where ClgCode= " + ClgCode + ";");
        }
    }
}