using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Entidades
{
    public class E_Servicio
    {
        public int IdFSR {get; set;}

        public int Folio { get; set;}    

        public string Estatus { get; set;}
        public string idCliente { get; set;}

        public string Inicio_Servicio { get; set;}

        public string Fin_Servicio { get; set;}

        public string Marca { get; set;}

        public string Modelo { get; set;}

        public string NoSerie { get; set;}

        public string DescripcionEquipo { get; set;}

        public string Equipo { get; set;}

        public string IdEquipo { get; set;}

        public string Cliente { get; set;}

        public string Telefono { get; set;}

        public string N_Responsable { get; set;}

        public string N_Reportado { get; set;}

        public string Email { get; set;}

        public string Direccion { get; set;}

        public string Localidad { get; set;}

        public string Departamento { get; set;}

        public int IdProblema { get; set;}

        public int IdContrato { get; set;}
        
        public int idServicio { get; set;}

        public string FechaInicio { get; set;}

        public string FechaFin { get; set; }

        public string FechaDeAgendaDeServicio { get; set;}
    }
}