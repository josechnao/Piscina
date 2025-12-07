using CapaEntidad;
using CapaEntidadPiscina;
using CapaNegocio;
using CapaNegocioPiscina;
using CapaPresentacionPiscina.Modals;
using CapaPresentacionPiscina.Utilidades;
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

            cboMetodoPago.SelectedIndex = 0;

            // Fechas solo visuales, no afectan la carga inicial
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            // SOLO ESTO 🎯
            CargarVentasSinFiltro();
        }


        private void CargarVentasSinFiltro()
        {
            CN_ReporteVentas objNegocio = new CN_ReporteVentas();

            // Rango exagerado para traer TODO
            DateTime desde = new DateTime(2000, 1, 1);
            DateTime hasta = DateTime.Today.AddDays(1);

            var lista = objNegocio.ListarVentas(desde, hasta, null);

            dgvReporteVentas.Rows.Clear();

            foreach (var item in lista)
            {
                // Si es CORTESIA, mostrar 0.00 siempre
                string totalMostrar = item.MetodoPago == "CORTESIA"
                    ? "0.00"
                    : item.Total.ToString("0.00");

                dgvReporteVentas.Rows.Add(
                    item.IdVenta,
                    item.NumeroVenta,
                    item.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                    item.Cajero,
                    item.MetodoPago,
                    totalMostrar,
                    "Ver"
                );
            }


            CalcularTotal();
        }

        private void CargarVentas()
        {
            CN_ReporteVentas objNegocio = new CN_ReporteVentas();

            string metodoPago = cboMetodoPago.SelectedItem.ToString();
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            List<EVentaReporte> lista = objNegocio.ListarVentas(desde, hasta, metodoPago);

            dgvReporteVentas.Rows.Clear();

            foreach (var item in lista)
            {
                // Si es CORTESIA, mostrar 0.00 siempre
                string totalMostrar = item.MetodoPago == "CORTESIA"
                    ? "0.00"
                    : item.Total.ToString("0.00");

                dgvReporteVentas.Rows.Add(
                    item.IdVenta,
                    item.NumeroVenta,
                    item.FechaHora.ToString("dd/MM/yyyy HH:mm"),
                    item.Cajero,
                    item.MetodoPago,
                    totalMostrar,
                    "Ver"
                );
            }

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarVentas();
            CalcularTotal();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cboMetodoPago.SelectedIndex = 0;
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            CargarVentasSinFiltro();
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
        private void CalcularTotal()
        {
            decimal suma = 0;

            foreach (DataGridViewRow row in dgvReporteVentas.Rows)
            {
                if (row.Cells["Total"].Value != null &&
                    !string.IsNullOrWhiteSpace(row.Cells["Total"].Value.ToString()))
                {
                    if (decimal.TryParse(row.Cells["Total"].Value.ToString(), out decimal valor))
                    {
                        suma += valor;
                    }
                }
            }

            txtTotal.Text = suma.ToString("0.00");
        }


        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Archivo PDF (*.pdf)|*.pdf";
            save.FileName = "ReporteVentas.pdf";

            if (save.ShowDialog() == DialogResult.OK)
            {
                CN_Negocios oNegocio = new CN_Negocios();
                ENegocio datos = oNegocio.ObtenerDatosNegocio();

                PDF_Reportes.ExportarReporteVentas(
                    save.FileName,
                    datos.Logo,              // byte[]
                    datos.NombreNegocio,
                    datos.Direccion,
                    datos.Ciudad,
                    dgvReporteVentas,
                    txtTotal.Text
                );

                MessageBox.Show("Reporte exportado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
