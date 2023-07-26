using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOLAB_OC.Modelo.Browser
{
    public interface IMailNotification_Repository
    {
        DataTable consultarTodosLosRegistros();
    }
}
