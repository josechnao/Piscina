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
    public partial class frmProveedores : Form
    {
        private CN_Proveedor objCN_Proveedor = new CN_Proveedor();
        private int idProveedorSeleccionado = 0;
        private int indiceSeleccionado = -1;
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load_1(object sender, EventArgs e)
        {
            CargarCombos();
            ListarProveedores();
        }


        private void CargarCombos()
        {
            // ESTADO
            DataTable tablaEstado = new DataTable();
            tablaEstado.Columns.Add("Valor", typeof(int));
            tablaEstado.Columns.Add("Texto", typeof(string));

            tablaEstado.Rows.Add(1, "Activo");
            tablaEstado.Rows.Add(0, "Inactivo");

            cboEstado.DataSource = tablaEstado;
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            // CARGAR FILTRO DE BÚSQUEDA
            DataTable tablaBusqueda = new DataTable();
            tablaBusqueda.Columns.Add("Valor", typeof(string));
            tablaBusqueda.Columns.Add("Texto", typeof(string));

            tablaBusqueda.Rows.Add("Nombre", "Nombre");
            tablaBusqueda.Rows.Add("Documento", "Documento");
            tablaBusqueda.Rows.Add("Telefono", "Telefono");
            tablaBusqueda.Rows.Add("Correo", "Correo");
            tablaBusqueda.Rows.Add("Estado", "Estado");   // <-- EN VEZ DE EstadoValor



            cboBusqueda.DataSource = tablaBusqueda;
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

        }



        private void ListarProveedores()
        {
            dgvData.Rows.Clear();

            List<Proveedor> lista = objCN_Proveedor.Listar();

            foreach (Proveedor p in lista)
            {
                dgvData.Rows.Add(new object[]
                {
            "", // btnSeleccionar
            p.IdProveedor,
            p.Nombre,
            p.Documento,
            p.Telefono,
            p.Correo,
            p.Estado ? "Activo" : "Inactivo",
            p.Estado // EstadoValor (oculto)
                });
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            string mensaje;

            Proveedor obj = new Proveedor()
            {
                IdProveedor = idProveedorSeleccionado,
                Nombre = txtNombre.Text,
                Documento = txtDocumento.Text,
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text,
                Estado = Convert.ToInt32(cboEstado.SelectedValue) == 1

            };

            int idGenerado = objCN_Proveedor.Guardar(obj, out mensaje);

            if (idGenerado > 0)
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarProveedores();
                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje;
            int resultado = objCN_Proveedor.Eliminar(idProveedorSeleccionado, out mensaje);

            if (resultado > 0)
            {
                MessageBox.Show("Proveedor eliminado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ListarProveedores();
                Limpiar();
                dgvData.ClearSelection();  // *** IMPORTANTE ***
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                // 🔒 Validación 1: si es la fila nueva, ignorar
                if (indice < 0 || dgvData.Rows[indice].IsNewRow)
                {
                    MessageBox.Show(
                        "Seleccione una fila válida.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // 🔒 Validación 2: si la celda IdProveedor viene null
                if (dgvData.Rows[indice].Cells["IdProveedor"].Value == null)
                {
                    MessageBox.Show(
                        "La fila seleccionada no contiene datos.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // ===========================
                //  AQUI VA TU CÓDIGO NORMAL
                // ===========================

                idProveedorSeleccionado = Convert.ToInt32(dgvData.Rows[indice].Cells["IdProveedor"].Value);
                txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                txtDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();
                txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();

                bool estado = Convert.ToBoolean(dgvData.Rows[indice].Cells["EstadoValor"].Value);
                cboEstado.SelectedValue = estado ? 1 : 0;

                indiceSeleccionado = indice;
            }
        }


        private void Limpiar()
        {
            txtNombre.Text = "";
            txtDocumento.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            cboEstado.SelectedIndex = 0;

            idProveedorSeleccionado = 0;
            indiceSeleccionado = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = cboBusqueda.SelectedValue.ToString();
            string textoFiltro = txtBusqueda.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(textoFiltro))
                return;

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.IsNewRow)
                    continue;

                object valor = row.Cells[columnaFiltro].Value;
                string textoCelda = valor == null ? "" : valor.ToString().Trim().ToUpper();

                // FILTRO ESPECIAL PARA ESTADO
                if (columnaFiltro == "Estado")
                {
                    bool coincide =
                        (textoFiltro.StartsWith("A") && textoCelda == "ACTIVO") ||
                        (textoFiltro.StartsWith("I") && textoCelda == "INACTIVO");

                    row.Visible = coincide;
                    continue;
                }

                // FILTRO NORMAL
                row.Visible = textoCelda.Contains(textoFiltro);
            }
        }







        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
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
