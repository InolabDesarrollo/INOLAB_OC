using DocumentFormat.OpenXml.Spreadsheet;
using INOLAB_OC.Vista.Ingenieros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace INOLAB_OC.Responsabilities
{
    public class Notificacion
    {
        CorreoElectronico correoElectronico;
        public Notificacion() {
            correoElectronico = new CorreoElectronico("notificaciones@inolab.com", "Notificaciones2021*");
        }

        public void enviarNotificacion(string correoReceptor, string cuerpoCorreo, string asuntoDelCorreo)
        {
            MailAddress emailSender = new MailAddress("notificaciones@inolab.com");
            MailAddress mailRecipient = new MailAddress(correoReceptor);
            MailMessage mensaje = new MailMessage(emailSender, mailRecipient);

            mensaje.To.Add(mailRecipient);
            mensaje.Bcc.Add("omarflores@inolab.com");
            mensaje.Subject = asuntoDelCorreo;
            mensaje.IsBodyHtml = true;
            mensaje.Body = cuerpoCorreo;

            correoElectronico.enviar(mensaje);
        }

        public void sendMailNotification(string[] correoReceptor, string cuerpoCorreo, string asuntoDelCorreo)
        {
            MailAddress emailSender = new MailAddress("notificaciones@inolab.com");
            List<string> listaDeCorreos = new List<string>();
            var correosEnCopia = "";
            for (int i = 1; i < correoReceptor.Length; i++)
            {
                listaDeCorreos.Add(correoReceptor[i].ToString());
            }
            correosEnCopia = String.Join(", ", listaDeCorreos);
            MailAddress correosReceptores = new MailAddress(correoReceptor[0].ToString());
            MailMessage mensaje = new MailMessage(emailSender, correosReceptores);

            mensaje.To.Add(correosReceptores);
            mensaje.Bcc.Add(correosEnCopia);
            mensaje.Subject = asuntoDelCorreo;
            mensaje.IsBodyHtml = true;
            mensaje.Body = cuerpoCorreo;
            correoElectronico.enviar(mensaje);
        }

    }
}