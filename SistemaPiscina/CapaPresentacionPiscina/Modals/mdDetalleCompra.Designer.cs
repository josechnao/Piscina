namespace CapaPresentacionPiscina.Modals
{
    partial class mdDetalleCompra
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnContinuar = new FontAwesome.Sharp.IconButton();
            this.dgvDetalleCompra = new System.Windows.Forms.DataGridView();
            this.txtRegistradoPor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFechaHora = new System.Windows.Forms.TextBox();
            this.txtMontoTotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumeroCompra = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleCompra)).BeginInit();
            this.SuspendLayout();
            // 
            // btnContinuar
            // 
            this.btnContinuar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnContinuar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinuar.ForeColor = System.Drawing.Color.White;
            this.btnContinuar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnContinuar.IconColor = System.Drawing.Color.Black;
            this.btnContinuar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnContinuar.IconSize = 26;
            this.btnContinuar.Location = new System.Drawing.Point(589, 551);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(191, 43);
            this.btnContinuar.TabIndex = 25;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnContinuar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnContinuar.UseVisualStyleBackColor = false;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // dgvDetalleCompra
            // 
            this.dgvDetalleCompra.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleCompra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Producto,
            this.Descripcion,
            this.Cantidad,
            this.PrecioCompra,
            this.PrecioVenta,
            this.SubTotal});
            this.dgvDetalleCompra.Location = new System.Drawing.Point(31, 260);
            this.dgvDetalleCompra.Name = "dgvDetalleCompra";
            this.dgvDetalleCompra.Size = new System.Drawing.Size(749, 277);
            this.dgvDetalleCompra.TabIndex = 24;
            // 
            // txtRegistradoPor
            // 
            this.txtRegistradoPor.Location = new System.Drawing.Point(611, 35);
            this.txtRegistradoPor.Name = "txtRegistradoPor";
            this.txtRegistradoPor.ReadOnly = true;
            this.txtRegistradoPor.Size = new System.Drawing.Size(137, 20);
            this.txtRegistradoPor.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(608, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "Registrado Por :";
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(324, 122);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.ReadOnly = true;
            this.txtDocumento.Size = new System.Drawing.Size(137, 20);
            this.txtDocumento.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(321, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Fecha/Hora :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(321, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Documento :";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(31, 191);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.ReadOnly = true;
            this.txtTelefono.Size = new System.Drawing.Size(137, 20);
            this.txtTelefono.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(28, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Teléfono :";
            // 
            // txtFechaHora
            // 
            this.txtFechaHora.Location = new System.Drawing.Point(324, 191);
            this.txtFechaHora.Name = "txtFechaHora";
            this.txtFechaHora.ReadOnly = true;
            this.txtFechaHora.Size = new System.Drawing.Size(137, 20);
            this.txtFechaHora.TabIndex = 22;
            // 
            // txtMontoTotal
            // 
            this.txtMontoTotal.Location = new System.Drawing.Point(611, 191);
            this.txtMontoTotal.Name = "txtMontoTotal";
            this.txtMontoTotal.ReadOnly = true;
            this.txtMontoTotal.Size = new System.Drawing.Size(137, 20);
            this.txtMontoTotal.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(608, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Monto Total :";
            // 
            // txtProveedor
            // 
            this.txtProveedor.Location = new System.Drawing.Point(28, 122);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.ReadOnly = true;
            this.txtProveedor.Size = new System.Drawing.Size(137, 20);
            this.txtProveedor.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Proveedor :";
            // 
            // txtNumeroCompra
            // 
            this.txtNumeroCompra.Location = new System.Drawing.Point(611, 122);
            this.txtNumeroCompra.Name = "txtNumeroCompra";
            this.txtNumeroCompra.ReadOnly = true;
            this.txtNumeroCompra.Size = new System.Drawing.Size(137, 20);
            this.txtNumeroCompra.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(608, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "N° de Compra :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 47);
            this.label1.TabIndex = 14;
            this.label1.Text = "DETALLE DE COMPRA";
            // 
            // Producto
            // 
            this.Producto.HeaderText = "Producto";
            this.Producto.Name = "Producto";
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            // 
            // PrecioCompra
            // 
            this.PrecioCompra.HeaderText = "P. Compra";
            this.PrecioCompra.Name = "PrecioCompra";
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.HeaderText = "P. Venta";
            this.PrecioVenta.Name = "PrecioVenta";
            // 
            // SubTotal
            // 
            this.SubTotal.HeaderText = "Subtotal";
            this.SubTotal.Name = "SubTotal";
            // 
            // mdDetalleCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(814, 606);
            this.Controls.Add(this.txtRegistradoPor);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgvDetalleCompra);
            this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFechaHora);
            this.Controls.Add(this.txtMontoTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProveedor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNumeroCompra);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mdDetalleCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mdDetalleCompra";
            this.Load += new System.EventHandler(this.mdDetalleCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleCompra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnContinuar;
        private System.Windows.Forms.DataGridView dgvDetalleCompra;
        private System.Windows.Forms.TextBox txtRegistradoPor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFechaHora;
        private System.Windows.Forms.TextBox txtMontoTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNumeroCompra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
    }
}