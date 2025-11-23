using CapaEntidadPiscina;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatosPiscina
{
    public class CD_Cliente
    {
        public Cliente BuscarPorDNI(string dni)
        {
            Cliente cli = null;

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_BUSCARCLIENTE_POR_DNI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DNI", dni);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        cli = new Cliente()
                        {
                            IdCliente = Convert.ToInt32(dr["IdCliente"]),
                            DNI = dr["DNI"].ToString(),
                            NombreCompleto = dr["NombreCompleto"].ToString(),
                            Telefono = dr["Telefono"].ToString()
                        };
                    }
                }
            }

            return cli;
        }
    }
}
