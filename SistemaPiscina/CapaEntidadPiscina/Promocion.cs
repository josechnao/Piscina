using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class EPromocion
    {
        public int IdPromocion { get; set; }
        public bool Estado { get; set; }
        public string Categoria { get; set; }
        public int UsuarioModifico { get; set; }
        public string FechaActualizacion { get; set; }
    }
}

