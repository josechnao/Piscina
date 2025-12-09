namespace CapaPresentacionPiscina.Modals
{
    partial class frmDetalleCajaTurno
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
            this.txtCajero = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFinal = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtInicial = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtGastosSuma = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCierre = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtVentasSuma = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtApertura = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGastosCant = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIdTurno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMetodosPago = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDiferencia = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVentasCant = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDetalleGasto = new System.Windows.Forms.DataGridView();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaRegistroGasto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.IdVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NroTicket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MetodoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleGasto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
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
            this.btnContinuar.Location = new System.Drawing.Point(821, 717);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(191, 43);
            this.btnContinuar.TabIndex = 25;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnContinuar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnContinuar.UseVisualStyleBackColor = false;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // txtCajero
            // 
            this.txtCajero.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCajero.Location = new System.Drawing.Point(19, 47);
            this.txtCajero.Name = "txtCajero";
            this.txtCajero.ReadOnly = true;
            this.txtCajero.Size = new System.Drawing.Size(137, 25);
            this.txtCajero.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Cajero :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFinal);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtInicial);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtGastosSuma);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtCierre);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtObservacion);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtVentasSuma);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtApertura);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtGastosCant);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtIdTurno);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMetodosPago);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDiferencia);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtVentasCant);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCajero);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(31, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(978, 215);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Caja";
            // 
            // txtFinal
            // 
            this.txtFinal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFinal.Location = new System.Drawing.Point(223, 105);
            this.txtFinal.Name = "txtFinal";
            this.txtFinal.ReadOnly = true;
            this.txtFinal.Size = new System.Drawing.Size(137, 25);
            this.txtFinal.TabIndex = 37;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(220, 85);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 17);
            this.label14.TabIndex = 36;
            this.label14.Text = "Final :";
            // 
            // txtInicial
            // 
            this.txtInicial.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInicial.Location = new System.Drawing.Point(19, 105);
            this.txtInicial.Name = "txtInicial";
            this.txtInicial.ReadOnly = true;
            this.txtInicial.Size = new System.Drawing.Size(137, 25);
            this.txtInicial.TabIndex = 35;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(16, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 17);
            this.label13.TabIndex = 34;
            this.label13.Text = "Inicial :";
            // 
            // txtGastosSuma
            // 
            this.txtGastosSuma.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGastosSuma.Location = new System.Drawing.Point(223, 167);
            this.txtGastosSuma.Name = "txtGastosSuma";
            this.txtGastosSuma.ReadOnly = true;
            this.txtGastosSuma.Size = new System.Drawing.Size(137, 25);
            this.txtGastosSuma.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(220, 147);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 17);
            this.label12.TabIndex = 32;
            this.label12.Text = "Gastos Suma :";
            // 
            // txtCierre
            // 
            this.txtCierre.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCierre.Location = new System.Drawing.Point(598, 45);
            this.txtCierre.Name = "txtCierre";
            this.txtCierre.ReadOnly = true;
            this.txtCierre.Size = new System.Drawing.Size(137, 25);
            this.txtCierre.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(595, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 17);
            this.label11.TabIndex = 30;
            this.label11.Text = "Cierre :";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacion.Location = new System.Drawing.Point(811, 47);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.ReadOnly = true;
            this.txtObservacion.Size = new System.Drawing.Size(158, 145);
            this.txtObservacion.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(808, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 17);
            this.label10.TabIndex = 28;
            this.label10.Text = "Observación :";
            // 
            // txtVentasSuma
            // 
            this.txtVentasSuma.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVentasSuma.Location = new System.Drawing.Point(19, 167);
            this.txtVentasSuma.Name = "txtVentasSuma";
            this.txtVentasSuma.ReadOnly = true;
            this.txtVentasSuma.Size = new System.Drawing.Size(137, 25);
            this.txtVentasSuma.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 147);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 17);
            this.label9.TabIndex = 26;
            this.label9.Text = "Ventas Suma :";
            // 
            // txtApertura
            // 
            this.txtApertura.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApertura.Location = new System.Drawing.Point(414, 45);
            this.txtApertura.Name = "txtApertura";
            this.txtApertura.ReadOnly = true;
            this.txtApertura.Size = new System.Drawing.Size(137, 25);
            this.txtApertura.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(411, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "Apertura :";
            // 
            // txtGastosCant
            // 
            this.txtGastosCant.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGastosCant.Location = new System.Drawing.Point(598, 105);
            this.txtGastosCant.Name = "txtGastosCant";
            this.txtGastosCant.ReadOnly = true;
            this.txtGastosCant.Size = new System.Drawing.Size(137, 25);
            this.txtGastosCant.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(595, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 17);
            this.label7.TabIndex = 22;
            this.label7.Text = "Gastos (cant) :";
            // 
            // txtIdTurno
            // 
            this.txtIdTurno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdTurno.Location = new System.Drawing.Point(223, 45);
            this.txtIdTurno.Name = "txtIdTurno";
            this.txtIdTurno.ReadOnly = true;
            this.txtIdTurno.Size = new System.Drawing.Size(137, 25);
            this.txtIdTurno.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(220, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Id Turno :";
            // 
            // txtMetodosPago
            // 
            this.txtMetodosPago.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMetodosPago.Location = new System.Drawing.Point(598, 167);
            this.txtMetodosPago.Name = "txtMetodosPago";
            this.txtMetodosPago.ReadOnly = true;
            this.txtMetodosPago.Size = new System.Drawing.Size(196, 25);
            this.txtMetodosPago.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(595, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Métodos de Pago :";
            // 
            // txtDiferencia
            // 
            this.txtDiferencia.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiferencia.Location = new System.Drawing.Point(414, 167);
            this.txtDiferencia.Name = "txtDiferencia";
            this.txtDiferencia.ReadOnly = true;
            this.txtDiferencia.Size = new System.Drawing.Size(137, 25);
            this.txtDiferencia.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(411, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Diferencia :";
            // 
            // txtVentasCant
            // 
            this.txtVentasCant.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVentasCant.Location = new System.Drawing.Point(414, 105);
            this.txtVentasCant.Name = "txtVentasCant";
            this.txtVentasCant.ReadOnly = true;
            this.txtVentasCant.Size = new System.Drawing.Size(137, 25);
            this.txtVentasCant.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(411, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Ventas (cant) :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 47);
            this.label1.TabIndex = 14;
            this.label1.Text = "DETALLE CAJA TURNO";
            // 
            // dgvDetalleGasto
            // 
            this.dgvDetalleGasto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleGasto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleGasto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Categoria,
            this.Descripcion,
            this.Monto,
            this.FechaRegistroGasto,
            this.Estado,
            this.EstadoDescripcion});
            this.dgvDetalleGasto.Location = new System.Drawing.Point(31, 534);
            this.dgvDetalleGasto.Name = "dgvDetalleGasto";
            this.dgvDetalleGasto.Size = new System.Drawing.Size(981, 167);
            this.dgvDetalleGasto.TabIndex = 26;
            // 
            // Categoria
            // 
            this.Categoria.DataPropertyName = "Categoria";
            this.Categoria.HeaderText = "Categoria";
            this.Categoria.Name = "Categoria";
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            // 
            // Monto
            // 
            this.Monto.DataPropertyName = "Monto";
            this.Monto.HeaderText = "Monto (Bs)";
            this.Monto.Name = "Monto";
            // 
            // FechaRegistroGasto
            // 
            this.FechaRegistroGasto.DataPropertyName = "FechaRegistro";
            this.FechaRegistroGasto.HeaderText = "Fecha / Hora";
            this.FechaRegistroGasto.Name = "FechaRegistroGasto";
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.Visible = false;
            // 
            // EstadoDescripcion
            // 
            this.EstadoDescripcion.HeaderText = "Estado";
            this.EstadoDescripcion.Name = "EstadoDescripcion";
            // 
            // dgvDetalleVenta
            // 
            this.dgvDetalleVenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalleVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleVenta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdVenta,
            this.NroTicket,
            this.MontoTotal,
            this.MetodoPago,
            this.FechaRegistro});
            this.dgvDetalleVenta.Location = new System.Drawing.Point(31, 319);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.Size = new System.Drawing.Size(981, 171);
            this.dgvDetalleVenta.TabIndex = 27;
            // 
            // IdVenta
            // 
            this.IdVenta.DataPropertyName = "IdVenta";
            this.IdVenta.HeaderText = "IdVenta";
            this.IdVenta.Name = "IdVenta";
            this.IdVenta.ReadOnly = true;
            this.IdVenta.Visible = false;
            // 
            // NroTicket
            // 
            this.NroTicket.DataPropertyName = "NroTicket";
            this.NroTicket.HeaderText = "Nro. Ticket";
            this.NroTicket.Name = "NroTicket";
            // 
            // MontoTotal
            // 
            this.MontoTotal.DataPropertyName = "MontoTotal";
            this.MontoTotal.HeaderText = "Total (Bs)";
            this.MontoTotal.Name = "MontoTotal";
            // 
            // MetodoPago
            // 
            this.MetodoPago.DataPropertyName = "MetodoPago";
            this.MetodoPago.HeaderText = "Método de Pago";
            this.MetodoPago.Name = "MetodoPago";
            // 
            // FechaRegistro
            // 
            this.FechaRegistro.DataPropertyName = "FechaRegistro";
            this.FechaRegistro.HeaderText = "Fecha / Hora";
            this.FechaRegistro.Name = "FechaRegistro";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(23, 291);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(184, 25);
            this.label15.TabIndex = 28;
            this.label15.Text = "DETALLE DE VENTA";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(26, 506);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(185, 25);
            this.label16.TabIndex = 29;
            this.label16.Text = "DETALLE DE GASTO";
            // 
            // frmDetalleCajaTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1028, 772);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.dgvDetalleVenta);
            this.Controls.Add(this.dgvDetalleGasto);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDetalleCajaTurno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDetalleCajaTurno";
            this.Load += new System.EventHandler(this.frmDetalleCajaTurno_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleGasto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnContinuar;
        private System.Windows.Forms.TextBox txtCajero;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInicial;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtGastosSuma;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCierre;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtVentasSuma;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtApertura;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGastosCant;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIdTurno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMetodosPago;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDiferencia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVentasCant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFinal;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dgvDetalleGasto;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn NroTicket;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn MetodoPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaRegistro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaRegistroGasto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoDescripcion;
    }
}