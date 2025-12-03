using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatosPiscina
{
    public class CD_Venta
    {
        public int RegistrarVenta(
            int idUsuario,
            int idCajaTurno,
            string dni,
            string nombreCompleto,
            string telefono,
            string metodoPago,
            decimal montoTotal,
            string xmlDetalle,
            out string numeroVenta,
            out string mensaje
        )
        {
            int idVentaGenerado = 0;
            numeroVenta = string.Empty;
            mensaje = string.Empty;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_VENTA", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@IdCajaTurno", idCajaTurno);
                    cmd.Parameters.AddWithValue("@DNI", dni);
                    cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                    cmd.Parameters.AddWithValue("@Telefono", (object)telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MetodoPago", metodoPago);
                    cmd.Parameters.AddWithValue("@MontoTotal", montoTotal);
                    cmd.Parameters.AddWithValue("@Detalle", xmlDetalle);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IdVentaGenerado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@NumeroVentaGenerado", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    bool resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                    if (resultado)
                    {
                        idVentaGenerado = Convert.ToInt32(cmd.Parameters["@IdVentaGenerado"].Value);
                        numeroVenta = cmd.Parameters["@NumeroVentaGenerado"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    idVentaGenerado = 0;
                    numeroVenta = string.Empty;
                    mensaje = ex.Message;
                }
            }

            return idVentaGenerado;
        }
    }
}
