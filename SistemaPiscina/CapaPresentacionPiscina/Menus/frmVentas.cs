using CapaEntidadPiscina;
using CapaNegocioPiscina;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml;


namespace CapaPresentacionPiscina.Menus
{

    public partial class frmVentas : Form
    {
        // ===== OBJETOS DE NEGOCIO =====
        CN_EntradaTipo cnEntradaTipo = new CN_EntradaTipo();
        CN_Producto cnProducto = new CN_Producto();
        private EPromocion promoActiva;
        private List<Producto> listaProductosOriginal = new List<Producto>();

        // 🔹 NUEVOS CAMPOS
        // ===== NUEVOS CAMPOS =====
        private int _idUsuario;
        private int? _idCajaTurnoActual;   // ← ahora nullable
        private string _rolUsuario;

        // Constructor que usa el formulario en tiempo de ejecución
        public frmVentas(int idUsuario, int? idCajaTurnoActual, string rolUsuario)
        {
            InitializeComponent();

            _idUsuario = idUsuario;
            _idCajaTurnoActual = idCajaTurnoActual;   // puede ser null
            _rolUsuario = rolUsuario;                 // ← NUEVO CAMPO
        }


        private void frmVentas_Load(object sender, EventArgs e)
        {
            CargarCategoriasFiltro();
            CargarEntradas();
            CargarProductos();
            CargarPromocion();
            CargarMetodosPago();
        }

        private void CargarPromocion()
        {
            try
            {
                CN_Promocion cnPromo = new CN_Promocion();
                promoActiva = cnPromo.Obtener();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando promoción: " + ex.Message);
            }
        }



        private void CargarEntradas()
        {
            try
            {
                List<EntradaTipo> lista = cnEntradaTipo.ListarEntradasVenta();

                if (lista == null || lista.Count == 0)
                {
                    // Opcional: mensaje para saber si no está trayendo nada
                    // MessageBox.Show("No se encontraron tipos de entrada activos.");
                    return;
                }

                foreach (EntradaTipo item in lista)
                {
                    // ⚠ Aquí asumo:
                    // IdEntradaTipo = 1 -> Adulto
                    // IdEntradaTipo = 2 -> Adolescente
                    // IdEntradaTipo = 3 -> Niño
                    // IdEntradaTipo = 4 -> Bebe

                    switch (item.IdEntradaTipo)
                    {
                        case 1: // Adulto
                            lblPrecioAdulto.Text = item.PrecioBase.ToString("0.00");
                            break;

                        case 2: // Adolescente
                            lblPrecioAdolescente.Text = item.PrecioBase.ToString("0.00");
                            break;

                        case 3: // Niño
                            lblPrecioNino.Text = item.PrecioBase.ToString("0.00");
                            break;

                        case 4: // Bebe
                            lblPrecioBebe.Text = item.PrecioBase.ToString("0.00");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar precios de entradas: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarEntradaAlDGV(int idEntrada, string nombre, decimal precioUnitario, int cantidad)
        {
            int cantidadCobrada = cantidad;
            bool aplicaPromo = false;

            // ============================================
            // 1. VERIFICAR SI APLICA PROMOCIÓN
            // ============================================
            if (promoActiva != null && promoActiva.Estado)
            {
                bool promoParaTodas = promoActiva.Categoria.ToUpper() == "TODAS";
                bool promoParaCategoria = promoActiva.Categoria == nombre;

                if (promoParaTodas || promoParaCategoria)
                {
                    aplicaPromo = true;

                    cantidadCobrada = (cantidad / 2) + (cantidad % 2);

                    MessageBox.Show(
                        $"La entrada '{nombre}' aplica a la promoción 2x1.\n" +
                        $"Pagas {cantidadCobrada} por {cantidad}.",
                        "Promoción aplicada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }

            decimal subtotal = cantidadCobrada * precioUnitario;

            // ============================================
            // 2. SI YA EXISTE EN LA TABLA → ACTUALIZAR
            // ============================================
            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (!row.IsNewRow &&
                    Convert.ToInt32(row.Cells["colId"].Value) == idEntrada &&
                    row.Cells["colTipo"].Value.ToString() == "Entrada")
                {
                    int cantActual = Convert.ToInt32(row.Cells["colCantidad"].Value);
                    int nuevaCant = cantActual + cantidad;

                    // Recalcular promo
                    int nuevaCantCobrada = nuevaCant;

                    if (aplicaPromo)
                        nuevaCantCobrada = (nuevaCant / 2) + (nuevaCant % 2);

                    row.Cells["colCantidad"].Value = nuevaCant;
                    row.Cells["colSubTotal"].Value = nuevaCantCobrada * precioUnitario;

                    // Actualizar colEsPromo
                    row.Cells["colEsPromo"].Value = aplicaPromo ? 1 : 0;

                    CalcularTotal();
                    return;
                }
            }

            // ============================================
            // 3. SI NO EXISTE → CREAR NUEVA FILA
            // ============================================
            dgvVenta.Rows.Add(
                idEntrada,          // colId
                nombre,             // colNombre
                "Entrada",          // colDescripcion (o tipo)
                cantidad,           // colCantidad (cantidad ingresada)
                precioUnitario,     // colPrecioUnitario
                subtotal,           // colSubTotal
                "X",                // colEliminar
                "Entrada",          // colTipo
                aplicaPromo ? 1 : 0 // colEsPromo (oculta)
            );

            CalcularTotal();
        }




        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (row.Cells["colSubTotal"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["colSubTotal"].Value);
                }
            }

            txtTotal.Text = total.ToString("0.00");
        }

        private void btnAdultoAdd_Click(object sender, EventArgs e)
        {
            int cantidad = (int)nudAdulto.Value;
            decimal precio = Convert.ToDecimal(lblPrecioAdulto.Text);

            if (cantidad > 0)
                AgregarEntradaAlDGV(1, "Adulto", precio, cantidad);

            nudAdulto.Value = 1;   // 🔥 Reinicia después de agregar
        }


        private void btnAdolescenteAdd_Click(object sender, EventArgs e)
        {
            int cantidad = (int)nudAdolescente.Value;
            decimal precio = Convert.ToDecimal(lblPrecioAdolescente.Text);

            if (cantidad > 0)
                AgregarEntradaAlDGV(2, "Adolescente", precio, cantidad);

            nudAdolescente.Value = 1;  // 🔥
        }

        private void btnNinoAdd_Click(object sender, EventArgs e)
        {
            int cantidad = (int)nudNino.Value;
            decimal precio = Convert.ToDecimal(lblPrecioNino.Text);

            if (cantidad > 0)
                AgregarEntradaAlDGV(3, "Niño", precio, cantidad);

            nudNino.Value = 1;   // 🔥
        }

        private void btnBebeAdd_Click(object sender, EventArgs e)
        {
            int cantidad = (int)nudBebe.Value;
            decimal precio = Convert.ToDecimal(lblPrecioBebe.Text);

            if (cantidad > 0)
                AgregarEntradaAlDGV(4, "Bebé", precio, cantidad);

            nudBebe.Value = 1;   // 🔥
        }


        private void dgvVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvVenta.Columns["colEliminar"].Index && e.RowIndex >= 0)
            {
                string tipo = dgvVenta.Rows[e.RowIndex].Cells["colTipo"].Value.ToString();
                int id = Convert.ToInt32(dgvVenta.Rows[e.RowIndex].Cells["colId"].Value);
                int cantidadDG = Convert.ToInt32(dgvVenta.Rows[e.RowIndex].Cells["colCantidad"].Value);

                // SOLO PRODUCTO devuelve stock
                if (tipo == "Producto")
                {
                    foreach (Control ctrl in flpProductos.Controls)
                    {
                        if (ctrl is GroupBox gb && gb.Tag is Producto p && p.IdProducto == id)
                        {
                            Label lblStock = gb.Controls["lblStock"] as Label;
                            NumericUpDown nud = gb.Controls["nudProducto"] as NumericUpDown;
                            FontAwesome.Sharp.IconButton btnAdd = gb.Controls["btnAdd"] as FontAwesome.Sharp.IconButton;

                            p.Stock += cantidadDG;
                            lblStock.Text = p.Stock.ToString();

                            nud.Enabled = true;
                            btnAdd.Enabled = true;

                            gb.BackColor = (p.Stock <= 5)
                                ? Color.FromArgb(255, 230, 230)
                                : Color.White;

                            break;
                        }
                    }
                }

                dgvVenta.Rows.RemoveAt(e.RowIndex);
                CalcularTotal();
            }
        }


        private void CargarProductos()
        {
            try
            {
                flpProductos.Controls.Clear();

                listaProductosOriginal = cnProducto.ListarProductosVenta(); // <--- Guardamos la lista original
                List<Producto> lista = listaProductosOriginal;

                foreach (Producto p in lista)
                {
                    if (p.Stock <= 0)
                        continue;

                    // === TARJETA PRINCIPAL ===
                    GroupBox gb = new GroupBox
                    {
                        Size = gbPlantilla.Size,
                        Font = new Font("Segoe UI", gbPlantilla.Font.Size, FontStyle.Bold),
                        ForeColor = Color.Black,
                        BackColor = Color.White,
                        Text = p.Nombre,
                        Visible = true,
                        Tag = p
                    };

                    // === LABEL DESCRIPCIÓN ===
                    Label lblDesc = new Label
                    {
                        Name = "lblDescripcion",
                        Text = p.Descripcion,
                        AutoSize = false,
                        Size = gbPlantilla.Controls["lblDescripcion"].Size,
                        Location = gbPlantilla.Controls["lblDescripcion"].Location,
                        Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                        ForeColor = Color.DimGray
                    };

                    // === LABEL PRECIO ===
                    Label lblPrecio = new Label
                    {
                        Name = "lblPrecioProducto",
                        Text = "Precio: " + p.PrecioVenta.ToString("0.00"),
                        AutoSize = false,
                        Size = gbPlantilla.Controls["lblPrecioProducto"].Size,
                        Location = gbPlantilla.Controls["lblPrecioProducto"].Location,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(40, 40, 40)
                    };

                    // === LABEL STOCK ===
                    Label lblStock = new Label
                    {
                        Name = "lblStock",
                        Text = p.Stock.ToString(),
                        AutoSize = false,
                        Size = gbPlantilla.Controls["lblStock"].Size,
                        Location = gbPlantilla.Controls["lblStock"].Location,
                        Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                        ForeColor = Color.DarkSlateGray
                    };

                    // === NUMERIC UP DOWN ===
                    NumericUpDown nudCant = new NumericUpDown
                    {
                        Name = "nudProducto",
                        Minimum = 1,
                        Maximum = 1000,
                        Value = 1,
                        Size = gbPlantilla.Controls["nudProducto"].Size,
                        Location = gbPlantilla.Controls["nudProducto"].Location,
                        Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                        BackColor = Color.WhiteSmoke
                    };

                    // === CLONAR ICONBUTTON ===
                    var btnPlantilla = (FontAwesome.Sharp.IconButton)gbPlantilla.Controls["btnAdd"];

                    FontAwesome.Sharp.IconButton btnAdd = new FontAwesome.Sharp.IconButton
                    {
                        Name = "btnAdd",
                        Text = "",
                        Size = btnPlantilla.Size,
                        Location = btnPlantilla.Location,

                        // Icono exacto del botón plantilla
                        IconChar = btnPlantilla.IconChar,
                        IconColor = btnPlantilla.IconColor,
                        IconFont = btnPlantilla.IconFont,
                        IconSize = btnPlantilla.IconSize,
                        ImageAlign = btnPlantilla.ImageAlign,
                        TextAlign = btnPlantilla.TextAlign,

                        BackColor = btnPlantilla.BackColor,
                        ForeColor = btnPlantilla.ForeColor,
                        FlatStyle = btnPlantilla.FlatStyle
                    };

                    btnAdd.FlatAppearance.BorderSize = btnPlantilla.FlatAppearance.BorderSize;

                    // ==========================
                    // EVENTOS
                    // ==========================

                    // 🔥 AL CAMBIAR NUD
                    // 🌿 AL CAMBIAR NUD
                    nudCant.ValueChanged += (s, e) =>
                    {
                        // Si ya no hay stock, no intentes ajustar el Value
                        if (p.Stock <= 0)
                        {
                            // Solo reflejamos visualmente que está en 0 y bloqueamos
                            lblStock.Text = "0";
                            gb.BackColor = Color.FromArgb(255, 220, 220);
                            nudCant.Enabled = false;
                            btnAdd.Enabled = false;
                            return;
                        }

                        // ✅ Limitar la selección al stock disponible, pero solo si hay stock
                        if (nudCant.Value > p.Stock)
                        {
                            nudCant.Value = p.Stock; // acá p.Stock siempre es >= 1
                        }

                        int solicitado = (int)nudCant.Value;
                        int restante = p.Stock - solicitado;

                        if (restante < 0) restante = 0;

                        lblStock.Text = restante.ToString();

                        gb.BackColor = (restante <= 5)
                            ? Color.FromArgb(255, 230, 230)   // alerta suave
                            : Color.White;
                    };



                    // 🔥 CLICK AGREGAR
                    btnAdd.Click += (s, e) =>
                    {
                        int solicitado = (int)nudCant.Value;

                        if (solicitado <= 0)
                            return;

                        if (solicitado > p.Stock)
                        {
                            MessageBox.Show(
                                "La cantidad seleccionada supera el stock disponible.",
                                "Stock insuficiente",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            nudCant.Value = p.Stock;
                            return;
                        }

                        // Agregar al DGV (este método RESTA el stock correctamente)
                        AgregarProductoAlDGV(p, lblStock, nudCant);

                        // Reiniciar numeric
                        nudCant.Value = 1;

                        // Colores y bloqueo
                        if (p.Stock <= 5)
                            gb.BackColor = Color.FromArgb(255, 230, 230);

                        if (p.Stock == 0)
                        {
                            nudCant.Enabled = false;
                            btnAdd.Enabled = false;
                            gb.BackColor = Color.FromArgb(255, 220, 220);
                        }
                    };




                    // ==========================
                    // AGREGAR A LA CARD
                    // ==========================
                    gb.Controls.Add(lblDesc);
                    gb.Controls.Add(lblPrecio);
                    gb.Controls.Add(lblStock);
                    gb.Controls.Add(nudCant);
                    gb.Controls.Add(btnAdd);

                    flpProductos.Controls.Add(gb);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }




        private void AgregarProductoAlDGV(Producto p, Label lblStock, NumericUpDown nud)
        {
            int cantidadAgregar = (int)nud.Value;

            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (!row.IsNewRow &&
                    Convert.ToInt32(row.Cells["colId"].Value) == p.IdProducto &&
                    row.Cells["colTipo"].Value.ToString() == "Producto")
                {
                    int cantActual = Convert.ToInt32(row.Cells["colCantidad"].Value);
                    int nuevaCant = cantActual + cantidadAgregar;

                    row.Cells["colCantidad"].Value = nuevaCant;
                    row.Cells["colSubTotal"].Value = nuevaCant * p.PrecioVenta;

                    // actualizar stock real
                    p.Stock -= cantidadAgregar;
                    lblStock.Text = p.Stock.ToString();

                    CalcularTotal();
                    return;
                }
            }

            // si no existe → agregar fila nueva
            dgvVenta.Rows.Add(
                p.IdProducto,
                p.Nombre,
                p.Descripcion,
                cantidadAgregar,
                p.PrecioVenta,
                cantidadAgregar * p.PrecioVenta,
                "X",
                "Producto"     // colTipo
            );

            // restar stock real
            p.Stock -= cantidadAgregar;
            lblStock.Text = p.Stock.ToString();

            CalcularTotal();
        }

        private void CargarCategoriasFiltro()
        {
            CN_Categoria cnCat = new CN_Categoria();
            var lista = cnCat.ListarActivas();

            cbCategoria.DataSource = lista;
            cbCategoria.DisplayMember = "Descripcion";
            cbCategoria.ValueMember = "IdCategoria";

            cbCategoria.SelectedIndex = -1; 
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categoría.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idCategoriaFiltro = Convert.ToInt32(cbCategoria.SelectedValue);

            foreach (Control ctrl in flpProductos.Controls)
            {
                if (ctrl is GroupBox gb && gb.Tag is Producto p)
                {
                    gb.Visible = (p.IdCategoria == idCategoriaFiltro);
                }
            }
        }






        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cbCategoria.SelectedIndex = -1;

            foreach (Control ctrl in flpProductos.Controls)
            {
                ctrl.Visible = true; // ← vuelve a mostrar todo
            }
        }


        private void CargarMetodosPago()
        {
            var metodos = new List<string>()
            {
                "EFECTIVO",
                "QR",
                "CORTESIA"
            };

            cbMetodoPago.DataSource = metodos;
            cbMetodoPago.SelectedIndex = 0;
        }

        private void cbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMetodoPago.SelectedItem == null)
                return;

            string metodo = cbMetodoPago.SelectedItem.ToString();

            if (metodo == "CORTESIA")
            {
                txtTotal.Text = "0.00";
            }
            else
            {
                CalcularTotal();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // 1. REVERTIR STOCK DE LOS PRODUCTOS DEL DGV
            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (row.IsNewRow) continue;

                string tipo = row.Cells["colTipo"].Value.ToString();

                if (tipo == "Producto")
                {
                    int idProd = Convert.ToInt32(row.Cells["colId"].Value);
                    int cantidadDG = Convert.ToInt32(row.Cells["colCantidad"].Value);

                    foreach (Control ctrl in flpProductos.Controls)
                    {
                        if (ctrl is GroupBox gb && gb.Tag is Producto p && p.IdProducto == idProd)
                        {
                            // Restaurar stock real
                            p.Stock += cantidadDG;

                            // Actualizar visual
                            Label lblStock = gb.Controls["lblStock"] as Label;
                            NumericUpDown nud = gb.Controls["nudProducto"] as NumericUpDown;
                            FontAwesome.Sharp.IconButton btnAdd = gb.Controls["btnAdd"] as FontAwesome.Sharp.IconButton;

                            lblStock.Text = p.Stock.ToString();
                            nud.Value = 1;
                            nud.Enabled = true;
                            btnAdd.Enabled = true;

                            gb.BackColor = Color.White;
                        }
                    }
                }
            }

            // 2. LIMPIAR EL DGV
            dgvVenta.Rows.Clear();

            // 3. REINICIAR CAMPOS DEL CLIENTE
            txtDocumento.Text = "";
            txtNombreCliente.Text = "";
            txtTelefono.Text = "";

            // 4. REINICIAR TOTAL Y METODO DE PAGO
            txtTotal.Text = "0.00";
            cbMetodoPago.SelectedIndex = 0;  // EFECTIVO

            // 5. REINICIAR FILTRO DE CATEGORÍA Y MOSTRAR TODOS LOS PRODUCTOS
            cbCategoria.SelectedIndex = -1;
            foreach (Control ctrl in flpProductos.Controls)
            {
                ctrl.Visible = true;
            }

            MessageBox.Show("Venta cancelada.",
                            "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private string ConstruirXmlDetalle()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Detalles");
            doc.AppendChild(root);

            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["colSubTotal"].Value == null) continue;

                string tipo = row.Cells["colTipo"].Value.ToString();
                int id = Convert.ToInt32(row.Cells["colId"].Value);
                int cantidad = Convert.ToInt32(row.Cells["colCantidad"].Value);
                decimal precioUnitario = Convert.ToDecimal(row.Cells["colPrecioUnitario"].Value);
                decimal subTotal = Convert.ToDecimal(row.Cells["colSubTotal"].Value);

                // Para formato decimal tipo 10.50 y no 10,50
                var ci = CultureInfo.InvariantCulture;

                if (tipo == "Entrada")
                {
                    bool esPromo = false;
                    if (row.Cells["colEsPromo"].Value != null)
                        esPromo = Convert.ToBoolean(row.Cells["colEsPromo"].Value);

                    // PrecioAplicado = subtotal / cantidad cobradas reales
                    // pero en tu DGV el subtotal ya es lo cobrado.
                    decimal precioAplicado = (cantidad > 0)
                        ? subTotal / cantidad
                        : precioUnitario;

                    XmlElement nod = doc.CreateElement("Entrada");
                    nod.SetAttribute("IdEntradaTipo", id.ToString());
                    nod.SetAttribute("Cantidad", cantidad.ToString());
                    nod.SetAttribute("PrecioUnitario", precioUnitario.ToString("0.00", ci));
                    nod.SetAttribute("PrecioAplicado", precioAplicado.ToString("0.00", ci));
                    nod.SetAttribute("SubTotal", subTotal.ToString("0.00", ci));
                    nod.SetAttribute("EsPromo", esPromo ? "1" : "0");

                    root.AppendChild(nod);
                }
                else if (tipo == "Producto")
                {
                    XmlElement nod = doc.CreateElement("Producto");
                    nod.SetAttribute("IdProducto", id.ToString());
                    nod.SetAttribute("Cantidad", cantidad.ToString());
                    nod.SetAttribute("PrecioUnitario", precioUnitario.ToString("0.00", ci));
                    nod.SetAttribute("SubTotal", subTotal.ToString("0.00", ci));

                    root.AppendChild(nod);
                }
            }

            return doc.OuterXml;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validaciones base
            if (dgvVenta.Rows.Cast<DataGridViewRow>().All(r => r.IsNewRow))
            {
                MessageBox.Show("Debe agregar al menos una entrada o producto.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Ingrese el DNI del cliente.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MessageBox.Show("Ingrese el nombre del cliente.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCliente.Focus();
                return;
            }

            decimal total;
            if (!decimal.TryParse(txtTotal.Text, out total))
            {
                MessageBox.Show("El total a cobrar no es válido.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string metodoPago = cbMetodoPago.Text; // EFECTIVO / QR / CORTESIA

            // 2. Construir XML
            string xmlDetalle = ConstruirXmlDetalle();

            if (string.IsNullOrWhiteSpace(xmlDetalle))
            {
                MessageBox.Show("No se pudo construir el detalle de la venta.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Llamar a la capa de negocio
            CN_Venta cnVenta = new CN_Venta();

            string numeroVenta;
            string mensaje;

            if (_idCajaTurnoActual == null && _rolUsuario.ToUpper() == "CAJERO")
            {
                MessageBox.Show("No puedes registrar ventas sin haber abierto una caja.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idVentaGenerada = cnVenta.RegistrarVenta(
                _idUsuario,                    // ya lo tienes en el form
                _idCajaTurnoActual.HasValue ? _idCajaTurnoActual.Value : (int?)null, // ← si es null, envía 0
                txtDocumento.Text.Trim(),
                txtNombreCliente.Text.Trim(),
                txtTelefono.Text.Trim(),
                metodoPago,
                total,
                xmlDetalle,
                out numeroVenta,
                out mensaje
            );

            if (idVentaGenerada != 0)
            {
                MessageBox.Show(
                    $"Venta registrada correctamente.\nNúmero: {numeroVenta}",
                    "Venta registrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

               
                // 🔹 LIMPIAMOS TODO DESPUÉS DE GUARDAR
                LimpiarFormulario();

            }
            else
            {
                MessageBox.Show(
                    "No se pudo registrar la venta.\n" + mensaje,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void LimpiarFormulario()
        {
            // Limpia textbox del cliente
            txtDocumento.Text = "";
            txtNombreCliente.Text = "";
            txtTelefono.Text = "";

            // Reinicia contadores de entradas
            nudAdulto.Value = 1;
            nudAdolescente.Value = 1;
            nudNino.Value = 1;
            nudBebe.Value = 1;

            // Limpia DGV
            dgvVenta.Rows.Clear();

            // Reinicia total
            txtTotal.Text = "0,00";

            // Opcional: limpiar método de pago
            cbMetodoPago.SelectedIndex = 0;

            // Enfoque inicial
            txtDocumento.Focus();
        }

    }
}
