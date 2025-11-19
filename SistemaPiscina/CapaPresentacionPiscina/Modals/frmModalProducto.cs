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
using System.Windows.Documents;
using System.Windows.Forms;



namespace CapaPresentacionPiscina.Modals
{
    public partial class frmModalProducto : Form
    {
        private int indiceSeleccionado = -1;
        public frmModalProducto()
        {
            InitializeComponent();
        }

        // ESTA PROPIEDAD PERMITE RETORNAR EL PRODUCTO SELECCIONADO
        public Producto ProductoSeleccionado { get; set; }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmModalProducto_Load(object sender, EventArgs e)
        {
            cboBusqueda.Items.Add("Codigo");
            cboBusqueda.Items.Add("Nombre");
            cboBusqueda.Items.Add("Categoria");
            cboBusqueda.SelectedIndex = 0;

            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto item in lista)
            {
                dgvData.Rows.Add(
                    "",
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.oCategoria.Descripcion,   
                      item.IdProducto   // <--- columna oculta
                );
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar" && e.RowIndex >= 0)
            {
                if (dgvData.Rows[e.RowIndex].IsNewRow) return;
                if (dgvData.Rows[e.RowIndex].Cells["IdProducto"].Value == null) return;

                indiceSeleccionado = e.RowIndex;

                txtCodigo.Text = dgvData.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                txtNombre.Text = dgvData.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = dgvData.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();
                txtCategoria.Text = dgvData.Rows[e.RowIndex].Cells["Categoria"].Value.ToString();
            }

        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Trim() == "")
            {
                MessageBox.Show("Seleccione un producto primero.");
                return;
            }

            ProductoSeleccionado = new Producto()
            {
                IdProducto = Convert.ToInt32(dgvData.Rows[indiceSeleccionado].Cells["IdProducto"].Value),
                Codigo = txtCodigo.Text,
                Nombre = txtNombre.Text
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = cboBusqueda.SelectedItem.ToString();
            string textoFiltro = txtBusqueda.Text.Trim().ToUpper();

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                bool mostrar = row.Cells[columnaFiltro].Value.ToString().ToUpper().Contains(textoFiltro);
                row.Visible = mostrar;
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            cboBusqueda.SelectedIndex = 0;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

    }
}