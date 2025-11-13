using CapaDatosPiscina;
using CapaEntidadPiscina;
using System.Collections.Generic;

namespace CapaNegocioPiscina
{
    public class CN_Rol
    {
        private CD_Rol objCD_Rol = new CD_Rol();

        public List<Rol> Listar()
        {
            return objCD_Rol.Listar();
        }
    }
}
