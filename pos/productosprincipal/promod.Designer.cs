namespace POS.productosprincipal
{
    partial class promod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(promod));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.barcode = new System.Windows.Forms.TextBox();
            this.descripcion = new System.Windows.Forms.RichTextBox();
            this.familia = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.impuesto = new System.Windows.Forms.ComboBox();
            this.precio = new System.Windows.Forms.TextBox();
            this.cantidad = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(395, 558);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // barcode
            // 
            this.barcode.Location = new System.Drawing.Point(25, 23);
            this.barcode.Name = "barcode";
            this.barcode.Size = new System.Drawing.Size(214, 20);
            this.barcode.TabIndex = 1;
            this.barcode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // descripcion
            // 
            this.descripcion.Location = new System.Drawing.Point(441, 49);
            this.descripcion.Name = "descripcion";
            this.descripcion.Size = new System.Drawing.Size(243, 165);
            this.descripcion.TabIndex = 2;
            this.descripcion.Text = "";
            // 
            // familia
            // 
            this.familia.Location = new System.Drawing.Point(441, 394);
            this.familia.Name = "familia";
            this.familia.Size = new System.Drawing.Size(243, 20);
            this.familia.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(548, 464);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 72);
            this.button1.TabIndex = 11;
            this.button1.Text = "Guardar cambios";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // impuesto
            // 
            this.impuesto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.impuesto.FormattingEnabled = true;
            this.impuesto.Items.AddRange(new object[] {
            "Impuesto",
            "Sin Impuesto"});
            this.impuesto.Location = new System.Drawing.Point(441, 351);
            this.impuesto.Name = "impuesto";
            this.impuesto.Size = new System.Drawing.Size(243, 21);
            this.impuesto.TabIndex = 13;
            // 
            // precio
            // 
            this.precio.Location = new System.Drawing.Point(441, 240);
            this.precio.Name = "precio";
            this.precio.Size = new System.Drawing.Size(243, 20);
            this.precio.TabIndex = 3;
            // 
            // cantidad
            // 
            this.cantidad.DecimalPlaces = 20;
            this.cantidad.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.cantidad.Location = new System.Drawing.Point(441, 292);
            this.cantidad.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.cantidad.Name = "cantidad";
            this.cantidad.Size = new System.Drawing.Size(243, 20);
            this.cantidad.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Código de barras";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(438, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Descripción";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(438, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Precio";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(438, 276);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Cantidad actual";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(438, 335);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Impuesto";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(438, 378);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Familia";
            // 
            // promod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(94)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(736, 618);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cantidad);
            this.Controls.Add(this.impuesto);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.familia);
            this.Controls.Add(this.precio);
            this.Controls.Add(this.descripcion);
            this.Controls.Add(this.barcode);
            this.Controls.Add(this.dataGridView1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "promod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "promod";
            this.Load += new System.EventHandler(this.promod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox barcode;
        private System.Windows.Forms.RichTextBox descripcion;
        private System.Windows.Forms.TextBox familia;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox impuesto;
        private System.Windows.Forms.TextBox precio;
        private System.Windows.Forms.NumericUpDown cantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}