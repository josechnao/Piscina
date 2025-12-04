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
    public class CD_Permiso
    {
        public List<Permiso> Listar(int idRol)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_LISTAR_PERMISOS_POR_ROL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdRol", idRol);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Permiso()
                        {
                            NombreMenu = dr["NombreMenu"].ToString(),
                            NombreFormulario = dr["NombreFormulario"].ToString()
                        });
                    }
                }
            }

            return lista;
        }
    }

}
