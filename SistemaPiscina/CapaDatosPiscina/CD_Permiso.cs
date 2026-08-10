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
        
        public async Task<List<Permiso>> ListarAsync(int idRol)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "SP_LISTAR_PERMISOS_POR_ROL",
                        con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 8;

                        cmd.Parameters.Add(
                            "@IdRol",
                            SqlDbType.Int
                        ).Value = idRol;

                        await con.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                lista.Add(new Permiso()
                                {
                                    NombreMenu =
                                        dr["NombreMenu"].ToString(),

                                    NombreFormulario =
                                        dr["NombreFormulario"].ToString()
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception(
                        "No se pudieron cargar los permisos desde la base de datos.\n\n" +
                        "Detalle: " + ex.Message,
                        ex
                    );
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Ocurrió un error al cargar los permisos.\n\n" +
                        "Detalle: " + ex.Message,
                        ex
                    );
                }
            }

            return lista;
        }
    }

}
