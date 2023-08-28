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

        public ReporteRefaccion(string idFolio, string numeroRefaccion, string cantidadRefaccion, string descripcion,string comentarioGerente)
        {
            this.idFolioServicio = idFolio;
            this.NumeroRefaccion = numeroRefaccion;
            this.CantidadRefaccion = cantidadRefaccion;
            this.Descripcion = descripcion;
            controlador = new C_Refaccion(repositorio);
            this.ComentarioGerente = comentarioGerente;
        }

        public ReporteRefaccion(string idFolio)
        {
            this.idFolioServicio = idFolio;
            controlador = new C_Refaccion(repositorio);
        }

        public ReporteRefaccion() {
            controlador = new C_Refaccion(repositorio);
        }

        public int agregarRefaccion()
        {
            ReporteRefaccion refaccion = new ReporteRefaccion(this.idFolioServicio,this.NumeroRefaccion,this.CantidadRefaccion,
                this.Descripcion);
            return repositorio.agregarRefaccion(refaccion);
        }

        public void actualizarRefaccion(string id)
        {
            ReporteRefaccion refaccion = new ReporteRefaccion(this.idFolioServicio, this.NumeroRefaccion, this.CantidadRefaccion,
                this.Descripcion,this.ComentarioGerente);
            repositorio.actualizarRegistroRefaccion(refaccion, id);
        }

        public DataTable consultarTodasLasRefacciones()
        {
            DataSet dataSet = controlador.consultarRefacciones(this.idFolioServicio); 
            DataTable dataTable = dataSet.Tables[0];
            return dataTable;
        }

        public string consultarIdReporteRefaccion(int fila)
        {
            DataTable refacciones = consultarTodasLasRefacciones();
            return refacciones.Rows[fila]["IdReporteRefaccion"].ToString();
        }

        public DataRow consultarFilaReporteRefaccion(int fila)
        {
            DataTable refacciones = consultarTodasLasRefacciones();
            DataRow rowDataTable = refacciones.Rows[fila];
            return rowDataTable;
        }

        public void eliminar(int id)
        {
            controlador.eliminarRefaccion(id);
        }

        public DataSet consultarReporteRefaccionPorIdIngeniero(string idIngeniero)
        {
            return controlador.consultarReporteRefaccionPorIdIngeniero(idIngeniero);
        }

        public DataSet consultarReporteRefaccionPorIdIngeniero(string idIngeniero, bool revisado)
        {
            return controlador.consultarReporteRefaccionPorIdIngeniero(idIngeniero,revisado);
        }
    }
}