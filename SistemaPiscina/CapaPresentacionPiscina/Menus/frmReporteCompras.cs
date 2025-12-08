using CapaEntidadPiscina;
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
    public partial class frmReporteCompras : Form
    {
        CN_ReporteCompras oCN_Reportes = new CN_ReporteCompras();

        public frmReporteCompras()
        {
            InitializeComponent();
        }

        private void frmReporteCompras_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            dtpDesde.Value = DateTime.Today.AddDays(-7); // ejemplo: últimos 7 días
            dtpHasta.Value = DateTime.Today;
            CargarCompras(); // cargar todo al inicio
        }

        private void CargarProveedores()
        {
            List<EProveedorCombo> lista = oCN_Reportes.ListarProveedores();

            cboProveedor.Items.Clear();
            cboProveedor.Items.Add(new { Id = 0, Nombre = "Todos" });

            foreach (var item in lista)
            {
                cboProveedor.Items.Add(new { Id = item.IdProveedor, Nombre = item.Nombre });
            }

            cboProveedor.DisplayMember = "Nombre";
            cboProveedor.ValueMember = "Id";
            cboProveedor.SelectedIndex = 0;
        }

        private void CargarCompras()
        {
            dgvCompras.Rows.Clear();

            int idProveedor = 0;
            if (cboProveedor.SelectedItem != null)
            {
                dynamic obj = cboProveedor.SelectedItem;
                idProveedor = obj.Id;
            }

            string documentoProveedor = txtNumeroDocumento.Text.Trim();  // documento proveedor
            string numeroDocumento = "";                                 // documento de compra NO SE FILTRA
            string numeroCorrelativo = txtNumeroCompra.Text.Trim();      // número de compra

            List<EReporteCompra> lista = oCN_Reportes.ListarCompras(
                dtpDesde.Value,
                dtpHasta.Value,
                idProveedor,
                documentoProveedor,   // DOC PROVEEDOR = OK
                numeroDocumento,      // DOC COMPRA = VACÍO
                numeroCorrelativo     // N° COMPRA
            );

            foreach (EReporteCompra item in lista)
            {
                dgvCompras.Rows.Add(
                    item.IdCompra,
                    item.NumeroCorrelativo,
                    item.Proveedor,
                    item.DocumentoProveedor,
                    item.NumeroDocumento,
                    item.Fecha,
                    item.TotalCompra.ToString("0.00"),
                    item.UsuarioNombre,
                    ""
                );
            }

            CalcularSumaTotal();
        }



        private void CalcularSumaTotal()
        {
            decimal suma = 0;

            foreach (DataGridViewRow row in dgvCompras.Rows)
            {
                if (row.Cells["TotalCompra"].Value != null)
                {
                    suma += Convert.ToDecimal(row.Cells["TotalCompra"].Value);
                }
            }

            txtSumaTotal.Text = suma.ToString("0.00");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCompras();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNumeroCompra.Text = "";
            txtNumeroDocumento.Text = "";
            txtNumeroDocumento.Text = "";
            cboProveedor.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;

            CargarCompras();
        }

        private void dgvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCompras.Columns[e.ColumnIndex].Name == "btnVerDetalle")
            {
                int idCompra = Convert.ToInt32(dgvCompras.Rows[e.RowIndex].Cells["IdCompra"].Value);

                mdDetalleCompra modal = new mdDetalleCompra(idCompra);
                modal.ShowDialog();
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvCompras.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Archivo PDF|*.pdf";
            save.FileName = "ReporteCompras.pdf";

            if (save.ShowDialog() == DialogResult.OK)
            {
                // Obtener datos del negocio DESDE LA BD
                CN_Negocios cnNegocio = new CN_Negocios();
                ENegocio negocio = cnNegocio.ObtenerDatosNegocio();

                // Llamar al PDF exclusivo de compras
                PDF_ReportesCompras.ExportarReporteCompras(
                    save.FileName,
                    negocio.Logo,                 // logo desde BD
                    negocio.NombreNegocio,        // nombre desde BD
                    negocio.Direccion,            // dirección desde BD
                    negocio.Ciudad,               // ciudad desde BD
                    dgvCompras,                   // dgv a exportar
                    txtSumaTotal.Text             // total general
                );

                MessageBox.Show("PDF generado correctamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
