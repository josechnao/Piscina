using CapaEntidadPiscina;
using CapaDatosPiscina;

namespace CapaNegocioPiscina
{
    public class CN_Cliente
    {
        public Cliente BuscarPorDNI(string dni)
        {
            return new CD_Cliente().BuscarPorDNI(dni);
        }
    }
}
