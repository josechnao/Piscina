using CapaEntidadPiscina;
using CapaNegocioPiscina;
using CapaPresentacionPiscina.Menus;
using CapaPresentacionPiscina.Modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacionPiscina
{
    public partial class frmInicioPiscina : Form
    {
        private Form formularioActivo = null;
        public int idCajaTurnoActual { get; set; }
        public string rolActual { get; set; }
        public int usuarioActual { get; set; }


        public frmInicioPiscina(string usuarioNombre, int idUsuario, string rol)
        {
            InitializeComponent();

            this.usuarioActual = idUsuario;
            this.rolActual = rol;             // ← YA LLEGA CORRECTO
            this.idCajaTurnoActual = 0;

            lblUsuarioActual.Text = "Usuario: " + usuarioNombre;
        }

        public frmInicioPiscina(string usuarioNombre, int idUsuario)
        {
            InitializeComponent();
            this.usuarioActual = idUsuario;
            this.rolActual = "";
            this.idCajaTurnoActual = 0;
            lblUsuarioActual.Text = "Usuario: " + usuarioNombre;
        }





        private void AbrirFormularioEnPanel(Form formHijo)
        {
            if (formularioActivo != null)
                formularioActivo.Close();

            formularioActivo = formHijo;
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;

            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(formHijo);
            panelContenedor.Tag = formHijo;

            formHijo.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            OcultarSubMenus();
            AbrirFormularioEnPanel(new frmUsuarios());
        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            OcultarSubMenus();
            MostrarSubMenu(pnlSubMenuProductos);
        }


        private void btnProveedores_Click(object sender, EventArgs e)
        {
            OcultarSubMenus();
            AbrirFormularioEnPanel(new frmProveedores());
        }
        private void MostrarSubMenu(Panel subMenu)
        {
            subMenu.Visible = !subMenu.Visible;
        }


        private void OcultarSubMenus()
        {
            pnlSubMenuProductos.Visible = false;
            // En el futuro agregas más submenús aquí
        }


        private void btnRegistrarProducto_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmProductos());
            
            

        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmCategoria());
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmCompras());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmVentas());
        }


        private void btnEntradasPromo_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmEntradaPromo());
        }

        private void btnMantenedor_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new frmMantenedor());
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Si no hay caja abierta → cerrar sesión directamente
            if (idCajaTurnoActual == 0)
            {
                DialogResult r = MessageBox.Show("¿Desea cerrar sesión?", "Confirmación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    this.Hide();
                    new frmLoginPiscina().Show();
                }

                return;
            }

            // Si hay caja abierta → mostrar el modal de cierre de caja
            frmCerrarCaja frm = new frmCerrarCaja(usuarioActual, idCajaTurnoActual);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Caja cerrada correctamente
                idCajaTurnoActual = 0; // Se limpia porque ya no existe caja abierta

                MessageBox.Show("Sesión cerrada.", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                new frmLoginPiscina().Show();
            }
            else
            {
                // El usuario canceló el cierre de caja
                MessageBox.Show("Cierre de caja cancelado. La sesión continúa activa.",
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
                        AbrirFormularioEnPanel(
                new frmGastos(this.rolActual, this.idCajaTurnoActual, this.usuarioActual)
            );
        }

    }
}
