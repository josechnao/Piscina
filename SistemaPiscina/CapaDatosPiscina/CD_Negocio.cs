using CapaDatosPiscina;
using CapaEntidad;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Negocio
    {
        public Negocio ObtenerDatos()
        {
            Negocio obj = new Negocio();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_NEGOCIO", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj.IdNegocio = Convert.ToInt32(dr["IdNegocio"]);
                            obj.NombreNegocio = dr["NombreNegocio"].ToString();
                            obj.Direccion = dr["Direccion"].ToString();
                            obj.Ciudad = dr["Ciudad"].ToString();
                            obj.Telefono = dr["Telefono"].ToString();

                            // Logo puede venir NULL
                            if (dr["Logo"] != DBNull.Value)
                                obj.Logo = (byte[])dr["Logo"];
                            else
                                obj.Logo = null;
                        }
                    }
                }
            }
            catch
            {
                obj = null;
            }

            return obj;
        }

        public bool ActualizarDatos(Negocio obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool resultado = false;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_UPDATE_NEGOCIO", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreNegocio", obj.NombreNegocio);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("@Ciudad", obj.Ciudad);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);

                    // Logo: si es null enviamos DBNull
                    cmd.Parameters.Add("@Logo", SqlDbType.VarBinary).Value =
                        (object)obj.Logo ?? DBNull.Value;

                    // ✔️ Agregar los OUTPUT requeridos por el SP
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }

    }
}
