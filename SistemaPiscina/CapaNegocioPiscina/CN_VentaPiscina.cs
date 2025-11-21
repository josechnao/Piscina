using CapaDatos;
using CapaDatosPiscina;
using CapaEntidadPiscina;
using System;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_VentaPiscina
    {
        private CD_VentaPiscina objDatos = new CD_VentaPiscina();

        public ResultadoVenta RegistrarVentaPiscina(
            int idUsuario,
            int? idCliente,
            string metodoPago,
            int idCajaTurno,
            List<DetalleVentaEntrada> entradas,
            List<DetalleVentaProducto> productos)
        {
            // Validaciones mínimas antes de llamar a la DAL
            if (string.IsNullOrWhiteSpace(metodoPago))
            {
                return new ResultadoVenta()
                {
                    Exito = false,
                    Mensaje = "Debe seleccionar un método de pago."
                };
            }

            if ((entradas == null || entradas.Count == 0) &&
                (productos == null || productos.Count == 0))
            {
                return new ResultadoVenta()
                {
                    Exito = false,
                    Mensaje = "Debe agregar al menos un producto o entrada."
                };
            }

            // Llamar a la capa de datos
            return objDatos.RegistrarVentaPiscina(
                idUsuario,
                idCliente,
                metodoPago,
                idCajaTurno,
                entradas,
                productos
            );
        }
    }
}
