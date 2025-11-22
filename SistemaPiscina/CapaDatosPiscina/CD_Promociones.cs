using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_Promociones
    {
        public bool RegistrarPromocion(
        Promocion promo,
        PromocionCondicion condicion,
        PromocionLimite limite,
        PromocionVigencia vigencia,
        out string mensaje)
        {
            mensaje = string.Empty;
            int resultado = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                using (SqlCommand cmd = new SqlCommand("SP_RegistrarPromocion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // === (1) Datos principales ===
                    cmd.Parameters.AddWithValue("@TipoPromo", promo.TipoPromo);
                    cmd.Parameters.AddWithValue("@IdEntradaTipo", promo.IdEntradaTipo);
                    cmd.Parameters.AddWithValue("@Porcentaje", promo.Porcentaje);

                    // Estado NO LO RECIBE EL SP → ELIMINADO

                    // === (2) Condición ===
                    cmd.Parameters.AddWithValue("@TipoCondicion", condicion.TipoCondicion);
                    cmd.Parameters.AddWithValue("@CantidadCondicion", (object)condicion.Cantidad ?? DBNull.Value);

                    // === (3) Límite ===
                    cmd.Parameters.AddWithValue("@TipoLimite", limite.TipoLimite);
                    cmd.Parameters.AddWithValue("@CantidadLimite", (object)limite.CantidadLimite ?? DBNull.Value);

                    // === (4) Vigencia ===
                    cmd.Parameters.AddWithValue("@TipoVigencia", vigencia.TipoVigencia);
                    cmd.Parameters.AddWithValue("@FechaInicio", (object)vigencia.FechaInicio ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaFin", (object)vigencia.FechaFin ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FechaDia", (object)vigencia.FechaDia ?? DBNull.Value);

                    // === (5) Salidas ===
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }

            return resultado == 1;
        }




        public List<PromocionListado> ListarPromociones2x1()
        {
            List<PromocionListado> lista = new List<PromocionListado>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ListarPromociones", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new PromocionListado
                                {
                                    IdPromocion = Convert.ToInt32(dr["IdPromocion"]),
                                    TipoPromo = dr["TipoPromo"].ToString(),
                                    Categoria = dr["Categoria"].ToString(),
                                    Porcentaje = Convert.ToDecimal(dr["Porcentaje"]),
                                    Estado = Convert.ToBoolean(dr["Estado"]),

                                    TipoCondicion = dr["TipoCondicion"].ToString(),
                                    CantidadCondicion = dr["CantidadCondicion"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CantidadCondicion"]),

                                    TipoLimite = dr["TipoLimite"].ToString(),
                                    CantidadLimite = dr["CantidadLimite"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["CantidadLimite"]),
                                    CantidadUsada = Convert.ToInt32(dr["CantidadUsada"]),

                                    TipoVigencia = dr["TipoVigencia"].ToString(),
                                    FechaInicio = dr["FechaInicio"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaInicio"]),
                                    FechaFin = dr["FechaFin"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaFin"]),
                                    FechaDia = dr["FechaDia"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaDia"]),
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                lista = new List<PromocionListado>();
            }

            return lista;
        }
        public bool EliminarPromocion(int idPromocion, out string mensaje)
        {
            mensaje = "";

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                using (SqlCommand cmd = new SqlCommand("SP_EliminarPromocion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPromocion", idPromocion);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }
        public PromocionConfiguracion ObtenerPromocionActiva()
        {
            PromocionConfiguracion promo = null;

            using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_PROMO_ACTIVA_COMPLETA", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        promo = new PromocionConfiguracion()
                        {
                            IdPromocion = Convert.ToInt32(dr["IdPromocion"]),
                            TipoPromo = dr["TipoPromo"].ToString(),
                            IdEntradaTipo = Convert.ToInt32(dr["IdEntradaTipo"]),
                            Porcentaje = dr["Porcentaje"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Porcentaje"]),
                            Estado = Convert.ToBoolean(dr["Estado"]),
                            NombreCategoria = dr["Categoria"] == DBNull.Value ? "" : dr["Categoria"].ToString(),
                            TipoCondicion = dr["TipoCondicion"]?.ToString(),
                            CantidadCondicion = dr["CantidadCondicion"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CantidadCondicion"]),

                            TipoLimite = dr["TipoLimite"]?.ToString(),
                            CantidadLimite = dr["CantidadLimite"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CantidadLimite"]),
                            CantidadUsada = dr["CantidadUsada"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CantidadUsada"]),

                            TipoVigencia = dr["TipoVigencia"]?.ToString(),
                            FechaInicio = dr["FechaInicio"] == DBNull.Value ? null : (DateTime?)dr["FechaInicio"],
                            FechaFin = dr["FechaFin"] == DBNull.Value ? null : (DateTime?)dr["FechaFin"],
                            FechaDia = dr["FechaDia"] == DBNull.Value ? null : (DateTime?)dr["FechaDia"]
                        };
                    }
                }
            }

            return promo;
        }



    }
}
