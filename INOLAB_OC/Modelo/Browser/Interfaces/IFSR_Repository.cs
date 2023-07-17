using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using INOLAB_OC.Entidades;

namespace INOLAB_OC.Modelo.Browser
{
    public interface IFSR_Repository
    {
        DataSet consultarFolioServicioPorEstatus(E_Servicio servicio, string idUsuario);

        DataSet consultarTodosLosFoliosDeServicio(string idUsuario);

        DataRow consultarInformacionDeFolioPorFolio(string folio);

        DataRow consultarInformacionDeFolioPorFolioYUsuario(string usuario, string folio);

        string consultarInicioDeServicio(string folio);

        void actualizarDatosDeServicio(E_Servicio folioServicioFSR, string idUsuario);

        void iniciarFolioServicio(DateTime fechaYHoraDeInicioDeServicio, string folio, string idIngeniero);

        void actualizarFechayHoraFinDeServicio(E_Servicio servicio, string idIngeniero);

        DateTime consultarFechaInicioDeFolio(string folio, string idIngeniero, string campoDondeSeConsulta);

        DateTime consultarFechaFinDeFolio(string folio, string idIngeniero, string campoDondeSeConsulta);

        string consultarValorDeCampo(string folio, string idUsuario, string campo);

        string consultarValorDeCampo(string folio, string campo);

        void actualizarValorDeCampo(string folio,  string campo, string valorDelCampo);

        void actualizarValorDeCampo(string folio, string campo, string valorDelCampo, string idUsuario);

        int consultarEstatusDeFolioServicio(string folio, string idUsuario);

        string consultarValorDeCampoPorTop(string folio, string campo);

        void actualizarHorasDeServicio(string folioFSR, string idUsuario);

        string consultarMailDeFolioServicio(string folioFSR);
    }
}
