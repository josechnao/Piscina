using System;
using System.Collections.Generic;
using CapaEntidadPiscina;
using CapaDatosPiscina;

namespace CapaNegocioPiscina
{
    public class CN_Venta
    {
        private CD_Venta dataVenta = new CD_Venta();
        private CD_Promocion dataPromo = new CD_Promocion();
        private CD_Producto dataProducto = new CD_Producto();

        // ===============================================================
        //              REGISTRAR VENTA COMPLETA
        // ===============================================================
        public int RegistrarVenta(Venta venta, out string mensaje, out string numeroVenta)
        {
            mensaje = string.Empty;
            numeroVenta = string.Empty;

            // ==========================
            //   VALIDAR STOCK (SNACKS)
            // ==========================
            if (venta.ListaProductos != null)
            {
                foreach (var item in venta.ListaProductos)
                {
                    Producto p = dataProducto.Listar().Find(x => x.IdProducto == item.IdProducto);

                    if (p == null)
                    {
                        mensaje = "Producto no encontrado.";
                        return 0;
                    }

                    if (item.Cantidad > p.Stock)
                    {
                        mensaje = $"Stock insuficiente para el producto: {p.Nombre}. Stock disponible: {p.Stock}";
                        return 0;
                    }
                }
            }

            // ===================================================
            //   APLICAR PROMOCIÓN (Solo a ENTRADAS)
            // ===================================================
            try
            {
                EPromocion promo = dataPromo.Obtener();

                if (promo != null && promo.Estado) // si hay una promo activa
                {
                    foreach (var entrada in venta.ListaEntradas)
                    {
                        bool aplica = false;

                        // Promo para todas las categorías
                        if (promo.Categoria == "Todas")
                            aplica = true;

                        // Promo para categoría específica
                        if (promo.Categoria == entrada.Descripcion) // Adulto, Adolescente, Niño, Bebe
                            aplica = true;

                        // Si aplica la promo 2x1
                        if (aplica)
                        {
                            // Cantidad aplicando 2x1
                            int unidadesPagadas = entrada.Cantidad / 2 + entrada.Cantidad % 2;

                            entrada.PrecioAplicado = entrada.PrecioUnitario;
                            entrada.SubTotal = unidadesPagadas * entrada.PrecioUnitario;
                        }
                        else
                        {
                            // Sin promo
                            entrada.PrecioAplicado = entrada.PrecioUnitario;
                            entrada.SubTotal = entrada.Cantidad * entrada.PrecioUnitario;
                        }
                    }
                }
                else
                {
                    // No hay promo activa → calcular normal
                    foreach (var entrada in venta.ListaEntradas)
                    {
                        entrada.PrecioAplicado = entrada.PrecioUnitario;
                        entrada.SubTotal = entrada.Cantidad * entrada.PrecioUnitario;
                    }
                }
            }
            catch (Exception)
            {
                mensaje = "Error al aplicar promoción.";
                return 0;
            }

            // ===================================================
            //     CALCULAR MONTO TOTAL (antes de enviar a BD)
            // ===================================================
            decimal totalEntradas = 0;
            decimal totalProductos = 0;

            if (venta.ListaEntradas != null)
            {
                foreach (var e in venta.ListaEntradas)
                    totalEntradas += e.SubTotal;
            }

            if (venta.ListaProductos != null)
            {
                foreach (var p in venta.ListaProductos)
                    totalProductos += p.SubTotal;
            }

            venta.MontoTotal = totalEntradas + totalProductos;

            // ================================
            //   SI ES CORTESÍA → TOTAL = 0
            // ================================
            if (venta.MetodoPago == "Cortesia")
            {
                venta.MontoTotal = 0;
            }

            // ===================================================
            //  ENVIAR A LA CAPA DATOS (SP COMPLETO)
            // ===================================================
            int idVenta = dataVenta.Registrar(venta, out mensaje, out numeroVenta);

            return idVenta;
        }
    }
}
