using System;

namespace CapaEntidadPiscina
{
    public class EGasto
    {
        public int IdGasto { get; set; }
        public int IdCategoriaGasto { get; set; }
        public string CategoriaDescripcion { get; set; }

        public int IdUsuario { get; set; }
        public string UsuarioNombre { get; set; }

        public int? IdCajaTurno { get; set; }       // Puede ser NULL en admin

        public decimal Monto { get; set; }
        public string Descripcion { get; set; }

        public string RolDescripcion { get; set; }  // Nombre del rol para mostrar
        public int IdRol { get; set; }              // ← NECESARIO (FALTABA)

        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
