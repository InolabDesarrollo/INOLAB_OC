using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Controlador;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using INOLAB_OC.Modelo.Browser.Interfaces;

namespace Test.Controlador
{
    [TestClass]
    public class SesionTest
    {
        [TestMethod]
        public void OtenerDatosDeUsuario_ReturnDataRow()
        {
            //Arrange 
            TestBrowserRepository repositoryio = new TestBrowserRepository();
            E_Usuario usuario = new E_Usuario();
            C_Usuario controladorSesson = new C_Usuario(repositoryio);


            // Acction
            DataRow resultadoEsperado = null;
            var valorActual = controladorSesson.optenerDatosDeUsuario(usuario);

            //Assert
            Assert.AreEqual(resultadoEsperado, valorActual);
        }

    }

    public class TestBrowserRepository : IUsuarioRepository
    {
        public string consultarValorDeCampo(string campo, string idUsuario)
        {
            throw new NotImplementedException();
        }

        public void executeStoreProcedureLogWeb(E_Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public DataRow OptenerDatosDeUsuario(E_Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
