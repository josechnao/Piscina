using CapaEntidad;
using CapaNegocio;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmMantenedor : Form
    {
        private Negocio _datosNegocio;
        private byte[] _logoEnBytes;  // Para almacenar la imagen actual o nueva

        public frmMantenedor()
        {
            InitializeComponent();
        }

        private void frmMantenedor_Load(object sender, EventArgs e)
        {
            CargarDatos();
            BloquearCampos();
        }

        // ================================================
        // Cargar datos del negocio
        // ================================================
        private void CargarDatos()
        {
            _datosNegocio = new CN_Negocio().ObtenerDatos();

            if (_datosNegocio != null)
            {
                txtNombreNegocio.Text = _datosNegocio.NombreNegocio;
                txtDireccion.Text = _datosNegocio.Direccion;
                txtCiudad.Text = _datosNegocio.Ciudad;
                txtTelefono.Text = _datosNegocio.Telefono;

                if (_datosNegocio.Logo != null)
                {
                    _logoEnBytes = _datosNegocio.Logo;
                    picLogo.Image = ByteArrayToImage(_logoEnBytes);
                }
                else
                {
                    _logoEnBytes = null;
                    picLogo.Image = null;
                }
            }
        }

        // ================================================
        // Convertir byte[] a Image
        // ================================================
        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        // ================================================
        // Convertir Image a byte[]
        // ================================================
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        // ================================================
        // Bloquear campos por defecto
        // ================================================
        private void BloquearCampos()
        {
            txtNombreNegocio.ReadOnly = true;
            txtDireccion.ReadOnly = true;
            txtCiudad.ReadOnly = true;
            txtTelefono.ReadOnly = true;

            btnSubirLogo.Enabled = false;
            btnGuardar.Enabled = false;
        }

        // ================================================
        // Habilitar edición
        // ================================================
        private void HabilitarCampos()
        {
            txtNombreNegocio.ReadOnly = false;
            txtDireccion.ReadOnly = false;
            txtCiudad.ReadOnly = false;
            txtTelefono.ReadOnly = false;

            btnSubirLogo.Enabled = true;
            btnGuardar.Enabled = true;
        }

        // ================================================
        // Botón: Editar
        // ================================================
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            HabilitarCampos();
            btnSubirLogo.Enabled = true;
            btnGuardar.Enabled = true;
        }

        // ================================================
        // Botón: Subir imagen
        // ================================================
        private void btnSubirLogo_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imagenes|*.jpg;*.jpeg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(ofd.FileName);
                picLogo.Image = img;
                _logoEnBytes = ImageToByteArray(img);
            }
        }

        // ================================================
        // Botón: Guardar cambios
        // ================================================

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Negocio obj = new Negocio()
            {
                IdNegocio = 1,
                NombreNegocio = txtNombreNegocio.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Logo = _logoEnBytes
            };

            bool respuesta = new CN_Negocio().GuardarDatos(obj, out mensaje);

            if (respuesta)
            {
                MessageBox.Show("Datos actualizados correctamente", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                BloquearCampos();   // Volvemos a bloquear
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}
