using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidadPiscina;
using CapaNegocioPiscina;

namespace CapaPresentacionPiscina.Modals
{
    public partial class mdDetalleCompra : Form
    {
        private int _idCompra;
        private CN_ReporteCompras oCN_Reportes = new CN_ReporteCompras();

        public mdDetalleCompra(int idCompra)
        {
            InitializeComponent();
            _idCompra = idCompra;
        }

        private void mdDetalleCompra_Load(object sender, EventArgs e)
        {
            CargarDetalleCompra();
        }

        private void CargarDetalleCompra()
        {
            // Llamar a negocio
            bool encontrado = oCN_Reportes.ObtenerDetalleCompra(
                _idCompra,
                out EReporteCompraCabecera cabecera,
                out List<EReporteCompraDetalle> detalle
            );

            if (!encontrado)
            {
                MessageBox.Show("No se encontró información de la compra.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            // ================================
            // CARGAR CABECERA EN LOS CONTROLES
            // ================================

            txtProveedor.Text = cabecera.ProveedorNombre;
            txtDocumento.Text = cabecera.DocumentoProveedor;
            txtTelefono.Text = cabecera.TelefonoProveedor;
            txtFechaHora.Text = cabecera.Fecha;
            txtNumeroCompra.Text = cabecera.NumeroCorrelativo.ToString();
            txtDocumento.Text = cabecera.NumeroDocumento;
            txtRegistradoPor.Text = cabecera.UsuarioNombre;
            txtMontoTotal.Text = cabecera.MontoTotal.ToString("0.00");


            // ============================
            // CARGAR DETALLE EN EL DGV
            // ============================
            dgvDetalleCompra.Rows.Clear();

            foreach (var item in detalle)
            {
                dgvDetalleCompra.Rows.Add(
                    item.Producto,
                    item.Descripcion,
                    item.Cantidad,
                    item.PrecioCompra.ToString("0.00"),
                    item.PrecioVenta.ToString("0.00"),
                    item.SubTotal.ToString("0.00")
                );
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
