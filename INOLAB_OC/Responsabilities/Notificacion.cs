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
            mensaje.Bcc.Add("omarflores@inolab.com, carlosfores@inolab.com");
            mensaje.Subject = asuntoDelCorreo;
            mensaje.IsBodyHtml = true;
            mensaje.Body = cuerpoCorreo;

            correoElectronico.enviar(mensaje);
        }

        public void sendMailNotification(string[] correosReceptores, string cuerpoCorreo, string asuntoDelCorreo)
        {
            MailAddress emailSender = new MailAddress("notificaciones@inolab.com");
            List<MailAddress> listaDeCorreos = new List<MailAddress>();

            foreach (string correo in correosReceptores)
            {
                listaDeCorreos.Add(new MailAddress(correo));
            }
            MailMessage mensaje = new MailMessage
            {
                From = emailSender,
                Subject = asuntoDelCorreo,
                IsBodyHtml = true,
                Body = cuerpoCorreo
            };

            foreach (MailAddress destinatario in listaDeCorreos)
            {
                mensaje.To.Add(destinatario);
            }
            mensaje.Bcc.Add("omarflores@inolab.com, carlosflores@inolab.com, ehuazo@inolab.com");
            correoElectronico.enviar(mensaje);
        }

    }
}