using INOLAB_OC.Modelo.Browser;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace INOLAB_OC.Controlador.Ingenieros
{
    public class C_MailNotification
    {
        private readonly IMailNotification_Repository repositorio;

        public C_MailNotification(IMailNotification_Repository repositorio)
        {
            this.repositorio = repositorio;
        }
        public string consultarTodosLosCorreoReceptores()
        {
            DataTable tablaCorreosReceptores = repositorio.consultarTodosLosRegistros();
            List<string> listaDecorreos = new List<string>();
            string correosReceptoresDeEmail = null;

            for (int i = 0; i < tablaCorreosReceptores.Rows.Count; i++)
            {
                listaDecorreos.Add(tablaCorreosReceptores.Rows[i]["Mail"].ToString());
                correosReceptoresDeEmail = String.Join(", ", listaDecorreos);
            }
            return correosReceptoresDeEmail;
        }
    }
}