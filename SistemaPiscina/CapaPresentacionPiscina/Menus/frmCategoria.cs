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
    public partial class frmCategoria : Form
    {
        private int idCategoriaSeleccionada = 0;

        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            // Cargar combo de estado (panel izquierdo)
            cboEstado.Items.Add(new { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new { Valor = 0, Texto = "Inactivo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;


            // Cargar combo de búsqueda
            cboBusqueda.Items.Add(new { Valor = "Descripcion", Texto = "Descripción" });
            cboBusqueda.Items.Add(new { Valor = "EstadoValor1", Texto = "Estado" });
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;


            // Cargar el listado
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            dgvCategoria.Rows.Clear();

            List<Categoria> lista = new CN_Categoria().Listar();

            foreach (Categoria item in lista)
            {
                dgvCategoria.Rows.Add(
                    "", // botón seleccionar
                    item.IdCategoria,
                    item.Descripcion,
                    item.EstadoValor,
                    item.Estado
                );
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = (cboBusqueda.SelectedItem as dynamic).Valor;
            string textoFiltro = txtBusqueda.Text.Trim().ToUpper();

            foreach (DataGridViewRow row in dgvCategoria.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string valorCelda = (row.Cells[columnaFiltro].Value ?? "").ToString().ToUpper();

                bool visible;

                if (string.IsNullOrEmpty(textoFiltro))
                {
                    // Si no hay texto, mostramos todo
                    visible = true;
                }
                else if (columnaFiltro == "EstadoValor1")     // Estado
                {
                    // Que EMPIECE por lo escrito (A = Activo, I = Inactivo)
                    visible = valorCelda.StartsWith(textoFiltro);
                }
                else
                {
                    // Para Descripcion usamos contiene
                    visible = valorCelda.Contains(textoFiltro);
                }

                row.Visible = visible;
            }
        }





        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvCategoria.Rows)
                row.Visible = true;
        }


        private void dgvCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategoria.Columns[e.ColumnIndex].Name == "btnSeleccionarCat")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    idCategoriaSeleccionada = Convert.ToInt32(dgvCategoria.Rows[indice].Cells["IdCategoria"].Value);

                    txtDescripcion.Text = dgvCategoria.Rows[indice].Cells["Descripcion"].Value.ToString();

                    // Estado
                    bool estado = Convert.ToBoolean(dgvCategoria.Rows[indice].Cells["Estado1"].Value);

                    // Seleccionar el estado en el combo (Activo = 1, Inactivo = 0)
                    foreach (var item in cboEstado.Items)
                    {
                        if ((item as dynamic).Valor == (estado ? 1 : 0))
                        {
                            cboEstado.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // VALIDACIÓN
            if (txtDescripcion.Text.Trim() == "")
            {
                MessageBox.Show("Debe completar la descripción.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int estado = (cboEstado.SelectedItem as dynamic).Valor;

            Categoria obj = new Categoria()
            {
                IdCategoria = idCategoriaSeleccionada,
                Descripcion = txtDescripcion.Text.Trim(),
                Estado = estado == 1 ? true : false
            };

            if (idCategoriaSeleccionada == 0)
            {
                // REGISTRAR
                int idGenerado = new CN_Categoria().Registrar(obj, out mensaje);

                if (idGenerado != 0)
                {
                    dgvCategoria.Rows.Add(
                        "",
                        idGenerado,
                        obj.Descripcion,
                        obj.Estado ? "Activo" : "Inactivo",
                        obj.Estado
                    );

                    MessageBox.Show("Categoría registrada correctamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                // EDITAR
                bool resultado = new CN_Categoria().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvCategoria.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => Convert.ToInt32(r.Cells["IdCategoria"].Value) == idCategoriaSeleccionada)
                        .FirstOrDefault();

                    if (row != null)
                    {
                        row.Cells["Descripcion"].Value = obj.Descripcion;
                        row.Cells["EstadoValor1"].Value = obj.Estado ? "Activo" : "Inactivo";
                        row.Cells["Estado1"].Value = obj.Estado;
                    }

                    MessageBox.Show("Categoría actualizada correctamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtDescripcion.Text = "";
            cboEstado.SelectedIndex = 0;

            idCategoriaSeleccionada = 0;
            txtDescripcion.Focus();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCategoriaSeleccionada == 0)
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string mensaje = string.Empty;
            bool estadoActual = Convert.ToBoolean(
                dgvCategoria.Rows
                .Cast<DataGridViewRow>()
                .Where(r => Convert.ToInt32(r.Cells["IdCategoria"].Value) == idCategoriaSeleccionada)
                .FirstOrDefault()
                .Cells["Estado1"].Value
            );

            bool nuevoEstado = !estadoActual;

            bool respuesta = new CN_Categoria().CambiarEstado(idCategoriaSeleccionada, nuevoEstado, out mensaje);

            if (respuesta)
            {
                DataGridViewRow row = dgvCategoria.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => Convert.ToInt32(r.Cells["IdCategoria"].Value) == idCategoriaSeleccionada)
                    .FirstOrDefault();

                row.Cells["EstadoValor1"].Value = nuevoEstado ? "Activo" : "Inactivo";
                row.Cells["Estado1"].Value = nuevoEstado;

                if (nuevoEstado)
                    MessageBox.Show("La categoría ha sido activada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("La categoría ha sido desactivada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Limpiar();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}
