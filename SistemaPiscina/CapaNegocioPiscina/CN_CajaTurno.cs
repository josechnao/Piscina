using CapaDatosPiscina;
using CapaEntidadPiscina;

namespace CapaNegocioPiscina
{
    public class CN_CajaTurno
    {
        private CD_CajaTurno objCD = new CD_CajaTurno();

        // ============================================
        // 1. Verificar si existe una caja abierta
        // ============================================
        public ECajaTurno VerificarCajaAbierta(int idUsuario)
        {
            return objCD.VerificarCajaAbierta(idUsuario);
        }

        // ============================================
        // 2. Abrir Caja (al iniciar sesión)
        // ============================================
        public int AbrirCaja(int idUsuario, decimal montoInicial, out string mensaje)
        {
            mensaje = string.Empty;

            if (montoInicial < 0)
            {
                mensaje = "El monto inicial no puede ser negativo.";
                return 0;
            }

            return objCD.AbrirCaja(idUsuario, montoInicial, out mensaje);
        }

        // ============================================
        // 3. Obtener Caja Activa (para cierre)
        // ============================================
        public ECajaTurno ObtenerCajaActiva(int idUsuario)
        {
            return objCD.ObtenerCajaActiva(idUsuario);
        }   

        // ============================================
        // 4. Cerrar Caja (al cerrar sesión)
        // ============================================
        public string CerrarCaja(int idCajaTurno, decimal montoFinal, string observacion)
        {
            if (montoFinal < 0)
                return "El monto final no puede ser negativo.";

            return objCD.CerrarCaja(idCajaTurno, montoFinal, observacion);
        }

        public ECajaTurno ObtenerCajaPorId(int idCajaTurno)
        {
            return objCD.ObtenerCajaPorId(idCajaTurno);
        }

    }
}
