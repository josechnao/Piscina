using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EReporteCajaTurnoResumen
    {
        public string Cajero { get; set; }
        public int IdCajaTurno { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public decimal MontoInicial { get; set; }
        public decimal MontoFinal { get; set; }
        public int TotalVentas { get; set; }
        public decimal VentasSumaTotal { get; set; }
        public int TotalGastos { get; set; }
        public decimal GastoTotalSuma { get; set; }
        public decimal Diferencia { get; set; }
        public string MetodoPagoResumen { get; set; }
        public string Observacion { get; set; }

    }
}

