using CapaPresentacionPiscina.Menus;
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


        public frmInicioPiscina()
        {
            InitializeComponent();
            MessageBox.Show(lblUsuarioActual.Text);
            lblUsuarioActual.Text = "Usuario:";
        }

        public frmInicioPiscina(string usuarioNombre)
        {
            InitializeComponent();
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
            AbrirFormularioEnPanel(new frmCategoria());
            

        }
    }
}
