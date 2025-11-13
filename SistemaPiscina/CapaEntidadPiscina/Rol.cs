using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public List<Permiso> Permisos { get; set; }
    }
}