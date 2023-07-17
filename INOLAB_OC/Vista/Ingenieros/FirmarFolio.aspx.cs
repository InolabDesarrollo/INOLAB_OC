
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.IO;
using INOLAB_OC.Modelo;
using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo.Browser;

namespace INOLAB_OC
{
    public partial class FirmarFolio : System.Web.UI.Page
    {
        static FSR_Repository repositorioFSR = new FSR_Repository();
        C_FSR controladorFSR;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idUsuario"] == null)
            {
                Response.Redirect("./Sesion.aspx");
            }
            else
            {
                lbluser.Text = Session["nameUsuario"].ToString();
            }

            controladorFSR = new C_FSR(repositorioFSR, Session["idUsuario"].ToString());
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                firma.Style.Add("display", "block");
                headerid.Style.Add("display", "none");
                sectionreport.Style.Add("display", "none");
                footerid.Style.Add("display", "none");
                string script = "startFirma();";
                ClientScript.RegisterStartupScript(this.GetType(), "Star", script, true);
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
                        throw new Exception(
                            "Missing user name from web.config file");

                    // Password
                    string password =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerPassword"];

                    if (string.IsNullOrEmpty(password))
                        throw new Exception(
                            "Missing password from web.config file");

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

        protected void Mostrar_pantalla_para_firmar_documento_Click(object sender, EventArgs e)
        {
            Response.Redirect("VistaPrevia.aspx");
        }

        protected void Almacenar_la_firma_Click(object sender, EventArgs e)
        {
            string imagenFirma = hidValue.Value;
            string nombreDelCliente = textboxnombre.Text;

            firma.Style.Add("display", "none");
            headerid.Style.Add("display", "block");
            sectionreport.Style.Add("display", "block");
            footerid.Style.Add("display", "flex");

            if (nombreDelCliente.Length < 1)
            {
                nombreDelCliente = "N/A";
            }
            if (actualizaFirmaActualEnElDocumento(nombreDelCliente, imagenFirma))
            {
                ReportViewer1.ServerReport.Refresh();
            }
        }

        protected bool actualizaFirmaActualEnElDocumento(string nombreDeImagenFirma, string image)
        { 
            try
            {
                string[] images = image.Split(',');
                string pattern = @"[^:\s*]\w+\/[\w-+\d.]+(?=[;| ])";
                string tipoDeImagen = "";
                string img1 = images[0];
                string imgenFirma = images[1];

                Regex rx = new Regex(pattern);
                Match m = rx.Match(img1);
                if (m.Success)
                    tipoDeImagen = m.Value;


                int idFirmaIngeniero = Conexion.insertarFirmaImagen(nombreDeImagenFirma, tipoDeImagen, imgenFirma);

                if (idFirmaIngeniero != 0)
                {
                    controladorFSR.actualizarValorDeCampoPorFolioYUsuario(Session["folio_p"].ToString(), "IDFirmaIng", Convert.ToString(idFirmaIngeniero));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar la información');</script>");
                Console.Write(ex.ToString());
                return false;
            }
        }

        protected void finalizarbtn_Click(object sender, EventArgs e)
        {//Realiza el update de los datos en FSR y sollicita la creación del PDF para mandarlo por correo electrónico 
            try
            {
                
                SqlDataReader sqldr = Conexion.getSqlDataReader("select Mail from FSR where Folio = " + Session["folio_p"].ToString() + " and IdFirmaImg is not null;");
                if (sqldr.Read())
                {
                    string mail = sqldr.GetValue(0).ToString();
                    Conexion.cerrarConexion();
                    Conexion.updateHorasDeServicio(Session["folio_p"], Session["idUsuario"]);

                    controladorFSR.actualizarValorDeCampoPorFolioYUsuario(Session["folio_p"].ToString(), "IdStatus", "3");

                    ReportViewer1.ServerReport.Refresh();
                    enviarEmailConInformacionDelFSR(CreatePDF(Session["folio_p"].ToString()), mail);
                    Response.Redirect("ServiciosAsignados.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Falta realizar la firma del reporte');</script>");
                    firma.Style.Add("display", "block");
                    headerid.Style.Add("display", "none");
                    sectionreport.Style.Add("display", "none");
                    footerid.Style.Add("display", "none");
                    string script = "startFirma();";
                    ClientScript.RegisterStartupScript(this.GetType(), "Star", script, true);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private void enviarEmailConInformacionDelFSR(string filepath, string mail)
        {
            try
            {
                string to, bcc, CorreoElectronicoEmisor, EncabezadoDelCorreoElectronico;
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
                    bcc = String.Join(", ", mails);
                }
                else
                {
                    bcc = "carlosflores@inolab.com";
                }
                Conexion.cerrarConexion();
                CorreoElectronicoEmisor = "notificaciones@inolab.com";
                EncabezadoDelCorreoElectronico = "FSR folio " + Session["folio_p"];
                MailMessage message = new MailMessage();

                message.Bcc.Add(bcc);
                message.From = new MailAddress(CorreoElectronicoEmisor);
                message.Body = crearCuerpoDelCorreoElectronico(Session["folio_p"].ToString(), "cliente");
                message.IsBodyHtml = true;
                message.Subject = EncabezadoDelCorreoElectronico;

                Attachment attach = new Attachment(filepath);
                message.Attachments.Add(attach);

                SmtpClient configuracionesCorreoEmisor = new SmtpClient();
                configuracionesCorreoEmisor.Port = 1025;
                configuracionesCorreoEmisor.Host = "smtp.inolab.com";
                configuracionesCorreoEmisor.EnableSsl = false;
                configuracionesCorreoEmisor.DeliveryMethod = SmtpDeliveryMethod.Network;
                configuracionesCorreoEmisor.UseDefaultCredentials = false;
                configuracionesCorreoEmisor.Credentials = new System.Net.NetworkCredential("notificaciones@inolab.com", "Notificaciones2021*");
                configuracionesCorreoEmisor.Send(message);
                message.Dispose();
                configuracionesCorreoEmisor.Dispose();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        private string CreatePDF(string fileName)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = ReportViewer1.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            string filepath = HttpRuntime.AppDomainAppPath + "Docs\\" + fileName + ".pdf";
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                Console.Write(fs.Name);
                fs.Dispose();
            }
            return filepath;
        }

        private string crearCuerpoDelCorreoElectronico(string folio, string cliente)
        {
            string cuerpoDelCorreo = string.Empty;

            using (StreamReader reader = new StreamReader(Server.MapPath("./HTML/index2.html")))
            {
                cuerpoDelCorreo = reader.ReadToEnd();
                reader.Dispose();
            }

            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{folio}", folio);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{cliente}", cliente);
            cuerpoDelCorreo = cuerpoDelCorreo.Replace("{slogan}", "data:image/png;base64," + GetBase64StringForImage(Server.MapPath("./Imagenes/slogan.png")));
            return cuerpoDelCorreo;
        }

        protected static string GetBase64StringForImage(string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        protected void firmaing_Click(object sender, EventArgs e)
        {
            //Muestra la pantalla para firmar el documento que le es requerido
            Response.Redirect("VistaPrevia.aspx");
        }
    }
}