using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_PromocionVigencia
    {
        public bool Registrar(PromocionVigencia obj, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RegistrarPromocionVigencia", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdPromocion", obj.IdPromocion);
                        cmd.Parameters.AddWithValue("@TipoVigencia", obj.TipoVigencia);
                        cmd.Parameters.AddWithValue("@FechaInicio",
                            obj.FechaInicio.HasValue ? (object)obj.FechaInicio.Value : DBNull.Value);

                        cmd.Parameters.AddWithValue("@FechaFin",
                            obj.FechaFin.HasValue ? (object)obj.FechaFin.Value : DBNull.Value);

                        cmd.Parameters.AddWithValue("@FechaDia",
                            obj.FechaDia.HasValue ? (object)obj.FechaDia.Value : DBNull.Value);


                        cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();

                        int resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                        mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                        return resultado == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }
    }
}
