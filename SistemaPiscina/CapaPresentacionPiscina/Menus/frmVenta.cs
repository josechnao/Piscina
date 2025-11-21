using CapaEntidadPiscina;
using CapaNegocioPiscina;
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
    public partial class frmVenta : Form
    {
        

        public frmVenta()
        {
            InitializeComponent();
        }

        private void frmVenta_Load(object sender, EventArgs e)
        {
            CargarCatalogoSnacks();
            CargarEntradas();
        }
        private GroupBox ClonarGroupBoxProducto(Producto p)
        {
            // Crear nuevo GroupBox
            GroupBox nuevo = new GroupBox();
            nuevo.Size = gbPlantillaProducto.Size;
            nuevo.BackColor = gbPlantillaProducto.BackColor;

            // ⚠️ IMPORTANTE: el nombre del producto va en el Text del GroupBox
            nuevo.Text = p.Nombre;

            // Clonar controles internos
            foreach (Control ctrl in gbPlantillaProducto.Controls)
            {
                Control clon = (Control)Activator.CreateInstance(ctrl.GetType());

                clon.Text = ctrl.Text;
                clon.Name = ctrl.Name;
                clon.Size = ctrl.Size;
                clon.Location = ctrl.Location;
                clon.Font = ctrl.Font;

                nuevo.Controls.Add(clon);
            }

            // Buscar los controles dentro del clon
            Label lblDescripcion = nuevo.Controls["lblProductoDescripcion"] as Label;
            Label lblPrecio = nuevo.Controls["lblProductoPrecio"] as Label;
            Label lblStock = nuevo.Controls["lblProductoStock"] as Label;
            NumericUpDown nud = nuevo.Controls["nudCantidadProducto"] as NumericUpDown;
            Button btnAdd = nuevo.Controls["btnAgregar"] as Button;

            // Reemplazar textos
            lblDescripcion.Text = p.Descripcion;
            lblPrecio.Text = "Precio: " + p.PrecioVenta.ToString("0.00");
            lblStock.Text = "Stock: " + p.Stock;

            // Guardar datos en Tag (útil para extraer IdProducto)
            nuevo.Tag = p;
            btnAdd.Tag = p;
            nud.Tag = p;

            // Evento del botón agregar (lógica temporal por ahora)
            btnAdd.Click += (s, e) => AgregarProductoAlDGV(p, (int)nud.Value);

            // Stock bajo → Pintar rojo
            if (p.Stock <= 3)
            {
                nuevo.BackColor = Color.FromArgb(255, 180, 180);
            }

            // Sin stock → Ocultar tarjeta
            if (p.Stock == 0)
            {
                nuevo.Visible = false;
            }

            return nuevo;
        }

        private void CargarEntradas()
        {
            List<EntradaTipo> entradas = new CN_EntradaTipo().Listar();

            foreach (EntradaTipo et in entradas)
            {
                switch (et.Descripcion.ToLower())
                {
                    case "adulto":
                        lblPrecioAdulto.Text = et.PrecioBase.ToString("0.00");
                        break;
                    case "adolescente":
                        lblPrecioAdolescente.Text = et.PrecioBase.ToString("0.00");
                        break;
                    case "niño":
                        lblPrecioNino.Text = et.PrecioBase.ToString("0.00");
                        break;
                    case "bebe":
                        lblPrecioBebe.Text = et.PrecioBase.ToString("0.00");
                        break;
                }
            }
        }

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
        private void AgregarProductoAlDGV(Producto p, int cantidad)
        {
            decimal subtotal = p.PrecioVenta * cantidad;

            dgvVenta.Rows.Add(new object[]
            {
        p.IdProducto,
        p.Nombre,
        p.Descripcion,
        cantidad,
        p.PrecioVenta.ToString("0.00"),
        subtotal.ToString("0.00"),
        "Eliminar"
            });

            CalcularTotal();
        }

        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvVenta.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }

            txtTotal.Text = total.ToString("0.00");
        }


        private void CargarCatalogoSnacks()
        {
            CN_Producto cnProducto = new CN_Producto();
            List<Producto> productos = cnProducto.Listar().Where(p => p.Estado && p.Stock > 0).ToList();

            foreach (Producto p in productos)
            {
                // Clonar plantila completa
                GroupBox gb = (GroupBox)ClonarControl(gbPlantillaProducto);

                // Cambiar el texto del groupBox (nombre del producto)
                gb.Text = p.Nombre;

                // Obtener controles clonados
                Label lblDescripcion = gb.Controls["lblProductoDescripcion"] as Label;
                Label lblPrecio = gb.Controls["lblProductoPrecio"] as Label;
                Label lblStock = gb.Controls["lblProductoStock"] as Label;
                NumericUpDown nudCantidad = gb.Controls["nudCantidadProducto"] as NumericUpDown;
                Button btnAgregar = gb.Controls["btnAgregar"] as Button;

                // Asignar datos
                lblDescripcion.Text = p.Descripcion;
                lblPrecio.Text = "Precio: " + p.PrecioVenta.ToString("0.00");
                lblStock.Text = "Stock: " + p.Stock;

                nudCantidad.Value = 0;
                nudCantidad.Maximum = p.Stock;  // evitar cantidades mayores

                // Evento agregar
                btnAgregar.Click += (s, ev) =>
                {
                    int cantidad = (int)nudCantidad.Value;

                    if (cantidad <= 0)
                    {
                        MessageBox.Show("Ingrese una cantidad válida");
                        return;
                    }1  1

                    if (cantidad > p.Stock)
                    {
                        MessageBox.Show("Stock insuficiente");
                        return;
                    }

                    AgregarProductoAlDGV(p, cantidad);
                };

                // Agregar al FlowLayoutPanel
                flpProductos.Controls.Add(gb);
            }
        }

        private Control ClonarControl(Control original)
        {
            Type tipo = original.GetType();
            Control copia = (Control)Activator.CreateInstance(tipo);

            // Copiar propiedades básicas
            copia.Text = original.Text;
            copia.Size = original.Size;
            copia.Location = original.Location;
            copia.BackColor = original.BackColor;
            copia.ForeColor = original.ForeColor;
            copia.Font = original.Font;
            copia.Name = original.Name;

            // Copiar hijos
            foreach (Control hijo in original.Controls)
            {
                copia.Controls.Add(ClonarControl(hijo));
            }

            return copia;
        }


    }
}
