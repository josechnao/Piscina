using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class DetalleVentaEntrada
    {
        public int IdEntradaTipo { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioAplicado { get; set; }
        public decimal SubTotal { get; set; }
    }
}
