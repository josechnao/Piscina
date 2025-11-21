using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_PromocionCondicion
    {
        public bool Registrar(PromocionCondicion obj, out string mensaje)
        {
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RegistrarPromocionCondicion", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdPromocion", obj.IdPromocion);
                        cmd.Parameters.AddWithValue("@TipoCondicion", obj.TipoCondicion);
                        cmd.Parameters.AddWithValue("@Cantidad",
    obj.Cantidad.HasValue ? (object)obj.Cantidad.Value : DBNull.Value);


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
