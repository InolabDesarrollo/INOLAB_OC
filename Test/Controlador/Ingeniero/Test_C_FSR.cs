using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser;
using INOLAB_OC.Modelo.Browser.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controlador.Ingeniero
{
    [TestClass]
    public class Test_C_FSR
    {
       static FSR_Repository_Test repositorio = new FSR_Repository_Test();
        C_FSR controladorFSR = new C_FSR(repositorio);

        [TestMethod]
        public void verificarSiIniciaOContinuaServicio_ReturnString()
        {
            //Arrange
            FSR_Repository_Test repositorio = new FSR_Repository_Test();
            C_FSR controladorFSR = new C_FSR(repositorio);

            //Action
            var resultado = controladorFSR.verificarSiIniciaOContinuaServicio("123");
            string esperado = "Iniciar Servicio";

            //Assert
            Assert.AreEqual(esperado, resultado);

        }

        [TestMethod]
        public void verificarSiSeEnviaEmailAlAsesor_returnBool()
        {
            //Arrange
            FSR_Repository_Test repositorio = new FSR_Repository_Test();
            C_FSR controladorFSR = new C_FSR(repositorio);

            var resultado = controladorFSR.verificarSiSeEnviaEmailAlAsesor("123", "NotAsesor");
            bool esperado = true;

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void verificarSiEnviaNotificacionDeObservacionesAlUsuario_return_string()
        {
            //Action
            var resultado = controladorFSR.verificarSiEnviaNotificacionDeObservacionesAlUsuario(false, "1233");
            string esperado = "No";

            //Asser
            Assert.AreEqual(esperado,resultado);
        }
    }

    public class FSR_Repository_Test : IFSR_Repository
    {
        public void actualizarDatosDeServicio(E_Servicio folioServicioFSR, string idUsuario)
        {
            throw new NotImplementedException();
        }

        public void actualizarFechayHoraFinDeServicio(E_Servicio servicio, string idIngeniero)
        {
            throw new NotImplementedException();
        }

        public void actualizarHorasDeServicio(string folioFSR, string idUsuario)
        {
            throw new NotImplementedException();
        }

        public void actualizarValorDeCampo(string folio, string campo, string valorDelCampo)
        {
            //actualiza BBD
        }

        public void actualizarValorDeCampo(string folio, string campo, string valorDelCampo, string idUsuario)
        {
            throw new NotImplementedException();
        }

        public int consultarEstatusDeFolioServicio(string folio, string idUsuario)
        {
            throw new NotImplementedException();
        }

        public DateTime consultarFechaFinDeFolio(string folio, string idIngeniero, string campoDondeSeConsulta)
        {
            throw new NotImplementedException();
        }

        public DateTime consultarFechaInicioDeFolio(string folio, string idIngeniero, string campoDondeSeConsulta)
        {
            throw new NotImplementedException();
        }

        public DataSet consultarFolioServicioPorEstatus(E_Servicio servicio, string idUsuario)
        {
            throw new NotImplementedException();
        }

        public DataRow consultarInformacionDeFolioPorFolio(string folio)
        {
            throw new NotImplementedException();
        }

        public DataRow consultarInformacionDeFolioPorFolioYUsuario(string usuario, string folio)
        {
            throw new NotImplementedException();
        }

        public string consultarInicioDeServicio(string folio)
        {
            return "";
        }

        public string consultarMailDeFolioServicio(string folioFSR)
        {
            throw new NotImplementedException();
        }

        public DataSet consultarTodosLosFoliosDeServicio(string idUsuario)
        {
            throw new NotImplementedException();
        }

        public string consultarValorDeCampo(string folio, string idUsuario, string campo)
        {
            throw new NotImplementedException();
        }

        public string consultarValorDeCampo(string folio, string campo)
        {
            return "Si";
        }

        public string consultarValorDeCampoPorTop(string folio, string campo)
        {
            throw new NotImplementedException();
        }

        public void iniciarFolioServicio(DateTime fechaYHoraDeInicioDeServicio, string folio, string idIngeniero)
        {
            throw new NotImplementedException();
        }
    }
}
