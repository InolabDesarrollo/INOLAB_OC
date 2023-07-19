using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Entidades.Ventas
{
    public class E_Funnel
    {
        int NoRegistro { get; set; }
        string Cliente { get; set; }
        string Marca { get; set; }
        string Modelo { get; set; } 
        int Valor { get; set; }
        string Moneda { get; set; }
        string FechaCierre { get; set; }
        string Estatus { get; set; }
        string Clasificacion { get; set; }
        string Localidad { get; set; }
        string Origen { get; set; }

    }
}