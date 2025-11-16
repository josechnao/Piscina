using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidadPiscina
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        // Solo para mostrar en el formulario (Activo/Inactivo)
        public string EstadoValor { get; set; }

        // Fecha como texto porque así manejas en tus otros módulos
        public string FechaRegistro { get; set; }
    }
}
