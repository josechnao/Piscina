using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class VentaDetalleCompleto
    {
        public EVentaDetalleEncabezado Encabezado { get; set; }
        public List<EVentaDetalleItem> Items { get; set; }

        public VentaDetalleCompleto()
        {
            Encabezado = new EVentaDetalleEncabezado();
            Items = new List<EVentaDetalleItem>();
        }
    }
}
