using DocumentFormat.OpenXml.Drawing;
using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Responsabilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ingenieros
{
    public class C_Comentario_Ingeniero
    {
        SeguimientoFSR_Repository repository;
        public C_Comentario_Ingeniero() {
            repository = new SeguimientoFSR_Repository();   
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
            Notificacion notificacion = new Notificacion();
            V_FSR_Repository repositorioV_FSR = new V_FSR_Repository();
            C_V_FSR controladorV_FSR = new C_V_FSR(repositorioV_FSR);

            string correoAsesor = controladorV_FSR.consultarValorDeCampo("Correoasesor1", Convert.ToString(idFolioServicio));
            string nombreIngeniero = controladorV_FSR.consultarValorDeCampo("Ingeniero", Convert.ToString(idFolioServicio));

            string cuerpoDelCorreo = " <!DOCTYPE html>\r\n\r\n" +
                " <html >\r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n</head>\r\n<body>\r\n   " +
                " <h2>Notificacion comentario de Ingeniero </h2><br /> " +
                " \r\n <table border=\"0\" cellpadding=\"8\">\r\n        <tr>\r\n            <td colspan=\"4\" >\r\n                <p  >\r\n                    <font COLOR=\"black\"  >El ingeniero "+nombreIngeniero+ " realizo el comentario en el folio de servicio "+idFolioServicio+" </font><br />  " +
                " \r\n <b><font COLOR=\"blue\" >Comentario Ingeniero: </font></b> <b>"+comentarioIngeniero+ "</b> <br />                  \r\n                </p><br />" +
                " \r\n <p>\r\n                    " +
                " Este correo se envia automaticamente, favor de NO responder." +
                " <br />\r\n                    Saludos\r\n                </p>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</body>" +
                " \r\n</html>";

            string asuntoDelCorreo = "Comentario de Ingeniero";
            notificacion.enviarNotificacion(correoAsesor, cuerpoDelCorreo, asuntoDelCorreo);
        }

        public string consultarComentario(int idFolioServicio)
        {
            DataTable informacionSeguimiento = repository.consultarInformacionSeguimiento(idFolioServicio); 
            string comentario = informacionSeguimiento.Rows[0]["Comentarios"].ToString();
            return comentario;
        }

    }

}