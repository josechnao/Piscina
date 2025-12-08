using System;
using System.Collections.Generic;
using CapaDatosPiscina;
using CapaEntidadPiscina;

namespace CapaNegocioPiscina
{
    public class CN_ReporteCajaTurno
    {
        private CD_ReporteCajaTurno objCapaDatos = new CD_ReporteCajaTurno();

        // =============================
        // 1) Resumen del formulario principal
        // =============================
        public List<EReporteCajaTurnoResumen> ListarResumen(DateTime fechaDesde, DateTime fechaHasta, int idUsuario)
        {
            return objCapaDatos.ListarResumen(fechaDesde, fechaHasta, idUsuario);
        }

        // =============================
        // 2) Resumen del turno (modal)
        // =============================
        public EReporteCajaTurnoResumen ObtenerDetalleTurno(int idCajaTurno)
        {
            return objCapaDatos.ObtenerDetalleTurno(idCajaTurno);
        }

        // =============================
        // 3) Ventas del turno (modal)
        // =============================
        public List<EReporteCajaTurnoVenta> ListarVentasTurno(int idCajaTurno)
        {
            return objCapaDatos.ListarVentasTurno(idCajaTurno);
        }

        // =============================
        // 4) Gastos del turno (modal)
        // =============================
        public List<EReporteCajaTurnoGasto> ListarGastosTurno(int idCajaTurno)
        {
            return objCapaDatos.ListarGastosTurno(idCajaTurno);
        }
    }
}
