using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Entidades
{
    public class E_Refaccion
    {
        private readonly string idFolioServicio;

        public  E_Refaccion()
        {

        }
        public E_Refaccion(string idFolioServicio)
        {
            this.idFolioServicio = idFolioServicio;
        }

        static Refaccion_Repository repositorioRefaccion = new Refaccion_Repository();
        C_Refaccion controladorRefaccion = new C_Refaccion(repositorioRefaccion);

        public string idRefaccion { get; set; }
        public string numRefaccion { get; set; }
        public string cantidadRefaccion { get; set; }
        public string descRefaccion { get; set; }
        public string idFSR { get; set; }

       
        public bool insertarRefaccion(string numeroDePartes, string cantidadDeRefacciones, string descripcionDeRefacion)
        {
            E_Refaccion refaccion = new E_Refaccion();
            refaccion.idFSR = idFolioServicio;
            refaccion.numRefaccion = numeroDePartes;
            refaccion.cantidadRefaccion = cantidadDeRefacciones;
            refaccion.descRefaccion = descripcionDeRefacion;

            int numeroDeFilasAfectadas = controladorRefaccion.agregarRefaccion(refaccion);
            return numeroDeFilasAfectadas == 1 ? true : false;
        }

        public TableRow crearFilaParaRefacciones(string numeroDePartes, string numeroDeRefacciones)
        {
            TableRow fila = new TableRow();
            TableCell[] celda = new TableCell[2];
            celda[0] = new TableCell();
            celda[1] = new TableCell();
            celda[0].Text = numeroDePartes;
            celda[1].Text = numeroDeRefacciones + " pieza(s)";
            fila.Cells.AddRange(celda);
            fila.Style.Add("HorizontalAlign", "Center");
            return fila;
        }
    }
}

