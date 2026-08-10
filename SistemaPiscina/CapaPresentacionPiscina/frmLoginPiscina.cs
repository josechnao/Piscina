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

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            // =========================================
            // 1. VALIDACIONES
            // =========================================
            string documento = txtDocumento.Text.Trim();
            string clave = txtClave.Text.Trim();

            if (string.IsNullOrWhiteSpace(documento) ||
                string.IsNullOrWhiteSpace(clave))
            {
                MessageBox.Show(
                    "Por favor ingrese su documento y clave.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Evita doble clic mientras procesa
            btnIngresar.Enabled = false;

            // Visualmente indica que está trabajando
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // =========================================
                // 2. LOGIN SIN BLOQUEAR LA INTERFAZ
                // =========================================
                CN_Usuario oCN = new CN_Usuario();

                Usuario usuario = await oCN.LoginAsync(
                    documento,
                    clave
                );

                // =========================================
                // 3. CREDENCIALES INCORRECTAS
                // =========================================
                if (usuario == null || usuario.IdUsuario == 0)
                {
                    MessageBox.Show(
                        "Documento o clave incorrectos.",
                        "Inicio de sesión",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtClave.Clear();
                    txtClave.Focus();

                    return;
                }

                // =========================================
                // 4. VALIDAR ROL
                // =========================================
                if (usuario.oRol == null ||
                    string.IsNullOrWhiteSpace(usuario.oRol.Descripcion))
                {
                    MessageBox.Show(
                        "El usuario no tiene un rol válido asignado.",
                        "Error de usuario",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                // Guardamos usuario logueado
                SesionUsuario.UsuarioActual = usuario;

                string rol = usuario.oRol.Descripcion.Trim().ToUpper();

                // =========================================
                // 5. FLUJO PARA CAJERO
                // =========================================
                if (rol == "CAJERO")
                {
                    CN_CajaTurno cajaCN = new CN_CajaTurno();

                    // También hacemos esta consulta fuera del hilo visual
                    ECajaTurno caja = await Task.Run(() =>
                        cajaCN.VerificarCajaAbierta(usuario.IdUsuario)
                    );

                    // No tiene caja abierta
                    if (!caja.TieneCajaAbierta)
                    {
                        frmAbrirCaja frm = new frmAbrirCaja(usuario.IdUsuario);

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            AbrirSistema(
                                usuario,
                                frm.IdCajaTurnoGenerada
                            );
                        }
                        else
                        {
                            SesionUsuario.UsuarioActual = null;

                            MessageBox.Show(
                                "La caja no se abrió. No puede ingresar al sistema.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }

                        return;
                    }

                    // Ya tiene caja abierta
                    AbrirSistema(
                        usuario,
                        caja.IdCajaTurno
                    );

                    return;
                }

                // =========================================
                // 6. FLUJO ADMIN / OTROS ROLES
                // =========================================
                AbrirSistema(
                    usuario,
                    null
                );
            }
            catch (Exception ex)
            {
                // =========================================
                // 7. ERROR REAL DEL SISTEMA / SQL
                // =========================================
                MessageBox.Show(
                    "No se pudo iniciar sesión.\n\n" +
                    "Detalle: " + ex.Message,
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                // =========================================
                // 8. RESTAURAR INTERFAZ
                // =========================================
                Cursor.Current = Cursors.Default;
                btnIngresar.Enabled = true;
            }
        }

        private void AbrirSistema(Usuario usuario, int? idCajaTurno)
        {
            frmInicioPiscina inicio = new frmInicioPiscina(
                usuario.NombreCompleto,
                usuario.IdUsuario,
                usuario.oRol.Descripcion
            );

            inicio.rolActual = usuario.oRol.Descripcion;
            inicio.idCajaTurnoActual = idCajaTurno;

            // Cuando se cierre la ventana principal,
            // decidimos si volver al Login o cerrar toda la aplicación.
            inicio.FormClosed += (s, e) =>
            {
                if (inicio.CerrarSesionSolicitada)
                {
                    // El usuario eligió "Cerrar sesión"
                    SesionUsuario.UsuarioActual = null;

                    // Limpiamos datos sensibles
                    txtClave.Clear();

                    // Puedes dejar el documento escrito para comodidad.
                    // Si prefieres limpiarlo también:
                    // txtDocumento.Clear();

                    this.Show();
                    this.Activate();

                    txtDocumento.Focus();
                }
                else
                {
                    // Si cerraron frmInicio con la X,
                    // cerramos también el Login original.
                    // Como Program.cs ejecuta este Login,
                    // esto termina correctamente la aplicación.
                    this.Close();
                }
            };

            inicio.Show();

            // Ocultamos el MISMO login.
            // No crearemos uno nuevo después.
            this.Hide();
        }

        private void frmLoginPiscina_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                System.Diagnostics.Debug.WriteLine(
                    "LOGIN SE HIZO VISIBLE: " + DateTime.Now.ToString("HH:mm:ss")
                );
            }
        }
    }
}
