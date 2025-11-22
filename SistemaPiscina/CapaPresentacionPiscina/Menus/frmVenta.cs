using CapaEntidadPiscina;
using CapaNegocioPiscina;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacionPiscina.Menus
{
    public partial class frmVenta : Form
    {
        private Dictionary<int, string> mapaEntradas = new Dictionary<int, string>
{
    { 1, "Adulto" },
    { 2, "Adolescente" },
    { 3, "Niño" },
    { 4, "Bebe" },
    { 0, "Todas" }    // Cuando aplica a todas las categorías
};

        private PromocionConfiguracion promoActiva;

        private bool PromoCumpleCondiciones(string tipoEntrada, int cantidadNueva)
        {
            if (promoActiva == null)
                return false;

            // ===== 1) Verificar categoría =====
            bool categoriaAplica =
                 promoActiva.IdEntradaTipo == 0 ||
                 promoActiva.NombreCategoria.Equals(tipoEntrada, StringComparison.OrdinalIgnoreCase);


            if (!categoriaAplica)
                return false;

            // ===== 2) Obtener la cantidad TOTAL acumulada =====
            int totalEntradas = cantidadNueva;

            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (row.Cells["NombreProducto"].Value != null &&
                    row.Cells["NombreProducto"].Value.ToString().Contains(tipoEntrada))
                {
                    if (row.Cells["Cantidad"].Value != null)
                        totalEntradas += Convert.ToInt32(row.Cells["Cantidad"].Value);
                }
            }

            // ===== 3) Compra mínima =====
            if (promoActiva.TipoCondicion == "CompraMinima")
            {
                if (totalEntradas < promoActiva.CantidadCondicion)
                    return false;
            }

            // ===== 4) Acumula/Vigencia =====
            if (promoActiva.TipoCondicion == "AcumulaVigencia")
            {
                if (totalEntradas < promoActiva.CantidadCondicion)
                    return false;
            }

            // ===== 5) Verificar límite =====
            if (promoActiva.TipoLimite == "DespuesDe")
            {
                if (promoActiva.CantidadUsada >= promoActiva.CantidadLimite)
                    return false;
            }


            // ===== 6) Verificar vigencia =====
            if (promoActiva.TipoVigencia == "SoloDia")
            {
                if (promoActiva.FechaDia?.Date != DateTime.Now.Date)
                    return false;
            }

            if (promoActiva.TipoVigencia == "Rango")
            {
                if (DateTime.Now.Date < promoActiva.FechaInicio.Value.Date ||
                    DateTime.Now.Date > promoActiva.FechaFin.Value.Date)
                    return false;
            }

            return true;
        }




        public frmVenta()
        {
            InitializeComponent();
        }

        private void frmVenta_Load(object sender, EventArgs e)
        {
            CargarEntradas();
            CargarProductosVenta();

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

                // Resetear cantidad
                nudAdulto.Value = 0;
            };

            btnAdolescenteAdd.Click += (s, ev) =>
            {
                AgregarEntradaAlDGV(
                    "Adolescente",
                    Convert.ToDecimal(lblPrecioAdolescente.Text),
                    (int)nudAdolescente.Value
                );

                nudAdolescente.Value = 0;
            };


            btnNinoAdd.Click += (s, ev) =>
            {
                AgregarEntradaAlDGV(
                    "Niño",
                    Convert.ToDecimal(lblPrecioNino.Text),
                    (int)nudNino.Value
                );

                nudNino.Value = 0;
            };


            btnBebeAdd.Click += (s, ev) =>
            {
                AgregarEntradaAlDGV(
                    "Bebe",
                    Convert.ToDecimal(lblPrecioBebe.Text),
                    (int)nudBebe.Value
                );

                nudBebe.Value = 0;
            };


            // ============================
            //   PROMOCIÓN ACTIVA
            // ============================
            promoActiva = new CN_Promociones().ObtenerPromocionActiva();

            if (promoActiva != null)
            {
                string categoria = "";

                switch (promoActiva.IdEntradaTipo)
                {
                    case 0: categoria = "todas las entradas"; break;
                    case 1: categoria = "Adulto"; break;
                    case 2: categoria = "Adolescente"; break;
                    case 3: categoria = "Niño"; break;
                    case 4: categoria = "Bebé"; break;
                    default: categoria = "desconocida"; break;
                }

                MessageBox.Show(
                    $"Promoción activa: {promoActiva.TipoPromo}\n" +
                    $"Aplicada a: {categoria}",
                    "Promoción del día",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

        }

        // ============================
        //   CLONAR TARJETA PRODUCTO
        // ============================

        private GroupBox ClonarGroupBoxProducto(Producto p)
        {
            GroupBox nuevo = new GroupBox();
            nuevo.Size = gbPlantillaProducto.Size;
            nuevo.BackColor = gbPlantillaProducto.BackColor;
            nuevo.Text = p.Nombre;

            foreach (Control ctrl in gbPlantillaProducto.Controls)
            {
                Control clon = (Control)Activator.CreateInstance(ctrl.GetType());

                clon.Name = ctrl.Name;
                clon.Text = ctrl.Text;
                clon.Size = ctrl.Size;
                clon.Location = ctrl.Location;
                clon.Font = ctrl.Font;
                clon.BackColor = ctrl.BackColor;
                clon.ForeColor = ctrl.ForeColor;

                if (ctrl is FontAwesome.Sharp.IconButton originalIcon)
                {
                    var nuevoIcon = clon as FontAwesome.Sharp.IconButton;
                    nuevoIcon.IconChar = originalIcon.IconChar;
                    nuevoIcon.IconColor = originalIcon.IconColor;
                    nuevoIcon.IconFont = originalIcon.IconFont;
                    nuevoIcon.IconSize = originalIcon.IconSize;
                    nuevoIcon.FlatStyle = originalIcon.FlatStyle;
                    nuevoIcon.FlatAppearance.BorderSize = originalIcon.FlatAppearance.BorderSize;
                    nuevoIcon.TextAlign = originalIcon.TextAlign;
                    nuevoIcon.ImageAlign = originalIcon.ImageAlign;
                    nuevoIcon.Padding = originalIcon.Padding;
                }

                nuevo.Controls.Add(clon);
            }

            Label lblDescripcion = nuevo.Controls["lblProductoDescripcion"] as Label;
            Label lblPrecio = nuevo.Controls["lblProductoPrecio"] as Label;
            Label lblStock = nuevo.Controls["lblProductoStock"] as Label;
            NumericUpDown nud = nuevo.Controls["nudCantidadProducto"] as NumericUpDown;
            Button btnAdd = nuevo.Controls["btnAgregar"] as Button;

            lblDescripcion.Text = p.Descripcion;
            lblPrecio.Text = "Precio: " + p.PrecioVenta.ToString("0.00");
            lblStock.Text = p.Stock.ToString();

            nuevo.Tag = p;
            btnAdd.Tag = p;
            nud.Tag = p;

            btnAdd.Click += (s, e) =>
            {
                int cant = (int)nud.Value;

                if (cant <= 0)
                {
                    MessageBox.Show("Seleccione una cantidad válida.");
                    return;
                }

                if (cant > p.Stock)
                {
                    MessageBox.Show("Stock insuficiente.");
                    return;
                }

                AgregarProductoAlDGV(p, cant);
            };

            if (p.Stock <= 3)
                nuevo.BackColor = Color.FromArgb(255, 200, 200);

            if (p.Stock == 0)
                nuevo.Visible = false;

            return nuevo;
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
            gbPlantillaProducto.Visible = false;

            List<Producto> lista = new CN_Producto().ListarProductosVenta();

            flpProductos.Controls.Clear();

            foreach (Producto p in lista)
            {
                GroupBox gb = ClonarGroupBoxProducto(p);
                flpProductos.Controls.Add(gb);
            }
        }

        // ============================
        //   AGREGAR PRODUCTO AL DGV
        // ============================
        private void AgregarProductoAlDGV(Producto p, int cantidad)
        {
            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (Convert.ToInt32(row.Cells["IdProducto"].Value) == p.IdProducto)
                {
                    int cantActual = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    int nuevaCant = cantActual + cantidad;

                    if (nuevaCant > p.Stock)
                    {
                        MessageBox.Show("Stock insuficiente.");
                        return;
                    }

                    row.Cells["Cantidad"].Value = nuevaCant;
                    row.Cells["Subtotal"].Value = (nuevaCant * p.PrecioVenta)
                        .ToString("0.00");

                    CalcularTotal();
                    ActualizarTarjetaStock(p);
                    return;
                }
            }

            dgvVenta.Rows.Add(new object[]
            {
                p.IdProducto,
                p.Nombre,
                p.Descripcion,
                cantidad,
                p.PrecioVenta.ToString("0.00"),
                (p.PrecioVenta * cantidad).ToString("0.00"),
                "Eliminar"
            });

            CalcularTotal();
            ActualizarTarjetaStock(p);
        }

        private void ActualizarTarjetaStock(Producto p)
        {
            foreach (Control ctrl in flpProductos.Controls)
            {
                if (ctrl is GroupBox gb)
                {
                    if (gb.Tag is Producto prod && prod.IdProducto == p.IdProducto)
                    {
                        Label lblStock = gb.Controls["lblProductoStock"] as Label;

                        int cantVendida = dgvVenta.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => Convert.ToInt32(r.Cells["IdProducto"].Value) == p.IdProducto)
                            .Sum(r => Convert.ToInt32(r.Cells["Cantidad"].Value));

                        int stockTemporal = p.Stock - cantVendida;

                        lblStock.Text = stockTemporal.ToString();

                        gb.BackColor = stockTemporal <= 3 ?
                            Color.FromArgb(255, 180, 180) :
                            Color.White;

                        gb.Visible = stockTemporal > 0;
                    }
                }
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
            string descripcion = "--------";

            bool promoAplica = PromoCumpleCondiciones(tipo, cantidad);
             // Mostrar mensaje UNA sola vez al agregar
            if (promoAplica)
            {
                promoActiva.CantidadUsada += cantidad;
            }

           
            if (promoAplica)
                MessageBox.Show($"Promoción activa: {promoActiva.TipoPromo}");

            // Calcular subtotal normal
            decimal subtotal = precio * cantidad;

            // Aplicar 2x1
            if (promoAplica && promoActiva.TipoPromo == "2x1")
            {
                int pagados = (cantidad / 2) + (cantidad % 2);
                subtotal = pagados * precio;
            }

            // Aplicar descuento % si corresponde
            if (promoAplica && promoActiva.TipoPromo == "Descuento")
            {
                subtotal = subtotal - (subtotal * (promoActiva.Porcentaje / 100m));
            }

            // Revisión si ya está en el DGV
            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                if (row.Cells["NombreProducto"].Value != null &&
                    row.Cells["NombreProducto"].Value.ToString() == nombre)
                {
                    int cantActual = Convert.ToInt32(row.Cells["Cantidad"].Value);
                    int nuevaCant = cantActual + cantidad;

                    decimal nuevoSubtotal = nuevaCant * precio;

                    if (promoAplica && promoActiva.TipoPromo == "2x1")
                    {
                        int pagados = (nuevaCant / 2) + (nuevaCant % 2);
                        nuevoSubtotal = pagados * precio;
                    }

                    if (promoAplica && promoActiva.TipoPromo == "Descuento")
                    {
                        nuevoSubtotal -= nuevoSubtotal * (promoActiva.Porcentaje / 100m);
                    }

                    row.Cells["Cantidad"].Value = nuevaCant;
                    row.Cells["Subtotal"].Value = nuevoSubtotal.ToString("0.00");

                    CalcularTotal();
                    return;
                }
            }

            // No existe en el dgv → agregar fila nueva
            dgvVenta.Rows.Add(new object[]
            {
        0,
        nombre,
        descripcion,
        cantidad,
        precio.ToString("0.00"),
        subtotal.ToString("0.00"),
        "Eliminar"
            });

            CalcularTotal();
        }

        private void dgvVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1) Validar que no esté en la última fila vacía
            if (e.RowIndex < 0 || e.RowIndex >= dgvVenta.Rows.Count - 1)
            {
                MessageBox.Show("Seleccione una fila válida que contenga datos.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2) Validar que haya hecho clic en la columna del botón
            if (dgvVenta.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                // Confirmación opcional (puedes quitar si no quieres confirmación)
                DialogResult dr = MessageBox.Show(
                    "¿Seguro que deseas eliminar este ítem?",
                    "Confirmación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dr == DialogResult.Yes)
                {
                    dgvVenta.Rows.RemoveAt(e.RowIndex);
                    CalcularTotal();      // Recalcular total
                }
            }
        }

    }
}
