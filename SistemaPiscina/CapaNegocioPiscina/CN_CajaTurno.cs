using CapaDatosPiscina;
using CapaEntidadPiscina;

namespace CapaNegocioPiscina
{
    public class CN_CajaTurno
    {
        private CD_CajaTurno objDatos = new CD_CajaTurno();

        // 1. Verificar
        public ECajaTurno VerificarCajaAbierta(int idUsuario)
        {
            return objDatos.VerificarCajaAbierta(idUsuario);
        }

        // 2. Abrir
        public int AbrirCaja(int idUsuario, decimal montoInicial, out string mensaje)
        {
            if (montoInicial < 0)
            {
                mensaje = "El monto inicial no puede ser negativo.";
                return 0;
            }

            return objDatos.AbrirCaja(idUsuario, montoInicial, out mensaje);
        }

        // 3. Resumen de ventas/gastos
        public (decimal MontoInicial, decimal TotalVentas, decimal TotalGastos)
        ObtenerResumen(int idCajaTurno)
            {
                return objDatos.ObtenerResumen(idCajaTurno);
            }


        // 4. Cerrar caja
        public bool CerrarCaja(ECajaTurno obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj.MontoFinal == null)
            {
                mensaje = "Debe ingresar el monto final.";
                return false;
            }

            if (!string.IsNullOrEmpty(obj.Observacion) && obj.Observacion.Length > 250)
            {
                mensaje = "La observación no puede exceder 250 caracteres.";
                return false;
            }

            return objDatos.CerrarCaja(obj, out mensaje);
        }

    }
}
