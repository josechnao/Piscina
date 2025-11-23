using CapaDatosPiscina;
using CapaEntidadPiscina;
using System.Collections.Generic;

namespace CapaNegocioPiscina
{
    public class CN_Venta
    {
        private CD_Venta objCD = new CD_Venta();

        public bool Registrar(
            Venta oVenta,
            List<DetalleVentaEntrada> detalleEntradas,
            List<DetalleVentaProducto> detalleProductos,
            out string numeroGenerado)
        {
            return objCD.RegistrarVenta(oVenta, detalleEntradas, detalleProductos, out numeroGenerado);
        }
    }
}
