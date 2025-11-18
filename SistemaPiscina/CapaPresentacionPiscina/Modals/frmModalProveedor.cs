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

namespace CapaPresentacionPiscina.Modals
{
    public partial class frmModalProveedor : Form
    {
        public Proveedor ProveedorSeleccionado { get; set; }
        public frmModalProveedor()
        {
            InitializeComponent();
        }

        private void frmModalProveedor_Load(object sender, EventArgs e)
        {
            // 1. Llenar combo de búsqueda
            cboBusqueda.Items.Add("Documento");
            cboBusqueda.Items.Add("Nombre");
            cboBusqueda.Items.Add("Telefono");
            cboBusqueda.Items.Add("Correo");
            cboBusqueda.SelectedIndex = 0;

            // 2. Cargar todos los proveedores en el DataGridView
            List<Proveedor> lista = new CN_Proveedor().Listar();

            foreach (Proveedor item in lista)
            {
                dgvProveedores.Rows.Add(
                    "",                    // Columna botón
                    item.Documento,
                    item.Nombre,
                    item.Telefono,
                    item.Correo
                );
            }
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            string filtro = cboBusqueda.SelectedItem.ToString();
            string texto = txtBusqueda.Text.Trim().ToLower();

            if (texto == "")
            {
                MessageBox.Show("Ingrese un valor para buscar.");
                return;
            }

            foreach (DataGridViewRow row in dgvProveedores.Rows)
            {
                bool coincide = false;

                switch (filtro)
                {
                    case "Documento":
                        coincide = row.Cells["Documento"].Value.ToString().ToLower().Contains(texto);
                        break;
                    case "Nombre":
                        coincide = row.Cells["Nombre"].Value.ToString().ToLower().Contains(texto);
                        break;
                    case "Correo":
                        coincide = row.Cells["Correo"].Value.ToString().ToLower().Contains(texto);
                        break;
                }

                row.Visible = coincide;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            cboBusqueda.SelectedIndex = 0;

            foreach (DataGridViewRow row in dgvProveedores.Rows)
            {
                row.Visible = true;
            }
        }

        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProveedores.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                txtDocumento.Text = dgvProveedores.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
                txtNombre.Text = dgvProveedores.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                txtTelefono.Text = dgvProveedores.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                txtCorreo.Text = dgvProveedores.Rows[e.RowIndex].Cells["Correo"].Value.ToString();
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Text.Trim() == "")
            {
                MessageBox.Show("Seleccione un proveedor primero.");
                return;
            }

            ProveedorSeleccionado = new Proveedor()
            {
                Documento = txtDocumento.Text,
                Nombre = txtNombre.Text
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
