using CapaEntidadPiscina;
using CapaNegocioPiscina;
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
            Usuario usuario = oCN.Login(txtDocumento.Text.Trim(), txtClave.Text.Trim());

            if (usuario.IdUsuario != 0)
            {
                // ===========================
                // FLUJO ESPECIAL PARA CAJERO
                // ===========================
                if (usuario.oRol.Descripcion.ToUpper() == "CAJERO")
                {
                    CN_CajaTurno cajaCN = new CN_CajaTurno();
                    ECajaTurno caja = cajaCN.VerificarCajaAbierta(usuario.IdUsuario);

                    // Si NO tiene caja abierta → abrir modal
                    if (!caja.TieneCajaAbierta)
                    {
                        frmAbrirCaja frm = new frmAbrirCaja(usuario.IdUsuario);

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            // Caja abierta correctamente
                            frmInicioPiscina inicio = new frmInicioPiscina(
                                usuario.NombreCompleto,
                                usuario.IdUsuario
                            );

                            inicio.idCajaTurnoActual = frm.IdCajaTurnoGenerada;
                            inicio.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("La caja no se abrió. No puede ingresar al sistema.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        return;
                    }
                    // Si YA tiene caja abierta → usarla directamente
                    else
                    {
                        frmInicioPiscina inicio = new frmInicioPiscina(
                            usuario.NombreCompleto,
                            usuario.IdUsuario
                        );

                        inicio.idCajaTurnoActual = caja.IdCajaTurno;
                        inicio.Show();
                        this.Hide();
                        return;
                    }
                }

                // ===========================
                // FLUJO PARA ADMINISTRADOR
                // ===========================
                else
                {
                    frmInicioPiscina inicio = new frmInicioPiscina(
                        usuario.NombreCompleto,
                        usuario.IdUsuario
                    );

                    inicio.idCajaTurnoActual = 0; // admin no maneja caja
                    inicio.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Documento o clave incorrectos.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
