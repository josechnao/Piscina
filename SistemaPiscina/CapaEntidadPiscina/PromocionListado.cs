using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class PromocionListado
    {
        public int IdPromocion { get; set; }
        public string TipoPromo { get; set; }      // ← Faltaba
        public string Categoria { get; set; }
        public decimal Porcentaje { get; set; }    // ← Faltaba
        public bool Estado { get; set; }

        public string TipoCondicion { get; set; }
        public int? CantidadCondicion { get; set; }

        public string TipoLimite { get; set; }
        public int? CantidadLimite { get; set; }
        public int CantidadUsada { get; set; }     // ← Faltaba

        public string TipoVigencia { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaDia { get; set; }
    }


}
