using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_DetalleCompra
    {
        public bool RegistrarDetalleCompra(DetalleCompra obj, out string mensaje)
        {
            mensaje = "";
            bool ok = true;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarDetalleCompra", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCompra", obj.IdCompra);
                    cmd.Parameters.AddWithValue("@IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("@PrecioCompra", obj.PrecioCompra);
                    cmd.Parameters.AddWithValue("@PrecioVenta", obj.PrecioVenta);
                    cmd.Parameters.AddWithValue("@Cantidad", obj.Cantidad);
                    cmd.Parameters.AddWithValue("@Subtotal", obj.SubTotal);

                    conn.Open();

                    // IMPORTANTE: no comparar con > 0 por NOCOUNT
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                ok = false;
                mensaje = ex.Message;
            }

            return ok;
        }


    }
}
