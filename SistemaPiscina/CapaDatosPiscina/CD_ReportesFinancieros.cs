using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;

namespace CapaDatosPiscina
{
    public class CD_ReportesFinancieros
    {
        public EResumenFinanciero ObtenerResumenFinanciero(DateTime fechaDesde, DateTime fechaHasta)
        {
            EResumenFinanciero resumen = null;

            using (SqlConnection con = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_RESUMEN_FINANCIERO_GENERAL", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FechaDesde", fechaDesde);
                cmd.Parameters.AddWithValue("@FechaHasta", fechaHasta);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        resumen = new EResumenFinanciero()
                        {
                            IngresosTotales = dr.GetDecimal(dr.GetOrdinal("IngresosTotales")),
                            PerdidasCortesias = dr.GetDecimal(dr.GetOrdinal("PerdidasCortesias")),
                            EgresosCompras = dr.GetDecimal(dr.GetOrdinal("EgresosCompras")),
                            EgresosGastos = dr.GetDecimal(dr.GetOrdinal("EgresosGastos")),
                        };
                    }
                }
            }

            return resumen;
        }
    }
}
