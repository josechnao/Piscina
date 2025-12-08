namespace CapaPresentacionPiscina.Menus
{
    partial class frmReporteCompras
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
            this.btnExportar = new FontAwesome.Sharp.IconButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSumaTotal = new System.Windows.Forms.TextBox();
            this.dgvCompras = new System.Windows.Forms.DataGridView();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cboProveedor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroDocumento = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNumeroCompra = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.IdCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroCorrelativo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsuarioNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnVerDetalle = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.LimeGreen;
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.IconChar = FontAwesome.Sharp.IconChar.FilePdf;
            this.btnExportar.IconColor = System.Drawing.Color.White;
            this.btnExportar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExportar.IconSize = 16;
            this.btnExportar.Location = new System.Drawing.Point(791, 644);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(129, 26);
            this.btnExportar.TabIndex = 63;
            this.btnExportar.Text = "Exportar PDF";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(567, 645);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 17);
            this.label5.TabIndex = 61;
            this.label5.Text = "Suma Total :";
            // 
            // txtSumaTotal
            // 
            this.txtSumaTotal.Location = new System.Drawing.Point(655, 644);
            this.txtSumaTotal.Name = "txtSumaTotal";
            this.txtSumaTotal.ReadOnly = true;
            this.txtSumaTotal.Size = new System.Drawing.Size(100, 20);
            this.txtSumaTotal.TabIndex = 62;
            // 
            // dgvCompras
            // 
            this.dgvCompras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdCompra,
            this.NumeroCorrelativo,
            this.Proveedor,
            this.DocumentoProveedor,
            this.NumeroDocumento,
            this.Fecha,
            this.TotalCompra,
            this.UsuarioNombre,
            this.btnVerDetalle});
            this.dgvCompras.Location = new System.Drawing.Point(20, 232);
            this.dgvCompras.Name = "dgvCompras";
            this.dgvCompras.ReadOnly = true;
            this.dgvCompras.Size = new System.Drawing.Size(922, 397);
            this.dgvCompras.TabIndex = 60;
            this.dgvCompras.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCompras_CellContentClick);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiar.IconColor = System.Drawing.Color.Black;
            this.btnLimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiar.IconSize = 26;
            this.btnLimpiar.Location = new System.Drawing.Point(849, 75);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(51, 43);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscar.IconColor = System.Drawing.Color.Black;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.IconSize = 26;
            this.btnBuscar.Location = new System.Drawing.Point(849, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(51, 43);
            this.btnBuscar.TabIndex = 19;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHasta.Location = new System.Drawing.Point(688, 72);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(126, 23);
            this.dtpHasta.TabIndex = 5;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDesde.Location = new System.Drawing.Point(550, 72);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(126, 23);
            this.dtpDesde.TabIndex = 4;
            // 
            // cboProveedor
            // 
            this.cboProveedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboProveedor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProveedor.FormattingEnabled = true;
            this.cboProveedor.Location = new System.Drawing.Point(201, 72);
            this.cboProveedor.Name = "cboProveedor";
            this.cboProveedor.Size = new System.Drawing.Size(134, 23);
            this.cboProveedor.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(685, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Hasta :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(547, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Desde :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNumeroCompra);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtNumeroDocumento);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dtpHasta);
            this.groupBox1.Controls.Add(this.dtpDesde);
            this.groupBox1.Controls.Add(this.cboProveedor);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(922, 130);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(373, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "N Documento :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(411, 47);
            this.label1.TabIndex = 58;
            this.label1.Text = "REPORTE DE COMPRAS";
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroDocumento.Location = new System.Drawing.Point(376, 72);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Size = new System.Drawing.Size(125, 23);
            this.txtNumeroDocumento.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(198, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 17);
            this.label6.TabIndex = 22;
            this.label6.Text = "Proveedor :";
            // 
            // txtNumeroCompra
            // 
            this.txtNumeroCompra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroCompra.Location = new System.Drawing.Point(34, 72);
            this.txtNumeroCompra.Name = "txtNumeroCompra";
            this.txtNumeroCompra.Size = new System.Drawing.Size(125, 23);
            this.txtNumeroCompra.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(31, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "N Compra :";
            // 
            // IdCompra
            // 
            this.IdCompra.HeaderText = "IdCompra";
            this.IdCompra.Name = "IdCompra";
            this.IdCompra.ReadOnly = true;
            this.IdCompra.Visible = false;
            // 
            // NumeroCorrelativo
            // 
            this.NumeroCorrelativo.HeaderText = "N° Compra";
            this.NumeroCorrelativo.Name = "NumeroCorrelativo";
            this.NumeroCorrelativo.ReadOnly = true;
            // 
            // Proveedor
            // 
            this.Proveedor.HeaderText = "Proveedor";
            this.Proveedor.Name = "Proveedor";
            this.Proveedor.ReadOnly = true;
            // 
            // DocumentoProveedor
            // 
            this.DocumentoProveedor.HeaderText = "Documento";
            this.DocumentoProveedor.Name = "DocumentoProveedor";
            this.DocumentoProveedor.ReadOnly = true;
            // 
            // NumeroDocumento
            // 
            this.NumeroDocumento.HeaderText = "N° Doc. Compra";
            this.NumeroDocumento.Name = "NumeroDocumento";
            this.NumeroDocumento.ReadOnly = true;
            this.NumeroDocumento.Visible = false;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // TotalCompra
            // 
            this.TotalCompra.HeaderText = "Total";
            this.TotalCompra.Name = "TotalCompra";
            this.TotalCompra.ReadOnly = true;
            // 
            // UsuarioNombre
            // 
            this.UsuarioNombre.HeaderText = "Registrado por";
            this.UsuarioNombre.Name = "UsuarioNombre";
            this.UsuarioNombre.ReadOnly = true;
            // 
            // btnVerDetalle
            // 
            this.btnVerDetalle.HeaderText = "Ver Detalle";
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.ReadOnly = true;
            this.btnVerDetalle.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnVerDetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // frmReporteCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(954, 681);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSumaTotal);
            this.Controls.Add(this.dgvCompras);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReporteCompras";
            this.Text = "frmReporteCompras";
            this.Load += new System.EventHandler(this.frmReporteCompras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnExportar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSumaTotal;
        private System.Windows.Forms.DataGridView dgvCompras;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.ComboBox cboProveedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumeroDocumento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNumeroCompra;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroCorrelativo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsuarioNombre;
        private System.Windows.Forms.DataGridViewButtonColumn btnVerDetalle;
    }
}