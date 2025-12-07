using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioPiscina
{
    public class CN_Negocios
    {
        private CD_Negocios objDatos = new CD_Negocios();

        public ENegocio ObtenerDatosNegocio()
        {
            return objDatos.Obtener();
        }
    }
}
