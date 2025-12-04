using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocioPiscina
{
    public class CN_Permiso
    {
        private CD_Permiso objDatos = new CD_Permiso();

        public List<Permiso> Listar(int idRol)
        {
            return objDatos.Listar(idRol);
        }
    }

}
