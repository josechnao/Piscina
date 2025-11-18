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
using CapaEntidadPiscina;

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmCompras : Form
    {
        public frmCompras()
        {
            InitializeComponent();
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new frmModalProveedor())
            {
                var resultado = modal.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    // Recibir los datos seleccionados desde el modal
                    txtDocumentoProveedor.Text = modal.ProveedorSeleccionado.Documento;
                    txtNombreProveedor.Text = modal.ProveedorSeleccionado.Nombre;
                }
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {

        }
    }
}
