using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_CajaTurno
    {
        public ECajaTurno VerificarCajaAbierta(int idUsuario)
        {
            ECajaTurno obj = new ECajaTurno();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_VERIFICAR_CAJA_ABIERTA", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    // Parámetros OUTPUT
                    cmd.Parameters.Add("@IdCajaTurno", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@EstadoCaja", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int idCaja = Convert.ToInt32(cmd.Parameters["@IdCajaTurno"].Value);
                    bool estadoCaja = Convert.ToBoolean(cmd.Parameters["@EstadoCaja"].Value);

                    obj.IdCajaTurno = idCaja;
                    obj.TieneCajaAbierta = estadoCaja;
                }
            }
            catch
            {
                obj.IdCajaTurno = 0;
                obj.TieneCajaAbierta = false;
            }

            return obj;
        }

        public int AbrirCaja(int idUsuario, decimal montoInicial, out string mensaje)
        {
            int idCajaGenerada = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ABRIR_CAJA", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@MontoInicial", montoInicial);

                    // Salidas
                    cmd.Parameters.Add("@IdCajaTurno", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    idCajaGenerada = Convert.ToInt32(cmd.Parameters["@IdCajaTurno"].Value);
                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idCajaGenerada = 0;
                mensaje = ex.Message;
            }

            return idCajaGenerada;
        }

        public ECajaTurno ObtenerCajaActiva(int idUsuario)
        {
            ECajaTurno obj = new ECajaTurno();

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_OBTENER_CAJA_ACTIVA", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj.IdCajaTurno = Convert.ToInt32(dr["IdCajaTurno"]);
                            obj.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            obj.MontoInicial = Convert.ToDecimal(dr["MontoInicial"]);

                            obj.MontoFinal = dr["MontoFinal"] != DBNull.Value
                                ? (decimal?)Convert.ToDecimal(dr["MontoFinal"])
                                : (decimal?)null;

                            obj.TotalVentas = dr["TotalVentas"] != DBNull.Value
                                ? (decimal?)Convert.ToDecimal(dr["TotalVentas"])
                                : (decimal?)null;

                            obj.TotalGastos = dr["TotalGastos"] != DBNull.Value
                                ? (decimal?)Convert.ToDecimal(dr["TotalGastos"])
                                : (decimal?)null;

                            obj.Diferencia = dr["Diferencia"] != DBNull.Value
                                ? (decimal?)Convert.ToDecimal(dr["Diferencia"])
                                : (decimal?)null;

                            obj.FechaCierre = dr["FechaCierre"] != DBNull.Value
                                ? (DateTime?)Convert.ToDateTime(dr["FechaCierre"])
                                : (DateTime?)null;


                            obj.Observacion = dr["Observacion"].ToString();

                            obj.Estado = Convert.ToBoolean(dr["Estado"]);
                        }
                    }
                }
            }
            catch
            {
                obj = new ECajaTurno(); // evitar errores en UI
            }

            return obj;
        }

        public string CerrarCaja(int idCajaTurno, decimal montoFinal, string observacion)
        {
            string mensaje = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_CERRAR_CAJA", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCajaTurno", idCajaTurno);
                    cmd.Parameters.AddWithValue("@MontoFinal", montoFinal);
                    cmd.Parameters.AddWithValue("@Observacion", (object)observacion ?? DBNull.Value);

                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    mensaje = cmd.Parameters["@Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return mensaje;
        }
    }
}


    
