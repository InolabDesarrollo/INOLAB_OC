using DocumentFormat.OpenXml.Wordprocessing;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Modelo.Browser.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador
{
    public class C_V_FSR
    {
        private  IV_FSR_Repository v_FsrRepository;

        public C_V_FSR(IV_FSR_Repository v_FsrRepository)
        {
            this.v_FsrRepository = v_FsrRepository;
        }
        public DataRow consultarInformacionDeFolioServicio(string folio)
        {
            DataRow informacionFolio = v_FsrRepository.consultarInformacionDeFolioPorFolio(folio);
            return informacionFolio;
        }

        public DataSet consultarFolioServicioPorEstatus(E_V_FSR servicio, string idUsuario)
        {
            DataSet foliosDeServicio = v_FsrRepository.consultarFolioServicioPorEstatus(servicio, idUsuario);
            return foliosDeServicio;
        }

        public DataSet consultarTodosLosFoliosDeIngeniero(string idUsuario)
        {
            DataSet foliosDeServicio = v_FsrRepository.consultarTodosLosFoliosDeServicio(idUsuario);
            return foliosDeServicio;
        }

        public bool verificarSiServicioTieneLaFechaDeHoy(E_V_FSR entidadFSR)
        {
            string fechaEnQueSeAgendoServicio = entidadFSR.FechaEnQueSeAgendoServicio;
            string fechaDelDiaActual = DateTime.Now.ToString("dd/MM/yyyy");

            if (fechaDelDiaActual.Equals(fechaEnQueSeAgendoServicio))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public System.Data.DataTable definirColumnasParaReporteServicio()
        {
            System.Data.DataTable reporteServicio = new System.Data.DataTable();

            string[] columnasParaReporteServicio = { "IdFSR", "Folio", "Cliente", "Departamento", "Direccion", "Telefono", "Localidad",
                        "N_Reportado","N_Responsable","Mail","TipoContrato","TipoProblema","TipoServicio","servicio","Ingeniero",
                    "mailIng","F_SolicitudServicio","FechaServicio","Equipo","Marca","Modelo","NoSerie","IdEquipo_C","Estatusid","Estatus","Observaciones",
                    "NoLlamada","Inicio_Servicio","Fin_Servicio","Dia","FallaReportada","HoraServicio","Confirmacion","Propuesta","Actividad","S_Confirmacion",
                    "Asesor1","Correoasesor1","CooreoIng","Proximo_Servicio","idcontrato","idservicio","idproblema","IdResp","Responsable","IdDocumenta","Documentador",
                    "Refaccion","Ingeniero_A1","IdIng_A1","mailIng_A1","Ingeniero_A2","IdIng_A2","mailIng_A2","F_InicioServicio","F_FinServicio","IdT_Servicio","OC","ArchivoAdjunto",
                    "DiaInicioServ","DiaFinServ", "DiasServ","NotAsesor","Funcionando","FallaEncontrada","FechaFirmCliente","NombreCliente"};

            foreach (string columna in columnasParaReporteServicio)
            {
                reporteServicio.Columns.Add(columna, typeof(string));
            }

            return reporteServicio;
        }

        public string consultarValorDeCampo(string campo, string folio)
        {
            return v_FsrRepository.consultarValorDeCampo(campo, folio);
        }

        public string consultarValorDeCampoTop(string campo, string folio)
        {
            return v_FsrRepository.consultarValorDeCampoTop(campo, folio);
        }

        public DataSet consultarFoliosConEstatusFinalizado(string idUsuario)
        {
            return v_FsrRepository.consultarFoliosFinalizados(idUsuario);
        }
    }
}