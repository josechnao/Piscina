using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EReporteCompra
    {
        public int IdCompra { get; set; }
        public int NumeroCorrelativo { get; set; }
        public string Proveedor { get; set; }
        public string DocumentoProveedor { get; set; }
        public string NumeroDocumento { get; set; }
        public string Fecha { get; set; }   // formato yyyy-MM-dd o dd/MM/yyyy
        public decimal TotalCompra { get; set; }
        public string UsuarioNombre { get; set; }
    }

}
