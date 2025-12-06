using CapaEntidadPiscina;
using CapaNegocioPiscina;
using CapaPresentacionPiscina.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmReporteVentas : Form
    {
        public frmReporteVentas()
        {
            InitializeComponent();
        }

        private void frmReporteVentas_Load(object sender, EventArgs e)
        {
            cboMetodoPago.Items.Clear();
            cboMetodoPago.Items.Add("Todos");
            cboMetodoPago.Items.Add("EFECTIVO");
            cboMetodoPago.Items.Add("QR");
            cboMetodoPago.Items.Add("CORTESIA");

            cboMetodoPago.SelectedIndex = 0; // por defecto "Todos"

            // Por estética, puedes setear las fechas al día actual
            dtpDesde.Value = new DateTime(2025, 1, 1);
            dtpHasta.Value = DateTime.Today;

            // 3. Cargar ventas automáticamente
            CargarVentas();
        }
        private void CargarVentas()
        {
            CN_ReporteVentas objNegocio = new CN_ReporteVentas();

            string metodoPago = cboMetodoPago.SelectedItem.ToString();
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            List<EVentaReporte> lista = objNegocio.ListarVentas(desde, hasta, metodoPago);

            dgvReporteVentas.Rows.Clear();

            foreach (EVentaReporte v in lista)
            {
                dgvReporteVentas.Rows.Add(
                    v.IdVenta,
                    v.NumeroVenta,
                    v.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                    v.Cajero,
                    v.MetodoPago,
                    v.Total.ToString("0.00"),
                    "Ver" // texto si usas botón normal (para icono deja string vacío "")
                );
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarVentas();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cboMetodoPago.SelectedIndex = 0;       // Método de pago = Todos
            dtpDesde.Value = new DateTime(2025, 1, 1);
            dtpHasta.Value = DateTime.Today;      // Hasta hoy

            CargarVentas();                       // 🔥 Recarga todas las ventas
        }


        private void dgvReporteVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvReporteVentas.Columns[e.ColumnIndex].Name == "btnVerDetalle")
            {
                int idVenta = Convert.ToInt32(dgvReporteVentas.Rows[e.RowIndex].Cells["IdVenta"].Value);

                frmModalDetalleVenta modal = new frmModalDetalleVenta(idVenta);
                modal.ShowDialog();
            }
        }
    }
}
