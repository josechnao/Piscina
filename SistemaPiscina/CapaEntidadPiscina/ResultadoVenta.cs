using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class ResultadoVenta
    {
        public int IdVenta { get; set; }
        public string NumeroVenta { get; set; }
        public string Mensaje { get; set; }
        public bool Exito { get; set; }
    }
}