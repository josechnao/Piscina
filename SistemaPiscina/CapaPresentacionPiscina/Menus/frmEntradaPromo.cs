using CapaEntidadPiscina;
using CapaNegocioPiscina;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmEntradaPromo : Form
    {
        EPromocion objPromoActual = null;
        CN_Promocion objCNPromo = new CN_Promocion();
        int usuarioActual = 1; // Cambia según tu login

        private CN_EntradaTipo objCnTipo = new CN_EntradaTipo();

        public frmEntradaPromo()
        {
            InitializeComponent();
        }

        private void frmEntradaPromo_Load(object sender, EventArgs e)
        {
            CargarPrecios();
            BloquearCampos();
            CargarPromo();
        }

        // =============================================================
        //  PRECIOS DE ENTRADAS
        // =============================================================

        private void btnEditarPrecios_Click(object sender, EventArgs e)
        {
            DesbloquearCampos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool ok = true;
            string mensaje = "";

            ok = ok && new CN_EntradaTipo().ActualizarPrecio(1, nudAdulto.Value, out mensaje);
            ok = ok && new CN_EntradaTipo().ActualizarPrecio(2, nudAdolescente.Value, out mensaje);
            ok = ok && new CN_EntradaTipo().ActualizarPrecio(3, nudNiño.Value, out mensaje);
            ok = ok && new CN_EntradaTipo().ActualizarPrecio(4, nudBebe.Value, out mensaje);

            if (ok)
            {
                MessageBox.Show("Precios actualizados correctamente", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                BloquearCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar los precios:\n" + mensaje, "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPrecios()
        {
            List<EntradaTipo> lista = new CN_EntradaTipo().Listar();

            foreach (EntradaTipo item in lista)
            {
                switch (item.Descripcion)
                {
                    case "Adulto":
                        nudAdulto.Value = item.PrecioBase;
                        break;
                    case "Adolescente":
                        nudAdolescente.Value = item.PrecioBase;
                        break;
                    case "Niño":
                        nudNiño.Value = item.PrecioBase;
                        break;
                    case "Bebé":
                        nudBebe.Value = item.PrecioBase;
                        break;
                }
            }
        }

        private void BloquearCampos()
        {
            nudAdulto.Enabled = false;
            nudAdolescente.Enabled = false;
            nudNiño.Enabled = false;
            nudBebe.Enabled = false;

            btnEditarPrecios.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void DesbloquearCampos()
        {
            nudAdulto.Enabled = true;
            nudAdolescente.Enabled = true;
            nudNiño.Enabled = true;
            nudBebe.Enabled = true;

            btnEditarPrecios.Enabled = false;
            btnGuardar.Enabled = true;
        }

        // =============================================================
        //  PROMOCIONES
        // =============================================================

        private void btnEditarPromo_Click(object sender, EventArgs e)
        {
            SetControlesPromo(true);
        }

        private void btnActivarPromo_Click(object sender, EventArgs e)
        {
            string categoria = "";

            if (rbPromoTodas.Checked) categoria = "Todas";
            else if (rbPromoAdulto.Checked) categoria = "Adulto";
            else if (rbPromoAdolescente.Checked) categoria = "Adolescente";
            else if (rbPromoNino.Checked) categoria = "Niño";
            else if (rbPromoBebe.Checked) categoria = "Bebe";

            EPromocion obj = new EPromocion()
            {
                IdPromocion = objPromoActual.IdPromocion,
                Categoria = categoria,
                Estado = true,
                UsuarioModifico = usuarioActual
            };

            string mensaje = "";
            bool ok = objCNPromo.Actualizar(obj, out mensaje);

            if (ok)
            {
                MessageBox.Show("Promoción activada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPromo();
                SetControlesPromo(false);
            }
            else
            {
                MessageBox.Show(mensaje, "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDesactivarPromo_Click(object sender, EventArgs e)
        {
            EPromocion obj = new EPromocion()
            {
                IdPromocion = objPromoActual.IdPromocion,
                Categoria = objPromoActual.Categoria,
                Estado = false,
                UsuarioModifico = usuarioActual
            };

            string mensaje = "";
            bool ok = objCNPromo.Actualizar(obj, out mensaje);

            if (ok)
            {
                MessageBox.Show("Promoción desactivada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarPromo();
                SetControlesPromo(false);
            }
            else
            {
                MessageBox.Show(mensaje, "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetControlesPromo(bool estado)
        {
            rbPromoTodas.Enabled = estado;
            rbPromoAdulto.Enabled = estado;
            rbPromoAdolescente.Enabled = estado;
            rbPromoNino.Enabled = estado;
            rbPromoBebe.Enabled = estado;

            btnActivarPromo.Enabled = estado;
            btnDesactivarPromo.Enabled = estado;
        }

        private void CargarPromo()
        {
            objPromoActual = objCNPromo.Obtener();

            if (objPromoActual != null)
            {
                // Actualizar estado visual
                ActualizarColorEstado(objPromoActual.Estado);

                // Seleccionar categoría
                switch (objPromoActual.Categoria)
                {
                    case "Adulto": rbPromoAdulto.Checked = true; break;
                    case "Adolescente": rbPromoAdolescente.Checked = true; break;
                    case "Niño": rbPromoNino.Checked = true; break;
                    case "Bebe": rbPromoBebe.Checked = true; break;
                    case "Todas": rbPromoTodas.Checked = true; break;
                }

                SetControlesPromo(false);
            }
        }

        private void ActualizarColorEstado(bool estado)
        {
            if (estado)
            {
                iconEstadoPromo.BackColor = Color.LightGreen;
                iconEstadoPromo.IconColor = Color.DarkGreen;
            }
            else
            {
                iconEstadoPromo.BackColor = Color.LightCoral;
                iconEstadoPromo.IconColor = Color.DarkRed;
            }
        }
    }
}
