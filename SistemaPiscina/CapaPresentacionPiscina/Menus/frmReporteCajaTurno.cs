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

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmReporteCajaTurno : Form
    {
        private CN_ReporteCajaTurno oCN_Reporte = new CN_ReporteCajaTurno();

        public frmReporteCajaTurno()
        {
            InitializeComponent();
        }

        private void frmReporteCajaTurno_Load(object sender, EventArgs e)
        {
            dgvTurnos.AutoGenerateColumns = false;
            CargarComboCajeros();
            CargarTurnosIniciales();  // ← nuevo
        }

        private void CargarTurnosIniciales()
        {
            DateTime fechaDesde = new DateTime(2000, 1, 1);
            DateTime fechaHasta = DateTime.Now;

            List<EReporteCajaTurnoResumen> lista =
                oCN_Reporte.ListarResumen(fechaDesde, fechaHasta, 0); // 0 = Todos los cajeros

            dgvTurnos.DataSource = lista;
            txtTotalTurnos.Text = lista.Count.ToString();
        }

        private void CargarComboCajeros()
        {
            List<Usuario> listaCajeros = new CN_Usuario().Listar();

            // Solo cajeros
            listaCajeros = listaCajeros
                .Where(x => x.oRol.Descripcion.ToUpper() == "CAJERO")
                .ToList();

            cboCajero.Items.Clear();
            cboCajero.DisplayMember = "NombreCompleto";
            cboCajero.ValueMember = "IdUsuario";

            cboCajero.Items.Add(new { IdUsuario = 0, NombreCompleto = "Todos" });

            foreach (var user in listaCajeros)
                cboCajero.Items.Add(new { IdUsuario = user.IdUsuario, NombreCompleto = user.NombreCompleto });

            cboCajero.SelectedIndex = 0;
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int idUsuario = 0;

            if (cboCajero.SelectedItem != null)
            {
                dynamic item = cboCajero.SelectedItem;
                idUsuario = item.IdUsuario;
            }

            DateTime fechaDesde = dtpDesde.Value.Date;
            DateTime fechaHasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

            List<EReporteCajaTurnoResumen> lista =
                oCN_Reporte.ListarResumen(fechaDesde, fechaHasta, idUsuario);

            dgvTurnos.DataSource = lista;

            txtTotalTurnos.Text = lista.Count.ToString();
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cboCajero.SelectedIndex = 0;
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            dgvTurnos.DataSource = null;
            txtTotalTurnos.Text = "0";
        }

        private void dgvTurnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvTurnos.Columns["btnDetalle"].Index)
            {
                int idTurno = Convert.ToInt32(dgvTurnos.Rows[e.RowIndex].Cells["IdCajaTurno"].Value);

                frmDetalleCajaTurno modal = new frmDetalleCajaTurno(idTurno);
                modal.ShowDialog();
            }
        }
    }
}
