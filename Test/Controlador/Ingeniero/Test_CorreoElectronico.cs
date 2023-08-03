using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using INOLAB_OC.Vista;
using INOLAB_OC.Vista.Ingenieros;

namespace Test.Controlador.Ingeniero
{
    [TestClass]
    public class Test_CorreoElectronico
    {
        [TestMethod]
        public void eviar_Mensaje()
        {
            //Array
            string correoElectronicoEmisor = "notificaciones@inolab.com";
            string correoCliente = "oscar";
            string correosCopia = "omarflores@inolab.com,omar.andreas.sotomayor@gmail.com";
            string contraseña = "Notificaciones2021*";

            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress(correoElectronicoEmisor);
            mensaje.To.Add(correoCliente);
            mensaje.Bcc.Add(correosCopia);
            mensaje.Subject = "Prueba Unitaria 3";
            mensaje.Body = "test";
            mensaje.IsBodyHtml = false;

            CorreoElectronico correo = new CorreoElectronico(correoElectronicoEmisor, contraseña);

            //Assert
            correo.enviar(mensaje);
            Assert.IsFalse(mensaje.IsBodyHtml);

        }
    }
}
