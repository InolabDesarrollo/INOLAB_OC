using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INOLAB_OC.Controlador.Ingenieros
{
    public class C_Agregar_Nombre_Del_Cliente
    {
        FSR_Repository repository;
        public C_Agregar_Nombre_Del_Cliente() {
            repository = new FSR_Repository();
        }

        public void controll(string folio, string nombreCliente)
        {
            repository.actualizarValorDeCampo(folio, "NombreCliente", nombreCliente);
            repository.actualizarValorDeCampo(folio, "FechaFirmaCliente", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
    }
}