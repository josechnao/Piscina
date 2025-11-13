using CapaEntidadPiscina;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using CapaNegocioPiscina;

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmUsuarios : Form
    {
        private int idUsuarioSeleccionado = 0;

        public frmUsuarios()
        {
            InitializeComponent();
            dataGridView1.Columns["btnSeleccionar"].Width = 100;
            dataGridView1.Columns["Documento"].Width = 100;
            dataGridView1.Columns["NombreCompleto"].Width = 220;
            dataGridView1.Columns["IdRol"].Width = 170;
            dataGridView1.Columns["EstadoValor"].Width = 92;
            this.Load += frmUsuarios_Load;
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            CargarCombos();   // ya lo tienes hecho
            CargarUsuarios();  // nueva línea
        }

        private void CargarCombos()
        {
            // =======================
            // CARGAR ROLES DESDE BD
            // =======================
            List<Rol> listaRoles = new CN_Rol().Listar();

            cboRol.DataSource = listaRoles;
            cboRol.DisplayMember = "Descripcion";
            cboRol.ValueMember = "IdRol";
            cboRol.SelectedIndex = 0;


            // =======================
            // CARGAR ESTADO (ACTIVO/INACTIVO)
            // =======================
            DataTable tablaEstado = new DataTable();
            tablaEstado.Columns.Add("Valor", typeof(int));
            tablaEstado.Columns.Add("Texto", typeof(string));

            tablaEstado.Rows.Add(1, "Activo");
            tablaEstado.Rows.Add(0, "Inactivo");

            cboEstado.DataSource = tablaEstado;
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;  // evita null




            // =======================
            // CARGAR FILTRO DE BÚSQUEDA
            // =======================
            cboFiltroBusqueda.Items.Add("Documento");
            cboFiltroBusqueda.Items.Add("NombreCompleto");
            cboFiltroBusqueda.Items.Add("DescripcionRol");
            cboFiltroBusqueda.Items.Add("EstadoValor");
            cboFiltroBusqueda.SelectedIndex = 0;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = cboFiltroBusqueda.SelectedItem.ToString();
            string texto = txtBusqueda.Text.Trim().ToLower();

            if (texto == "")
            {
                MessageBox.Show("Ingrese un texto para buscar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow)
                    continue;

                object valor = row.Cells[columnaFiltro].Value;
                string textoCelda = valor == null ? "" : valor.ToString().ToLower();

                // Coincide si empieza por lo que escribiste
                bool coincide = textoCelda.StartsWith(texto);
                row.Visible = coincide;
            }


        }



        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cboFiltroBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CargarUsuarios()
        {
            dataGridView1.Rows.Clear();

            List<Usuario> lista = new CN_Usuario().Listar();

            foreach (Usuario u in lista)
            {
                dataGridView1.Rows.Add(
                    "",                 // 0 - Seleccionar (vacío por ahora)
                    u.IdUsuario,        // 1 - IdUsuario (oculto)
                    u.Documento,        // 2 - Documento
                    u.NombreCompleto,   // 3 - Nombre Completo
                    u.Clave,            // 4 - Clave (oculto)
                    u.IdRol,            // 5 - IdRol (oculto)
                    u.RolDescripcion,   // 6 - Rol (visible: "Administrador", etc.)
                    u.Estado,           // 7 - Estado (bool, oculto)
                    u.EstadoTexto       // 8 - EstadoValor (visible: "Activo"/"Inactivo")
                );
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que se haya hecho clic en la columna Seleccionar
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnSeleccionar" && e.RowIndex >= 0)
            {
                idUsuarioSeleccionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["IdUsuario"].Value);

                txtDocumento.Text = dataGridView1.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
                txtNombreCompleto.Text = dataGridView1.Rows[e.RowIndex].Cells["NombreCompleto"].Value.ToString();
                txtClave.Text = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                txtConfirmarClave.Text = dataGridView1.Rows[e.RowIndex].Cells["Clave"].Value.ToString();
                cboRol.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["IdRol"].Value;
                cboEstado.SelectedValue = (dataGridView1.Rows[e.RowIndex].Cells["EstadoValor"].Value.ToString() == "Activo") ? 1 : 0;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (txtDocumento.Text.Trim() == "" ||
                txtNombreCompleto.Text.Trim() == "" ||
                txtClave.Text.Trim() == "" ||
                txtConfirmarClave.Text.Trim() == "")
            {
                MessageBox.Show("Completa todos los campos.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtClave.Text != txtConfirmarClave.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear objeto Usuario
            Usuario u = new Usuario()
            {
                IdUsuario = idUsuarioSeleccionado,    // SI ES 0 → INSERTA / SI ES >0 → EDITA
                Documento = txtDocumento.Text.Trim(),
                NombreCompleto = txtNombreCompleto.Text.Trim(),
                Clave = txtClave.Text.Trim(),
                IdRol = Convert.ToInt32(cboRol.SelectedValue),
                Estado = (cboEstado.SelectedValue.ToString() == "1")

            };

            int idGenerado = new CN_Usuario().Guardar(u);

            if (idGenerado > 0)
            {
                MessageBox.Show("Usuario guardado correctamente.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarUsuarios();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo guardar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            idUsuarioSeleccionado = 0;

            txtDocumento.Clear();
            txtNombreCompleto.Clear();
            txtClave.Clear();
            txtConfirmarClave.Clear();

            if (cboRol.Items.Count > 0) cboRol.SelectedIndex = 0;
            if (cboEstado.Items.Count > 0) cboEstado.SelectedIndex = 0;
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si hay un usuario seleccionado
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la lista primero.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmar eliminación
            if (MessageBox.Show("¿Seguro que desea eliminar este usuario?",
                                "Confirmar",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool eliminado = new CN_Usuario().Eliminar(idUsuarioSeleccionado);

                if (eliminado)
                {
                    MessageBox.Show("Usuario eliminado correctamente.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarUsuarios();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el usuario.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = true;
            }
            cboFiltroBusqueda.SelectedIndex = 0;
        }


    }
}
