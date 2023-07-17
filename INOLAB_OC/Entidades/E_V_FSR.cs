using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Entidades
{
    public class E_V_FSR
    {
        public int IdFSR { get; set; }
        public int Folio { get; set; }
        public string Cliente { get; set; }
        public string Departamento { get; set; }
        public int Telefono { get; set; }
        public string Estatus { get; set; }
        public string FechaEnQueSeAgendoServicio { get; set; }
    }
}