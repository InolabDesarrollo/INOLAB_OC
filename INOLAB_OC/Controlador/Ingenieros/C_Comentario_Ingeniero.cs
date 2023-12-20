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
        SeguimientoFSR_Repository repository;
        V_FSR_Repository repositorioV_FSR;
        public C_Comentario_Ingeniero() {
            repository = new SeguimientoFSR_Repository();
            repositorioV_FSR = new V_FSR_Repository();
        }

        public void insertarComentarioIngeniero(SeguimientoFSR seguimiento)
        {
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
            if (comentarioIngeniero.Length >=10 )
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
            string asuntoDelCorreo = "Comentario de Ingeniero";

            Notificacion notificacion = new Notificacion();
            notificacion.enviarNotificacion(correoAsesor, cuerpoDelCorreo, asuntoDelCorreo);
        }

        private string crearCuerpoDeCorreoComentario(int idFolioServicio, string comentarioIngeniero)
        {
            
            string nombreIngeniero = repositorioV_FSR.consultarValorDeCampo("Ingeniero", Convert.ToString(idFolioServicio));
            string cuerpo = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    " +
                " <title>Comentarios de ingeniero para folio {folio} </title>\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"main.css\">\r\n    <style >\r\n        a {\r\n            font-weight: bold;\r\n            color: black;\r\n        }\r\n        body{\r\n            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;\r\n            background: rgb(255,255,250);\r\n            margin: 10px;\r\n            background-repeat: no-repeat;\r\n            background-attachment: fixed;\r\n        }" +
                " \r\n    </style>\r\n</head>\r\n    <body>\r\n\r\n        <table border=\"0\" cellpadding=\"8\"> \r\n            <tr>\r\n            </tr>\r\n            <tr>\r\n                <th valign=\"bottom\" colspan=\"3\">\r\n                    <font size=\"5\">" +
                "\r\n                        Comentario de ingeniero en folio: {folio}\r\n                    </font>\r\n                </th>\r\n            </tr>\r\n            <tr></tr>\r\n            <tr>\r\n                <td colspan=\"4\">\r\n                    <p  >\r\n                        <font COLOR=\"black\"  >El ingeniero  realizo el siguiente comentario en el  folio de servicio </font><br /> \r\n                        <b><font COLOR=\"blue\" >Ingeniero: </font></b> <a class=\"a\">{Ingeniero}</a> <br />\r\n                        <b><font COLOR=\"blue\" >Comentario:</font></b> <a class=\"a\">{comentario}</a> <br />                  \r\n                    </p><br />\r\n                    <p>\r\n                        Este correo se envia automaticamente, favor de NO responder.<br />\r\n                        Saludos\r\n                    </p>\r\n\r\n                </td>\r\n            </tr>\r\n            <tr></tr>\r\n            <tr>\r\n            </tr>\r\n        </table>\r\n    </body>\r\n</html>";
            
            cuerpo = cuerpo.Replace("{folio}", Convert.ToString(idFolioServicio));
            cuerpo = cuerpo.Replace("{Ingeniero}", nombreIngeniero);
            cuerpo = cuerpo.Replace("{comentario}", comentarioIngeniero);

            return cuerpo;
        }

        public string consultarComentario(int idFolioServicio)
        {
            DataTable informacionSeguimiento = repository.consultarInformacionSeguimiento(idFolioServicio); 
            string comentario = informacionSeguimiento.Rows[0]["Comentarios"].ToString();
            return comentario;
        }

    }

}