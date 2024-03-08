using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INOLAB_OC.Controlador;
using INOLAB_OC.Controlador.Ingenieros;
using INOLAB_OC.Modelo.Inolab;

namespace Test.Controlador.Ingeniero
{
    [TestClass]
    public class SLC5Test
    {
        [TestMethod]
        public void verificarSiHayFoliosConEstatusDiferenteDeFinalizado_ReturnBool()
        {
            //Arrange
            SCL5RepositoryTest repositorio = new SCL5RepositoryTest();
            C_SCL5 controladorSCL5Test = new C_SCL5(repositorio);
            //Action
            var resultado = controladorSCL5Test.verificarSiHayFoliosConEstatusDiferenteDeFinalizado(3, "1271");
            bool esperado = true;
            //Assert
            Assert.AreEqual(resultado, esperado);
        }

    }

    public class SCL5RepositoryTest : ISCL5Repository
    {
        public void actualizarValorDeCampo(string campo, string valorDeCampo, string folio)
        {
            throw new NotImplementedException();
        }

        public string consultarEstatusDeFoliosPorCallIdYVisOrder(string callId, int visOrder)
        {
            return "no Finalizado";
        }

        public string consultarNumeroDeFoliosPorCallId(string callId)
        {
            throw new NotImplementedException();
        }

        public int contarFilasDeTablaPorCallId(string callId)
        {
            throw new NotImplementedException();
        }

        public string seleccionarValorDeCampo(string campo, string folio)
        {
            throw new NotImplementedException();
        }

        public string seleccionarValorDeCampoTop(string campo, string folio)
        {
            throw new NotImplementedException();
        }
    }

}
