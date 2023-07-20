using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Entidades.Ventas
{
    public class E_Llamada_Vista
    {
        public Int32 Registro { get ; set; }
        public int IdLlamada { get; set; }
        public DateTime FechaLlamada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Cliente { get; set; }
        public string Horario { get; set; }
        public string Asesor { get; set; }
        public string Comentario { get; set; }
        public string Tipo { get; set; }
        public string Objetivo { get; set; }
        public string FechaActualizacion { get; set; }
    }
}