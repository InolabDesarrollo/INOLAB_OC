using INOLAB_OC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Modelo.Inolab
{
    public class SCL5Repository : ISCL5Repository
    {
        public void actualizarValorDeCampo(string campo, string valorDeCampo, string folio)
        {
            string query = "Update SCL5 set "+campo+" = '"+ valorDeCampo + "' where U_FSR ='" + folio + "'";
            ConexionInolab.executeQuery(query);
        }

        public string seleccionarValorDeCampo(string campo, string folio)
        {
            string query = "Select "+campo+" FROM SCL5 where U_FSR = "+folio+";";
            return ConexionInolab.getText(query);
        }

        public string seleccionarValorDeCampoTop(string campo, string folio)
        {
            string query= "Select top (1) "+campo+ " FROM SCL5 where U_FSR= " + folio+";";
            return ConexionInolab.getText(query);
        }

        public int contarFilasDeTablaPorCallId(string callId)
        {
            string query = "Select count(*) FROM SCL5 where SrvcCallId = "+callId+";";
            return ConexionInolab.getScalar(query);
        }

        public string consultarNumeroDeFoliosPorCallId(string callId)
        {
            return ConexionInolab.getText("Select count (DISTINCT U_ESTATUS) FROM SCL5 where SrvcCallId = " + callId.ToString());
        }

        public string consultarEstatusDeFoliosPorCallIdYVisOrder(string callId, int visOrder)
        {
            string query = "Select U_ESTATUS FROM SCL5 where SrvcCallId = " + callId +
                    "and VisOrder = " + visOrder;
            return  ConexionInolab.getText(query);
        }
    }
}