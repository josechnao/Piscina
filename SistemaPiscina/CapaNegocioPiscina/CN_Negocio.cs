using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Negocio
    {
        private CD_Negocio objCapaDatos = new CD_Negocio();

        public Negocio ObtenerDatos()
        {
            return objCapaDatos.ObtenerDatos();
        }

        public bool GuardarDatos(Negocio obj, out string mensaje)
        {
            mensaje = string.Empty;

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(obj.NombreNegocio))
            {
                mensaje = "El nombre del negocio no puede estar vacío.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Direccion))
            {
                mensaje = "La dirección no puede estar vacía.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Ciudad))
            {
                mensaje = "La ciudad no puede estar vacía.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Telefono))
            {
                mensaje = "El teléfono no puede estar vacío.";
                return false;
            }

            // Si pasa validaciones → actualizar
            return objCapaDatos.ActualizarDatos(obj, out mensaje);
        }
    }
}
