﻿using INOLAB_OC.Responsabilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Modelo.Browser
{
    public class SeguimientoFSR_Repository
    {
        public void insertarComentarioIngeniero(SeguimientoFSR seguimiento)
        {
            Conexion.executeQuery("INSERT INTO SeguimientoFSR (FolioFSR,FechaSistema,Comentarios,IdIng)\r\n" +
                " VALUES('"+seguimiento.IdFsr+"','"+seguimiento.FechaSistema+"','"+seguimiento.ComentarioIngeniero+"','"+seguimiento.IdIngeniero+"')");
        }
      
    }
}