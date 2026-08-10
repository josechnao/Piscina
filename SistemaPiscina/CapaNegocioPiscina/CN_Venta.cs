using CapaDatosPiscina;
using CapaEntidadPiscina;
using System.Threading.Tasks;

namespace CapaNegocioPiscina
{
    public class CN_Venta
    {
        private CD_Venta objDatos = new CD_Venta();

        public async Task<ResultadoVenta> RegistrarVentaAsync(
    int idUsuario,
    int? idCajaTurno,
    string dni,
    string nombreCompleto,
    string telefono,
    string metodoPago,
    decimal montoTotal,
    string xmlDetalle
)
        {
            if (string.IsNullOrWhiteSpace(dni))
            {
                return new ResultadoVenta
                {
                    Exito = false,
                    Mensaje = "El DNI del cliente es obligatorio."
                };
            }

            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                return new ResultadoVenta
                {
                    Exito = false,
                    Mensaje = "El nombre del cliente es obligatorio."
                };
            }

            if (montoTotal < 0)
            {
                return new ResultadoVenta
                {
                    Exito = false,
                    Mensaje = "El monto total no es válido."
                };
            }

            return await objDatos.RegistrarVentaAsync(
                idUsuario,
                idCajaTurno,
                dni,
                nombreCompleto,
                telefono,
                metodoPago,
                montoTotal,
                xmlDetalle
            );
        }
    }
}
