using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class PromocionLimite
    {
        public int IdPromocion { get; set; }
        public string TipoLimite { get; set; }        // "SinLimite" / "DespuesDe"
        public int? CantidadLimite { get; set; }      // null si es sin límite
        public int CantidadUsada { get; set; }        // siempre inicia en 0
    }
}
