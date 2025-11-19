using CapaDatosPiscina;
using System;
using System.Data;
using System.Data.SqlClient;

public class CD_CorrelativoCompra
{
    public int ObtenerCorrelativo()
    {
        int ultimoNumero = 0;

        using (SqlConnection conn = new SqlConnection(Conexion.cadena))
        {
            SqlCommand cmd = new SqlCommand("SP_ObtenerCorrelativoCompra", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            ultimoNumero = Convert.ToInt32(cmd.ExecuteScalar());
        }

        return ultimoNumero;
    }

    public bool ActualizarCorrelativo()
    {
        bool ok = false;

        using (SqlConnection conn = new SqlConnection(Conexion.cadena))
        {
            SqlCommand cmd = new SqlCommand("SP_IncrementarCorrelativoCompra", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();
            ok = cmd.ExecuteNonQuery() > 0;
        }

        return ok;
    }
}
