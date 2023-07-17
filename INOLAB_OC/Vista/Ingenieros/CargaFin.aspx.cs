using INOLAB_OC.Modelo;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Diagnostics;
using System.EnterpriseServices;
using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Modelo.Inolab;
using INOLAB_OC.Controlador.Ingenieros;

namespace INOLAB_OC
{
    public partial class CargaFin : Page
    {
        static FSR_Repository repositorioFSR = new FSR_Repository();
        C_FSR controladorFSR;

        static V_FSR_Repository repositorioV_FSR = new V_FSR_Repository();
        C_V_FSR controladorV_FSR;
       
        static SCL5Repository repositorioSCL5 = new SCL5Repository();
        C_SCL5 controladorSCL5;

        static OSCL_Repository repositorioOSCL = new OSCL_Repository();
        C_OSCL controladorOSCL;

        C_OCLG controladorOCLG = new C_OCLG();
        protected void Page_Load(object sender, EventArgs e)
        {
            controladorFSR = new C_FSR(repositorioFSR, Session["folio_p"].ToString());
            controladorV_FSR = new C_V_FSR(repositorioV_FSR);
            controladorSCL5 = new C_SCL5(repositorioSCL5);

            if (Session["idUsuario"] == null)
            {
                Response.Redirect("./Sesion.aspx");
            }
            else
            {
                lbluser.Text = Session["nameUsuario"].ToString();
            }
        }
      
        int cargai =0;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                // Set the processing mode for the ReportViewer to Remote
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;

                ServerReport serverReport = ReportViewer1.ServerReport;

                // Set the report server URL and report path
                serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
                serverReport.ReportPath = "/OC/FSR Servicio v2";

                // Create the sales order number report parameter
                ReportParameter salesOrderNumber = new ReportParameter();
                salesOrderNumber.Name = "folio";
                salesOrderNumber.Values.Add(Session["folio_p"].ToString());

                // Set the report parameters for the report
                ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { salesOrderNumber });
                ReportViewer1.ShowParameterPrompts = false;
            }
        }

        [Serializable]
        public sealed class MyReportServerCredentials :
            IReportServerCredentials
        {//Inicializa el Reporteador
            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    // Use the default Windows user.  Credentials will be
                    // provided by the NetworkCredentials property.
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    /* Read the user information from the Web.config file.  
                     By reading the information on demand instead of 
                     storing it, the credentials will not be stored in 
                     session, reducing the vulnerable surface area to the
                     Web.config file, which can be secured with an ACL.

                     User name */
                    string userName =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerUser"];

                    if (string.IsNullOrEmpty(userName))
                        throw new Exception("Missing user name from web.config file");

                    // Password
                    string password =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerPassword"];

                    if (string.IsNullOrEmpty(password))
                        throw new Exception("Missing password from web.config file");

                    // Domain
                    string domain =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerDomain"];

                    return new NetworkCredential(userName, password, domain);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie,
                        out string userName, out string password,
                        out string authority)
            {
                authCookie = null;
                userName = null;
                password = null;
                authority = null;

                // Not using form credentials
                return false;
            }
        }

        private void actualizarEstatusDeCierreDeActividadEnSap()
        {
            string folioFSR = Session["folio_p"].ToString();
            try
            {
                controladorOCLG.actualizarEstatusFolio(folioFSR);

                controladorSCL5.actualizarValorDeCampo("U_ESTATUS", "Finalizado", folioFSR);                
                string callId = controladorSCL5.seleccionarValorDeCampo("SrvcCallId", folioFSR);
                                          
                string numeroDeValoresEnEstatus =  controladorSCL5.consultarNumeroDeFoliosPorCallId(callId.ToString());
                int numeroDeValoresEnSLC5 = controladorSCL5.contarFilasDeTablaPorCallId(callId.ToString());

                bool HayFoliosConEstatusDiferenteDeFinalizado = controladorSCL5.verificarSiHayFoliosConEstatusDiferenteDeFinalizado(numeroDeValoresEnSLC5, callId.ToString());
                
                controladorOSCL = new C_OSCL(repositorioOSCL, int.Parse(callId));
                controladorOSCL.verificarSiSeCierraLaLLamada(numeroDeValoresEnEstatus, HayFoliosConEstatusDiferenteDeFinalizado);
                
            }
            catch (Exception er)
            {
                Response.Write("<script>alert('Fallo en subir a sap ');</script>");
            }
        }

        private string cuerpoDelCorreoElectronico(string folioDeServicio, string cliente)
        {
            string cuerpoDelCorreo = string.Empty;

            using (StreamReader reader = new StreamReader(Server.MapPath("./HTML/index2.html")))
            {
                cuerpoDelCorreo = reader.ReadToEnd();
                reader.Dispose();
            }

            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{folio}", folioDeServicio);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{cliente}", cliente);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{slogan}", "data:image/png;base64," + convertirImagenAStringBase64(Server.MapPath("./Imagenes/slogan.png")));

            return cuerpoDelCorreo;
        }

        private string CreatePDF(string fileName)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            //Setup the report viewer object and get the array of bytes 
            byte[] bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.  
            string filepath = HttpRuntime.AppDomainAppPath + "Docs\\" + fileName + ".pdf";
            //Si existe este documento en el apartado de Docs, lo sustituye con el nuevo que se esta subiendo
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            //Se crea el PDF del folio para guardarlo en esa localizacion
            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                Console.Write(fs.Name);
                fs.Dispose();
            }
            return filepath;
        }

        protected static string convertirImagenAStringBase64(string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        private void envioDeCorreoElectronicoConInformacionFSR(string mail)
        {
            try
            {
                string asuntoDelCorreoElectronico = "Notificación de observaciones Folio: " + Session["folio_p"].ToString();

                //Datos y cuerpo del correo de a quien ira dirigido y de parte de quien 
                MailAddress correoElectronicoEmisor = new MailAddress("notificaciones@inolab.com");
                MailAddress correoElectronicoReceptor = new MailAddress(mail);
                MailMessage mensajeDeCorreoElectronico = new MailMessage(correoElectronicoEmisor, correoElectronicoReceptor);

                mensajeDeCorreoElectronico.Bcc.Add("luisrosales@inolab.com");
                mensajeDeCorreoElectronico.Body = generarCuerpoDelCorreoVentas(Session["folio_p"].ToString());
                mensajeDeCorreoElectronico.IsBodyHtml = true;
                mensajeDeCorreoElectronico.Subject = asuntoDelCorreoElectronico;

           
                SmtpClient configuracionCorreoElectronico = new SmtpClient();
                configuracionCorreoElectronico.Port = 1025;
                configuracionCorreoElectronico.Host = "smtp.inolab.com";
                configuracionCorreoElectronico.EnableSsl = false;
                configuracionCorreoElectronico.DeliveryMethod = SmtpDeliveryMethod.Network;
                configuracionCorreoElectronico.UseDefaultCredentials = false;
                configuracionCorreoElectronico.Credentials = new System.Net.NetworkCredential("notificaciones@inolab.com", "Notificaciones2021*");
                configuracionCorreoElectronico.Send(mensajeDeCorreoElectronico);
                
                mensajeDeCorreoElectronico.Dispose();
                configuracionCorreoElectronico.Dispose();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private string generarCuerpoDelCorreoVentas(string folio)
        {//Realiza el replace en HTML para la creación del correo
            string cuerpoDelCorreo = string.Empty;
            string folioFSR = Session["folio_p"].ToString();
            using (StreamReader reader = new StreamReader(Server.MapPath("./HTML/index_not_ase.html")))
            {
                cuerpoDelCorreo = reader.ReadToEnd();
                reader.Dispose();
            }
      
            string observacionesDelFolio = controladorFSR.consultarValorDeCampoTop(folioFSR,
                "Observaciones");


            string llamada = "Interna";
            try
            {
                llamada = controladorSCL5.seleccionarValorDeCampoTop("SrvcCallId", folioFSR);
            }
            catch (Exception es)
            {
                Console.Write(es.ToString());
            }
        
            string cliente = controladorFSR.consultarValorDeCampoTop(folioFSR, "Cliente");
            string equipo =  controladorFSR.consultarValorDeCampoTop(folioFSR, "Equipo");
            
            string tipoDeServicio =  controladorV_FSR.consultarValorDeCampoTop("TipoServicio", folioFSR);
            string ingeniero = controladorV_FSR.consultarValorDeCampoTop("Ingeniero", folioFSR);

            string actividad =  controladorV_FSR.consultarValorDeCampoTop("Actividad", folioFSR);
            string OrdenVenta =  controladorV_FSR.consultarValorDeCampoTop("OC", folioFSR);


            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{folio}", folio);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{slogan}", "data:image/png;base64," + convertirImagenAStringBase64(Server.MapPath("./Imagenes/slogan.png")));
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{observaciones}", observacionesDelFolio);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{n_llamada}", llamada);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{act_iv}", actividad);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{OrdenVenta}", OrdenVenta);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{cliente}", cliente);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{equipo}", equipo);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{servicio}", tipoDeServicio);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{ingeniero}", ingeniero);
            return cuerpoDelCorreo;
        }

        private void enviarCorreoElectronicoParaFacturacion(string filepath)
        {
            try
            {
                string asuntoDelCorreoElectronico = "Servicio Terminado. Folio: " + Session["folio_p"].ToString();

                //Datos de a quein ira y de quien va dirigido el correo (me anexo como copia oculta para monitorear que se realice el proceso de forma correcta)
                MailAddress correoElectronicoEmisor = new MailAddress("notificaciones@inolab.com");
                MailAddress correoElectronicoReceptor = new MailAddress("facturacion@inolab.com");
                MailMessage mensajeDelCorreo = new MailMessage(correoElectronicoEmisor, correoElectronicoReceptor);
               
                mensajeDelCorreo.Bcc.Add("luisrosales@inolab.com");
                mensajeDelCorreo.Body = cuerpoDelCorreoElectronicoParaFacturacion(Session["folio_p"].ToString());
                mensajeDelCorreo.IsBodyHtml = true;
                mensajeDelCorreo.Subject = asuntoDelCorreoElectronico;

                Attachment folioQueSeEnviara = new Attachment(filepath);
                mensajeDelCorreo.Attachments.Add(folioQueSeEnviara);

                SmtpClient configuracionEmisor = new SmtpClient();

                configuracionEmisor.Port = 1025;
                configuracionEmisor.Host = "smtp.inolab.com";
                configuracionEmisor.EnableSsl = false;
                configuracionEmisor.DeliveryMethod = SmtpDeliveryMethod.Network;
                configuracionEmisor.UseDefaultCredentials = false;
                configuracionEmisor.Credentials = new System.Net.NetworkCredential("notificaciones@inolab.com", "Notificaciones2021*");
                configuracionEmisor.Send(mensajeDelCorreo);
                mensajeDelCorreo.Dispose();
                configuracionEmisor.Dispose();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private string cuerpoDelCorreoElectronicoParaFacturacion(string folio)
        {
            string cuerpoDelCorreo = string.Empty;
            string folioFSR = Session["folio_p"].ToString();
            using (StreamReader reader = new StreamReader(Server.MapPath("./HTML/index_not_fact.html")))
            {
                cuerpoDelCorreo = reader.ReadToEnd();
                reader.Dispose();
            }

            //En caso de ser llamada interna, no tendra datos en SAP (Este campo queda como "Interno")
            string tipoDeLLamada = "Interna";
            try
            {
                tipoDeLLamada = controladorSCL5.seleccionarValorDeCampoTop("SrvcCallId", Session["folio_p"].ToString());
            }
            catch (Exception es)
            {
                Console.Write(es.ToString());        
            }
            
            //Obtencion de datos para el correo
            string cliente = controladorFSR.consultarValorDeCampoTop("Cliente", folioFSR);
            string actividad =  controladorFSR.consultarValorDeCampoTop("Actividad", folioFSR);
            string OrdenVenta = controladorV_FSR.consultarValorDeCampoTop("OC", folioFSR);

            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{folio}", folio);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{slogan}", "data:image/png;base64," + convertirImagenAStringBase64(Server.MapPath("./Imagenes/slogan.png")));
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{n_llamada}", tipoDeLLamada);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{act_iv}", actividad);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{OrdenVenta}", OrdenVenta);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{cliente}", cliente);
            return cuerpoDelCorreo;
        }

        
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            string folioFSR = Session["folio_p"].ToString();
            string idUsuario = Session["idUsuario"].ToString();
            Timer1.Enabled = false;
            //Realiza el update de los datos en FSR y sollicita la creación del PDF para mandarlo por correo electrónico    
                string correoDeCliente =  controladorFSR.consultarMailDeFolioServicio(folioFSR);
                
                if (!correoDeCliente.Equals("") || correoDeCliente != null)
                {
                    string correoElectronicoCliente = correoDeCliente;

                   //Actualizacion de estatus de el FSR con los datos correspondientes
                   controladorFSR.actualizarHorasDeServicio(folioFSR, idUsuario);
                   actualizarDatosEnSap();
                  
                    C_CargaFin.cambiarEstatusDeFolioAFinalizado(folioFSR);
                    actualizarEstatusDeCierreDeActividadEnSap();
                     
                    ReportViewer1.ServerReport.Refresh();
                    string path = CreatePDF(Session["folio_p"].ToString());
                    notificarAlAsesorDeVentasDadosDeFolioServicio();
                     
                    verificarElTipoDeContrato(path, correoElectronicoCliente);
                    envioDeCorreoElectronicoConInformacionFSR(path, correoElectronicoCliente);
                    Response.Redirect("ServiciosAsignados.aspx");
                }
                else
                {
                    Response.Redirect("VistaPrevia.aspx");
                }
 
        }

        private void actualizarDatosEnSap()
        {          
            string folio = Session["folio_p"].ToString();
            string ClgCode = controladorSCL5.seleccionarValorDeCampo("ClgID", folio);
            controladorOCLG.concatenacionDeFolioYEstatus(folio, ClgCode);
        }

        private void verificarElTipoDeContrato(string path, string correoElectronicoCliente)
        {
            string idContrato = controladorFSR.consultarValorDeCampoTop(Session["folio_p"].ToString(), "IdT_Contrato");
            string servicioPuntual = "7";

            if (idContrato.Equals(servicioPuntual))
            {
                enviarCorreoElectronicoParaFacturacion(path);
            }
           
        }

        private void notificarAlAsesorDeVentasDadosDeFolioServicio()
        {
            if (Session["not_ase"].ToString() == "Si")
            {
                string correoElectronicoAsesor = controladorV_FSR.consultarValorDeCampoTop("Correoasesor1", Session["folio_p"].ToString());
                envioDeCorreoElectronicoConInformacionFSR(correoElectronicoAsesor);
            }
        }

        private void envioDeCorreoElectronicoConInformacionFSR(string filepath, string mail)
        {
            //Envio del correo al cliente con su folio en estatus de Finalizado (hecho por paquito-cabeza [Incomprensible para mis conocimientos de C# :c])
            try
            {
                string to, correoDestinatario, correoElectronicoEmisor, folioDeServico;
                Console.Write(mail);
                to = "";
                SqlDataReader notificacionesDelEmail = Conexion.getSqlDataReader("select * from MailNotification;");

                if (notificacionesDelEmail.HasRows)
                {
                    List<String> mails = new List<string>();
                    while (notificacionesDelEmail.Read())
                    {
                        mails.Add(notificacionesDelEmail.GetValue(2).ToString());
                    }
                    correoDestinatario = String.Join(", ", mails);
                }
                else
                {
                    correoDestinatario = "notificaciones@inolab.com";
                }

                Conexion.cerrarConexion();
                correoElectronicoEmisor = "notificaciones@inolab.com";
                folioDeServico = "FSR folio " + Session["folio_p"];
                MailMessage message = new MailMessage();

                message.Bcc.Add(correoDestinatario);
                message.From = new MailAddress(correoElectronicoEmisor);
                message.Body = cuerpoDelCorreoElectronico(Session["folio_p"].ToString(), "cliente");
                message.IsBodyHtml = true;
                message.Subject = folioDeServico;

                Attachment attach = new Attachment(filepath);
                message.Attachments.Add(attach);

                SmtpClient configuracionDeCorreoEmisor = new SmtpClient();
                configuracionDeCorreoEmisor.Port = 1025;
                configuracionDeCorreoEmisor.Host = "smtp.inolab.com";
                configuracionDeCorreoEmisor.EnableSsl = false;
                configuracionDeCorreoEmisor.DeliveryMethod = SmtpDeliveryMethod.Network;
                configuracionDeCorreoEmisor.UseDefaultCredentials = false;
                configuracionDeCorreoEmisor.Credentials = new NetworkCredential("notificaciones@inolab.com", "Notificaciones2021*");
                configuracionDeCorreoEmisor.Send(message);
                message.Dispose();
                configuracionDeCorreoEmisor.Dispose();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
    }
}