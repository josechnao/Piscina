using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_Negocios
    {
        public ENegocio Obtener()
        {
            ENegocio obj = null;

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Negocio", con);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        obj = new ENegocio()
                        {
                            IdNegocio = Convert.ToInt32(dr["IdNegocio"]),
                            NombreNegocio = dr["NombreNegocio"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            Ciudad = dr["Ciudad"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Logo = dr["Logo"] == DBNull.Value ? null : (byte[])dr["Logo"]
                        };
                    }
                }
            }

            return obj;
        }
    }
}
