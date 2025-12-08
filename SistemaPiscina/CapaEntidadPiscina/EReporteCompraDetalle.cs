using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EReporteCompraDetalle
    {
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal SubTotal { get; set; }
    }

}
