using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Entidades.Ventas
{
    public class E_Funnel
    {
        public int NoRegistro { get; set; }
        public string Cliente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Valor { get; set; }
        public string Moneda { get; set; }

        public string FechaCierre { get; set; }
        public string Estatus { get; set; }
        public string Clasificacion { get; set; }
        public string FechaCreacion { get; set; }
        public string Asesor { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Equipo { get; set; }
        public string Contacto { get; set; }

        public string Localidad { get; set; }
        public string Origen { get; set; }
        public string TipoVenta { get; set; }


    }
}