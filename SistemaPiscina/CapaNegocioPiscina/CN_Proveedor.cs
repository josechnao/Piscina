using CapaDatosPiscina;
using CapaEntidadPiscina;
using System.Collections.Generic;

namespace CapaNegocioPiscina
{
    public class CN_Proveedor
    {
        private CD_Proveedor objCD = new CD_Proveedor();

        public List<Proveedor> Listar()
        {
            return objCD.Listar();
        }

        public int Guardar(Proveedor obj, out string mensaje)
        {
            return objCD.Guardar(obj, out mensaje);
        }


        public int Eliminar(int idProveedor, out string mensaje)
        {
            return objCD.Eliminar(idProveedor, out mensaje);
        }
    }
}
