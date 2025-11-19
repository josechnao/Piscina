using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTARPRODUCTOS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Codigo = dr["Codigo"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"]),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                Estado = Convert.ToBoolean(dr["EstadoValor"]),

                                oCategoria = new Categoria()
                                {
                                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                                    Descripcion = dr["Categoria"].ToString()
                                }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();
                }
            }

            return lista;
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPRODUCTO", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@IdCategoria", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    resultado = 0;
                    Mensaje = ex.Message;
                }
            }

            return resultado;
        }

        public int Editar(Producto obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITARPRODUCTO", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("@Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@IdCategoria", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    resultado = 0;
                    Mensaje = ex.Message;
                }
            }

            return resultado;
        }

        public int CambiarEstado(int idProducto, bool estado, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_CAMBIARESTADO_PRODUCTO", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    resultado = 0;
                    Mensaje = ex.Message;
                }
            }

            return resultado;
        }

        public bool ActualizarStock(int idProducto, int cantidad)
        {
            bool ok = false;

            using (SqlConnection conn = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarStockProducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);

                conn.Open();
                ok = cmd.ExecuteNonQuery() > 0;
            }

            return ok;
        }
        public bool ActualizarPrecios(int idProducto, decimal precioCompra, decimal precioVenta)
        {
            bool ok = false;

            using (SqlConnection conn = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ActualizarPreciosProducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                cmd.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                cmd.Parameters.AddWithValue("@PrecioVenta", precioVenta);

                conn.Open();
                ok = cmd.ExecuteNonQuery() > 0;
            }

            return ok;
        }


    }
}
