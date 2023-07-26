using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Modelo.Browser
{
    public class MailNotification_Repository : IMailNotification_Repository
    {
        public DataTable consultarTodosLosRegistros()
        {
            DataTable correosReceptores = Conexion.getDataTable("select * from MailNotification;");
            return correosReceptores;
        }

    }
}