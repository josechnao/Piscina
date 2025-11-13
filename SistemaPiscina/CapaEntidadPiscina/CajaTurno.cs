using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class CajaTurno
    {
        public int IdCajaTurno { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime? FechaCierre { get; set; }
        public decimal MontoInicial { get; set; }
        public decimal MontoFinal { get; set; }
        public string Observacion { get; set; }

        public Usuario oUsuario { get; set; }
    }
}
