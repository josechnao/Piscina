using System;
using System.Data;
using System.Data.SqlClient;
using CapaEntidadPiscina;
using System.Threading.Tasks;

namespace CapaDatosPiscina
{
    public class CD_Venta
    {

        public async Task<ResultadoVenta> RegistrarVentaAsync(
    int idUsuario,
    int? idCajaTurno,
    string dni,
    string nombreCompleto,
    string telefono,
    string metodoPago,
    decimal montoTotal,
    string xmlDetalle
)
        {
            ResultadoVenta respuesta = new ResultadoVenta
            {
                IdVenta = 0,
                NumeroVenta = string.Empty,
                Mensaje = string.Empty,
                Exito = false
            };

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REGISTRAR_VENTA", oconexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Evita que la interfaz quede esperando demasiado
                        cmd.CommandTimeout = 15;

                        cmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;

                        cmd.Parameters.Add("@IdCajaTurno", SqlDbType.Int).Value =
                            (object)idCajaTurno ?? DBNull.Value;

                        cmd.Parameters.Add("@DNI", SqlDbType.VarChar, 50).Value = dni;

                        cmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar, 150).Value =
                            nombreCompleto;

                        cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 50).Value =
                            string.IsNullOrWhiteSpace(telefono)
                                ? (object)DBNull.Value
                                : telefono;

                        cmd.Parameters.Add("@MetodoPago", SqlDbType.VarChar, 50).Value =
                            metodoPago;

                        cmd.Parameters.Add("@MontoTotal", SqlDbType.Decimal).Value =
                            montoTotal;

                        cmd.Parameters.Add("@Detalle", SqlDbType.Xml).Value =
                            xmlDetalle;

                        cmd.Parameters.Add(
                            "@Resultado",
                            SqlDbType.Bit
                        ).Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(
                            "@Mensaje",
                            SqlDbType.VarChar,
                            500
                        ).Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(
                            "@IdVentaGenerado",
                            SqlDbType.Int
                        ).Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(
                            "@NumeroVentaGenerado",
                            SqlDbType.VarChar,
                            50
                        ).Direction = ParameterDirection.Output;

                        await oconexion.OpenAsync();

                        await cmd.ExecuteNonQueryAsync();

                        bool exito = Convert.ToBoolean(
                            cmd.Parameters["@Resultado"].Value
                        );

                        respuesta.Exito = exito;

                        respuesta.Mensaje =
                            cmd.Parameters["@Mensaje"].Value?.ToString() ?? "";

                        if (exito)
                        {
                            respuesta.IdVenta = Convert.ToInt32(
                                cmd.Parameters["@IdVentaGenerado"].Value
                            );

                            respuesta.NumeroVenta =
                                cmd.Parameters["@NumeroVentaGenerado"].Value?.ToString() ?? "";
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception(
                        "No se pudo registrar la venta en la base de datos.\n\n" +
                        "Detalle: " + ex.Message,
                        ex
                    );
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Ocurrió un error al registrar la venta.\n\n" +
                        "Detalle: " + ex.Message,
                        ex
                    );
                }
            }

            return respuesta;
        }
    }
}
