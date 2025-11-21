using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class PromocionCondicion
    {
        public int IdPromocion { get; set; }
        public string TipoCondicion { get; set; }     // "CompraMinima", "AcumulaDia", etc.
        public int? Cantidad { get; set; }            // nullable si es ilimitado
    }
}
