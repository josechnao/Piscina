using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EVentaReporte
    {
        public int IdVenta { get; set; }
        public string NumeroVenta { get; set; }
        public DateTime FechaHora { get; set; }
        public string Cajero { get; set; }
        public string MetodoPago { get; set; }
        public decimal Total { get; set; }
    }

}
