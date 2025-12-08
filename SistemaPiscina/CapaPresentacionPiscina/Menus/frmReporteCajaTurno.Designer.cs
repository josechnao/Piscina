namespace CapaPresentacionPiscina.Menus
{
    partial class frmReporteCajaTurno
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
            this.txtTotalTurnos = new System.Windows.Forms.TextBox();
            this.dgvTurnos = new System.Windows.Forms.DataGridView();
            this.btnLimpiar = new FontAwesome.Sharp.IconButton();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cboCajero = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Cajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCajaTurno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaApertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaCierre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalVentas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalGastos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Diferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Observacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDetalle = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).BeginInit();
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
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(513, 644);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 17);
            this.label5.TabIndex = 61;
            this.label5.Text = "Turnos Encontrados :";
            // 
            // txtTotalTurnos
            // 
            this.txtTotalTurnos.Location = new System.Drawing.Point(655, 644);
            this.txtTotalTurnos.Name = "txtTotalTurnos";
            this.txtTotalTurnos.ReadOnly = true;
            this.txtTotalTurnos.Size = new System.Drawing.Size(100, 20);
            this.txtTotalTurnos.TabIndex = 62;
            // 
            // dgvTurnos
            // 
            this.dgvTurnos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTurnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurnos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cajero,
            this.IdCajaTurno,
            this.FechaApertura,
            this.FechaCierre,
            this.MontoInicial,
            this.MontoFinal,
            this.TotalVentas,
            this.TotalGastos,
            this.Diferencia,
            this.Observacion,
            this.btnDetalle});
            this.dgvTurnos.Location = new System.Drawing.Point(20, 232);
            this.dgvTurnos.Name = "dgvTurnos";
            this.dgvTurnos.ReadOnly = true;
            this.dgvTurnos.Size = new System.Drawing.Size(922, 397);
            this.dgvTurnos.TabIndex = 60;
            this.dgvTurnos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTurnos_CellContentClick);
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
            this.dtpHasta.Location = new System.Drawing.Point(588, 72);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(147, 23);
            this.dtpHasta.TabIndex = 5;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDesde.Location = new System.Drawing.Point(355, 75);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(139, 23);
            this.dtpDesde.TabIndex = 4;
            // 
            // cboCajero
            // 
            this.cboCajero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCajero.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCajero.FormattingEnabled = true;
            this.cboCajero.Location = new System.Drawing.Point(23, 75);
            this.cboCajero.Name = "cboCajero";
            this.cboCajero.Size = new System.Drawing.Size(155, 23);
            this.cboCajero.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(585, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Hasta :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(352, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Desde :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.dtpHasta);
            this.groupBox1.Controls.Add(this.dtpDesde);
            this.groupBox1.Controls.Add(this.cboCajero);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(922, 130);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtrado de Ventas";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "Cajero :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(391, 47);
            this.label1.TabIndex = 58;
            this.label1.Text = "REPORTE DE CAJEROS";
            // 
            // Cajero
            // 
            this.Cajero.DataPropertyName = "Cajero";
            this.Cajero.HeaderText = "Cajero";
            this.Cajero.Name = "Cajero";
            this.Cajero.ReadOnly = true;
            // 
            // IdCajaTurno
            // 
            this.IdCajaTurno.DataPropertyName = "IdCajaTurno";
            this.IdCajaTurno.HeaderText = "Turno";
            this.IdCajaTurno.Name = "IdCajaTurno";
            this.IdCajaTurno.ReadOnly = true;
            // 
            // FechaApertura
            // 
            this.FechaApertura.DataPropertyName = "FechaApertura";
            this.FechaApertura.HeaderText = "Apertura";
            this.FechaApertura.Name = "FechaApertura";
            this.FechaApertura.ReadOnly = true;
            // 
            // FechaCierre
            // 
            this.FechaCierre.DataPropertyName = "FechaCierre";
            this.FechaCierre.HeaderText = "Cierre";
            this.FechaCierre.Name = "FechaCierre";
            this.FechaCierre.ReadOnly = true;
            // 
            // MontoInicial
            // 
            this.MontoInicial.DataPropertyName = "MontoInicial";
            this.MontoInicial.HeaderText = "Inicial";
            this.MontoInicial.Name = "MontoInicial";
            this.MontoInicial.ReadOnly = true;
            // 
            // MontoFinal
            // 
            this.MontoFinal.DataPropertyName = "MontoFinal";
            this.MontoFinal.HeaderText = "Final";
            this.MontoFinal.Name = "MontoFinal";
            this.MontoFinal.ReadOnly = true;
            // 
            // TotalVentas
            // 
            this.TotalVentas.DataPropertyName = "TotalVentas";
            this.TotalVentas.HeaderText = "Ventas";
            this.TotalVentas.Name = "TotalVentas";
            this.TotalVentas.ReadOnly = true;
            // 
            // TotalGastos
            // 
            this.TotalGastos.DataPropertyName = "TotalGastos";
            this.TotalGastos.HeaderText = "Gastos";
            this.TotalGastos.Name = "TotalGastos";
            this.TotalGastos.ReadOnly = true;
            // 
            // Diferencia
            // 
            this.Diferencia.DataPropertyName = "Diferencia";
            this.Diferencia.HeaderText = "Diferencia";
            this.Diferencia.Name = "Diferencia";
            this.Diferencia.ReadOnly = true;
            // 
            // Observacion
            // 
            this.Observacion.DataPropertyName = "Observacion";
            this.Observacion.HeaderText = "Observacion";
            this.Observacion.Name = "Observacion";
            this.Observacion.ReadOnly = true;
            // 
            // btnDetalle
            // 
            this.btnDetalle.HeaderText = "Ver";
            this.btnDetalle.Name = "btnDetalle";
            this.btnDetalle.ReadOnly = true;
            // 
            // frmReporteCajaTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(954, 681);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTotalTurnos);
            this.Controls.Add(this.dgvTurnos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReporteCajaTurno";
            this.Text = "frmReporteCajaTurno";
            this.Load += new System.EventHandler(this.frmReporteCajaTurno_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnExportar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalTurnos;
        private System.Windows.Forms.DataGridView dgvTurnos;
        private FontAwesome.Sharp.IconButton btnLimpiar;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.ComboBox cboCajero;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCajaTurno;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaApertura;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaCierre;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalVentas;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalGastos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Diferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observacion;
        private System.Windows.Forms.DataGridViewButtonColumn btnDetalle;
    }
}