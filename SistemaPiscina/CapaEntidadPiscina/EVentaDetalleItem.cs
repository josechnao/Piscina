using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EVentaDetalleItem
    {
        public string TipoItem { get; set; }         // Entrada / Producto (por si lo usas luego)
        public string NombreItem { get; set; }
        public string DescripcionItem { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }
        public string MetodoPago { get; set; }
    }

}
