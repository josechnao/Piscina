using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioPiscina
{
    public class CN_CajaTurno
    {
        private CD_CajaTurno objCD = new CD_CajaTurno();

        public CajaTurno ObtenerCajaActiva(int idUsuario)
        {
            return objCD.ObtenerCajaActiva(idUsuario);
        }
    }

}

