using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2013.Excel;
using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Responsabilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ingenieros
{
    public class C_Comentario_Ingeniero
    {
        private SeguimientoFSR_Repository repository;
        private V_FSR_Repository repositorioV_FSR;
        private string body;
        public C_Comentario_Ingeniero() {
            repository = new SeguimientoFSR_Repository();
            repositorioV_FSR = new V_FSR_Repository();
        }

        public void insertarComentarioIngeniero(SeguimientoFSR seguimiento, string bodyNotification)
        {
            body = bodyNotification;
            bool existeComentarioDeIngeniero = repository.verificarSiExisteComentarioDeIngeniero(seguimiento.IdFsr);
            if (existeComentarioDeIngeniero)
            {
                repository.actualizarComentario(seguimiento);
            }
            else
            {
                repository.insertarComentarioIngeniero(seguimiento);
            }

            bool verificarSiSeEnviaNotificacionAlAsesor = validarSiSeEnviaCorreoAlAsesor(seguimiento.ComentarioIngeniero);
            if (verificarSiSeEnviaNotificacionAlAsesor)
            {
                enviarNotificacionAlAsesor(seguimiento.IdFsr, seguimiento.ComentarioIngeniero);
            }
        }

        private bool validarSiSeEnviaCorreoAlAsesor(string comentarioIngeniero)
        {
            if (comentarioIngeniero.Length >=15 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private void enviarNotificacionAlAsesor(int idFolioServicio, string comentarioIngeniero)
        {         
            string correoAsesor = repositorioV_FSR.consultarValorDeCampo("Correoasesor1", Convert.ToString(idFolioServicio));         
            string cuerpoDelCorreo = crearCuerpoDeCorreoComentario(idFolioServicio, comentarioIngeniero);
            
            string numeroDeActividad = repositorioV_FSR.consultarValorDeCampo("Actividad", Convert.ToString(idFolioServicio));
            string asuntoDelCorreo = "Comentario de La actividad "+numeroDeActividad;

            Notificacion notificacion = new Notificacion();
            notificacion.enviarNotificacion(correoAsesor, cuerpoDelCorreo, asuntoDelCorreo);
        }

        private string crearCuerpoDeCorreoComentario(int idFolioServicio, string comentarioIngeniero)
        {          
            string nombreIngeniero = repositorioV_FSR.consultarValorDeCampo("Ingeniero", Convert.ToString(idFolioServicio));
            string cuerpo = body;
            
            string numeroLLamada = repositorioV_FSR.consultarValorDeCampo("NoLlamada", Convert.ToString(idFolioServicio));
            string numeroDeActividad = repositorioV_FSR.consultarValorDeCampo("Actividad", Convert.ToString(idFolioServicio));
            string cliente = repositorioV_FSR.consultarValorDeCampo("Cliente", Convert.ToString(idFolioServicio));

            cuerpo = cuerpo.Replace("{folio}", Convert.ToString(idFolioServicio));
            cuerpo = cuerpo.Replace("{ingeniero}", nombreIngeniero);
            cuerpo = cuerpo.Replace("{comentario}", comentarioIngeniero);
            cuerpo = cuerpo.Replace("{n_llamada}", numeroLLamada);
            cuerpo = cuerpo.Replace("{actividad}", numeroDeActividad);
            cuerpo = cuerpo.Replace("{cliente}", cliente);

            return cuerpo;
        }

        public string consultarComentario(int idFolioServicio)
        {
            string comentario = "";
            if (repository.checkIfCommentExist(idFolioServicio))
            {
                DataTable informacionSeguimiento = repository.consultarInformacionSeguimiento(idFolioServicio);
                comentario = informacionSeguimiento.Rows[0]["Comentarios"].ToString();
                return comentario;
            }
            else
            {
                return comentario;
            }
        }
    }

}