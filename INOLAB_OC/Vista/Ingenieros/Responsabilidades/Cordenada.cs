using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace INOLAB_OC.Vista.Ingenieros.Responsabilidades
{
    public class Cordenada
    {
        private readonly GridView gridView;
       
        public Cordenada(GridView gridView) {
            this.gridView = gridView;
        }

        public string consultarValorDeCelda(int fila, int columna)
        {
            GridViewRow filasDelDataGridView = gridView.Rows[fila];
            string valorCelda = filasDelDataGridView.Cells[columna].Text;
            return valorCelda;
        }

    }
}