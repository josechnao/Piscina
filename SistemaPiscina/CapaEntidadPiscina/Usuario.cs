using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class Usuario
    {
        public static Usuario usuarioActual { get; set; }

        public int IdUsuario { get; set; }
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }
        public string Clave { get; set; }
        public int IdRol { get; set; }
        public bool Estado { get; set; }

        public Rol oRol { get; set; }

        // SOLO para mostrar en el DGV
        public string RolDescripcion
        {
            get { return oRol != null ? oRol.Descripcion : ""; }
        }

        // SOLO para mostrar el texto de estado
        public string EstadoTexto
        {
            get { return Estado ? "Activo" : "Inactivo"; }
        }
    }
}



