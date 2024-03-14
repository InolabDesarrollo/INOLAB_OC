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
        private const string idAreaAnalitica = "1";
        private const string idAreaFisicoquimicos = "2";
        private const string idAreaTemperatura = "3";

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
            this.enviarNotificacionAlAsesor(seguimiento);
        }  
        
        private void enviarNotificacionAlAsesor(SeguimientoFSR seguimiento)
        {                  
            string cuerpoDelCorreo = this.crearCuerpoDeCorreoComentario(seguimiento.IdFsr, seguimiento.ComentarioIngeniero);          
            string numeroDeActividad = repositorioV_FSR.consultarValorDeCampo("Actividad", Convert.ToString(seguimiento.IdFsr));
            string asuntoDelCorreo = "Comentario del folio "+ seguimiento.IdFsr;
            string[] correosParaNotificar = this.buscarCorreosParaNotificar(Convert.ToString(seguimiento.IdFsr), seguimiento.IdIngeniero);

            Notificacion notificacion = new Notificacion();
            notificacion.sendMailNotification(correosParaNotificar, cuerpoDelCorreo, asuntoDelCorreo);
        }

        private string crearCuerpoDeCorreoComentario(int idFolioServicio, string comentarioIngeniero)
        {          
            string nombreIngeniero = repositorioV_FSR.consultarValorDeCampo("Ingeniero", Convert.ToString(idFolioServicio));
            string cuerpo = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <title>Comentario de Ingeniero en Folio N° {folio} </title>\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"main.css\">\r\n    <style>\r\n        a {\r\n            color: black;\r\n            font-weight: normal;\r\n        }\r\n\r\n        body {\r\n            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;\r\n            background: rgb(255,255,250);\r\n            margin: 10px;\r\n            background-repeat: no-repeat;\r\n            background-attachment: fixed;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n\r\n<table border=\"0\" cellpadding=\"8\">\r\n    <tr>\r\n        <th valign=\"bottom\" colspan=\"3\">\r\n            <font size=\"5\">\r\n                Comentario de Ingeniero en Folio N° {folio}\r\n            </font>\r\n        </th>\r\n        <th valign=\"top\" align=\"center\" colspan=\"3\">\r\n            <img src=\"http://www.inolab.com/images/logoInolab.jpg\"><br><br><br>\r\n        </th>\r\n    </tr>\r\n    <tr></tr>\r\n    <tr>\r\n        <td colspan=\"4\">\r\n            Buen día, <br><br>\r\n            El ingeniero {ingeniero} ha reportado lo siguiente:<br><br>\r\n            <font COLOR=\"blue\">Número de Llamada:</font>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n            <a class=\"a\">{n_llamada}</a><br>\r\n\r\n            <font COLOR=\"blue\">Número de Actividad:</font> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n            <a class=\"a\">{actividad}</a><br>\r\n\r\n            <font COLOR=\"blue\">Cliente:</font>  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;\r\n            <a class=\"a\">{cliente}</a><br>\r\n            \r\n            <font COLOR=\"blue\">Equipo:</font>  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;\r\n            <a class=\"a\">{equipo}</a><br>\r\n            \r\n            <font COLOR=\"blue\">Modelo:</font>  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;\r\n            <a class=\"a\">{modelo}</a><br>\r\n            \r\n            <font COLOR=\"blue\">Numero de serie:</font>  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;\r\n            <a class=\"a\">{numeroDeSerie}</a><br>\r\n            \r\n            <font COLOR=\"blue\">Comentario:</font>  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;\r\n            <a class=\"a\">{descripcionDeAccion}</a>  <br /><br>\r\n        </td>\r\n    </tr>\r\n    <td colspan=\"4\">\r\n        Favor no responder este mensaje que ha sido emitido automáticamente por el sistema de <br>\r\n        notificaciones de  <b>INOLAB Especialistas.</b>\r\n    </td>\r\n    <tr>\r\n    </tr>\r\n    <tr>\r\n    </tr>\r\n</table>\r\n</body>\r\n</html>";
            
            string numeroLLamada = repositorioV_FSR.consultarValorDeCampo("NoLlamada", Convert.ToString(idFolioServicio));
            string numeroDeActividad = repositorioV_FSR.consultarValorDeCampo("Actividad", Convert.ToString(idFolioServicio));
            string cliente = repositorioV_FSR.consultarValorDeCampo("Cliente", Convert.ToString(idFolioServicio));
            string equipo = repositorioV_FSR.consultarValorDeCampo("Equipo", Convert.ToString(idFolioServicio));
            string modelo = repositorioV_FSR.consultarValorDeCampo("Modelo", Convert.ToString(idFolioServicio));
            string numeroDeSerie = repositorioV_FSR.consultarValorDeCampo("NoSerie", Convert.ToString(idFolioServicio));

            cuerpo = cuerpo.Replace("{folio}", Convert.ToString(idFolioServicio));
            cuerpo = cuerpo.Replace("{ingeniero}", nombreIngeniero);
            cuerpo = cuerpo.Replace("{descripcionDeAccion}", comentarioIngeniero);
            cuerpo = cuerpo.Replace("{n_llamada}", numeroLLamada);
            cuerpo = cuerpo.Replace("{actividad}", numeroDeActividad);
            cuerpo = cuerpo.Replace("{cliente}", cliente);

            cuerpo = cuerpo.Replace("{equipo}", equipo);
            cuerpo = cuerpo.Replace("{modelo}", modelo);
            cuerpo = cuerpo.Replace("{numeroDeSerie}", numeroDeSerie);
            return cuerpo;
        }

        private string[] buscarCorreosParaNotificar(string idFolioServicio, string Ingeniero)
        {
            try
            {
                string[] correos = new string[3];
                correos[0] = this.obtenerCorreoDeJefeDeArea(Ingeniero);
                correos[1] = repositorioV_FSR.consultarValorDeCampo("Correoasesor1", Convert.ToString(idFolioServicio));
                correos[2] = "coordinacion-servicio@inolab.com";
                return correos;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }          
        }

        private string obtenerCorreoDeJefeDeArea(string engineer)
        {
            UsuarioRepository repository = new UsuarioRepository();
            string areaDelIngeniero = repository.consultarValorDeCampo("IngArea", engineer);
            string correoDeJefeDeArea = "";
            switch (areaDelIngeniero)
            {
                case idAreaAnalitica:
                    correoDeJefeDeArea = "amoreno@inolab.com";
                    break;
                case idAreaFisicoquimicos:
                    correoDeJefeDeArea = "ssantos@inolab.com";
                    break;
                case idAreaTemperatura:
                    correoDeJefeDeArea = "gustavohernandez@inolab.com";
                    break;
            }
            return correoDeJefeDeArea;
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