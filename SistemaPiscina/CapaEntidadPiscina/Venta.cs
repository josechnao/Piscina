using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdUsuario { get; set; }
        public int? IdCliente { get; set; } // puede ser null
        public string NumeroVenta { get; set; }
        public decimal MontoTotal { get; set; }
        public string MetodoPago { get; set; }
        public string FechaRegistro { get; set; }
        public int IdCajaTurno { get; set; }

        // Listas relacionadas
        public List<DetalleVentaEntrada> ListaEntradas { get; set; }
        public List<DetalleVentaProducto> ListaProductos { get; set; }
    }
}

