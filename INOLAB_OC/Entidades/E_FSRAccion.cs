using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Entidades
{
    public class E_FSRAccion
    {
        public string idFSRAccion { get; set; }
        public string FechaAccion { get; set; }
        public string HorasAccion { get; set; } 
        public string AccionR { get; set; }
        public string idFolioFSR { get; set; }
        public string idUsuario { get; set; }
        public string FechaSistema { get; set; }

        static FSR_AccionRepository repositorioFsrAccion = new FSR_AccionRepository();
        C_FSR_Accion controladorFSRAccion;
        public E_FSRAccion(string idFolioServicio,string _idUsuario)
        {
            idFolioFSR = idFolioServicio;
            idUsuario = _idUsuario;
            FechaSistema= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            controladorFSRAccion = new C_FSR_Accion(repositorioFsrAccion);
        }

        public E_FSRAccion()
        {

        }
        public int agregarAccion()
        {
            E_FSRAccion accion= new E_FSRAccion();
            accion.FechaAccion = FechaAccion;
            accion.HorasAccion= HorasAccion;
            accion.AccionR= AccionR;
            accion.idFolioFSR = idFolioFSR;
            accion.idUsuario = idUsuario;
            accion.FechaSistema = FechaSistema;

            return controladorFSRAccion.agregarAccionFSR(accion);
        }

        public  DataSet consultarAcciones()
        {
            return controladorFSRAccion.consultarDatosDeFSRAccion(idFolioFSR);
        }

        public void eliminarAccion(string idAccion)
        {
            controladorFSRAccion.eliminarAccionFSR(idAccion);
        }

        public E_FSRAccion consultarAccion(int idFSRAccion)
        {
            return controladorFSRAccion.consultarFSRAccion(idFSRAccion);
        }
    }
}