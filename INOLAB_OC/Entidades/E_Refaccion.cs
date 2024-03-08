using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Vista.Ingenieros.Responsabilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Entidades
{
    public class E_Refaccion:Refaccion
    {
        static Refaccion_Repository repositorioRefaccion = new Refaccion_Repository();
        C_Refaccion controladorRefaccion = new C_Refaccion(repositorioRefaccion);

        public E_Refaccion()
        {

        }
        public E_Refaccion(string _idFolioServicio)
        {
            this.idFolioServicio = _idFolioServicio;
        }

        public  bool insertarRefaccion(string numeroDePartes, string cantidadDeRefacciones, string descripcionDeRefacion)
        {
            E_Refaccion refaccion = new E_Refaccion();
            refaccion.idFolioServicio = idFolioServicio;
            refaccion.NumeroRefaccion = numeroDePartes;
            refaccion.CantidadRefaccion = cantidadDeRefacciones;
            refaccion.Descripcion = descripcionDeRefacion;

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

        public DataSet consultarRefacciones()
        {
            return controladorRefaccion.consultarRefacciones(idFolioServicio);
        }

    }

}

