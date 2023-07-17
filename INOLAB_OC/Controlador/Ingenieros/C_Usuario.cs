using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo;
using INOLAB_OC.Modelo.Browser.Interfaces;
using Microsoft.ReportingServices.Interfaces;

namespace INOLAB_OC.Controlador
{
    public class C_Usuario
    {
        private static IUsuarioRepository _userRepository;

        public C_Usuario(IUsuarioRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public DataRow optenerDatosDeUsuario(E_Usuario usuario)
        { 
            DataRow datosDelUsuario  = _userRepository.OptenerDatosDeUsuario(usuario);
            return datosDelUsuario;
        }

        public void loggearUsuario(E_Usuario usuario)
        {
            _userRepository.executeStoreProcedureLogWeb(usuario);
        }

        public bool validarSiUsuarioEsGefeDeArea(string idUsuario)
        {
            if (idUsuario == "54" || idUsuario == "60" ||
                    idUsuario == "30")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string consultarValorDeCampo(string campo, string idUsuario)
        {
           return _userRepository.consultarValorDeCampo(campo, idUsuario);
        }
        
    }
}