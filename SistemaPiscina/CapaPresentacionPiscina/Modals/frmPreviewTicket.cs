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
    public partial class frmPreviewTicket : Form
    {
        public string TicketTexto { get; set; }
        public frmPreviewTicket()
        {
            InitializeComponent();
        }

        private void frmPreviewTicket_Load(object sender, EventArgs e)
        {
            rtbTicketPreview.Text = TicketTexto;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
