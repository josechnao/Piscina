using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class DetalleVentaProducto
    {
        public int IdDetalleProducto { get; set; }
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }  // nombre del snack
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
