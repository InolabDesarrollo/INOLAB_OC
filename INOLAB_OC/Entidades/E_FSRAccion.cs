using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Entidades
{
    public class E_FSRAccion
    {
        public string idFSRAccion { get; set; }
        public string FechaAccion { get; set; }
        public string HorasAccion { get; set; } 
        public string AccionR { get; set; }
        public string idFolioFSR { get; set; }
        public string idUsuario { get; set; }
        public string FechaSistema { get; set; }

        public E_FSRAccion()
        {

        }
    }
}