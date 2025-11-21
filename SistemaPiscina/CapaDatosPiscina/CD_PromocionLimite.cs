using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_PromocionLimite
    {
        public bool Registrar(PromocionLimite obj, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RegistrarPromocionLimite", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdPromocion", obj.IdPromocion);
                        cmd.Parameters.AddWithValue("@TipoLimite", obj.TipoLimite);
                        cmd.Parameters.AddWithValue("@CantidadLimite",
    obj.CantidadLimite.HasValue ? (object)obj.CantidadLimite.Value : DBNull.Value);

                        cmd.Parameters.AddWithValue("@CantidadUsada", obj.CantidadUsada);

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
