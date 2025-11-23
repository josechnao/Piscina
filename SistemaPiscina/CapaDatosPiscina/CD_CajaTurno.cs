using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatosPiscina
{
    public class CD_CajaTurno
    {
        public CajaTurno ObtenerCajaActiva(int idUsuario)
        {
            CajaTurno obj = null;

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_CAJA_OBTENER_ACTIVA", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        obj = new CajaTurno()
                        {
                            IdCajaTurno = Convert.ToInt32(dr["IdCajaTurno"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            FechaApertura = Convert.ToDateTime(dr["FechaApertura"]),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        };
                    }
                }
            }

            return obj;
        }

    }
}
