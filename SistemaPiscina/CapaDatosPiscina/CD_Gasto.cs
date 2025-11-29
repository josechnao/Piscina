using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatosPiscina
{
    public class CD_Gasto
    {

        // ======================================================
        // 1. REGISTRAR GASTO
        // ======================================================
        public int Registrar(EGasto obj, out string mensaje)
        {
            int idGastoGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_GASTO", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCategoriaGasto", obj.IdCategoriaGasto);
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdCajaTurno", (object)obj.IdCajaTurno ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Monto", obj.Monto);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);

                    SqlParameter resultado = new SqlParameter("@Resultado", SqlDbType.Int);
                    resultado.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultado);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    idGastoGenerado = Convert.ToInt32(resultado.Value);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return idGastoGenerado;
        }


        // ======================================================
        // 2. EDITAR GASTO
        // ======================================================
        public bool Editar(EGasto obj, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EDITAR_GASTO", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdGasto", obj.IdGasto);
                    cmd.Parameters.AddWithValue("@IdCategoriaGasto", obj.IdCategoriaGasto);
                    cmd.Parameters.AddWithValue("@Monto", obj.Monto);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);

                    SqlParameter resultado = new SqlParameter("@Resultado", SqlDbType.Int);
                    resultado.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultado);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(resultado.Value) == 1;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return respuesta;
        }


        // ======================================================
        // 3. CAMBIAR ESTADO
        // ======================================================
        public bool CambiarEstado(int idGasto, bool estado, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_CAMBIAR_ESTADO_GASTO", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdGasto", idGasto);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    SqlParameter resultado = new SqlParameter("@Resultado", SqlDbType.Int);
                    resultado.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultado);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(resultado.Value) == 1;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return respuesta;
        }


        // ======================================================
        // 4. LISTAR GASTOS – ADMIN
        // ======================================================
        public List<EGasto> ListarAdmin()
        {
            List<EGasto> lista = new List<EGasto>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTAR_GASTOS_ADMIN", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EGasto()
                            {
                                IdGasto = Convert.ToInt32(dr["IdGasto"]),
                                IdCategoriaGasto = Convert.ToInt32(dr["IdCategoriaGasto"]),
                                CategoriaDescripcion = dr["Categoria"].ToString(),

                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                UsuarioNombre = dr["Usuario"].ToString(),
                                RolDescripcion = dr["RolDescripcion"].ToString(),
                                IdRol = Convert.ToInt32(dr["IdRol"]),

                                IdCajaTurno = dr["IdCajaTurno"] != DBNull.Value
                                            ? Convert.ToInt32(dr["IdCajaTurno"])
                                            : (int?)null,

                                Monto = Convert.ToDecimal(dr["Monto"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EGasto>();
            }

            return lista;
        }


        // ======================================================
        // 5. LISTAR GASTOS – CAJERO
        // ======================================================
        public List<EGasto> ListarCajero(int idCajaTurno)
        {
            List<EGasto> lista = new List<EGasto>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTAR_GASTOS_CAJERO", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCajaTurno", idCajaTurno);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EGasto()
                            {
                                IdGasto = Convert.ToInt32(dr["IdGasto"]),
                                IdCategoriaGasto = Convert.ToInt32(dr["IdCategoriaGasto"]),
                                CategoriaDescripcion = dr["Categoria"].ToString(),

                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                UsuarioNombre = dr["Usuario"].ToString(),
                                RolDescripcion = dr["RolDescripcion"].ToString(),
                                IdRol = Convert.ToInt32(dr["IdRol"]),

                                IdCajaTurno = Convert.ToInt32(dr["IdCajaTurno"]),

                                Monto = Convert.ToDecimal(dr["Monto"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EGasto>();
            }

            return lista;
        }


        // ======================================================
        // 6. FILTRAR – ADMIN
        // ======================================================
        public List<EGasto> FiltrarAdmin(string descripcion, int idCategoria, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            List<EGasto> lista = new List<EGasto>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_FILTRAR_GASTOS_ADMIN", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Descripcion", (object)descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdCategoriaGasto", idCategoria);
                    cmd.Parameters.AddWithValue("@FechaDesde", (object)fechaDesde ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaHasta", (object)fechaHasta ?? DBNull.Value);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EGasto()
                            {
                                IdGasto = Convert.ToInt32(dr["IdGasto"]),
                                IdCategoriaGasto = Convert.ToInt32(dr["IdCategoriaGasto"]),
                                CategoriaDescripcion = dr["Categoria"].ToString(),

                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                UsuarioNombre = dr["Usuario"].ToString(),
                                RolDescripcion = dr["RolDescripcion"].ToString(),
                                IdRol = Convert.ToInt32(dr["IdRol"]),

                                IdCajaTurno = dr["IdCajaTurno"] != DBNull.Value
                                            ? Convert.ToInt32(dr["IdCajaTurno"])
                                            : (int?)null,

                                Monto = Convert.ToDecimal(dr["Monto"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EGasto>();
            }

            return lista;
        }


        // ======================================================
        // 7. FILTRAR – CAJERO
        // ======================================================
        public List<EGasto> FiltrarCajero(int idCajaTurno, string descripcion, int idCategoria)
        {
            List<EGasto> lista = new List<EGasto>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_FILTRAR_GASTOS_CAJERO", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCajaTurno", idCajaTurno);
                    cmd.Parameters.AddWithValue("@Descripcion", (object)descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdCategoriaGasto", idCategoria);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EGasto()
                            {
                                IdGasto = Convert.ToInt32(dr["IdGasto"]),
                                IdCategoriaGasto = Convert.ToInt32(dr["IdCategoriaGasto"]),
                                CategoriaDescripcion = dr["Categoria"].ToString(),

                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                UsuarioNombre = dr["Usuario"].ToString(),
                                RolDescripcion = dr["RolDescripcion"].ToString(),
                                IdRol = Convert.ToInt32(dr["IdRol"]),

                                IdCajaTurno = Convert.ToInt32(dr["IdCajaTurno"]),

                                Monto = Convert.ToDecimal(dr["Monto"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EGasto>();
            }

            return lista;
        }
    }
}
