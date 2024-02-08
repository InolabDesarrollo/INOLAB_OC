using INOLAB_OC.Responsabilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Test
{
    [TestClass]
    public class Test_Notificacion
    {
        [TestMethod]
        public void enviarNotificacion_String()
        {
            //Arrary           
            string cuerpo = generarCuerpoDelCorreo();

            string correoReceptor = "omarflores@inolab.com";
            string asuntoDeCorreo = "test";
            Notificacion envio = new Notificacion();

            //Action 
            envio.enviarNotificacion(correoReceptor, cuerpo, asuntoDeCorreo);

            //Assert
            Assert.AreEqual(asuntoDeCorreo, "test");
        }

        private string generarCuerpoDelCorreo()
        {
            int folio = 12;
            string nombreIngeniero = "Omar Flores";
            string comentario = "Comentario de prueba para notificacion de los comentarios del ingeniero ";
            string cuerpo = "\r\n<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <title>Notificacion de Comentario de Ingeniero en Folio: {folio}</title>\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"main.css\">\r\n    <style >\r\n        a {\r\n            font-weight: bold;\r\n            color: black;\r\n        }\r\n        body{\r\n            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;\r\n            background: rgb(255,255,250);\r\n            margin: 10px;\r\n            background-repeat: no-repeat;\r\n            background-attachment: fixed;\r\n        }\r\n    </style>\r\n</head>\r\n    <body>\r\n\r\n        <table border=\"0\" cellpadding=\"8\"> \r\n            <tr>\r\n            </tr>\r\n            <tr>\r\n                <th valign=\"bottom\" colspan=\"3\">\r\n                    <font size=\"5\">\r\n                        Notificacion de Comentario de Ingeniero en Folio:  {folio} \r\n                    </font>\r\n                </th>\r\n                <th valign=\"top\" align=\"center\" colspan=\"3\">\r\n                    <img src=\"http://www.inolab.com/images/logoInolab.jpg\">\r\n                    <img src=\"{slogan}\" width=\"35%\">\r\n                </th>\r\n            </tr>\r\n            <tr></tr>\r\n            <tr>\r\n                <td colspan=\"4\">\r\n                    Buen día!  <br><br>\r\n                    El ingeniero {ingeniero} ha realizádo el comentario sobre la siguiente actividad: {actividad}<br><br>\r\n                    <b><font COLOR=\"blue\" >Número de Llamada:</font>  </b>  <b><a class=\"a\">{n_llamada}</a><br>\r\n                    <b><font COLOR=\"blue\" >Numero de Actividad:</font></b> <b><a class=\"a\">{actividad}</a><br>\r\n                    <b><font COLOR=\"blue\" >Cliente:</font></b>        <b><a class=\"a\">{cliente}</a><br>\r\n                    <b><font COLOR=\"blue\" >Ingeniero:</font></b>      <b><a class=\"a\">{ingeniero}</a><br>\r\n                    <b><font COLOR=\"blue\" >Comentario:</font></b>     <b><a class=\"a\">{comentario}</a></b>  <br />\r\n                </td>\r\n            </tr>\r\n            <tr></tr>\r\n            <tr>\r\n            </tr>\r\n        </table>      \r\n    </body>\r\n</html>";

            cuerpo = cuerpo.Replace("{folio}", Convert.ToString(folio));
            cuerpo = cuerpo.Replace("{n_llamada}", "112-34");
            cuerpo = cuerpo.Replace("{actividad}", "455");
            cuerpo = cuerpo.Replace("{cliente}", "Cliente Prueba");
            cuerpo = cuerpo.Replace("{ingeniero}", nombreIngeniero);
            cuerpo = cuerpo.Replace("{comentario}", comentario);
            
            return cuerpo;
        }
    }
}
