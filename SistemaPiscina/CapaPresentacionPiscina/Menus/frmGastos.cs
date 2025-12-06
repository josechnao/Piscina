using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CapaEntidadPiscina;
using CapaNegocioPiscina;

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmGastos : Form
    {
        // ============================================
        //  CAMPOS
        // ============================================
        private int idGasto = 0;              // Para saber si guardamos o editamos
        private int? idCajaTurnoActual = null;
        private string rolUsuario = "";       // Admin o Cajero
        private int usuarioActual = 0;

        // ============================================
        //  CONSTRUCTOR
        // ============================================
        public frmGastos(string rol, int? idCajaTurno, int idUsuario)
        {
            InitializeComponent();
            rolUsuario = rol;
            idCajaTurnoActual = idCajaTurno;
            usuarioActual = idUsuario;
        }


        // Helper para no repetir comparaciones
        private bool EsCajero()
        {
            return !string.IsNullOrEmpty(rolUsuario) &&
                   rolUsuario.Trim().ToUpper() == "CAJERO";
        }

        // ============================================
        //  LOAD
        // ============================================
        private void frmGastos_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarRolesFiltro();
            CargarGastos();

            // El cajero NO ve el panel de filtros
            if (EsCajero())
            {
                pnlFiltro.Visible = false;
            }
            else
            {
                pnlFiltro.Visible = true;
            }
        }

        // ============================================
        //  CARGAR COMBOS
        // ============================================
        private void CargarCategorias()
        {
            List<ECategoriaGasto> lista = new CN_CategoriaGasto().Listar();

            // Combo de REGISTRO
            cboCategoria.DataSource = null;
            cboCategoria.DataSource = lista;
            cboCategoria.DisplayMember = "Descripcion";
            cboCategoria.ValueMember = "IdCategoriaGasto";

            // Clonamos la lista para el filtro
            List<ECategoriaGasto> listaFiltro = new List<ECategoriaGasto>(lista);

            // Agregar opción "Todos" al filtro
            listaFiltro.Insert(0, new ECategoriaGasto
            {
                IdCategoriaGasto = 0,
                Descripcion = "Todos"
            });

            cboFiltroCategoria.DataSource = null;
            cboFiltroCategoria.DataSource = listaFiltro;
            cboFiltroCategoria.DisplayMember = "Descripcion";
            cboFiltroCategoria.ValueMember = "IdCategoriaGasto";
        }

        private void CargarRolesFiltro()
        {
            List<Rol> listaRol = new CN_Rol().Listar();

            // Opción "Todos"
            listaRol.Insert(0, new Rol { IdRol = 0, Descripcion = "Todos" });

            cboFiltroRol.DataSource = null;
            cboFiltroRol.DataSource = listaRol;
            cboFiltroRol.DisplayMember = "Descripcion";
            cboFiltroRol.ValueMember = "IdRol";
        }

        // ============================================
        //  GUARDAR / EDITAR
        // ============================================
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // VALIDACIONES

            if (Convert.ToInt32(cboCategoria.SelectedValue) == 0)
            {
                MessageBox.Show("Debe seleccionar una categoría.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar una descripción.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Debe ingresar un monto válido mayor a 0.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // CREAR OBJETO
            EGasto obj = new EGasto()
            {
                IdGasto = idGasto,
                IdCategoriaGasto = Convert.ToInt32(cboCategoria.SelectedValue),
                Descripcion = txtDescripcion.Text.Trim(),
                Monto = monto,
                IdUsuario = usuarioActual,
                IdCajaTurno = EsCajero() ? (int?)idCajaTurnoActual : null
            };

            if (idGasto == 0)
            {
                // REGISTRAR
                int idGenerado = new CN_Gasto().Registrar(obj, out mensaje);

                if (idGenerado > 0)
                {
                    MessageBox.Show("Gasto registrado correctamente.", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarGastos();
                }
                else
                {
                    MessageBox.Show(mensaje, "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // EDITAR
                bool ok = new CN_Gasto().Editar(obj, out mensaje);

                if (ok)
                {
                    MessageBox.Show("Gasto actualizado correctamente.", "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarGastos();
                }
                else
                {
                    MessageBox.Show(mensaje, "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Limpiar()
        {
            idGasto = 0;
            cboCategoria.SelectedIndex = 0;
            txtDescripcion.Text = "";
            txtMonto.Text = "";
            btnGuardar.Text = "Guardar";
        }

        // =============================
        //  CARGAR DGV (ADMIN / CAJERO)
        // =============================
        private void CargarGastos()
        {
            dgvGastos.Rows.Clear();

            List<EGasto> lista;

            if (EsCajero())
            {
                if (!idCajaTurnoActual.HasValue)
                {
                    MessageBox.Show("No hay una caja abierta para este cajero.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lista = new CN_Gasto().ListarCajero(idCajaTurnoActual.Value);
            }
            else
            {
                // Admin: ve TODOS los gastos
                lista = new CN_Gasto().ListarAdmin();
            }


            foreach (var item in lista)
            {
                dgvGastos.Rows.Add(
                    item.IdGasto,                 // 0 - IdGastos (oculta)
                    "",                           // 1 - btnSeleccionar
                    item.CategoriaDescripcion,    // 2 - Categoria
                    item.Descripcion,             // 3 - Descripcion
                    item.Monto.ToString("0.00"),  // 4 - Monto
                    item.UsuarioNombre,           // 5 - UsuarioNombre
                    item.RolDescripcion,          // 6 - RolDescripcion
                    item.FechaRegistro.ToString("yyyy-MM-dd HH:mm"), // 7
                    item.Estado ? "Activo" : "Inactivo",             // 8
                    item.IdCategoriaGasto,        // 9 - IdCategoriaGasto (oculta)
                    item.IdUsuario,               // 10 - IdUsuario (oculta)
                    item.IdCajaTurno              // 11 - IdCajaTurno (oculta)
                );
            }
        }





        // ============================================
        //  DGV: ESTADO / SELECCIONAR
        // ============================================
        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string nombreColumna = dgvGastos.Columns[e.ColumnIndex].Name;

            // 1) CAMBIAR ESTADO
            if (nombreColumna == "btnEstado")
            {
                int idGasto = Convert.ToInt32(
                    dgvGastos.Rows[e.RowIndex].Cells["IdGastos"].Value
                );

                bool estadoActual = Convert.ToBoolean(
                    dgvGastos.Rows[e.RowIndex].Cells["Estado"].Value
                );

                string pregunta = estadoActual
                    ? "¿Está seguro de INACTIVAR este gasto?"
                    : "¿Está seguro de ACTIVAR este gasto?";

                if (MessageBox.Show(pregunta, "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool nuevoEstado = !estadoActual;
                    string mensaje = "";

                    bool respuesta = new CN_Gasto().CambiarEstado(idGasto, nuevoEstado, out mensaje);

                    if (respuesta)
                    {
                        dgvGastos.Rows[e.RowIndex].Cells["Estado"].Value = nuevoEstado;
                        dgvGastos.Rows[e.RowIndex].Cells["btnEstado"].Value =
                            nuevoEstado ? "Activo" : "Inactivo";
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                return;
            }

            // 2) SELECCIONAR PARA EDITAR
            if (nombreColumna == "btnSeleccionar")
            {
                // Evitar la fila nueva (*)
                if (dgvGastos.Rows[e.RowIndex].IsNewRow)
                {
                    MessageBox.Show("Seleccione una fila válida.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar que IdGasto exista
                var celdaId = dgvGastos.Rows[e.RowIndex].Cells["IdGastos"].Value;

                if (celdaId == null || celdaId == DBNull.Value || celdaId.ToString() == "")
                {
                    MessageBox.Show("Seleccione una fila válida.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                idGasto = Convert.ToInt32(celdaId);

                cboCategoria.SelectedValue =
                    Convert.ToInt32(dgvGastos.Rows[e.RowIndex].Cells["IdCategoriaGasto"].Value);

                txtDescripcion.Text =
                    dgvGastos.Rows[e.RowIndex].Cells["Descripcion"].Value.ToString();

                txtMonto.Text =
                    dgvGastos.Rows[e.RowIndex].Cells["Monto"].Value.ToString();

                btnGuardar.Text = "Actualizar";
                return;
            }



        }

        // ============================================
        //  BOTÓN BUSCAR
        // ============================================
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvGastos.Rows.Clear();

            List<EGasto> lista;

            // Cargar datos base
            if (EsCajero())
            {
                if (!idCajaTurnoActual.HasValue)
                {
                    MessageBox.Show("No hay una caja abierta para este cajero.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                lista = new CN_Gasto().ListarCajero(idCajaTurnoActual.Value);
            }
            else
            {
                lista = new CN_Gasto().ListarAdmin();
            }

            // 1) FILTRO DESCRIPCIÓN
            string texto = txtBuscar.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(texto))
                lista = lista.Where(x => x.Descripcion.ToLower().Contains(texto)).ToList();

            // 2) FILTRO CATEGORÍA
            int cat = Convert.ToInt32(cboFiltroCategoria.SelectedValue);
            if (cat != 0)
                lista = lista.Where(x => x.IdCategoriaGasto == cat).ToList();

            // 3) FILTRO ROL POR ID
            if (!EsCajero())
            {
                int idRolSeleccionado = Convert.ToInt32(cboFiltroRol.SelectedValue);

                if (idRolSeleccionado != 0) // 0 = "Todos"
                {
                    lista = lista.Where(x => x.IdRol == idRolSeleccionado).ToList();
                }
            }

            // 4) FILTRO FECHAS
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

            lista = lista.Where(x =>
                x.FechaRegistro >= desde &&
                x.FechaRegistro <= hasta
            ).ToList();


            // CARGAR DGV
            foreach (var item in lista)
            {
                dgvGastos.Rows.Add(
                    item.IdGasto,
                    "",
                    item.CategoriaDescripcion,
                    item.Descripcion,
                    item.Monto.ToString("0.00"),
                    item.UsuarioNombre,
                    item.RolDescripcion,
                    item.FechaRegistro.ToString("yyyy-MM-dd HH:mm"),
                    item.Estado ? "Activo" : "Inactivo",
                    item.IdCategoriaGasto,
                    item.IdUsuario,
                    item.IdCajaTurno
                );
            }
        }




        // ============================================
        //  BOTÓN LIMPIAR BÚSQUEDA
        // ============================================
        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            cboFiltroCategoria.SelectedIndex = 0; // "Todos"
            cboFiltroRol.SelectedIndex = 0;       // "Todos"
            dtpDesde.Value = DateTime.Now.Date;
            dtpHasta.Value = DateTime.Now.Date;

            CargarGastos();
        }
    }
}
