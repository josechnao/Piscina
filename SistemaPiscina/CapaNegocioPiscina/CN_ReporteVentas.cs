using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using static CapaDatosPiscina.CD_ReporteVentas;

namespace CapaNegocioPiscina
{
    public class CN_ReporteVentas
    {
        private CD_ReporteVentas objDatos = new CD_ReporteVentas();

        // ============================================================
        // LISTAR VENTAS (REPORTE GENERAL)
        // ============================================================
        public List<EVentaReporte> ListarVentas(DateTime fechaDesde, DateTime fechaHasta, string metodoPago)
        {
            return objDatos.ListarVentasReporte(fechaDesde, fechaHasta, metodoPago);
        }

        // ============================================================
        // OBTENER DETALLE DE UNA VENTA (ENCABEZADO + ITEMS)
        // ============================================================
        public VentaDetalleCompleto ObtenerDetalleVenta(int idVenta)
        {
            return objDatos.ObtenerDetalleVenta(idVenta);
        }

    }
}
