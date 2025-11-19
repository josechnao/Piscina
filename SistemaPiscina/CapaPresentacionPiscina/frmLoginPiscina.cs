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

namespace CapaPresentacionPiscina
{
    public partial class frmLoginPiscina : Form
    {
        public frmLoginPiscina()
        {
            InitializeComponent();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Closing(object sender, EventArgs e)
        {

        }
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Text.Trim() == "" || txtClave.Text.Trim() == "")
            {
                MessageBox.Show("Por favor ingrese su documento y clave.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CN_Usuario oCN = new CN_Usuario();
            Usuario oUsuario = oCN.Login(txtDocumento.Text.Trim(), txtClave.Text.Trim());

            if (oUsuario.IdUsuario != 0)
            {
                // Guardar usuario en sesión
                SesionUsuario.UsuarioActual = oUsuario;

                // Abrimos menú
                frmInicioPiscina inicio = new frmInicioPiscina(oUsuario.NombreCompleto);
                inicio.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Documento o clave incorrectos.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
