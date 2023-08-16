using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Vista.Ingenieros.Responsabilidades
{
    public abstract class Refaccion
    {
        public  string idFolioServicio { get; set; }
        public  string IdRefaccion { get; set; }
        public string NumeroRefaccion { get; set; }
        public  string CantidadRefaccion { get; set; }
        public  string Descripcion { get; set; }

        public abstract bool insertarRefaccion(string numeroDePartes, string cantidadDeRefacciones, string descripcionDeRefacion);        
        
    }
}