using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatosPiscina
{
    public class CD_Venta
    {
        public bool RegistrarVenta(
            Venta oVenta,
            List<DetalleVentaEntrada> detalleEntradas,
            List<DetalleVentaProducto> detalleProductos,
            out string numeroGenerado)
        {
            numeroGenerado = "";
            bool respuesta = false;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REGISTRAR_VENTA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros simples
                        cmd.Parameters.AddWithValue("@IdUsuario", oVenta.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdCliente", (object)oVenta.IdCliente ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MetodoPago", oVenta.MetodoPago);
                        cmd.Parameters.AddWithValue("@IdCajaTurno", oVenta.IdCajaTurno);
                        cmd.Parameters.AddWithValue("@MontoTotal", oVenta.MontoTotal);

                        // TABLA DETALLE ENTRADAS
                        DataTable tablaEntradas = new DataTable();
                        tablaEntradas.Columns.Add("IdEntradaTipo", typeof(int));
                        tablaEntradas.Columns.Add("Cantidad", typeof(int));
                        tablaEntradas.Columns.Add("PrecioUnitario", typeof(decimal));
                        tablaEntradas.Columns.Add("PrecioAplicado", typeof(decimal));
                        tablaEntradas.Columns.Add("SubTotal", typeof(decimal));

                        foreach (var d in detalleEntradas)
                        {
                            tablaEntradas.Rows.Add(
                                d.IdEntradaTipo,
                                d.Cantidad,
                                d.PrecioUnitario,
                                d.PrecioAplicado,
                                d.SubTotal
                            );
                        }

                        SqlParameter pEntradas = cmd.Parameters.AddWithValue("@DetalleEntradas", tablaEntradas);
                        pEntradas.SqlDbType = SqlDbType.Structured;
                        pEntradas.TypeName = "dbo.DetalleEntradaType";

                        // TABLA DETALLE PRODUCTOS
                        DataTable tablaProductos = new DataTable();
                        tablaProductos.Columns.Add("IdProducto", typeof(int));
                        tablaProductos.Columns.Add("Cantidad", typeof(int));
                        tablaProductos.Columns.Add("PrecioUnitario", typeof(decimal));
                        tablaProductos.Columns.Add("SubTotal", typeof(decimal));

                        foreach (var p in detalleProductos)
                        {
                            tablaProductos.Rows.Add(
                                p.IdProducto,
                                p.Cantidad,
                                p.PrecioUnitario,
                                p.SubTotal
                            );
                        }

                        SqlParameter pProductos = cmd.Parameters.AddWithValue("@DetalleProductos", tablaProductos);
                        pProductos.SqlDbType = SqlDbType.Structured;
                        pProductos.TypeName = "dbo.DetalleProductoType";

                        // OUTPUT
                        SqlParameter output = new SqlParameter("@NumeroVentaGenerado", SqlDbType.VarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(output);

                        con.Open();
                        int filas = cmd.ExecuteNonQuery();

                        numeroGenerado = output.Value.ToString();
                        respuesta = filas > 0;
                    }
                }
            }
            catch (Exception)
            {
                respuesta = false;
                numeroGenerado = "";
            }

            return respuesta;
        }
    }
}
