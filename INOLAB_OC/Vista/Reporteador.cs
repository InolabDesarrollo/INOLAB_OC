using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;

namespace INOLAB_OC.Vista
{
    public class Reporteador
    {
        ReportViewer reportViewer = new ReportViewer();
        public Reporteador()
        {
        }

        [Serializable]
        public sealed class MyReportServerCredentials : IReportServerCredentials
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
                     Web.config file, which can be secured with an ACL.*/
                    string userName =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerUser"];

                    if (string.IsNullOrEmpty(userName))
                        throw new Exception("Missing user name from web.config file");

                    string password =
                        ConfigurationManager.AppSettings
                            ["MyReportViewerPassword"];

                    if (string.IsNullOrEmpty(password))
                        throw new Exception("Missing password from web.config file");

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
                return false;
            }
        }

        public ReportViewer crearReporteador(string folioFSR)
        {
            reportViewer.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            ServerReport serverReport = reportViewer.ServerReport;

            serverReport.ReportServerUrl = new Uri("http://INOLABSERVER01/Reportes_Inolab");
            serverReport.ReportPath = "/OC/FSR Servicio v2";

            ReportParameter reportParameters = new ReportParameter();
            reportParameters.Name = "folio";
            reportParameters.Values.Add(folioFSR);

            reportViewer.ServerReport.SetParameters(new ReportParameter[] { reportParameters });
            reportViewer.ShowParameterPrompts = false;
            return reportViewer;
        }

    }

}