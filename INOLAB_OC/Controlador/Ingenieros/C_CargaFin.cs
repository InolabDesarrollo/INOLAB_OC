using INOLAB_OC.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador
{
    public class C_CargaFin
    {
        public static void cambiarEstatusDeFolioAFinalizado(string folio)
        {
            string actualizacionDeEstatusFolioAFinalizado = "UPDATE FSR SET IdStatus = 3 WHERE Folio = " + folio + " and IdStatus = 2;";
            Conexion.executeQuery(actualizacionDeEstatusFolioAFinalizado);
        }
    }
}