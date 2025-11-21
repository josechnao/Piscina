using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;
using System.Collections.Generic;

namespace CapaDatosPiscina
{
    public class CD_VentaPiscina
    {
        public ResultadoVenta RegistrarVentaPiscina(
            int idUsuario,
            int? idCliente,
            string metodoPago,
            int idCajaTurno,
            List<DetalleVentaEntrada> entradas,
            List<DetalleVentaProducto> productos)
        {
            ResultadoVenta resultado = new ResultadoVenta();
            resultado.Exito = false;
            resultado.PromoParcial = false;
            resultado.Mensaje = "";

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_RegistrarVentaPiscina", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // ► 1. Parámetros simples
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@IdCliente", (object)idCliente ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MetodoPago", metodoPago);
                        cmd.Parameters.AddWithValue("@IdCajaTurno", idCajaTurno);

                        // ► 2. Crear TVP para Entradas
                        DataTable tvpEntradas = new DataTable();
                        tvpEntradas.Columns.Add("IdEntradaTipo", typeof(int));
                        tvpEntradas.Columns.Add("Cantidad", typeof(int));
                        tvpEntradas.Columns.Add("PrecioUnitario", typeof(decimal));

                        foreach (var item in entradas)
                        {
                            tvpEntradas.Rows.Add(item.IdEntradaTipo, item.Cantidad, item.PrecioUnitario);
                        }

                        SqlParameter paramEntradas = cmd.Parameters.AddWithValue("@DetalleEntradas", tvpEntradas);
                        paramEntradas.TypeName = "TVP_DetalleEntrada";

                        // ► 3. Crear TVP para Productos
                        DataTable tvpProductos = new DataTable();
                        tvpProductos.Columns.Add("IdProducto", typeof(int));
                        tvpProductos.Columns.Add("Cantidad", typeof(int));
                        tvpProductos.Columns.Add("PrecioUnitario", typeof(decimal));

                        foreach (var item in productos)
                        {
                            tvpProductos.Rows.Add(item.IdProducto, item.Cantidad, item.PrecioUnitario);
                        }

                        SqlParameter paramProductos = cmd.Parameters.AddWithValue("@DetalleProductos", tvpProductos);
                        paramProductos.TypeName = "TVP_DetalleProducto";

                        // ► 4. Parámetros OUTPUT
                        SqlParameter paramNumeroVenta = new SqlParameter("@NumeroVenta", SqlDbType.VarChar, 50);
                        paramNumeroVenta.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramNumeroVenta);

                        SqlParameter paramPromoParcial = new SqlParameter("@PromoParcial", SqlDbType.Bit);
                        paramPromoParcial.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramPromoParcial);

                        SqlParameter paramResultado = new SqlParameter("@Resultado", SqlDbType.Bit);
                        paramResultado.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramResultado);

                        SqlParameter paramMensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 500);
                        paramMensaje.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramMensaje);

                        // ► 5. Ejecutar SP
                        cmd.ExecuteNonQuery();

                        // ► 6. Leer OUTPUTS
                        resultado.Exito = Convert.ToBoolean(paramResultado.Value);
                        resultado.Mensaje = paramMensaje.Value.ToString();
                        resultado.NumeroVenta = paramNumeroVenta.Value.ToString();
                        resultado.PromoParcial = Convert.ToBoolean(paramPromoParcial.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = ex.Message;
            }

            return resultado;
        }
    }

}
