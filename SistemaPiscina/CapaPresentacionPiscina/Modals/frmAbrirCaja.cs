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

namespace CapaPresentacionPiscina.Modals
{
    public partial class frmAbrirCaja : Form
    {
        private int _idUsuario;
        public int IdCajaTurnoGenerada { get; set; }
        public frmAbrirCaja(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
        }

        private void frmAbrirCaja_Load(object sender, EventArgs e)
        {

        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            // Validación de monto
            if (!decimal.TryParse(txtMontoInicial.Text.Trim(), out decimal montoInicial))
            {
                MessageBox.Show("Ingrese un monto válido.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (montoInicial < 0)
            {
                MessageBox.Show("El monto inicial no puede ser negativo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Procesar apertura
            CN_CajaTurno objCN = new CN_CajaTurno();

            string mensaje = string.Empty;
            int idCaja = objCN.AbrirCaja(_idUsuario, montoInicial, out mensaje);

            if (idCaja > 0)
            {
                IdCajaTurnoGenerada = idCaja;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
