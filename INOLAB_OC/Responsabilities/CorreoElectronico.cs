using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;

namespace INOLAB_OC.Vista.Ingenieros
{
   
    public class CorreoElectronico
    {
        SmtpClient smtpClient = new SmtpClient();
        public CorreoElectronico(string correoElectronicoEmisor, string contraseñaDeCorreo) {
            smtpClient.Host = "smtp.inolab.com";
            smtpClient.EnableSsl = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 1025;
            smtpClient.Credentials = new NetworkCredential(correoElectronicoEmisor, contraseñaDeCorreo);
        }

        public void enviar(System.Net.Mail.MailMessage mensaje)
        {          
            try
            {
                smtpClient.Send(mensaje);
                mensaje.Dispose();
                smtpClient.Dispose();

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }
        }
        
    }
}