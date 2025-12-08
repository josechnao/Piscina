using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EReporteCajaTurnoGasto
    {
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Estado { get; set; }

        // NUEVO
        public string EstadoDescripcion { get; set; }
    }

}
