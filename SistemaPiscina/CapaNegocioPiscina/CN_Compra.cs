using System;
using System.Collections.Generic;
using CapaDatosPiscina;
using CapaEntidadPiscina;

namespace CapaNegocioPiscina
{
    public class CN_Compra
    {
        private CD_Compra cdCompra = new CD_Compra();
        private CD_DetalleCompra cdDetalle = new CD_DetalleCompra();
        private CD_CorrelativoCompra cdCorrelativo = new CD_CorrelativoCompra();
        private CD_Producto cdProducto = new CD_Producto();

        public bool RegistrarCompra(Compra compra, List<DetalleCompra> detalles, out string mensaje)
        {
            mensaje = "";
            bool ok = true;

            try
            {
                // 1) Obtener correlativo actual
                int ultimoCorrelativo = cdCorrelativo.ObtenerCorrelativo();
                compra.NumeroCorrelativo = ultimoCorrelativo;

                // 2) Registrar compra
                int idCompraGenerado = cdCompra.RegistrarCompra(compra, out mensaje);

                if (idCompraGenerado == 0)
                {
                    ok = false;
                    if (string.IsNullOrEmpty(mensaje))
                        mensaje = "No se pudo registrar la compra (IdCompra generado = 0).";
                    return ok;
                }


                // 3) Registrar detalles
                foreach (var det in detalles)
                {
                    det.IdCompra = idCompraGenerado;

                    bool detOk = cdDetalle.RegistrarDetalleCompra(det, out string msgDet);

                    if (!detOk)
                    {
                        ok = false;
                        mensaje = msgDet;
                        return ok;
                    }

                    // 4) Actualizar precios (si en tu formulario decides que cambió)
                    cdProducto.ActualizarPrecios(det.IdProducto, det.PrecioCompra, det.PrecioVenta);

                    // 5) Actualizar stock
                    cdProducto.ActualizarStock(det.IdProducto, det.Cantidad);
                }

                // 6) Incrementar correlativo
                cdCorrelativo.ActualizarCorrelativo();


            }
            catch (Exception ex)
            {
                ok = false;
                mensaje = "ERROR EN CN_Compra: " + ex.Message;
            }

            return ok;
        }
    }
}
