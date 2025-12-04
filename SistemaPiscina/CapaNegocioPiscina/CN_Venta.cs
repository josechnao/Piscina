using CapaDatosPiscina;

namespace CapaNegocioPiscina
{
    public class CN_Venta
    {
        private CD_Venta objDatos = new CD_Venta();

        public int RegistrarVenta(
            int idUsuario,
            int? idCajaTurno,          // ← ahora nullable
            string dni,
            string nombreCompleto,
            string telefono,
            string metodoPago,
            decimal montoTotal,
            string xmlDetalle,
            out string numeroVenta,
            out string mensaje
        )
        {
            // Inicialización de respuestas
            mensaje = string.Empty;
            numeroVenta = string.Empty;

            // Validaciones
            if (string.IsNullOrWhiteSpace(dni))
            {
                mensaje = "El DNI del cliente es obligatorio.";
                return 0;
            }

            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                mensaje = "El nombre del cliente es obligatorio.";
                return 0;
            }

            if (montoTotal < 0)
            {
                mensaje = "El monto total no es válido.";
                return 0;
            }

            // Enviar a la capa de datos
            return objDatos.RegistrarVenta(
                idUsuario,
                idCajaTurno,   // ← puede ser null y está bien
                dni,
                nombreCompleto,
                telefono,
                metodoPago,
                montoTotal,
                xmlDetalle,
                out numeroVenta,
                out mensaje
            );
        }
    }
}
