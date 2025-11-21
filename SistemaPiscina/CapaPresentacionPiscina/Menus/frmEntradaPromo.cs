using CapaEntidadPiscina;
using CapaNegocioPiscina;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmEntradaPromo : Form
    {

        // ================================
        //  ATRIBUTOS
        // ================================
        private CN_EntradaTipo objCnTipo = new CN_EntradaTipo();
        private CN_Promociones objCNPromos = new CN_Promociones();

        

        public frmEntradaPromo()
        {
            InitializeComponent();
        }

        private void frmEntradaPromo_Load(object sender, EventArgs e)
        {
            CargarPrecios();
            BloquearCampos();
            CargarCategorias();
            CargarCategoriasDesc();

            // Estados iniciales de los paneles de promo
            SeleccionarCondicion();
            SeleccionarLimite();
            SeleccionarVigencia();

            CargarDgvPromos();
            
        }

        // ================================
        //  PRECIOS ENTRADAS (NO TOCAR)
        // ================================
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

        // ==========================================================
        //  SECCIÓN: PROMO 2x1 — CATEGORÍAS
        // ==========================================================
        private void CargarCategorias()
        {
            List<EntradaTipo> lista = objCnTipo.Listar();

            lista.Insert(0, new EntradaTipo
            {
                IdEntradaTipo = 0,
                Descripcion = "Todos"
            });

            cboCat2x1.DataSource = lista;
            cboCat2x1.DisplayMember = "Descripcion";
            cboCat2x1.ValueMember = "IdEntradaTipo";
        }
        // ==========================================================
        //  SECCIÓN: PROMO DESCUENTO — CATEGORÍAS
        // ==========================================================
        private void CargarCategoriasDesc()
        {
            List<EntradaTipo> lista = objCnTipo.Listar();

            lista.Insert(0, new EntradaTipo
            {
                IdEntradaTipo = 0,
                Descripcion = "Todos"
            });

            cboDesCategoria.DataSource = lista;
            cboDesCategoria.DisplayMember = "Descripcion";
            cboDesCategoria.ValueMember = "IdEntradaTipo";
        }

        // ==========================================================
        //  SECCIÓN: HELPERS — Reset / Habilitar
        // ==========================================================
        private void ResetCondicion()
        {
            rdbCompraMin.Checked = false;
            rdbAcumulaDia.Checked = false;
            rdbAcumulaVigencia.Checked = false;

            nudCondCompra.Value = 0;
            nudCondAcumula.Value = 0;
        }

        private void ResetLimite()
        {
            rdbSinLimite.Checked = false;
            rdbDetenerDespues.Checked = false;

            nudLimite.Value = 0;
        }

        private void ResetVigencia()
        {
            rdbSinFecha.Checked = false;
            rdbSoloDia.Checked = false;
            rdbRango.Checked = false;

            dtpSoloDia.Value = DateTime.Now;
            dtpFechaIni.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
        }

        private void HabilitarCondicion(bool enable)
        {
            rdbCompraMin.Enabled = enable;
            rdbAcumulaDia.Enabled = enable;
            rdbAcumulaVigencia.Enabled = enable;

            nudCondCompra.Enabled = enable && rdbCompraMin.Checked;
            nudCondAcumula.Enabled = enable && rdbAcumulaDia.Checked;
        }

        private void HabilitarLimite(bool enable)
        {
            rdbSinLimite.Enabled = enable;
            rdbDetenerDespues.Enabled = enable;

            nudLimite.Enabled = enable && rdbDetenerDespues.Checked;
        }

        private void HabilitarVigencia(bool enable)
        {
            rdbSinFecha.Enabled = enable;
            rdbSoloDia.Enabled = enable;
            rdbRango.Enabled = enable;

            dtpSoloDia.Enabled = enable && rdbSoloDia.Checked;
            dtpFechaIni.Enabled = enable && rdbRango.Checked;
            dtpFechaFin.Enabled = enable && rdbRango.Checked;
        }

        // ==========================================================
        //  EVENTOS: Condición
        // ==========================================================
        private void SeleccionarCondicion()
        {
            nudCondCompra.Enabled = rdbCompraMin.Checked;
            nudCondAcumula.Enabled = rdbAcumulaDia.Checked;
        }

        private void rdbCompraMin_CheckedChanged(object sender, EventArgs e) => SeleccionarCondicion();
        private void rdbAcumulaDia_CheckedChanged(object sender, EventArgs e) => SeleccionarCondicion();
        private void rdbAcumulaVigencia_CheckedChanged(object sender, EventArgs e) => SeleccionarCondicion();


        // ==========================================================
        //  EVENTOS: Límite
        // ==========================================================
        private void SeleccionarLimite()
        {
            nudLimite.Enabled = rdbDetenerDespues.Checked;
        }

        private void rdbSinLimite_CheckedChanged(object sender, EventArgs e) => SeleccionarLimite();
        private void rdbDetenerDespues_CheckedChanged(object sender, EventArgs e) => SeleccionarLimite();


        // ==========================================================
        //  EVENTOS: Vigencia
        // ==========================================================
        private void SeleccionarVigencia()
        {
            dtpSoloDia.Enabled = rdbSoloDia.Checked;
            dtpFechaIni.Enabled = rdbRango.Checked;
            dtpFechaFin.Enabled = rdbRango.Checked;
        }

        private void rdbSinFecha_CheckedChanged(object sender, EventArgs e) => SeleccionarVigencia();
        private void rdbSoloDia_CheckedChanged(object sender, EventArgs e) => SeleccionarVigencia();
        private void rdbRango_CheckedChanged(object sender, EventArgs e) => SeleccionarVigencia();


        // ==========================================================
        //  BOTÓN: GUARDAR 2x1
        // ==========================================================
        private void btnGuardar2x1_Click(object sender, EventArgs e)
        {
            string mensaje;

            // 1) Validar categoría
            if (cboCat2x1.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una categoría para la promoción.",
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idEntradaTipo = (int)cboCat2x1.SelectedValue; // 0 = Todos

            // 2) Validar condición
            string tipoCondicion = "";
            int? cantidadCond = null;

            if (rdbCompraMin.Checked)
            {
                if (nudCondCompra.Value <= 0)
                {
                    MessageBox.Show("Ingresa una cantidad válida para 'Aplica si compra al menos'.",
                        "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tipoCondicion = "CompraMinima";
                cantidadCond = (int)nudCondCompra.Value;
            }
            else if (rdbAcumulaDia.Checked)
            {
                if (nudCondAcumula.Value <= 0)
                {
                    MessageBox.Show("Ingresa una cantidad válida para 'Acumula cuando acumule en el día'.",
                        "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tipoCondicion = "AcumulaDia";
                cantidadCond = (int)nudCondAcumula.Value;
            }
            else if (rdbAcumulaVigencia.Checked)
            {
                tipoCondicion = "AcumulaVigencia";
                cantidadCond = null;
            }
            else
            {
                MessageBox.Show("Selecciona una condición para la promoción.",
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3) Validar límite
            string tipoLimite = "";
            int? cantidadLimite = null;

            if (rdbSinLimite.Checked)
            {
                tipoLimite = "SinLimite";
                cantidadLimite = null;
            }
            else if (rdbDetenerDespues.Checked)
            {
                if (nudLimite.Value <= 0)
                {
                    MessageBox.Show("Ingresa un valor válido para 'Detener después de'.",
                        "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tipoLimite = "DespuesDe";
                cantidadLimite = (int)nudLimite.Value;
            }
            else
            {
                MessageBox.Show("Selecciona un límite de uso para la promoción.",
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4) Validar vigencia
            string tipoVigencia = "";
            DateTime? fechaIni = null;
            DateTime? fechaFin = null;
            DateTime? fechaDia = null;

            if (rdbSinFecha.Checked)
            {
                tipoVigencia = "SinFecha";
            }
            else if (rdbSoloDia.Checked)
            {
                tipoVigencia = "SoloDia";
                fechaDia = dtpSoloDia.Value.Date;
            }
            else if (rdbRango.Checked)
            {
                if (dtpFechaIni.Value.Date > dtpFechaFin.Value.Date)
                {
                    MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin.",
                        "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                tipoVigencia = "Rango";
                fechaIni = dtpFechaIni.Value.Date;
                fechaFin = dtpFechaFin.Value.Date;
            }
            else
            {
                MessageBox.Show("Selecciona el tipo de vigencia de la promoción.",
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5) Armar entidades
            var promo = new Promocion
            {
                TipoPromo = "2x1",
                IdEntradaTipo = idEntradaTipo,
                Porcentaje = 0,   // en 2x1 no usamos porcentaje
                Estado = true
            };

            var cond = new PromocionCondicion
            {
                TipoCondicion = tipoCondicion,
                Cantidad = cantidadCond
            };

            var lim = new PromocionLimite
            {
                TipoLimite = tipoLimite,
                CantidadLimite = cantidadLimite,
                CantidadUsada = 0
            };

            var vig = new PromocionVigencia
            {
                TipoVigencia = tipoVigencia,
                FechaInicio = fechaIni,
                FechaFin = fechaFin,
                FechaDia = fechaDia
            };

            // 6) Guardar usando la capa de negocio
            bool resultado = objCNPromos.RegistrarPromocion(promo, cond, lim, vig, out mensaje);

            if (resultado)
            {
                MessageBox.Show("Promoción 2x1 registrada correctamente.",
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLimpiar2x1_Click(null, null); // limpiamos el formulario
            }
            else
            {
                MessageBox.Show("No se pudo registrar la promoción:\n" + mensaje,
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CargarDgvPromos();

        }

        // ==========================================================
        //  BOTÓN: LIMPIAR 2x1
        // ==========================================================
        private void btnLimpiar2x1_Click(object sender, EventArgs e)
        {
            // Categoría
            if (cboCat2x1.Items.Count > 0)
                cboCat2x1.SelectedIndex = 0;

            // Condición / límite / vigencia
            ResetCondicion();
            ResetLimite();
            ResetVigencia();

            // Reaplicar estados de enabled
            SeleccionarCondicion();
            SeleccionarLimite();
            SeleccionarVigencia();
        }
        private void CargarDgvPromos()
        {
            dgvPromos.Rows.Clear();

            List<PromocionListado> lista = objCNPromos.ListarPromociones2x1();

            foreach (var item in lista)
            {
                dgvPromos.Rows.Add(
                    "",                     // Botón eliminar
                    item.IdPromocion,       // ID
                    item.TipoPromo,         // <--- NUEVA COLUMNA TIPO PROMO
                    item.Categoria,
                    item.TipoCondicion,
                    item.CantidadCondicion,
                    item.TipoLimite,
                    item.CantidadLimite,
                    item.TipoVigencia,
                    item.FechaInicio,       // puede ser NULL → WinForms acepta
                    item.FechaFin,
                    item.FechaDia,
                    item.Estado
                );




            }
        }

        private void dgvPromos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evita errores cuando se hace clic en encabezados
            if (e.RowIndex < 0)
                return;

            // Verifica si se hizo clic en la columna Eliminar
            if (dgvPromos.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                // Validar que la fila tenga datos reales
                if (dgvPromos.Rows[e.RowIndex].Cells["IdPromocion"].Value == null ||
                    dgvPromos.Rows[e.RowIndex].Cells["IdPromocion"].Value == DBNull.Value ||
                    string.IsNullOrWhiteSpace(dgvPromos.Rows[e.RowIndex].Cells["IdPromocion"].Value.ToString()))
                {
                    MessageBox.Show("No hay promoción para eliminar en esta fila.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Si llega aquí, el Id existe y se puede eliminar
                int idPromocion = Convert.ToInt32(dgvPromos.Rows[e.RowIndex].Cells["IdPromocion"].Value);

                DialogResult resp = MessageBox.Show(
                    "¿Seguro que deseas eliminar esta promoción?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resp == DialogResult.Yes)
                {
                    string mensaje = "";
                    bool eliminado = new CN_Promociones().EliminarPromocion(idPromocion, out mensaje);

                    if (eliminado)
                    {
                        MessageBox.Show("Promoción eliminada correctamente", "Mensaje",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDgvPromos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la promoción:\n" + mensaje,
                            "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void btnDesGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = "";

            // 1) Validar categoría
            if (cboDesCategoria.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una categoría.", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCategoria = (int)cboDesCategoria.SelectedValue;

            // 2) Validar porcentaje
            if (numDesPorcentaje.Value <= 0)
            {
                MessageBox.Show("Ingresa un porcentaje válido.", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal porcentaje = numDesPorcentaje.Value;

            // 3) Condición
            string tipoCond = "";
            int? cantCond = null;

            if (rbDesCompraMinima.Checked)
            {
                tipoCond = "CompraMinima";
                cantCond = (int)numDesCompraMinima.Value;
            }
            else if (rbDesAcumDia.Checked)
            {
                tipoCond = "AcumulaDia";
                cantCond = (int)numDesAcumDia.Value;
            }
            else if (rbDesAcumVigencia.Checked)
            {
                tipoCond = "AcumulaVigencia";
                cantCond = null;
            }
            else
            {
                MessageBox.Show("Selecciona una condición.", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4) Límite
            string tipoLimite = "";
            int? cantLimite = null;

            if (rbDesSinLimite.Checked)
            {
                tipoLimite = "SinLimite";
            }
            else if (rbDesLimiteDespues.Checked)
            {
                tipoLimite = "DespuesDe";
                cantLimite = (int)numDesLimiteDespues.Value;
            }
            else
            {
                MessageBox.Show("Selecciona un límite.", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5) Vigencia
            string tipoVig = "";
            DateTime? fIni = null, fFin = null, fDia = null;

            if (rbDesSinFecha.Checked)
            {
                tipoVig = "SinFecha";
            }
            else if (rbDesSoloDia.Checked)
            {
                tipoVig = "SoloDia";
                fDia = dtpDesSoloDia.Value;
            }
            else if (rbDesRango.Checked)
            {
                tipoVig = "Rango";
                fIni = dtpDesDesde.Value;
                fFin = dtpDesHasta.Value;
            }
            else
            {
                MessageBox.Show("Selecciona una vigencia.", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 6) Construir entidades
            var promo = new Promocion
            {
                TipoPromo = "Descuento",
                IdEntradaTipo = idCategoria,
                Porcentaje = porcentaje,
                Estado = true
            };

            var cond = new PromocionCondicion
            {
                TipoCondicion = tipoCond,
                Cantidad = cantCond
            };

            var lim = new PromocionLimite
            {
                TipoLimite = tipoLimite,
                CantidadLimite = cantLimite,
                CantidadUsada = 0
            };

            var vig = new PromocionVigencia
            {
                TipoVigencia = tipoVig,
                FechaInicio = fIni,
                FechaFin = fFin,
                FechaDia = fDia
            };

            // 7) Guardar
            bool ok = objCNPromos.RegistrarPromocion(promo, cond, lim, vig, out mensaje);

            if (ok)
            {
                MessageBox.Show("Promoción de descuento registrada.",
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarDescuento();
                CargarDgvPromos();
            }
            else
            {
                MessageBox.Show("Error:\n" + mensaje,
                    "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarDescuento()
        {
            cboDesCategoria.SelectedIndex = 0;

            numDesPorcentaje.Value = numDesPorcentaje.Minimum;


            rbDesCompraMinima.Checked = false;
            rbDesAcumDia.Checked = false;
            rbDesAcumVigencia.Checked = false;

            numDesCompraMinima.Value = 0;
            numDesAcumDia.Value = 0;

            rbDesSinLimite.Checked = false;
            rbDesLimiteDespues.Checked = false;

            numDesLimiteDespues.Value = 0;

            rbDesSinFecha.Checked = false;
            rbDesSoloDia.Checked = false;
            rbDesRango.Checked = false;

            dtpDesSoloDia.Value = DateTime.Now;
            dtpDesDesde.Value = DateTime.Now;
            dtpDesHasta.Value = DateTime.Now;
        }

        private void btnDesLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarDescuento();
        }
    }
}
