using System;
using System.Collections.Generic;
using CapaDatosPiscina;
using CapaEntidadPiscina;
using System.Threading.Tasks;


namespace CapaNegocioPiscina
{
    public class CN_EntradaTipo
    {
        private CD_EntradaTipo objCD_EntradaTipo = new CD_EntradaTipo();

        public List<EntradaTipo> Listar()
        {
            return objCD_EntradaTipo.Listar();
        }

        public async Task<List<EntradaTipo>> ListarEntradasVentaAsync()
        {
            return await objCD_EntradaTipo.ListarEntradasVentaAsync();
        }

        public bool ActualizarPrecio(int idEntradaTipo, decimal nuevoPrecio, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación básica
            if (nuevoPrecio < 0)
            {
                mensaje = "El precio no puede ser negativo.";
                return false;
            }

            return objCD_EntradaTipo.ActualizarPrecio(idEntradaTipo, nuevoPrecio, out mensaje);
        }
        public List<EntradaTipo> ListarEntradasVenta()
        {
            return objCD_EntradaTipo.ListarEntradasVenta();
        }

    }

}
