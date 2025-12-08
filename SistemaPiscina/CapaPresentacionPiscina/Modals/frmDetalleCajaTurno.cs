using System;
using System.Windows.Forms;
using CapaNegocioPiscina;
using CapaEntidadPiscina;

namespace CapaPresentacionPiscina.Modals
{
    public partial class frmDetalleCajaTurno : Form
    {
        // =============================
        // VARIABLES INTERNAS
        // =============================
        private int _idTurno;
        private CN_ReporteCajaTurno oCN_Reporte = new CN_ReporteCajaTurno();

        // =============================
        // CONSTRUCTOR
        // =============================
        public frmDetalleCajaTurno(int idTurno)
        {
            InitializeComponent();
            _idTurno = idTurno;
        }

        // =============================
        // FORM LOAD
        // =============================
        private void frmDetalleCajaTurno_Load(object sender, EventArgs e)
        {
            CargarResumen();
            CargarVentas();
            CargarGastos();
            dgvDetalleVenta.AutoGenerateColumns = false;
            dgvDetalleGasto.AutoGenerateColumns = false;

        }

        // =============================
        // RESUMEN (PANEL SUPERIOR)
        // =============================
        private void CargarResumen()
        {
            EReporteCajaTurnoResumen resumen = oCN_Reporte.ObtenerDetalleTurno(_idTurno);

            if (resumen == null)
            {
                MessageBox.Show("No se encontraron datos del turno.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtCajero.Text = resumen.Cajero;
            txtIdTurno.Text = resumen.IdCajaTurno.ToString();

            txtApertura.Text = resumen.FechaApertura.ToString("dd/MM/yyyy HH:mm");
            txtCierre.Text = resumen.FechaCierre?.ToString("dd/MM/yyyy HH:mm") ?? "—";

            txtInicial.Text = resumen.MontoInicial.ToString("0.00");
            txtFinal.Text = resumen.MontoFinal.ToString("0.00");

            txtVentasCant.Text = resumen.TotalVentas.ToString();
            txtGastosCant.Text = resumen.TotalGastos.ToString();

            txtVentasSuma.Text = resumen.VentasSumaTotal.ToString("0.00");
            txtGastosSuma.Text = resumen.GastoTotalSuma.ToString("0.00");

            txtDiferencia.Text = resumen.Diferencia.ToString("0.00");

            txtMetodosPago.Text = resumen.MetodoPagoResumen;
            txtObservacion.Text = resumen.Observacion;
        }

        // =============================
        // DETALLE DE VENTAS
        // =============================
        private void CargarVentas()
        {
            var lista = oCN_Reporte.ListarVentasTurno(_idTurno);

            dgvDetalleVenta.DataSource = lista;

            dgvDetalleVenta.Columns["IdVenta"].Visible = false;
            dgvDetalleVenta.Columns["MontoTotal"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgvDetalleVenta.Columns["MontoTotal"].DefaultCellStyle.Format = "0.00";
            dgvDetalleVenta.Columns["FechaRegistro"].DefaultCellStyle.Format =
                "dd/MM/yyyy HH:mm";

            dgvDetalleVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // =============================
        // DETALLE DE GASTOS
        // =============================
        private void CargarGastos()
        {
            dgvDetalleGasto.AutoGenerateColumns = false;

            var lista = oCN_Reporte.ListarGastosTurno(_idTurno);
            dgvDetalleGasto.DataSource = lista;

            dgvDetalleGasto.Columns["Monto"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;
            dgvDetalleGasto.Columns["Monto"].DefaultCellStyle.Format = "0.00";

            dgvDetalleGasto.Columns["FechaRegistroGasto"].DefaultCellStyle.Format =
                "dd/MM/yyyy HH:mm";


            foreach (DataGridViewRow row in dgvDetalleGasto.Rows)
            {
                int estado = Convert.ToInt32(row.Cells["Estado"].Value);

                row.Cells["EstadoDescripcion"].Value =
                    estado == 1 ? "Activo" : "Inactivo";
            }

            dgvDetalleGasto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }




        // =============================
        // BOTÓN CERRAR
        // =============================
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
