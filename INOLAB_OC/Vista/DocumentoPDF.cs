using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Vista
{
    public class DocumentoPDF
    {
        private readonly string folioFSR;
        Warning[] warnings;
        string[] streamIds;
        string mimeType = string.Empty;
        string encoding = string.Empty;
        string extension = string.Empty;

        Reporteador reporteador = new Reporteador();
        ReportViewer reportViewer = new ReportViewer();
        byte[] bytes;
        string filepath;
        public DocumentoPDF(string folioFSR) {
            reportViewer = reporteador.crearReporteador(folioFSR);
            this.folioFSR = folioFSR;
        }
        public string crearReporteFinalFSR()
        {
            bytes = reportViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            filepath = HttpRuntime.AppDomainAppPath + "Docs\\" + folioFSR + ".pdf";
            if(File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Dispose();
            }
            return filepath; 
        }


    }
}