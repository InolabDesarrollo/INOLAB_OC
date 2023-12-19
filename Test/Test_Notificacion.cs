using INOLAB_OC.Responsabilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class Test_Notificacion
    {

        [TestMethod]
        public void enviarNotificacion_String()
        {
            //Arrary
            string cuerpoCorreo = "<!DOCTYPE html>\r\n\r\n<html >\r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n</head>\r\n<body>\r\n   <h2>Entrega de Estandard </h2><br />\r\n    <table border=\"0\" cellpadding=\"8\">\r\n        <tr>\r\n            <td colspan=\"4\" >\r\n                <p  >\r\n                    <font COLOR=\"purple\"  >test</font><br />                   \r\n\r\n                </p><br />\r\n                <p>\r\n                    Este correo se envia automaticamente, favor de NO responder.<br />\r\n                    Saludos\r\n                </p>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n</body>\r\n</html>";
            string correoReceptor = "omarflores@inolab.com";
            string asuntoDeCorreo = "test";
            Notificacion envio = new Notificacion();

            //Action 
            envio.enviarNotificacion(correoReceptor, cuerpoCorreo, asuntoDeCorreo);

            Assert.AreEqual(asuntoDeCorreo, "test");
        }

    }
}
