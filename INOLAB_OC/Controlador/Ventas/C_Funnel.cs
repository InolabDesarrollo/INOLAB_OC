using INOLAB_OC.Entidades.Ventas;
using INOLAB_OC.Modelo.Comercial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ventas
{
    public class C_Funnel
    {
        Funnel_Repository repositorioFunnel = new Funnel_Repository();
        public DataRow consultarDatosFunnelPorNoRegistro(int numeroDeRegistro)
        {
            return repositorioFunnel.consultarDatosFunnelPorNoRegistro(numeroDeRegistro);
        }

        public DataRow consultarDatosFunnelPorNoRegistroYUsuario(int numeroDeRegistro, string asesor)
        {
            return repositorioFunnel.consultarDatosFunnelPorNoRegistroYUsuario(numeroDeRegistro, asesor);
        }

        public void actualizarDatosFunel(E_Funnel entidad)
        {
             repositorioFunnel.actualizarDatosFunnel(entidad);
        }

        public DataSet consultarDatosPorAsesorYClasificacion(string asesor, string clasificacion)
        {
            return repositorioFunnel.consultarDatosPorAsesorYClasificacion(asesor, clasificacion);
        }

        public DataSet consultarDatosFunnelPorAsesor(string asesor)
        {
            return repositorioFunnel.consultarDatosFunnelPorAsesor(asesor);
        }

        public DataSet consultaGteAsesorClasificacion(string asesor, string clasificacion_A)
        {
            return repositorioFunnel.consultaGteAsesorClasificacion(asesor,clasificacion_A);
        }

        public DataSet consultarDatosFunnelAutorizar()
        {
            return repositorioFunnel.consultaFunnelAutorizar();
        }
        // CONSUL AUTORIZACION POR GTE
        public DataSet consultarDatosFunnelAutorizarGTE(string gte)
        {
            return repositorioFunnel.consultaFunnelAutorizarGTE(gte);
        }

    }
}