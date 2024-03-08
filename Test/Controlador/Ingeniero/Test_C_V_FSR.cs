using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;
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
    public  class Test_C_V_FSR
    {
        [TestMethod]
        public void verificarSiServicioTieneLaFechaDeHoy_Bool()
        {
           //Array 
           E_V_FSR entidadV_FSR = new E_V_FSR();
           entidadV_FSR.FechaEnQueSeAgendoServicio = "06/07/2023";

           TestRepositorioV_FSR repository = new TestRepositorioV_FSR();
           C_V_FSR controladorV_FSR = new C_V_FSR(repository);
            
          //Action
          var result = controladorV_FSR.verificarSiServicioTieneLaFechaDeHoy(entidadV_FSR);

            //Asert
            Assert.Equals(result, true);
        }

        public class TestRepositorioV_FSR : IV_FSR_Repository
        {
            public DataSet consultarFolioServicioPorEstatus(E_V_FSR servicio, string idUsuario)
            {
                throw new NotImplementedException();
            }

            public DataSet consultarFoliosFinalizados(string idUsuario)
            {
                throw new NotImplementedException();
            }

            public DataSet consultarFoliosPorArea(string area)
            {
                throw new NotImplementedException();
            }

            public DataRow consultarInformacionDeFolioPorFolio(string folio)
            {
                throw new NotImplementedException();
            }

            public DataSet consultarTodosLosFoliosDeServicio(string idUsuario)
            {
                throw new NotImplementedException();
            }

            public string consultarValorDeCampo(string campo, string valorDeCampo)
            {
                throw new NotImplementedException();
            }

            public string consultarValorDeCampoTop(string campo, string folio)
            {
                throw new NotImplementedException();
            }
        }
    }
}
