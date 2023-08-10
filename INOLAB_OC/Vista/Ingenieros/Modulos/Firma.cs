using INOLAB_OC.Controlador;
using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace INOLAB_OC.Vista.Ingenieros
{
    public class Firma
    {
        static FSR_Repository repositorioFSR = new FSR_Repository();
        private readonly string idUsuario;
        private readonly string folioServicio;
        C_FSR controladorFSR;
        
        public Firma(string idUsuario,string folioServicio) {
            controladorFSR = new C_FSR(repositorioFSR, idUsuario);
            this.idUsuario = idUsuario;
            this.folioServicio = folioServicio;
        }

        public bool actualizarFirmaIngeniero(string nombreDeImagen, string imagen)
        {
            try
            {
                string[] imagenes = imagen.Split(',');
                string tipoDeImagen =  definirTipoDeImagen(imagenes[0]); 
                string imagenFirma = imagenes[1];
  
                int idFirmaIngeniero = Conexion.insertarFirmaIngeniero(nombreDeImagen, tipoDeImagen, imagenFirma);
                if (idFirmaIngeniero != 0)
                {
                    controladorFSR.actualizarValorDeCampoPorFolioYUsuario(folioServicio, "IDFirmaIng", Convert.ToString(idFirmaIngeniero));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }

        public bool actualizarFirmaCliente(string nombreDeImagen, string imagen)
        {
            try
            {
                string[] imagenes = imagen.Split(',');
                string tipoDeImagen = definirTipoDeImagen(imagenes[0]);
                string imagenFirma = imagenes[1];

                int idFirmaImagen = Conexion.insertarFirmaImagen(nombreDeImagen, tipoDeImagen, imagenFirma);
                if (idFirmaImagen != 0)
                {
                    controladorFSR.actualizarValorDeCampoPorFolio(folioServicio, "IdFirmaImg", Convert.ToString(idFirmaImagen));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
        }

        private string definirTipoDeImagen(string imagen)
        {
            string pattern = @"[^:\s*]\w+\/[\w-+\d.]+(?=[;| ])";
            string tipoDeImagen = "";

            Regex regex = new Regex(pattern);
            Match mach = regex.Match(imagen);
            if (mach.Success)
            {
                tipoDeImagen = mach.Value;
            }
            return tipoDeImagen;
        }

        public int verificarSiSeAgregoFirmaDeCliente()
        {
            int firmaCliente = -1;
            try
            {
                firmaCliente = Convert.ToInt32(controladorFSR.consultarValorDeCampoPorFolioyUsuario(folioServicio, "IdFirmaImg"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return firmaCliente;
        }

        public int verificarSiSeAgregoFirmaDeIngeniero()
        {
            int firmaIngeniero = -1;
            try
            {
                firmaIngeniero = Convert.ToInt32(controladorFSR.consultarValorDeCampoPorFolioyUsuario(folioServicio, "IDFirmaIng"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return firmaIngeniero;
        }


    }
}