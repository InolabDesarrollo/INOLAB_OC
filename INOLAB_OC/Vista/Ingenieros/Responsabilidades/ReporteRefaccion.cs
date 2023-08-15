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
        public  TableRow crearFilaParaRefacciones(string numeroDePartes, string numeroDeRefacciones, string descripcion)
        {
            TableRow fila = new TableRow();
            TableCell[] celda = new TableCell[3];
            celda[0] = new TableCell();
            celda[1] = new TableCell();
            celda[2] = new TableCell();

            celda[0].Text = numeroDePartes;
            celda[1].Text = numeroDeRefacciones + " pieza(s)";
            celda[2].Text = descripcion;

            fila.Cells.AddRange(celda);
            fila.Style.Add("HorizontalAlign", "Center");
            return fila;
        }

        public override bool insertarRefaccion(string numeroDePartes, string cantidadDeRefacciones, string descripcionDeRefacion)
        {
            throw new NotImplementedException();
        }



    }
}