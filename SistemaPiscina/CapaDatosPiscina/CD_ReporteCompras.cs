using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatosPiscina
{
    public class CD_ReporteCompras
    {
        public List<EProveedorCombo> ListarProveedores()
        {
            List<EProveedorCombo> lista = new List<EProveedorCombo>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REPORTE_LISTAR_PROVEEDORES", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EProveedorCombo
                            {
                                IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                Nombre = dr["Nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EProveedorCombo>();
            }

            return lista;
        }

        public List<EReporteCompra> ListarCompras(
    DateTime fechaInicio,
    DateTime fechaFin,
    int idProveedor,
    string documentoProveedor,
    string numeroDocumento,
    string numeroCorrelativo)
        {
            List<EReporteCompra> lista = new List<EReporteCompra>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REPORTE_LISTAR_COMPRAS", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@DocumentoProveedor", documentoProveedor);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", numeroDocumento);
                    cmd.Parameters.AddWithValue("@NumeroCorrelativo", numeroCorrelativo);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new EReporteCompra
                            {
                                IdCompra = Convert.ToInt32(dr["IdCompra"]),
                                NumeroCorrelativo = Convert.ToInt32(dr["NumeroCorrelativo"]),
                                Proveedor = dr["Proveedor"].ToString(),
                                DocumentoProveedor = dr["DocumentoProveedor"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Fecha = dr["Fecha"].ToString(),
                                TotalCompra = Convert.ToDecimal(dr["TotalCompra"]),
                                UsuarioNombre = dr["UsuarioNombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<EReporteCompra>();
            }

            return lista;
        }


        public bool ObtenerDetalleCompra(
    int idCompra,
    out EReporteCompraCabecera cabecera,
    out List<EReporteCompraDetalle> detalle)
        {
            cabecera = new EReporteCompraCabecera();
            detalle = new List<EReporteCompraDetalle>();
            bool encontrado = false;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_REPORTE_DETALLE_COMPRA", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCompra", idCompra);

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // =============================
                        // 1) PRIMER RESULTSET: CABECERA
                        // =============================
                        if (dr.Read())
                        {
                            cabecera = new EReporteCompraCabecera
                            {
                                ProveedorNombre = dr["ProveedorNombre"].ToString(),
                                DocumentoProveedor = dr["DocumentoProveedor"].ToString(),
                                TelefonoProveedor = dr["TelefonoProveedor"].ToString(),
                                NumeroCorrelativo = Convert.ToInt32(dr["NumeroCorrelativo"]),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                Fecha = dr["Fecha"].ToString(),
                                UsuarioNombre = dr["UsuarioNombre"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"])
                            };

                            encontrado = true;
                        }

                        // Pasamos al segundo resultset
                        if (dr.NextResult())
                        {
                            while (dr.Read())
                            {
                                detalle.Add(new EReporteCompraDetalle
                                {
                                    Producto = dr["Producto"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                    PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"]),
                                    PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                    SubTotal = Convert.ToDecimal(dr["SubTotal"])
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                encontrado = false;
                detalle = new List<EReporteCompraDetalle>();
            }

            return encontrado;
        }

    }
}