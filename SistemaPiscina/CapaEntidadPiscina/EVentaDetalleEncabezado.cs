using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EVentaDetalleEncabezado
    {
        public int IdVenta { get; set; }
        public string NumeroVenta { get; set; }
        public DateTime FechaHora { get; set; }
        public string MetodoPago { get; set; }
        public decimal MontoTotal { get; set; }

        // Datos del Cliente
        public string ClienteNombre { get; set; }
        public string ClienteDNI { get; set; }
        public string ClienteTelefono { get; set; }

        // Datos del Usuario (Opcional mostrar)
        public string Cajero { get; set; }
    }

}
