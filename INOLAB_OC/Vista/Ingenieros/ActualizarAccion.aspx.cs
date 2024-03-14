using INOLAB_OC.Controlador;
using INOLAB_OC.Entidades;
using INOLAB_OC.Modelo.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Vista.Ingenieros
{
    public partial class ActualizarAccion : System.Web.UI.Page
    {
        private static E_FSRAccion accion;
        C_FSR_Accion controller;
        private static string idAccion;
        protected void Page_Load(object sender, EventArgs e)
        {
            FSR_AccionRepository repository = new FSR_AccionRepository();
            controller = new C_FSR_Accion(repository);
            if (!IsPostBack)
            {
                accion = (E_FSRAccion)Session["Entidad_Accion"];
                idAccion = accion.idFSRAccion;
                TxtBox_actualizar_fecha_accion.Text = this.convertirFechaAFormatoDeseado(accion.FechaAccion);
                TxtBox_Actualizar_Accion.Text = accion.AccionR;
                TxtBox_actualizar_horas_dedicadas.Text = accion.HorasAccion;
            }
        }

        private string convertirFechaAFormatoDeseado(string fechaOrigen)
        {
            DateTime fechaOriginal = DateTime.ParseExact(fechaOrigen, "dd-MM-yyyy", null);
            string fechaReFormateada = fechaOriginal.ToString("yyyy-MM-dd");
            return fechaReFormateada;
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalleFSR.aspx");
        }

        public void Actualizar_Click(object sender, EventArgs e)
        {
            accion = new E_FSRAccion();
            accion.idFSRAccion = idAccion;
            accion.FechaAccion = TxtBox_actualizar_fecha_accion.Text;
            accion.HorasAccion = TxtBox_actualizar_horas_dedicadas.Text;
            accion.AccionR = TxtBox_Actualizar_Accion.Text;

            controller.actualizarAccion(accion);
            Response.Redirect("DetalleFSR.aspx");
        }
    }
}