using DocumentFormat.OpenXml.Presentation;
using INOLAB_OC.Entidades.Ventas;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;

namespace INOLAB_OC.Modelo.Comercial
{
    public class Llamada_Vista_Repository
    {
        public void ejecutarStoreProcedureStprSavePlan(E_Llamada_Vista entidad)
        {
            ConexionComercial.executeStoreProcedureStrp_Save_Plan(entidad.Tipo, entidad.Cliente, entidad.FechaLlamada, entidad.Comentario,
                entidad.Asesor, entidad.Objetivo, entidad.Horario);
        }

        public DataSet cargarDatosDependiendoElTipoDeRegistro(string tipoDeRegistro, string asesor)
        {
            string query = "Select * from  Llamada_vista where FechaLlamada between DATEADD(wk,DATEDIFF(wk,0,getdate()),0) and dateadd(wk,datediff(wk,0,getdate()),4) and tipo ='" + tipoDeRegistro + "' and asesor='" + asesor + "'";
            return ConexionComercial.getDataSet(query);
        }

        public DataSet mostrarTodosLosDatosDelAsesor(string asesor)
        {
            string query = " Select * from  Llamada_Vista where asesor='" + asesor + "' and FechaLlamada between DATEADD(wk,DATEDIFF(wk,0,getdate()),0) and dateadd(wk,datediff(wk,0,getdate()),4)";
            return ConexionComercial.getDataSet(query);
        }

        public DataRow mostrarTodosLosDatosPorIdYAsesor(int idLlamada, string asesor)
        {
            return ConexionComercial.getDataRow("select * from llamada_vista where idllamada = " + idLlamada + " and asesor='" + asesor + "'");
        }

        public void actualizarDatosDeRegistro(E_Llamada_Vista entidad)
        {
            ConexionComercial.executeStoreProcedureStp_Update_Plan(entidad.Registro, entidad.FechaLlamada, entidad.Cliente,
                entidad.Comentario, entidad.Tipo, entidad.Objetivo);

        }
    }
}