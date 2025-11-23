using CapaEntidadPiscina;
using CapaNegocioPiscina;
using CapaPresentacionPiscina.Modals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace CapaPresentacionPiscina.Menus
{
    public partial class frmVenta : Form
    {
        // =====================================
        //       PROMO 2x1 — DATOS ACTUALES
        // =====================================
        EPromocion objPromoActual = null;
        CN_Promocion objCNP = new CN_Promocion();

        private int _idUsuario;

        public frmVenta(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
        }



        private void frmVenta_Load(object sender, EventArgs e)
        {
            SesionCaja = ObtenerCajaActiva(_idUsuario);

            if (SesionCaja == null)
            {
                MessageBox.Show("No hay una caja abierta. Debes abrir caja para continuar.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            CargarEntradas();
            CargarProductosVenta();
            CargarPromo();   // <<< IMPORTANTE

            cboMetodoPago.Items.Clear();
            cboMetodoPago.Items.Add("Efectivo");
            cboMetodoPago.Items.Add("QR");
            cboMetodoPago.Items.Add("Cortesía");
            cboMetodoPago.SelectedIndex = 0; // Default

            // ============================
            //     BOTONES DE ENTRADAS
            // ============================
            btnAdultoAdd.Click += (s, ev) =>
            {
                AgregarEntradaAlDGV(
                    "Adulto",
                    Convert.ToDecimal(lblPrecioAdulto.Text),
                    (int)nudAdulto.Value
                );
                nudAdulto.Value = 1;
            };

            btnAdolescenteAdd.Click += (s, ev) =>
            {
                AgregarEntradaAlDGV(
                    "Adolescente",
                    Convert.ToDecimal(lblPrecioAdolescente.Text),
                    (int)nudAdolescente.Value
                );
                nudAdolescente.Value = 1;
            };

            btnNinoAdd.Click += (s, ev) =>
            {
                AgregarEntradaAlDGV(
                    "Niño",
                    Convert.ToDecimal(lblPrecioNino.Text),
                    (int)nudNino.Value
                );
                nudNino.Value = 1;
            };

            btnBebeAdd.Click += (s, ev) =>
            {
                AgregarEntradaAlDGV(
                    "Bebé",
                    Convert.ToDecimal(lblPrecioBebe.Text),
                    (int)nudBebe.Value
                );
                nudBebe.Value = 1;
            };
        }

        private CajaTurno ObtenerCajaActiva(int idUsuario)
        {
            CN_CajaTurno objCaja = new CN_CajaTurno();
            return objCaja.ObtenerCajaActiva(idUsuario);
        }

        // ============================
        //   CARGAR ENTRADAS
        // ============================
        private void CargarEntradas()
        {
            List<EntradaTipo> entradas = new CN_EntradaTipo().Listar();

            foreach (EntradaTipo et in entradas)
            {
                string tipo = et.Descripcion.Trim().ToLower();

                switch (tipo)
                {
                    case "adulto":
                        lblPrecioAdulto.Text = et.PrecioBase.ToString("0.00");
                        break;

                    case "adolescente":
                        lblPrecioAdolescente.Text = et.PrecioBase.ToString("0.00");
                        break;

                    case "niño":
                    case "nino":
                        lblPrecioNino.Text = et.PrecioBase.ToString("0.00");
                        break;

                    case "bebe":
                    case "bebé":
                        lblPrecioBebe.Text = et.PrecioBase.ToString("0.00");
                        break;
                }
            }
        }

        // ============================
        //   CARGAR PRODUCTOS
        // ============================
        private void CargarProductosVenta()
        {
            flpProductos.Controls.Clear();

            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto p in lista)
            {
                // Crear nuevo GroupBox basado en la plantilla
                GroupBox card = new GroupBox();
                card.Size = gbPlantillaProducto.Size;
                card.BackColor = Color.White;


                // Clonar todos los controles internos
                foreach (Control ctrl in gbPlantillaProducto.Controls)
                {
                    Control clon = (Control)Activator.CreateInstance(ctrl.GetType());
                    clon.Name = ctrl.Name;
                    clon.Text = ctrl.Text;
                    clon.Size = ctrl.Size;
                    clon.Location = ctrl.Location;
                    clon.Font = ctrl.Font;
                    clon.ForeColor = ctrl.ForeColor;

                    // --- Controlar el color según el tipo ---
                    if (ctrl is Label)
                    {
                        clon.BackColor = Color.White;
                    }
                    else if (ctrl is NumericUpDown)
                    {
                        clon.BackColor = Color.White;
                    }
                    else if (ctrl is FontAwesome.Sharp.IconButton iconOriginal)
                    {
                        var nuevoIcon = clon as FontAwesome.Sharp.IconButton;

                        // Copiar PROPIEDADES del icon button
                        nuevoIcon.IconChar = iconOriginal.IconChar;
                        nuevoIcon.IconColor = iconOriginal.IconColor;
                        nuevoIcon.IconFont = iconOriginal.IconFont;
                        nuevoIcon.IconSize = iconOriginal.IconSize;
                        nuevoIcon.FlatStyle = iconOriginal.FlatStyle;
                        nuevoIcon.FlatAppearance.BorderSize = iconOriginal.FlatAppearance.BorderSize;

                        // Mantener su backcolor ORIGINAL (verde)
                        nuevoIcon.BackColor = iconOriginal.BackColor;
                    }
                    else
                    {
                        // Para cualquier otro control, respetar color original
                        clon.BackColor = ctrl.BackColor;
                    }

                    card.Controls.Add(clon);
                }


                // Obtener referencias del clon
                Label lblDescripcion = card.Controls["lblProductoDescripcion"] as Label;
                Label lblPrecio = card.Controls["lblProductoPrecio"] as Label;
                Label lblStock = card.Controls["lblProductoStock"] as Label;
                NumericUpDown nud = card.Controls["nudCantidadProducto"] as NumericUpDown;
                Button btnAdd = card.Controls["btnAgregar"] as Button;

                // Asignar datos reales al nuevo card
                lblDescripcion.Text = p.Nombre;
                lblPrecio.Text = "Bs. " + p.PrecioVenta.ToString("0.00");
                lblStock.Text = "Stock: " + p.Stock;
                nud.Value = 1;

                // Guardar producto en Tag para referencia
                card.Tag = p;
                btnAdd.Tag = p;
                nud.Tag = p;

                // Evento del botón agregar
                btnAdd.Click += (s, ev) =>
                {
                    int cantidadSeleccionada = (int)nud.Value;

                    if (cantidadSeleccionada <= 0)
                    {
                        MessageBox.Show("Seleccione una cantidad válida.");
                        return;
                    }

                    AgregarProductoAlDGV(p, cantidadSeleccionada);

                    // 🔥 Resetear cantidad
                    nud.Value = 1;
                };


                // Ocultar si el stock está en 0
                card.Visible = p.Stock > 0;

                // Agregar tarjeta al FlowLayoutPanel
                flpProductos.Controls.Add(card);
            }
        }

        // ============================
        //   CALCULAR TOTAL
        // ============================
        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }

            txtTotal.Text = total.ToString("0.00");
        }

        // ============================
        //   AGREGAR ENTRADA AL DGV
        // ============================
        private void AgregarEntradaAlDGV(string tipo, decimal precio, int cantidad)
        {
            if (cantidad <= 0)
            {
                MessageBox.Show("Seleccione una cantidad válida.");
                return;
            }

            string nombre = "Entrada " + tipo;

            // ---------------------------------------------
            //      DETERMINAR SI APLICA PROMO 2x1
            // ---------------------------------------------
            bool promoActiva = PromoAplicaPara(tipo);

            // Cantidad que realmente se cobra
            int cantidadCobrar = promoActiva
                ? cantidad - (cantidad / 2)      // Fórmula 2x1
                : cantidad;

            // ---------------------------------------------
            //      SI YA EXISTE LA FILA → ACTUALIZAR
            // ---------------------------------------------
            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (row.Cells["NombreProducto"].Value != null &&
                    row.Cells["NombreProducto"].Value.ToString() == nombre)
                {
                    int cantActual = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    int nuevaCant = cantActual + cantidad;

                    int cobrar = promoActiva
                        ? nuevaCant - (nuevaCant / 2)
                        : nuevaCant;

                    row.Cells["Cantidad"].Value = nuevaCant;
                    row.Cells["Subtotal"].Value = (cobrar * precio).ToString("0.00");

                    // Colorear fila si corresponde
                    MarcarFilaPromo(row, promoActiva);

                    CalcularTotal();
                    return;
                }
            }

            // ---------------------------------------------
            //      NO EXISTE → NUEVA FILA
            // ---------------------------------------------
            decimal subtotal = cantidadCobrar * precio;

            int nuevaFila = dgvVenta.Rows.Add(
                0,                  // IdProducto = 0 (porque es entrada)
                nombre,
                "--------",
                cantidad,
                precio.ToString("0.00"),
                subtotal.ToString("0.00"),
                "Eliminar"
            );

            // Colorear fila recién creada
            MarcarFilaPromo(dgvVenta.Rows[nuevaFila], promoActiva);

            // Mensaje solo cuando agregas una fila nueva (no actualizar)
            if (promoActiva)
            {
                MessageBox.Show(
                    $"PROMO ACTIVA 2×1 aplicada para {tipo}.",
                    "Promoción",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            CalcularTotal();
        }



        private void AgregarProductoAlDGV(Producto p, int cantidad)
        {
            // VALIDACIÓN: cantidad válida
            if (cantidad <= 0)
            {
                MessageBox.Show("Seleccione una cantidad válida.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // CALCULAR CANTIDAD YA VENDIDA EN EL DGV PARA ESTE PRODUCTO (stock temporal)
            int cantidadesPrevias = dgvVenta.Rows.Cast<DataGridViewRow>()
                .Where(r => Convert.ToInt32(r.Cells["IdProducto"].Value) == p.IdProducto)
                .Sum(r => Convert.ToInt32(r.Cells["Cantidad"].Value));

            int stockDisponible = p.Stock - cantidadesPrevias;

            if (cantidad > stockDisponible)
            {
                MessageBox.Show("Stock insuficiente.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VERIFICAR SI YA EXISTE LA FILA DEL PRODUCTO
            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (Convert.ToInt32(row.Cells["IdProducto"].Value) == p.IdProducto)
                {
                    // SUMAMOS CANTIDAD
                    int cantActual = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    int nuevaCantidad = cantActual + cantidad;

                    row.Cells["Cantidad"].Value = nuevaCantidad;
                    row.Cells["Subtotal"].Value =
                        (nuevaCantidad * p.PrecioVenta).ToString("0.00");

                    CalcularTotal();
                    ActualizarTarjetaStock(p);
                    return;
                }
            }

            // SI NO EXISTE → NUEVA FILA
            dgvVenta.Rows.Add(
                p.IdProducto,
                p.Nombre,
                p.Descripcion,
                cantidad,
                p.PrecioVenta.ToString("0.00"),
                (p.PrecioVenta * cantidad).ToString("0.00"),
                "Eliminar"
            );

            CalcularTotal();
            ActualizarTarjetaStock(p);
        }

        private void ActualizarTarjetaStock(Producto p)
        {
            foreach (GroupBox card in flpProductos.Controls.OfType<GroupBox>())
            {
                if (card.Tag is Producto prod && prod.IdProducto == p.IdProducto)
                {
                    Label lblStock = card.Controls["lblProductoStock"] as Label;

                    int cantidadesVendidas = dgvVenta.Rows.Cast<DataGridViewRow>()
                        .Where(r => Convert.ToInt32(r.Cells["IdProducto"].Value) == p.IdProducto)
                        .Sum(r => Convert.ToInt32(r.Cells["Cantidad"].Value));

                    int stockTemporal = p.Stock - cantidadesVendidas;

                    lblStock.Text = "Stock: " + stockTemporal;

                    // Colores según stock
                    if (stockTemporal <= 3)
                        card.BackColor = Color.FromArgb(255, 200, 200);
                    else
                        card.BackColor = Color.White;

                    // Si ya no hay stock limpiar tarjeta
                    card.Visible = stockTemporal > 0;
                }
            }
        }

        // ============================
        //      ELIMINAR FILA
        // ============================
        private void dgvVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvVenta.Rows.Count)
            {
                MessageBox.Show("Seleccione una fila válida.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvVenta.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                DialogResult dr = MessageBox.Show(
                    "¿Seguro que deseas eliminar este ítem?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dr == DialogResult.Yes)
                {
                    dgvVenta.Rows.RemoveAt(e.RowIndex);
                    CalcularTotal();
                    RecalcularColoresPromo();
                }
            }
        }

        private void cboMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMetodoPago.SelectedItem.ToString() == "Cortesía")
            {
                txtTotal.Text = "0.00"; // Total gratis

                MessageBox.Show(
                    "Venta marcada como CORTESÍA.\n\nNo se cobrará nada, pero sí se descontará el stock y se registrará en reporte.",
                    "Cortesía",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                // Recalcular el total normal
                CalcularTotal();
            }
        }

        private void CargarPromo()
        {
            objPromoActual = objCNP.Obtener();
        }


        private bool PromoAplicaPara(string tipoEntrada)
        {
            if (objPromoActual == null) return false;
            if (!objPromoActual.Estado) return false;

            // Promo es para todos
            if (objPromoActual.Categoria == "Todas")
                return true;

            // Coincide categoría exacta
            return objPromoActual.Categoria == tipoEntrada;
        }
        private void MarcarFilaPromo(DataGridViewRow row, bool aplicaPromo)
        {
            if (aplicaPromo)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(200, 255, 200); // Verde suave
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void RecalcularColoresPromo()
        {
            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                // Evitar nulls al eliminar filas
                var cell = row.Cells["NombreProducto"].Value;
                if (cell == null)
                {
                    MarcarFilaPromo(row, false);
                    continue;
                }

                string nombre = cell.ToString();

                if (nombre.StartsWith("Entrada "))
                {
                    string tipo = nombre.Replace("Entrada ", "").Trim();
                    bool aplica = PromoAplicaPara(tipo);
                    MarcarFilaPromo(row, aplica);
                }
                else
                {
                    // Productos nunca aplican promo
                    MarcarFilaPromo(row, false);
                }
            }
        }
        private void BuscarCliente()
        {
            string dni = txtDocumento.Text.Trim();
            if (dni == "") return;

            Cliente cli = new CN_Cliente().BuscarPorDNI(dni);

            if (cli != null)
            {
                // AUTO RELLENO
                txtNombre.Text = cli.NombreCompleto;
                txtTelefono.Text = cli.Telefono;

                // 🔥 IMPORTANTE → Guardar cliente en sesión
                SesionCliente = cli;
            }
            else
            {
                // Cliente nuevo
                txtNombre.Text = "";
                txtTelefono.Text = "";
                SesionCliente = null;   // Cliente nuevo

                MessageBox.Show("Nuevo cliente. Complete sus datos.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BuscarCliente();
                e.Handled = true;
                e.SuppressKeyPress = true; // evita el “ding”
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string ticket = GenerarTicketPreview();

            frmPreviewTicket preview = new frmPreviewTicket();
            preview.TicketTexto = ticket;   // ← ESTA LÍNEA ES LA QUE FALTABA

            if (preview.ShowDialog() == DialogResult.OK)
            {
                string numeroGenerado = "";
                if (GuardarVentaEnBD(out numeroGenerado))
                {
                    ImprimirTicket(numeroGenerado);
                    MessageBox.Show("Venta registrada con éxito");
                    LimpiarFormulario();
                }
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        private int ObtenerIdEntradaDesdeNombre(string nombre)
        {
            // nombre = "Entrada Adulto"
            string tipo = nombre.Replace("Entrada", "").Trim();

            // Lista de EntradaTipo ya cargada en memoria (tu método CargarEntradas)
            var lista = new CN_EntradaTipo().Listar();

            return lista.FirstOrDefault(x =>
                x.Descripcion.Equals(tipo, StringComparison.OrdinalIgnoreCase))?.IdEntradaTipo ?? 0;
        }

        private bool GuardarVentaEnBD(out string numeroGenerado)
        {
            numeroGenerado = "";
            try
            {
                // ================================
                // VALIDACIONES BÁSICAS
                // ================================
                if (dgvVenta.Rows.Count == 0)
                {
                    MessageBox.Show("No hay productos o entradas para guardar la venta.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (cboMetodoPago.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione un método de pago.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // ================================
                // OBJETO PRINCIPAL: VENTA
                // ================================
                Venta oVenta = new Venta()
                {
                    IdUsuario = _idUsuario,
                    IdCliente = string.IsNullOrWhiteSpace(txtDocumento.Text)
                                ? (int?)null
                                : SesionCliente.IdCliente,

                    MetodoPago = cboMetodoPago.SelectedItem.ToString(),
                    MontoTotal = Convert.ToDecimal(txtTotal.Text),
                    IdCajaTurno = SesionCaja.IdCajaTurno    // si ya manejas cajaTurno
                };

                // ================================

                // DETALLE DE ENTRADAS
                // ================================
                List<DetalleVentaEntrada> detalleEntradas = new List<DetalleVentaEntrada>();

                foreach (DataGridViewRow row in dgvVenta.Rows)
                {
                    string nombre = row.Cells["NombreProducto"].Value.ToString();

                    if (nombre.StartsWith("Entrada"))
                    {
                        int idEntrada = ObtenerIdEntradaDesdeNombre(nombre); // función auxiliar
                        int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                        decimal precioUnit = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value);
                        decimal subtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value);

                        detalleEntradas.Add(new DetalleVentaEntrada()
                        {
                            IdEntradaTipo = idEntrada,
                            Cantidad = cantidad,
                            PrecioUnitario = precioUnit,
                            PrecioAplicado = precioUnit, // por si hay promo no pagada
                            SubTotal = subtotal
                        });
                    }
                }

                // ================================
                // DETALLE DE PRODUCTOS
                // ================================
                List<DetalleVentaProducto> detalleProductos = new List<DetalleVentaProducto>();

                foreach (DataGridViewRow row in dgvVenta.Rows)
                {
                    if (row.Cells["IdProducto"].Value == null) continue;

                    int idProd = Convert.ToInt32(row.Cells["IdProducto"].Value);
                    int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    decimal precio = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value);
                    decimal subtotal = Convert.ToDecimal(row.Cells["Subtotal"].Value);

                    detalleProductos.Add(new DetalleVentaProducto()
                    {
                        IdProducto = idProd,
                        Cantidad = cantidad,
                        PrecioUnitario = precio,
                        SubTotal = subtotal
                    });
                }

                // ================================
                // LLAMADA A NEGOCIO
                // ================================
                CN_Venta objVenta = new CN_Venta();
                bool resultado = objVenta.Registrar(
                    oVenta,
                    detalleEntradas,
                    detalleProductos,
                    out numeroGenerado
                );

                return resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la venta:\n" + ex.Message,
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ===============================================
        //  GENERAR TICKET — SOLO TEXTO PREVIEW
        // ===============================================
        private string GenerarTicketPreview()
        {
            // Aquí luego implementamos el diseño moderno del ticket
            return "PREVIEW DEL TICKET...\n(Contenido temporal)";
        }

        // ===============================================
        //  IMPRIMIR TICKET
        // ===============================================
        private void ImprimirTicket(string contenido)
        {
            // Lógica real de impresión la agregamos después
            MessageBox.Show("Simulación de impresión:\n\n" + contenido);
        }

        // ===============================================
        //  LIMPIAR FORMULARIO DESPUÉS DE GUARDAR VENTA
        // ===============================================
        private void LimpiarFormulario()
        {
            dgvVenta.Rows.Clear();
            txtNombre.Text = "";
            txtDocumento.Text = "";
            txtTelefono.Text = "";
            txtTotal.Text = "0.00";
            cboMetodoPago.SelectedIndex = 0;
        }

        // ===============================================
        //  SESIONES — DATOS TEMPORALES
        // ===============================================
        private Cliente SesionCliente = null;

        private CajaTurno SesionCaja = null;
        // ⚠️ si no tienes esta clase creada todavía, hacemos una temporal
        public class CajaTurno
        {
            public int IdCajaTurno { get; set; }
        }


    }
}
