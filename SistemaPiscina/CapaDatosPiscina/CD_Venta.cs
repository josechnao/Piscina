using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_Venta
    {
        public int Registrar(Venta obj, out string mensaje, out string numeroVenta)
        {
            int resultado = 0;
            mensaje = string.Empty;
            numeroVenta = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REGISTRAR_VENTA_COMPLETA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros principales
                        cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);

                        // Cliente opcional
                        if (obj.IdCliente.HasValue)
                            cmd.Parameters.AddWithValue("@IdCliente", obj.IdCliente.Value);
                        else
                            cmd.Parameters.AddWithValue("@IdCliente", DBNull.Value);

                        cmd.Parameters.AddWithValue("@MetodoPago", obj.MetodoPago);
                        cmd.Parameters.AddWithValue("@IdCajaTurno", obj.IdCajaTurno);

                        // =============================
                        //     TVP: DETALLE ENTRADAS
                        // =============================
                        DataTable dtEntradas = new DataTable();
                        dtEntradas.Columns.Add("IdEntradaTipo", typeof(int));
                        dtEntradas.Columns.Add("Cantidad", typeof(int));
                        dtEntradas.Columns.Add("PrecioUnitario", typeof(decimal));
                        dtEntradas.Columns.Add("PrecioAplicado", typeof(decimal));
                        dtEntradas.Columns.Add("SubTotal", typeof(decimal));

                        if (obj.ListaEntradas != null)
                        {
                            foreach (DetalleVentaEntrada item in obj.ListaEntradas)
                            {
                                dtEntradas.Rows.Add(
                                    item.IdEntradaTipo,
                                    item.Cantidad,
                                    item.PrecioUnitario,
                                    item.PrecioAplicado,
                                    item.SubTotal
                                );
                            }
                        }

                        SqlParameter pEntradas = cmd.Parameters.AddWithValue("@DetalleEntradas", dtEntradas);
                        pEntradas.SqlDbType = SqlDbType.Structured;


                        // =============================
                        //     TVP: DETALLE PRODUCTOS
                        // =============================
                        DataTable dtProductos = new DataTable();
                        dtProductos.Columns.Add("IdProducto", typeof(int));
                        dtProductos.Columns.Add("Cantidad", typeof(int));
                        dtProductos.Columns.Add("PrecioUnitario", typeof(decimal));
                        dtProductos.Columns.Add("SubTotal", typeof(decimal));

                        if (obj.ListaProductos != null)
                        {
                            foreach (DetalleVentaProducto item in obj.ListaProductos)
                            {
                                dtProductos.Rows.Add(
                                    item.IdProducto,
                                    item.Cantidad,
                                    item.PrecioUnitario,
                                    item.SubTotal
                                );
                            }
                        }

                        SqlParameter pProductos = cmd.Parameters.AddWithValue("@DetalleProductos", dtProductos);
                        pProductos.SqlDbType = SqlDbType.Structured;


                        // =============================
                        //     OUTPUT PARAMETERS
                        // =============================
                        SqlParameter pResultado = new SqlParameter("@Resultado", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(pResultado);

                        SqlParameter pMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(pMensaje);


                        // =============================
                        //     EJECUTAR
                        // =============================
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                        mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                        // Si resultado > 0, mensaje contiene el NumeroVenta
                        if (resultado > 0)
                        {
                            numeroVenta = mensaje;
                            mensaje = "Venta registrada correctamente.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                mensaje = ex.Message;
                numeroVenta = string.Empty;
            }

            return resultado;
        }
    }
}
