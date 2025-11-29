using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatosPiscina
{
    public class CD_CategoriaGasto
    {
        public List<ECategoriaGasto> Listar()
        {
            List<ECategoriaGasto> lista = new List<ECategoriaGasto>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_LISTAR_CATEGORIA_GASTO", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ECategoriaGasto()
                            {
                                IdCategoriaGasto = Convert.ToInt32(dr["IdCategoriaGasto"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<ECategoriaGasto>();
            }

            return lista;
        }
    }
}
