using DocumentFormat.OpenXml.Presentation;
using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Vista.Ingenieros.Responsabilidades
{
    public class ReporteRefaccion : Refaccion
    {
        ReporteRefaccionRepository repositorio = new ReporteRefaccionRepository();
        C_Refaccion controlador;

        public ReporteRefaccion(string idFolio,string numeroRefaccion,string cantidadRefaccion,string descripcion)
        {
            this.idFolioServicio = idFolio;
            this.NumeroRefaccion = numeroRefaccion;
            this.CantidadRefaccion=cantidadRefaccion;
            this.Descripcion= descripcion;
            controlador = new C_Refaccion(repositorio);
        }

        public ReporteRefaccion(string idFolio)
        {
            this.idFolioServicio = idFolio;
            controlador = new C_Refaccion(repositorio);
        }

        public override bool insertarRefaccion(string numeroDePartes, string cantidadDeRefacciones, string descripcionDeRefacion)
        {
            throw new NotImplementedException();
        }

        public int agregarRefaccion()
        {
            ReporteRefaccion refaccion = new ReporteRefaccion(this.idFolioServicio,this.NumeroRefaccion,this.CantidadRefaccion,
                this.Descripcion);
            return repositorio.agregarRefaccion(refaccion);
        }

        public DataTable consultarTodasLasRefacciones()
        {
            DataSet dataSet = controlador.consultarNumeroYCantidadDeRefaccion(this.idFolioServicio); 
            DataTable dataTable = dataSet.Tables[0];
            return dataTable;
        }

    }
}