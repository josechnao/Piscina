using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EResumenFinanciero
    {
        public decimal IngresosTotales { get; set; }
        public decimal PerdidasCortesias { get; set; }
        public decimal EgresosCompras { get; set; }
        public decimal EgresosGastos { get; set; }

        // Propiedad calculada (no viene del SP)
        public decimal GananciaNeta
        {
            get { return IngresosTotales - EgresosCompras - EgresosGastos; }
        }
    }
}
