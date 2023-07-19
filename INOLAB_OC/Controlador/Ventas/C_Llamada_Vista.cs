using DocumentFormat.OpenXml.Drawing.Charts;
using INOLAB_OC.Entidades.Ventas;
using INOLAB_OC.Modelo;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ventas
{
    public class C_Llamada_Vista
    {
        public void actualizarLLamada(E_Llamada_Vista entidad)
        {
            ConexionComercial.executeStoreProcedureStrp_Save_Plan(entidad.Tipo, entidad.Cliente, entidad.FechaLlamada, entidad.Comentario,
                entidad.Asesor, entidad.Objetivo, entidad.Horario);
        }
    }
}