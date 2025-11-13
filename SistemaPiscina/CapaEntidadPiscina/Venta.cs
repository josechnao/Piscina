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
        public DateTime FechaVenta { get; set; }
        public string MetodoPago { get; set; }
        public decimal Total { get; set; }
        public int Correlativo { get; set; }

        public Usuario oUsuario { get; set; }
        public List<DetalleVenta> oDetalleVenta { get; set; }
    }
}
