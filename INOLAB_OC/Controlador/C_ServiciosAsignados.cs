using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using INOLAB_OC.Modelo;

namespace INOLAB_OC.Controlador
{
    public class C_ServiciosAsignados
    {
        public static DataRow InformacionDeFolioParaReporteServicios(string folio)
        {
            string query = "select * from  v_fsr where Folio = " + folio;
            return Conexion.getDataRow(query);
        }
    }
}