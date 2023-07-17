using INOLAB_OC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOLAB_OC.Modelo.Inolab
{
    public interface ISCL5Repository
    {
        void actualizarValorDeCampo(string campo, string valorDeCampo, string folio);

        string seleccionarValorDeCampoTop(string campo, string folio);

        string seleccionarValorDeCampo(string campo, string folio);

        int contarFilasDeTablaPorCallId(string callId);

        string consultarNumeroDeFoliosPorCallId(string callId);

        string consultarEstatusDeFoliosPorCallIdYVisOrder(string callId, int visOrder);
    }
}
