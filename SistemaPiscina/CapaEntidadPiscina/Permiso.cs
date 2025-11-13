using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class Permiso
    {
        public int IdPermiso { get; set; }
        public int IdRol { get; set; }
        public string NombreMenu { get; set; }
        public string NombreFormulario { get; set; }
        public bool Estado { get; set; }
    }
}