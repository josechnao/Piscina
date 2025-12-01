using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_EntradaTipo
    {
        public List<EntradaTipo> Listar()
        {
            List<EntradaTipo> lista = new List<EntradaTipo>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    string query = "SP_ListarEntradaTipo";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new EntradaTipo()
                                {
                                    IdEntradaTipo = Convert.ToInt32(dr["IdEntradaTipo"]),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    PrecioBase = Convert.ToDecimal(dr["PrecioBase"]),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EntradaTipo>();
            }

            return lista;
        }


        public bool ActualizarPrecio(int idEntradaTipo, decimal nuevoPrecio, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    string query = "SP_ActualizarPreciosEntrada";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdEntradaTipo", idEntradaTipo);
                        cmd.Parameters.AddWithValue("@NuevoPrecio", nuevoPrecio);

                        con.Open();

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        resultado = filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                resultado = false;
            }

            return resultado;
        }

        public List<EntradaTipo> ListarEntradasVenta()
        {
            List<EntradaTipo> lista = new List<EntradaTipo>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTAR_ENTRADASTIPO_ACTIVAS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EntradaTipo()
                            {
                                IdEntradaTipo = Convert.ToInt32(dr["IdEntradaTipo"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                PrecioBase = Convert.ToDecimal(dr["PrecioBase"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch
                {
                    lista = new List<EntradaTipo>();
                }
            }

            return lista;
        }


    }
}
