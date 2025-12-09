using CapaEntidadPiscina;
using CapaNegocioPiscina;
using System;
using System.Windows.Forms;

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmReporteGral : Form
    {
        public frmReporteGral()
        {
            InitializeComponent();
        }

        // ================================
        //  MÉTODO PRINCIPAL DEL REPORTE
        // ================================
        private void CargarResumen(DateTime desde, DateTime hasta)
        {
            try
            {
                CN_ReportesFinancieros cn = new CN_ReportesFinancieros();
                EResumenFinanciero r = cn.ObtenerResumenFinanciero(desde, hasta);

                if (r != null)
                {
                    // Asignación a tus labels
                    lblIngresosTotales.Text = r.IngresosTotales.ToString("0.00");
                    lblPerdidas.Text = r.PerdidasCortesias.ToString("0.00");
                    lblEgresosCompras.Text = r.EgresosCompras.ToString("0.00");
                    lblEgresosGastos.Text = r.EgresosGastos.ToString("0.00");

                    // Ganancia neta (calculada en la Entidad)
                    lblGananciaNeta.Text = r.GananciaNeta.ToString("0.00");
                }
                else
                {
                    // Si viene null, evitamos poner valores vacíos
                    lblIngresosTotales.Text = "0.00";
                    lblPerdidas.Text = "0.00";
                    lblEgresosCompras.Text = "0.00";
                    lblEgresosGastos.Text = "0.00";
                    lblGananciaNeta.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el resumen:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================
        //   LOAD DEL FORMULARIO
        // ================================
        private void frmReporteGral_Load(object sender, EventArgs e)
        {
            // Fecha visible al usuario (hoy)
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            // Fecha interna para cargar TODO el historial
            DateTime fechaDesdeInterna = new DateTime(2000, 1, 1);
            DateTime fechaHastaInterna = DateTime.Today;

            CargarResumen(fechaDesdeInterna, fechaHastaInterna);
        }

      
        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            CargarResumen(desde, hasta);
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            // Mostrar al usuario fechas actuales
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            // Recalcular todo el historial
            DateTime fechaDesdeInterna = new DateTime(2000, 1, 1);
            DateTime fechaHastaInterna = DateTime.Today;

            CargarResumen(fechaDesdeInterna, fechaHastaInterna);
        }
    }
}
