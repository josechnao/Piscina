using CapaEntidadPiscina;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatosPiscina
{
    public class CD_Promocion
    {
        public EPromocion Obtener()
        {
            EPromocion obj = new EPromocion();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_OBTENER_PROMO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                obj.IdPromocion = Convert.ToInt32(dr["IdPromocion"]);
                                obj.Estado = Convert.ToBoolean(dr["Estado"]);
                                obj.Categoria = dr["Categoria"].ToString();
                                obj.UsuarioModifico = Convert.ToInt32(dr["UsuarioModifico"]);
                                obj.FechaActualizacion = dr["FechaActualizacion"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj = null; // coherente con tus otros módulos
                throw ex;
            }

            return obj;
        }


        public bool Actualizar(EPromocion obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ACTUALIZAR_PROMO", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                        cmd.Parameters.AddWithValue("@Categoria", obj.Categoria);
                        cmd.Parameters.AddWithValue("@UsuarioModifico", obj.UsuarioModifico);

                        conn.Open();

                        int filas = cmd.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            mensaje = "No se pudo actualizar la promoción.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }
    }
}
