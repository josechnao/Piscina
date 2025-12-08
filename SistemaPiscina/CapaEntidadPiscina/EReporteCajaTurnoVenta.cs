using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EReporteCajaTurnoVenta
    {
        public int IdVenta { get; set; }
        public string NroTicket { get; set; }

        public decimal MontoTotal { get; set; }
        public string MetodoPago { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
