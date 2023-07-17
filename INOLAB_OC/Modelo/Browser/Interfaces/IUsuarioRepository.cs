using INOLAB_OC.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INOLAB_OC.Modelo.Browser.Interfaces
{
    public interface IUsuarioRepository
    {
        DataRow OptenerDatosDeUsuario(E_Usuario usuario);
        void executeStoreProcedureLogWeb(E_Usuario usuario);

        string consultarValorDeCampo(string campo, string idUsuario);
    }
}
