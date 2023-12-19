using INOLAB_OC.Responsabilities;
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
                " VALUES("+seguimiento.IdFsr+",'"+seguimiento.FechaSistema+"','"+seguimiento.ComentarioIngeniero+"','"+seguimiento.IdIngeniero+"')");
        }

        public void actualizarComentario(SeguimientoFSR seguimiento)
        {
            Conexion.executeQuery("UPDATE SeguimientoFSR set FechaSistema = '"+seguimiento.FechaSistema+"', " +
                " Comentarios ='"+seguimiento.ComentarioIngeniero+"', IdIng = '"+seguimiento.IdIngeniero+"' WHERE FolioFSR = "+seguimiento.IdFsr+";");
        }

        public bool verificarSiExisteComentarioDeIngeniero(int idFolioServicio)
        {
            return Conexion.isThereSomeInformation("Select * from SeguimientoFSR WHERE FolioFSR = "+idFolioServicio+";");
        }
      
    }
}