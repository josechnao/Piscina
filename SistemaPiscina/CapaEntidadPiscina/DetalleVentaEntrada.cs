using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class DetalleVentaEntrada
    {
        public int IdDetalleEntrada { get; set; }
        public int IdEntradaTipo { get; set; }
        public string Descripcion { get; set; }  // Adulto, Adolescente, etc.
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioAplicado { get; set; } // por si aplica 2x1
        public decimal SubTotal { get; set; }
    }
}
