using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Vista.Ingenieros.Responsabilidades
{
    public class ReporteRefaccion : Refaccion
    {

        public ReporteRefaccion(string idFolio)
        {
            this.idFolioServicio = idFolioServicio;
        }

        public ReporteRefaccion(string idFolio,string numeroRefaccion,string cantidadRefaccion,string descripcion)
        {
            this.idFolioServicio = idFolioServicio;
            this.NumeroRefaccion = numeroRefaccion;
            this.CantidadRefaccion=cantidadRefaccion;
            this.Descripcion= descripcion;
        }

        public override bool insertarRefaccion(string numeroDePartes, string cantidadDeRefacciones, string descripcionDeRefacion)
        {
            throw new NotImplementedException();
        }



    }
}