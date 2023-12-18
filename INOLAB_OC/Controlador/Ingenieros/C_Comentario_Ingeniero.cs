using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Responsabilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ingenieros
{
    public class C_Comentario_Ingeniero
    {
        SeguimientoFSR_Repository repository;
        public C_Comentario_Ingeniero() {
            repository = new SeguimientoFSR_Repository();   
        }

        public void insertarComentarioIngeniero(SeguimientoFSR seguimiento)
        {
            repository.insertarComentarioIngeniero(seguimiento);
        }


    }

}