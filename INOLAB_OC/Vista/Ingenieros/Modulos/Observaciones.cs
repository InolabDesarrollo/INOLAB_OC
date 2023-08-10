using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Vista.Ingenieros
{
    public class Observaciones
    {
        static FSR_Repository repositorio = new FSR_Repository();
        private readonly string idFolioServicio;
        private readonly string idUsuario;

        C_FSR controladorFSR;
        public Observaciones(string idFolioServicio,string idUsuario) {
            controladorFSR = new C_FSR(repositorio, idUsuario);
            this.idFolioServicio = idFolioServicio;
            this.idUsuario = idUsuario;
        }

        public string consultarObservaciones()
        {
            string observaciones="";             
            string observacionesDeFolioServicio = controladorFSR.consultarValorDeCampoPorFolioyUsuario(idFolioServicio, "Observaciones");
            if (observacionesDeFolioServicio != null)
            {
                observaciones = observacionesDeFolioServicio;
            }
            return observaciones;     
        }

        public void actualizarObservaciones(string observaciones)
        {
            if (observaciones.Length > 0)
            {
                controladorFSR.actualizarValorDeCampoPorFolioYUsuario(idFolioServicio, "Observaciones", observaciones);
            }
        }

        public string verificarNotificacionEnvioDeObservaciones(bool notificacionEnvioObservaciones)
        {      
             return controladorFSR.verificarSiEnviaNotificacionDeObservacionesAlUsuario(notificacionEnvioObservaciones, idFolioServicio);                
        }

    }
}