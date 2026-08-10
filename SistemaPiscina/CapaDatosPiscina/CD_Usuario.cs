using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CapaDatosPiscina
{
    public class CD_Usuario
    {

        public async Task<Usuario> LoginAsync(string documento, string clave)
        {
            Usuario obj = new Usuario();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_LOGIN", oconexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Evita esperas demasiado largas
                        cmd.CommandTimeout = 8;

                        cmd.Parameters.Add("@Documento", SqlDbType.VarChar, 50).Value = documento;
                        cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = clave;

                        await oconexion.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                obj = new Usuario()
                                {
                                    IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                    Documento = dr["Documento"].ToString(),
                                    NombreCompleto = dr["NombreCompleto"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"]),

                                    oRol = new Rol()
                                    {
                                        IdRol = Convert.ToInt32(dr["IdRol"]),
                                        Descripcion = dr["DescripcionRol"].ToString()
                                    }
                                };
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception(
                        "No se pudo conectar correctamente con la base de datos. " +
                        "Verifique que SQL Server esté iniciado.\n\nDetalle: " +
                        ex.Message,
                        ex
                    );
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Ocurrió un error al iniciar sesión.\n\nDetalle: " +
                        ex.Message,
                        ex
                    );
                }
            }

            return obj;
        }

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTAR_USUARIOS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                oRol = new Rol()
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    Descripcion = dr["Rol"].ToString()
                                },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch
                {
                    lista = new List<Usuario>();
                }
            }

            return lista;
        }

        public int Guardar(Usuario u)
        {
            int resultado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_GUARDAR_USUARIO", oconexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", u.IdUsuario);
                cmd.Parameters.AddWithValue("@Documento", u.Documento);
                cmd.Parameters.AddWithValue("@NombreCompleto", u.NombreCompleto);
                cmd.Parameters.AddWithValue("@Clave", u.Clave);
                cmd.Parameters.AddWithValue("@IdRol", u.IdRol);
                cmd.Parameters.AddWithValue("@Estado", u.Estado);

                SqlParameter pResultado = new SqlParameter("@Resultado", SqlDbType.Int);
                pResultado.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pResultado);

                oconexion.Open();
                cmd.ExecuteNonQuery();

                resultado = Convert.ToInt32(pResultado.Value);
            }

            return resultado;
        }

        public bool Eliminar(int idUsuario)
        {
            bool resultado = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINAR_USUARIO", oconexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                SqlParameter pResultado = new SqlParameter("@Resultado", SqlDbType.Bit);
                pResultado.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pResultado);

                oconexion.Open();
                cmd.ExecuteNonQuery();

                resultado = Convert.ToBoolean(pResultado.Value);
            }

            return resultado;
        }

    }
}
