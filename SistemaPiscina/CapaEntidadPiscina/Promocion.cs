using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class Promocion
    {
        public int IdPromocion { get; set; }
        public string TipoPromo { get; set; }      // "2x1" o "Descuento"
        public int IdEntradaTipo { get; set; }
        public decimal Porcentaje { get; set; }    // solo para descuentos
        public bool Estado { get; set; }
    }
}

