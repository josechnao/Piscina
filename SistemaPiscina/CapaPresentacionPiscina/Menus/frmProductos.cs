using CapaEntidadPiscina;
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

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmProductos : Form
    {
        private int idProductoSeleccionado = 0;
        private int indiceSeleccionado = -1;
        public frmProductos()
        {
            InitializeComponent();
        }

        List<Categoria> listaCategorias = new List<Categoria>();
        private void CargarCategorias()
        {

            listaCategorias = new CN_Categoria().Listar();

            cboCategoria.Items.Clear();

            foreach (Categoria item in listaCategorias)
            {
                if (item.Estado)    // TRUE = Activo
                {
                    cboCategoria.Items.Add(item.Descripcion);
                }
            }

            cboCategoria.SelectedIndex = 0;
        }

        private void CargarEstados()
        {
            cboEstado.Items.Clear();
            cboEstado.Items.Add("Activo");    // 1
            cboEstado.Items.Add("Inactivo");  // 0
            cboEstado.SelectedIndex = 0;
        }

        private void CargarFiltrosBusqueda()
        {
            cboBusqueda.Items.Clear();
            cboBusqueda.Items.Add("Codigo");
            cboBusqueda.Items.Add("Nombre");
            cboBusqueda.Items.Add("Categoria");

            cboBusqueda.SelectedIndex = 0;
        }

        private int ObtenerIdCategoria(string descripcion)
        {
            foreach (Categoria item in listaCategorias)
            {
                if (item.Descripcion.Trim().ToLower() == descripcion.Trim().ToLower())
                {
                    return item.IdCategoria;
                }
            }

            return 0;
        }

        private void CargarProductos()
        {
            dgvData.Rows.Clear();

            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto item in lista)
            {
                dgvData.Rows.Add(
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.oCategoria.Descripcion,
                    item.IdCategoria,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado ? 1 : 0,
                    item.Estado ? "Activo" : "Inactivo"
                );
            }
        }


        private void frmProductos_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarEstados();
            CargarFiltrosBusqueda();
            CargarProductos();  // este lo hacemos después del DGV
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Producto obj = new Producto()
            {
                IdProducto = idProductoSeleccionado,            // ← CLAVE
                Codigo = txtCodigo.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                IdCategoria = ObtenerIdCategoria(cboCategoria.Text),
                Estado = cboEstado.Text == "Activo" ? true : false
            };

            int respuesta = 0;

            if (idProductoSeleccionado == 0)
            {
                // INSERTAR
                respuesta = new CN_Producto().Registrar(obj, out mensaje);
            }
            else
            {
                // EDITAR
                respuesta = new CN_Producto().Editar(obj, out mensaje);
            }

            if (respuesta > 0)
            {
                if (idProductoSeleccionado == 0)
                {
                    MessageBox.Show("✔ El producto fue registrado con éxito.",
                                    "Registro completo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("✔ El producto fue actualizado correctamente.",
                                    "Actualización exitosa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }

                CargarProductos();
                Limpiar();
            }

        }



        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar selección
            if (idProductoSeleccionado == 0)

            {
                MessageBox.Show("Debe seleccionar un producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Está seguro de inactivar este producto?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int idProducto = idProductoSeleccionado;
                string mensaje = string.Empty;

                int resp = new CN_Producto().CambiarEstado(idProducto, false, out mensaje);

                if (resp > 0)
                {
                    MessageBox.Show("Producto inactivado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProductos();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("No se pudo inactivar: " + mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void Limpiar()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            cboCategoria.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;

            txtCodigo.Focus();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar" && e.RowIndex >= 0)
            {
                indiceSeleccionado = e.RowIndex;

                idProductoSeleccionado = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["IdProducto"].Value.ToString());
                txtCodigo.Text = dgvData.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                txtNombre.Text = dgvData.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = dgvData.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();

                // Categoría (texto)
                string categoriaTexto = dgvData.Rows[e.RowIndex].Cells["Categoria"].Value.ToString();

                // Seleccionar categoría en el combo
                cboCategoria.SelectedItem = categoriaTexto;

                // Estado (Activo / Inactivo)
                string estadoTexto = dgvData.Rows[e.RowIndex].Cells["Estado"].Value.ToString();
                cboEstado.SelectedItem = estadoTexto;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = cboBusqueda.SelectedItem.ToString();
            string textoBusqueda = txtBusqueda.Text.Trim().ToUpper();

            if (textoBusqueda == "")
            {
                MessageBox.Show("Debe ingresar un texto para buscar.", "Advertencia",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.Cells[columnaFiltro].Value != null)
                {
                    string valor = row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper();

                    if (valor.Contains(textoBusqueda))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }


        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            cboBusqueda.SelectedIndex = 0;

            // Mostrar todas las filas nuevamente
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

    }
}
