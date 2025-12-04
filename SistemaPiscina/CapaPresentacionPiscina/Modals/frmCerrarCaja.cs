using CapaEntidadPiscina;
using CapaNegocioPiscina;
using System;
using System.Windows.Forms;

namespace CapaPresentacionPiscina.Modals
{
    public partial class frmCerrarCaja : Form
    {
        private int _idUsuario;
        private int _idCajaTurno;

        private decimal montoInicial = 0;
        private decimal totalVentas = 0;
        private decimal totalGastos = 0;
        private decimal totalSistema = 0;

        public frmCerrarCaja(int idUsuario, int idCajaTurno)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            _idCajaTurno = idCajaTurno;
        }

        private void frmCerrarCaja_Load(object sender, EventArgs e)
        {
            CN_CajaTurno cajaCN = new CN_CajaTurno();

            // Traemos MontoInicial, Ventas y Gastos del turno
            var resumen = cajaCN.ObtenerResumen(_idCajaTurno);

            montoInicial = resumen.MontoInicial;
            totalVentas = resumen.TotalVentas;
            totalGastos = resumen.TotalGastos;

            // 💰 Fórmula correcta:
            // TotalSistema = MontoInicial + Ventas - Gastos
            totalSistema = montoInicial + totalVentas - totalGastos;

            txtTotalSistema.Text = totalSistema.ToString("0.00");

            txtTotalSistema.ReadOnly = true;
            txtDiferencia.ReadOnly = true;

            txtMontoFinal.Focus();
        }

        private void txtMontoFinal_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMontoFinal.Text.Trim(), out decimal contado))
            {
                decimal diferencia = contado - totalSistema;
                txtDiferencia.Text = diferencia.ToString("0.00");
            }
            else
            {
                txtDiferencia.Text = "0.00";
            }
        }

        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMontoFinal.Text.Trim(), out decimal montoFinal))
            {
                MessageBox.Show("Ingrese un monto final válido.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal diferencia = montoFinal - totalSistema;

            ECajaTurno obj = new ECajaTurno()
            {
                IdCajaTurno = _idCajaTurno,
                MontoFinal = montoFinal,
                TotalVentas = totalVentas,
                TotalGastos = totalGastos,
                Diferencia = diferencia,
                Observacion = txtObservacion.Text.Trim()
            };

            CN_CajaTurno cajaCN = new CN_CajaTurno();
            string mensaje;

            bool ok = cajaCN.CerrarCaja(obj, out mensaje);

            if (ok)
            {
                MessageBox.Show("Caja cerrada correctamente.",
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo cerrar la caja.\n" + mensaje,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
