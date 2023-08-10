using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Vista.Ingenieros
{

    public class Fallas
    {
        static FSR_Repository repositorio = new FSR_Repository();
        private readonly string idUsuario;
        private readonly string idFolioServicio;
        C_FSR controladorFSR;
        public Fallas(string idUsuario, string idFolioServicio) {
            controladorFSR = new C_FSR(repositorio, idUsuario);
            this.idUsuario = idUsuario;
            this.idFolioServicio = idFolioServicio;
        }

        public void actualizarFallas(string fallas)
        {
            if (fallas.Length > 0)
            {
                controladorFSR.actualizarValorDeCampoPorFolioYUsuario(idFolioServicio, "FallaEncontrada", fallas);
            }          
        }

        public string consultarFallaEncontrada()
        {
            string fallaEncontrada = controladorFSR.consultarValorDeCampoPorFolioyUsuario(idFolioServicio, "FallaEncontrada");
            if (fallaEncontrada != null)
            {
                return fallaEncontrada;
            }
            else
            {
                return "";
            }           
        }
    }
}