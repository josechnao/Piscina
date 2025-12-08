using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EReporteCompraCabecera
    {
        public string ProveedorNombre { get; set; }
        public string DocumentoProveedor { get; set; }
        public string TelefonoProveedor { get; set; }
        public int NumeroCorrelativo { get; set; }
        public string NumeroDocumento { get; set; }
        public string Fecha { get; set; }
        public string UsuarioNombre { get; set; }
        public decimal MontoTotal { get; set; }
    }

}
