using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatosPiscina
{
    public class CD_ReporteVentas
    {
        public List<EVentaReporte> ListarVentasReporte(DateTime fechaDesde, DateTime fechaHasta, string metodoPago)
        {
            List<EVentaReporte> lista = new List<EVentaReporte>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REPORTE_VENTAS_GENERAL", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FechaDesde", fechaDesde);
                        cmd.Parameters.AddWithValue("@FechaHasta", fechaHasta);

                        if (string.IsNullOrEmpty(metodoPago) || metodoPago == "Todos")
                            cmd.Parameters.AddWithValue("@MetodoPago", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@MetodoPago", metodoPago);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new EVentaReporte()
                                {
                                    IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                    NumeroVenta = dr["NumeroVenta"].ToString(),
                                    FechaHora = Convert.ToDateTime(dr["FechaRegistro"]),
                                    Cajero = dr["Cajero"].ToString(),
                                    MetodoPago = dr["MetodoPago"].ToString(),
                                    Total = Convert.ToDecimal(dr["MontoTotal"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<EVentaReporte>(); // evitar crash
            }

            return lista;
        }


        public VentaDetalleCompleto ObtenerDetalleVenta(int idVenta)
        {
            VentaDetalleCompleto detalle = new VentaDetalleCompleto();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REPORTE_VENTA_DETALLE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdVenta", idVenta);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            /* ===========================
                               1) PRIMER RESULTSET
                               Encabezado
                               =========================== */
                            if (dr.Read())
                            {
                                detalle.Encabezado = new EVentaDetalleEncabezado()
                                {
                                    IdVenta = idVenta,
                                    NumeroVenta = dr["NumeroVenta"].ToString(),
                                    FechaHora = Convert.ToDateTime(dr["FechaRegistro"]),
                                    MetodoPago = dr["MetodoPago"].ToString(),
                                    MontoTotal = Convert.ToDecimal(dr["MontoTotal"]),

                                    ClienteNombre = dr["ClienteNombre"].ToString(),
                                    ClienteDNI = dr["ClienteDNI"].ToString(),
                                    ClienteTelefono = dr["ClienteTelefono"].ToString(),

                                    Cajero = dr["Cajero"].ToString()
                                };
                            }

                            /* ===========================
                               CAMBIAR AL SEGUNDO RESULTSET
                               =========================== */
                            if (dr.NextResult())
                            {
                                while (dr.Read())
                                {
                                    detalle.Items.Add(new EVentaDetalleItem()
                                    {
                                        TipoItem = dr["TipoItem"].ToString(),
                                        NombreItem = dr["NombreItem"].ToString(),
                                        DescripcionItem = dr["DescripcionItem"].ToString(),
                                        PrecioUnitario = Convert.ToDecimal(dr["PrecioUnitario"]),
                                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                        SubTotal = Convert.ToDecimal(dr["SubTotal"]),
                                        MetodoPago = dr["MetodoPago"].ToString()
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                detalle = new VentaDetalleCompleto(); // evita crash
            }

            return detalle;
        }

    }
}