using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_CajaTurno
    {
        // ============================================
        // 1. VERIFICAR CAJA ABIERTA
        // ============================================
        public ECajaTurno VerificarCajaAbierta(int idUsuario)
        {
            ECajaTurno obj = new ECajaTurno();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_VERIFICAR_CAJA_ABIERTA", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj.IdCajaTurno = Convert.ToInt32(dr["IdCajaTurno"]);
                            obj.MontoInicial = Convert.ToDecimal(dr["MontoInicial"]);
                            obj.FechaApertura = Convert.ToDateTime(dr["FechaApertura"]);
                            obj.Estado = Convert.ToBoolean(dr["Estado"]);
                            obj.TieneCajaAbierta = true;
                        }
                        else
                        {
                            obj.TieneCajaAbierta = false;
                        }
                    }
                }
            }
            catch
            {
                obj = new ECajaTurno();
            }

            return obj;
        }

        // ============================================
        // 2. ABRIR CAJA
        // ============================================
        public int AbrirCaja(int idUsuario, decimal montoInicial, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_ABRIR_CAJA", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@MontoInicial", montoInicial);

                    SqlParameter resultado = new SqlParameter("@Resultado", SqlDbType.Int);
                    resultado.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultado);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    idGenerado = Convert.ToInt32(resultado.Value);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return idGenerado;
        }

        // ============================================
        // 3. RESUMEN DEL TURNO
        // ============================================
        public (decimal MontoInicial, decimal TotalVentas, decimal TotalGastos)
        ObtenerResumen(int idCajaTurno)
            {
            decimal montoInicial = 0;
            decimal totalVentas = 0;
            decimal totalGastos = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RESUMEN_CAJA_TURNO", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCajaTurno", idCajaTurno);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            montoInicial = Convert.ToDecimal(dr["MontoInicial"]);
                            totalVentas = Convert.ToDecimal(dr["TotalVentas"]);
                            totalGastos = Convert.ToDecimal(dr["TotalGastos"]);
                        }
                    }
                }
            }
            catch
            {
                // si algo falla dejamos todo en 0
                montoInicial = 0;
                totalVentas = 0;
                totalGastos = 0;
            }

            return (montoInicial, totalVentas, totalGastos);
        }


        // ============================================
        // 4. CERRAR CAJA
        // ============================================
        public bool CerrarCaja(ECajaTurno obj, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = false;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_CERRAR_CAJA", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCajaTurno", obj.IdCajaTurno);
                    cmd.Parameters.AddWithValue("@MontoFinal", obj.MontoFinal);
                    cmd.Parameters.AddWithValue("@TotalVentas", obj.TotalVentas);
                    cmd.Parameters.AddWithValue("@TotalGastos", obj.TotalGastos);
                    cmd.Parameters.AddWithValue("@Diferencia", obj.Diferencia);
                    cmd.Parameters.AddWithValue("@Observacion",
                        (object)obj.Observacion ?? DBNull.Value);

                    SqlParameter resultado = new SqlParameter("@Resultado", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(resultado);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(resultado.Value);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return respuesta;
        }




    }
}
