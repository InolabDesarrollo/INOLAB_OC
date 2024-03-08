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
            string cuerpo = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <title>Comentario de Ingeniero en  Folio N° {folio} </title>\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"main.css\">\r\n    <style>\r\n        a {\r\n            color: black;\r\n            font-weight: normal;\r\n        }\r\n\r\n        body {\r\n            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;\r\n            background: rgb(255,255,250);\r\n            margin: 10px;\r\n            background-repeat: no-repeat;\r\n            background-attachment: fixed;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n\r\n    <table border=\"0\" cellpadding=\"8\">\r\n        <tr>\r\n        </tr>\r\n        <tr>\r\n            <th valign=\"bottom\" colspan=\"3\">\r\n                <font size=\"5\">\r\n                    Comentario de Ingeniero en  Folio N° {folio}\r\n                </font>\r\n            </th>\r\n            <th valign=\"top\" align=\"center\" colspan=\"3\">\r\n                <img src=\"http://www.inolab.com/images/logoInolab.jpg\"><br><br><br>\r\n            </th>\r\n        </tr>\r\n        <tr></tr>\r\n        <tr>\r\n            <td colspan=\"4\">\r\n                Buen día, <br><br>\r\n                El ingeniero {ingeniero} ha realizádo el comentario sobre la siguiente <b> actividad: {actividad}</b><br><br>\r\n                <font COLOR=\"blue\">Número de Llamada:</font>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n                <a class=\"a\">{n_llamada}</a><br>\r\n\r\n                <font COLOR=\"blue\">Número de Actividad:</font> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  \r\n                <a class=\"a\">{actividad}</a><br>\r\n\r\n                <font COLOR=\"blue\">Cliente:</font>  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;  \r\n                <a class=\"a\">{cliente}</a><br>\r\n\r\n                <font COLOR=\"blue\">Ingeniero:</font>  &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;  \r\n                <a class=\"a\">{ingeniero}</a><br>\r\n\r\n                <font COLOR=\"blue\">Comentario:</font>  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;\r\n                <a class=\"a\">{comentario}</a>  <br /><br>\r\n                      \r\n            </td>\r\n        </tr>\r\n            <td colspan=\"4\">\r\n                Favor no responder este mensaje que ha sido emitido automáticamente por el sistema de <br>\r\n                notificaciones de  <b>INOLAB Especialistas.</b>\r\n            </td>\r\n        <tr>\r\n\r\n        </tr>\r\n        <tr>\r\n        </tr>\r\n    </table>\r\n</body>\r\n</html>";
            
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