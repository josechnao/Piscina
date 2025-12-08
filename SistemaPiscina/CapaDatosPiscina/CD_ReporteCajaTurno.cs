using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_ReporteCajaTurno
    {
        public List<EReporteCajaTurnoResumen> ListarResumen(DateTime fechaDesde, DateTime fechaHasta, int idUsuario)
        {
            List<EReporteCajaTurnoResumen> lista = new List<EReporteCajaTurnoResumen>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REPORTE_CAJATURNO_RESUMEN", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FechaDesde", fechaDesde);
                        cmd.Parameters.AddWithValue("@FechaHasta", fechaHasta);
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new EReporteCajaTurnoResumen()
                                {
                                    Cajero = dr["Cajero"].ToString(),
                                    IdCajaTurno = Convert.ToInt32(dr["IdCajaTurno"]),
                                    FechaApertura = Convert.ToDateTime(dr["FechaApertura"]),
                                    FechaCierre = dr["FechaCierre"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaCierre"]),
                                    MontoInicial = Convert.ToDecimal(dr["MontoInicial"]),
                                    MontoFinal = Convert.ToDecimal(dr["MontoFinal"]),

                                    TotalVentas = Convert.ToInt32(dr["TotalVentas"]),
                                    VentasSumaTotal = Convert.ToDecimal(dr["VentasSumaTotal"]),

                                    TotalGastos = Convert.ToInt32(dr["TotalGastos"]),
                                    GastoTotalSuma = Convert.ToDecimal(dr["GastoTotalSuma"]),

                                    Diferencia = Convert.ToDecimal(dr["Diferencia"]),

                                    MetodoPagoResumen = dr["MetodoPagoResumen"].ToString(),
                                    Observacion = dr["Observacion"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EReporteCajaTurnoResumen>();
            }

            return lista;
        }

        public EReporteCajaTurnoResumen ObtenerDetalleTurno(int idCajaTurno)
        {
            EReporteCajaTurnoResumen obj = null;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REPORTE_CAJATURNO_DETALLE_TURNO", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCajaTurno", idCajaTurno);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                obj = new EReporteCajaTurnoResumen()
                                {
                                    Cajero = dr["Cajero"].ToString(),
                                    IdCajaTurno = Convert.ToInt32(dr["IdCajaTurno"]),
                                    FechaApertura = Convert.ToDateTime(dr["FechaApertura"]),
                                    FechaCierre = dr["FechaCierre"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FechaCierre"]),
                                    MontoInicial = Convert.ToDecimal(dr["MontoInicial"]),
                                    MontoFinal = Convert.ToDecimal(dr["MontoFinal"]),

                                    TotalVentas = Convert.ToInt32(dr["TotalVentas"]),
                                    VentasSumaTotal = Convert.ToDecimal(dr["VentasSumaTotal"]),

                                    TotalGastos = Convert.ToInt32(dr["TotalGastos"]),
                                    GastoTotalSuma = Convert.ToDecimal(dr["GastoTotalSuma"]),

                                    Diferencia = Convert.ToDecimal(dr["Diferencia"]),

                                    MetodoPagoResumen = dr["MetodoPagoResumen"].ToString(),
                                    Observacion = dr["Observacion"].ToString()
                                };
                            }
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

        public List<EReporteCajaTurnoVenta> ListarVentasTurno(int idCajaTurno)
        {
            List<EReporteCajaTurnoVenta> lista = new List<EReporteCajaTurnoVenta>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REPORTE_CAJATURNO_VENTAS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCajaTurno", idCajaTurno);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new EReporteCajaTurnoVenta()
                                {
                                    IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                    NroTicket = dr["NroTicket"].ToString(),
                                    MontoTotal = Convert.ToDecimal(dr["MontoTotal"]),
                                    MetodoPago = dr["MetodoPago"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EReporteCajaTurnoVenta>();
            }

            return lista;
        }


        public List<EReporteCajaTurnoGasto> ListarGastosTurno(int idCajaTurno)
        {
            List<EReporteCajaTurnoGasto> lista = new List<EReporteCajaTurnoGasto>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REPORTE_CAJATURNO_GASTOS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCajaTurno", idCajaTurno);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new EReporteCajaTurnoGasto()
                                {
                                    Categoria = dr["Categoria"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Monto = Convert.ToDecimal(dr["Monto"]),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]),
                                    Estado = Convert.ToInt32(dr["Estado"]),
                                    EstadoDescripcion = Convert.ToInt32(dr["Estado"]) == 1 ? "Activo" : "Inactivo"
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EReporteCajaTurnoGasto>();
            }

            return lista;
        }
    }
}
