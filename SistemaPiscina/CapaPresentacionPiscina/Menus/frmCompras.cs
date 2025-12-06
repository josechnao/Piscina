using CapaEntidadPiscina;  
using CapaPresentacionPiscina.Modals;
using System;
using System.Collections.Generic;
using CapaNegocioPiscina;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CapaPresentacionPiscina.Menus
{
    public partial class frmCompras : Form
    {

        private int _idProveedorSeleccionado = 0;
        private int _idProductoSeleccionado = 0;

        private List<DetalleCompra> _listaDetalles = new List<DetalleCompra>();

        public frmCompras()
        {
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Clear();

            cboTipoDocumento.Items.Add("FACTURA");
            cboTipoDocumento.Items.Add("RECIBO");
            cboTipoDocumento.Items.Add("BOLETA");
            cboTipoDocumento.Items.Add("SIN DOCUMENTO");

            cboTipoDocumento.SelectedIndex = 0;  // Primera opción seleccionada

            CargarCorrelativo();  // <-- Ya lo tienes listo
        }
        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            // Si ya hay productos agregados NO permitir cambiar de proveedor
            if (_listaDetalles.Count > 0)
            {
                MessageBox.Show(
                    "No puede cambiar el proveedor porque ya agregó productos.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            using (var modal = new frmModalProveedor())
            {
                var resultado = modal.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    _idProveedorSeleccionado = modal.ProveedorSeleccionado.IdProveedor;

                    txtDocumentoProveedor.Text = modal.ProveedorSeleccionado.Documento;
                    txtNombreProveedor.Text = modal.ProveedorSeleccionado.Nombre;
                }
            }
        }


        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new frmModalProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _idProductoSeleccionado = modal.ProductoSeleccionado.IdProducto;
                    txtCodigoProducto.Text = modal.ProductoSeleccionado.Codigo;
                    txtNombreProducto.Text = modal.ProductoSeleccionado.Nombre;
                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {

            if (_idProductoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecioCompra.Text, out decimal precioCompra))
            {
                MessageBox.Show("Precio de compra inválido.");
                return;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out decimal precioVenta))
            {
                MessageBox.Show("Precio de venta inválido.");
                return;
            }

            // ❗ Aquí estaba tu error
            int cantidad = (int)nudCantidad.Value;

            if (cantidad <= 0)
            {
                MessageBox.Show("Cantidad inválida.");
                return;
            }

            decimal subtotal = precioCompra * cantidad;

            // Agregar al DGV
            dgvCompras.Rows.Add(
                null, // columna eliminar
                _idProductoSeleccionado,
                txtCodigoProducto.Text,
                txtNombreProducto.Text,
                precioCompra.ToString("0.00"),
                precioVenta.ToString("0.00"),
                cantidad,
                subtotal.ToString("0.00")
            );

            // Agregar a la lista interna
            _listaDetalles.Add(new DetalleCompra
            {
                IdProducto = _idProductoSeleccionado,
                PrecioCompra = precioCompra,
                PrecioVenta = precioVenta,
                Cantidad = cantidad,
                SubTotal = subtotal
            });

            CalcularTotal();
            LimpiarProducto();
        }


        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvCompras.Rows)
            {
                var valor = row.Cells["SubTotal"].Value;

                if (valor != null && valor.ToString() != "")
                {
                    total += Convert.ToDecimal(valor);
                }
            }

            txtTotalPagar.Text = total.ToString("0.00");
        }





        private void LimpiarProducto()
        {
            _idProductoSeleccionado = 0;
            txtCodigoProducto.Clear();
            txtNombreProducto.Clear();
            txtPrecioCompra.Clear();
            txtPrecioVenta.Clear();
            nudCantidad.Value = 1;
        }

        private void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            if (_idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor.", "Aviso");
                return;
            }

            if (_listaDetalles.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto.");
                return;
            }

            Compra compra = new Compra
            {
                IdUsuario = SesionUsuario.UsuarioActual.IdUsuario,
                IdProveedor = _idProveedorSeleccionado,
                TipoDocumento = cboTipoDocumento.Text,
                NumeroDocumento = txtCorrelativo.Text,
                NumeroCorrelativo = Convert.ToInt32(txtCorrelativo.Text),
                MontoTotal = Convert.ToDecimal(txtTotalPagar.Text)
            };

            bool respuesta = new CN_Compra().RegistrarCompra(compra, _listaDetalles, out string mensaje);

            if (respuesta)
            {
                MessageBox.Show("Compra registrada correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarTodo();
            }
            else
            {
                MessageBox.Show("Error: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarTodo()
        {
            // proveedor
            _idProveedorSeleccionado = 0;
            txtDocumentoProveedor.Clear();
            txtNombreProveedor.Clear();

            // productos
            LimpiarProducto();

            // lista y dgv
            _listaDetalles.Clear();
            dgvCompras.Rows.Clear();

            txtTotalPagar.Text = "0.00";

            // recargar correlativo
            CargarCorrelativo();
        }
        private void CargarCorrelativo()
        {
            int numero = new CN_CorrelativoCompra().ObtenerCorrelativo();
            txtCorrelativo.Text = numero.ToString("000000");
        }

        private void dgvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que se hizo clic en una fila válida
            if (e.RowIndex < 0) return;

            // Validar que se hizo clic en la columna "Eliminar"
            if (dgvCompras.Columns[e.ColumnIndex].Name != "btnEliminar")
                return;

            // Verificar si es la fila nueva (la del asterisco)
            if (dgvCompras.Rows[e.RowIndex].IsNewRow)
            {
                MessageBox.Show("Seleccione una fila con datos para eliminar.",
                                "Aviso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            // Obtener el ID del producto de la fila seleccionada
            int idProducto = Convert.ToInt32(dgvCompras.Rows[e.RowIndex].Cells["IdProducto"].Value);

            // Remover del DGV
            dgvCompras.Rows.RemoveAt(e.RowIndex);

            // Remover de la lista interna
            var item = _listaDetalles.FirstOrDefault(x => x.IdProducto == idProducto);
            if (item != null)
                _listaDetalles.Remove(item);

            // Recalcular total
            CalcularTotal();

            // Si ya no hay productos, permitir cambiar proveedor de nuevo
            if (_listaDetalles.Count == 0)
            {
                _idProveedorSeleccionado = 0;
                txtDocumentoProveedor.Text = "";
                txtNombreProveedor.Text = "";
            }
        }


    }
}
