using INOLAB_OC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Modelo.Browser.Interfaces
{
    public interface IV_FSR_Repository
    {
        DataRow consultarInformacionDeFolioPorFolio(string folio);

        DataSet consultarFolioServicioPorEstatus(E_V_FSR servicio, string idUsuario);

        DataSet consultarTodosLosFoliosDeServicio(string idUsuario);

        string consultarValorDeCampo(string campo, string valorDeCampo);

        string consultarValorDeCampoTop(string campo, string folio);

        DataSet consultarFoliosFinalizados(string idUsuario);

        DataSet consultarFoliosPorArea(string area);

    }
}