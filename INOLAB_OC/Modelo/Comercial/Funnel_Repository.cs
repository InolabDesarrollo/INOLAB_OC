﻿using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Presentation;
using INOLAB_OC.Entidades.Ventas;
using INOLAB_OC.Modelo.Comercial.Interfaces;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Modelo.Comercial
{
    public class Funnel_Repository
    {
        public DataRow consultarDatosFunnelPorNoRegistro(int numeroDeRegistro)
        {
            string query = "select * from funnel where noregistro = " + numeroDeRegistro;
            return ConexionComercial.getDataRow(query);
        }

        public DataRow consultarDatosFunnelPorNoRegistroYUsuario(int numeroDeRegistro, string asesor)
        {
            string query = "select * from funnel where noregistro = " + numeroDeRegistro + " and asesor='" + asesor + "'";
            return ConexionComercial.getDataRow(query);
        }
        public void actualizarDatosFunnel(E_Funnel funnel)
        {
            ConexionComercial.executeStp_Update_Funnel(funnel.NoRegistro, funnel.Cliente, funnel.Clasificacion, funnel.FechaActualizacion, funnel.Equipo, funnel.Marca,
                funnel.Modelo, funnel.Valor, funnel.Estatus, funnel.Asesor, funnel.Contacto, funnel.Localidad, funnel.Origen, funnel.TipoVenta);
        }

    }
}