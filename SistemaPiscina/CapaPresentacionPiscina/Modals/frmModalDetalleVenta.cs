using CapaNegocioPiscina;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacionPiscina.Modals
{
    public partial class frmModalDetalleVenta : Form
    {
        private int _idVenta;
        public frmModalDetalleVenta(int idVenta)
        {
            InitializeComponent();
            _idVenta = idVenta;
        }

        private void frmModalDetalleVenta_Load(object sender, EventArgs e)
        {
            CargarDetalleVenta();
        }
        private void CargarDetalleVenta()
        {
            CN_ReporteVentas objNegocio = new CN_ReporteVentas();
            var detalle = objNegocio.ObtenerDetalleVenta(_idVenta);

            // ===========================
            // 1) Cargar Encabezado
            // ===========================
            txtNumeroVenta.Text = detalle.Encabezado.NumeroVenta;
            txtMetodoPago.Text = detalle.Encabezado.MetodoPago;
            txtFechaHora.Text = detalle.Encabezado.FechaHora.ToString("dd/MM/yyyy HH:mm");
            txtMontoTotal.Text = detalle.Encabezado.MontoTotal.ToString("0.00");

            // Datos del cliente
            txtCliente.Text = detalle.Encabezado.ClienteNombre;
            txtDNI.Text = detalle.Encabezado.ClienteDNI;
            txtTelefono.Text = detalle.Encabezado.ClienteTelefono;

            // ===========================
            // 2) Cargar Items al DGV
            // ===========================
            dgvDetalleVenta.Rows.Clear();

            foreach (var item in detalle.Items)
            {
                dgvDetalleVenta.Rows.Add(
                    _idVenta,                                   // Columna IdVenta (oculta)
                    item.NombreItem,                           // Producto / Entrada
                    item.DescripcionItem,                      // Descripción
                    item.PrecioUnitario.ToString("0.00"),      // P. Unitario
                    item.Cantidad,                             // Cantidad
                    item.SubTotal.ToString("0.00"),            // Sub Total
                    item.MetodoPago                            // Método de Pago
                );
            }

        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            this.Close(); //Cierra el modal
        }
    }
}
