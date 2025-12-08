using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;
using System.Collections.Generic;

namespace CapaNegocioPiscina
{
    public class CN_ReporteCompras
    {
        private CD_ReporteCompras objDatos = new CD_ReporteCompras();

        // =============================
        // 1. LISTAR PROVEEDORES (COMBO)
        // =============================
        public List<EProveedorCombo> ListarProveedores()
        {
            return objDatos.ListarProveedores();
        }

        // ===================================
        // 2. LISTAR COMPRAS (FORM PRINCIPAL)
        // ===================================
        public List<EReporteCompra> ListarCompras(
            DateTime fechaInicio,
            DateTime fechaFin,
            int idProveedor,
            string documentoProveedor,
            string numeroDocumento,
            string numeroCorrelativo)
        {
            // Validación ligera: si pasan nulos, convertirlos a ""
            documentoProveedor = documentoProveedor ?? "";
            numeroDocumento = numeroDocumento ?? "";
            numeroCorrelativo = numeroCorrelativo ?? "";

            return objDatos.ListarCompras(
                fechaInicio,
                fechaFin,
                idProveedor,
                documentoProveedor,
                numeroDocumento,
                numeroCorrelativo
            );
        }

        // ===================================
        // 3. DETALLE DE UNA COMPRA (MODAL)
        // ===================================
        public bool ObtenerDetalleCompra(
            int idCompra,
            out EReporteCompraCabecera cabecera,
            out List<EReporteCompraDetalle> detalle)
        {
            return objDatos.ObtenerDetalleCompra(idCompra, out cabecera, out detalle);
        }
    }
}
