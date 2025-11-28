using System;

namespace CapaEntidadPiscina
{
    public class ECajaTurno
    {
        public int IdCajaTurno { get; set; }
        public int IdUsuario { get; set; }

        public decimal MontoInicial { get; set; }
        public decimal? MontoFinal { get; set; }

        public decimal? TotalVentas { get; set; }
        public decimal? TotalGastos { get; set; }
        public decimal? Diferencia { get; set; }

        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }

        public string Observacion { get; set; }
        public bool Estado { get; set; }

        // Esta propiedad NO está en la BD, pero sirve para el login
        public bool TieneCajaAbierta { get; set; }
    }
}
