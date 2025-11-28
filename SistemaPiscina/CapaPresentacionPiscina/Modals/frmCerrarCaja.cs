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

namespace CapaPresentacionPiscina.Modals
{
    public partial class frmCerrarCaja : Form
    {
        private int _idUsuario;
        private int _idCajaTurno;

        public frmCerrarCaja(int idUsuario, int idCajaTurno)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            _idCajaTurno = idCajaTurno;
        }

        private void frmCerrarCaja_Load(object sender, EventArgs e)
        {
            CN_CajaTurno cn = new CN_CajaTurno();
            ECajaTurno caja = cn.ObtenerCajaActiva(_idUsuario);

            if (caja == null || caja.IdCajaTurno == 0)
            {
                MessageBox.Show("No se encontró una caja activa.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            // Guardamos IdCajaTurno real (por si vino de login)
            _idCajaTurno = caja.IdCajaTurno;

            decimal montoInicial = caja.MontoInicial;
            decimal totalVentas = caja.TotalVentas ?? 0;
            decimal totalGastos = caja.TotalGastos ?? 0;

            // Calcular total sistema
            decimal totalSistema = montoInicial + totalVentas - totalGastos;

            txtTotalSistema.Text = totalSistema.ToString("0.00");
            txtDiferencia.Text = "0.00";  // inicia en cero
        }


        private void txtMontoFinal_TextChanged(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMontoFinal.Text.Trim(), out decimal montoFinal))
            {
                txtDiferencia.Text = "0.00";
                return;
            }

            decimal totalSistema = decimal.Parse(txtTotalSistema.Text);
            decimal diferencia = montoFinal - totalSistema;

            txtDiferencia.Text = diferencia.ToString("0.00");
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMontoFinal.Text.Trim(), out decimal montoFinal))
            {
                MessageBox.Show("Ingrese un monto final válido.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (montoFinal < 0)
            {
                MessageBox.Show("El monto final no puede ser negativo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string observacion = txtObservacion.Text.Trim();
            CN_CajaTurno cn = new CN_CajaTurno();

            string mensaje = cn.CerrarCaja(_idCajaTurno, montoFinal, observacion);

            if (mensaje == "Caja cerrada correctamente.")
            {
                MessageBox.Show("Caja cerrada exitosamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
