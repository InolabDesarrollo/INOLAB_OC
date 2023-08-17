using DocumentFormat.OpenXml.Office2013.Excel;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;
using Microsoft.Reporting.WebForms;

namespace INOLAB_OC.Controlador
{
    public class C_FSR
    {
        private  IFSR_Repository _fsrRepository;
        private string _idUsuario;

        public C_FSR(IFSR_Repository browserRepository, string idUsuario) {
            _fsrRepository = browserRepository;
            _idUsuario = idUsuario;
        }
        public C_FSR(IFSR_Repository browserRepository)
        {
            _fsrRepository = browserRepository;
 
        }

        public string verificarSiIniciaOContinuaServicio(string folioFSR)
        {
            string estatusDeServicio = null;
            string inicioServicio = _fsrRepository.consultarInicioDeServicio(folioFSR);

            if (inicioServicio != "")
            {
                estatusDeServicio = "Continuar Servicio";
            }
            else if (inicioServicio.Equals("") || inicioServicio == null)
            {
                estatusDeServicio = "Iniciar Servicio";
            }

            return estatusDeServicio;
        }

        public  void actualizarDatosDeServicio(E_Servicio folioServicioFSR)
        {
            _fsrRepository.actualizarDatosDeServicio(folioServicioFSR, _idUsuario);
        }

        public void iniciarFolioServicio(DateTime fechaYHoraDeInicioDeServicio, string folio)
        {
            _fsrRepository.iniciarFolioServicio(fechaYHoraDeInicioDeServicio, folio, _idUsuario);
        }

        public static void actualizarFolioSap(string folio)
        {
            string consulta = "Select ClgID FROM SCL5 where U_FSR = " + folio;
            int idFolioActividadSap = ConexionInolab.getScalar(consulta);
            ConexionInolab.executeQuery(" UPDATE OCLG SET tel = '" + folio + " En Proceso' where ClgCode=" + idFolioActividadSap.ToString() + ";");

        }

        public  void actualizarFechayHoraFinDeServicio(E_Servicio servicio)
        {
            _fsrRepository.actualizarFechayHoraFinDeServicio(servicio, _idUsuario);
        }

        public  DateTime traerFechaYhoraDeInicioDeFolio(string folio)
        {
            object fecha;
            DateTime fechaYHoraInicioServicio;
            string campoDondeSeConsulta;
            try
            {
                campoDondeSeConsulta = "WebFechaIni";
                fechaYHoraInicioServicio = _fsrRepository.consultarFechaInicioDeFolio(folio, _idUsuario, campoDondeSeConsulta);
 
                return fechaYHoraInicioServicio;

            }
            catch (Exception ex)
            {
                campoDondeSeConsulta = "Inicio_Servicio";
                fechaYHoraInicioServicio = _fsrRepository.consultarFechaInicioDeFolio(folio, _idUsuario, campoDondeSeConsulta);
                
                return fechaYHoraInicioServicio;
            }

        }

        public  DateTime traerFechaYhoraDeFinDeFolio(string folio)
        {
            DateTime fechaYHoraFinServicio;
            string campoDondeSeConsulta;
            try
            {
                campoDondeSeConsulta = "WebFechaFin";
                fechaYHoraFinServicio = _fsrRepository.consultarFechaFinDeFolio(folio, _idUsuario, campoDondeSeConsulta);
                return fechaYHoraFinServicio;

            }
            catch (Exception ex)
            {
                campoDondeSeConsulta = "Fin_Servicio";
                fechaYHoraFinServicio = _fsrRepository.consultarFechaFinDeFolio(folio, _idUsuario, campoDondeSeConsulta);
                return fechaYHoraFinServicio;
            }
        } 

        public  DataRow consultarInformacionFolioServicioPorFolioYUsuario( string usuario, string folio)
        {
            DataRow informacionServicio = _fsrRepository.consultarInformacionDeFolioPorFolioYUsuario(usuario, folio);
            return informacionServicio;
        }

        public string consultarValorDeCampoPorFolioyUsuario(string numeroDeFolioServicio, string campoQueSeConsulta)
        {
           return _fsrRepository.consultarValorDeCampo(numeroDeFolioServicio, _idUsuario, campoQueSeConsulta);
        }

        public string consultarValorDeCampoPorFolio(string numeroDeFolioServicio, string campoQueSeConsulta)
        {
            return _fsrRepository.consultarValorDeCampo(numeroDeFolioServicio, campoQueSeConsulta);
        }

        public bool verificarSiSeEnviaEmailAlAsesor(string idFolioServicio, string campoDondeVerificaras)
        {      
                bool envioDeNotificaciones; 

                string notificacionAlAsesor = _fsrRepository.consultarValorDeCampo(idFolioServicio, campoDondeVerificaras);
                if (notificacionAlAsesor.Equals("Si"))
                {
                    envioDeNotificaciones = true;
                }
                else
                {
                    envioDeNotificaciones = false;
                }
                return envioDeNotificaciones;       
        }

        public void actualizarValorDeCampoPorFolio(string numeroDeFolioServicio, string campoQueActualizas, string valorDelCampo)
        {
            _fsrRepository.actualizarValorDeCampo(numeroDeFolioServicio, campoQueActualizas, valorDelCampo);
        }

        public string verificarSiEnviaNotificacionDeObservacionesAlUsuario(bool Envio_de_notificacion_de_observacion, string idFolioServicio)
        {
            string envioDeNotificacion ="";
            if (Envio_de_notificacion_de_observacion == true)
            {
                _fsrRepository.actualizarValorDeCampo(idFolioServicio, "NotAsesor", "Si");
                envioDeNotificacion = "Si";
            }
            else if (Envio_de_notificacion_de_observacion == false)
            {
                _fsrRepository.actualizarValorDeCampo(idFolioServicio, "NotAsesor", "No");
                envioDeNotificacion = "No";
            }
            return envioDeNotificacion;
        }

        public void actualizarValorDeCampoPorFolioYUsuario(string numeroDeFolioServicio, string campoQueActualizas, 
            string valorDelCampo)
        {
            _fsrRepository.actualizarValorDeCampo(numeroDeFolioServicio, campoQueActualizas, valorDelCampo, _idUsuario);
        }

        public void verificarSiServicioFuncionaCorrectamente(string numeroDeFolioServicio, string CHECKED_ESTA_FUNCIONANDO)
        {
            string texto = CHECKED_ESTA_FUNCIONANDO;
        
            _fsrRepository.actualizarValorDeCampo(numeroDeFolioServicio, "Funcionando", texto, _idUsuario);
        }

        public int consultarEstatusDeFolioServicio(string folioServicio)
        {
            return _fsrRepository.consultarEstatusDeFolioServicio(folioServicio, _idUsuario);
        }

        public string consultarValorDeCampoTop(string folio, string campo)
        {
            return _fsrRepository.consultarValorDeCampoPorTop(folio, campo);
        }

        public void actualizarHorasDeServicio(string folioFSR, string idUsuario)
        {
            _fsrRepository.actualizarHorasDeServicio(folioFSR,  idUsuario);
        }

        public string consultarMailDeFolioServicio(string folioFSR)
        {
            return _fsrRepository.consultarMailDeFolioServicio(folioFSR);
        }

        public void actualizarValorDeCampoNull(string folioFSR)
        {
            _fsrRepository.actualizarValorDeCampoNull(folioFSR);
        }

    }
}