using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Presentation;
using INOLAB_OC.Entidades.Ventas;
using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Comercial;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ventas
{
    public class C_Llamada_Vista
    {
        Llamada_Vista_Repository repositoryLlamada = new Llamada_Vista_Repository();
        public void actualizarLLamada(E_Llamada_Vista entidad)
        {
            repositoryLlamada.ejecutarStoreProcedureStprSavePlan(entidad);

        }
        public System.Data.DataSet cargarDatosDependiendoTipoDeRegistro(string tipoDeRegistro, string usuario)
        {
            return repositoryLlamada.cargarDatosDependiendoElTipoDeRegistro(tipoDeRegistro, usuario);
        }

        public System.Data.DataSet mostrarTodosLosDatosDelAsesor(string usuario)
        {
            return repositoryLlamada.mostrarTodosLosDatosDelAsesor(usuario);
        }

        public DataRow mostrarTodosLosDatosPorIdYAsesor(int idLlamada, string asesor)
        {
            return repositoryLlamada.mostrarTodosLosDatosPorIdYAsesor(idLlamada, asesor);
        }

        public void actualizarDatosDeRegistro(E_Llamada_Vista entidad)
        {
            repositoryLlamada.actualizarDatosDeRegistro(entidad);
        }

    }
}