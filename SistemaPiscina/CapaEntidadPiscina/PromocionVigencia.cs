using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class PromocionVigencia
    {
        public int IdPromocion { get; set; }
        public string TipoVigencia { get; set; }      // "SinFecha", "SoloDia", "Rango"
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaDia { get; set; }
    }
}

