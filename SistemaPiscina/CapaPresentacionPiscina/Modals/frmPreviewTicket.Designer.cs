namespace CapaPresentacionPiscina.Modals
{
    partial class frmPreviewTicket
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
            this.rtbTicketPreview = new System.Windows.Forms.RichTextBox();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.btnConfirmar = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // rtbTicketPreview
            // 
            this.rtbTicketPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtbTicketPreview.Location = new System.Drawing.Point(0, 0);
            this.rtbTicketPreview.Name = "rtbTicketPreview";
            this.rtbTicketPreview.ReadOnly = true;
            this.rtbTicketPreview.Size = new System.Drawing.Size(384, 573);
            this.rtbTicketPreview.TabIndex = 0;
            this.rtbTicketPreview.Text = "";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btnCancelar.IconColor = System.Drawing.Color.Black;
            this.btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancelar.IconSize = 25;
            this.btnCancelar.Location = new System.Drawing.Point(12, 617);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(162, 32);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnConfirmar.IconColor = System.Drawing.Color.Black;
            this.btnConfirmar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnConfirmar.IconSize = 26;
            this.btnConfirmar.Location = new System.Drawing.Point(210, 617);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(162, 32);
            this.btnConfirmar.TabIndex = 16;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // frmPreviewTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 661);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.rtbTicketPreview);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmPreviewTicket";
            this.Text = "Vista Previa del Ticket";
            this.Load += new System.EventHandler(this.frmPreviewTicket_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbTicketPreview;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnConfirmar;
    }
}