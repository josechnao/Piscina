using CapaNegocioPiscina;
using System;
using System.Windows.Forms;

namespace CapaPresentacionPiscina.Modals
{
    public partial class frmAbrirCaja : Form
    {
        private int _idUsuario;

        public int IdCajaTurnoGenerada { get; private set; } = 0;

        public frmAbrirCaja(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
        }

        private void frmAbrirCaja_Load(object sender, EventArgs e)
        {
            txtMontoInicial.Select();
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            // Validar monto
            if (!decimal.TryParse(txtMontoInicial.Text.Trim(), out decimal montoInicial))
            {
                MessageBox.Show("Ingrese un monto válido.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (montoInicial < 0)
            {
                MessageBox.Show("El monto inicial no puede ser negativo.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Llamar a la capa de negocio
            CN_CajaTurno cajaCN = new CN_CajaTurno();
            string mensaje = "";

            int idGenerado = cajaCN.AbrirCaja(_idUsuario, montoInicial, out mensaje);

            if (idGenerado > 0)
            {
                IdCajaTurnoGenerada = idGenerado;

                MessageBox.Show("Caja abierta correctamente.", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo abrir la caja.\n" + mensaje, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
