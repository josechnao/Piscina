using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_Compra
    {
        public int RegistrarCompra(Compra obj, out string mensaje)
        {
            mensaje = "";
            int idGenerado = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarCompra", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdProveedor", obj.IdProveedor);
                    cmd.Parameters.AddWithValue("@TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("@NumeroCorrelativo", obj.NumeroCorrelativo);
                    cmd.Parameters.AddWithValue("@MontoTotal", obj.MontoTotal);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    idGenerado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                idGenerado = 0;
            }

            return idGenerado;
        }

    }
}
