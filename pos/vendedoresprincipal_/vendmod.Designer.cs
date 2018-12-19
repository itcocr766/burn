namespace POS.vendedoresprincipal_
{
    partial class vendmod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(vendmod));
            this.direccionn = new System.Windows.Forms.Label();
            this.direccion = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.codigo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.correo = new System.Windows.Forms.TextBox();
            this.telefono = new System.Windows.Forms.TextBox();
            this.cod = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // direccionn
            // 
            this.direccionn.AutoSize = true;
            this.direccionn.ForeColor = System.Drawing.Color.White;
            this.direccionn.Location = new System.Drawing.Point(428, 63);
            this.direccionn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.direccionn.Name = "direccionn";
            this.direccionn.Size = new System.Drawing.Size(52, 13);
            this.direccionn.TabIndex = 57;
            this.direccionn.Text = "Dirección";
            // 
            // direccion
            // 
            this.direccion.Location = new System.Drawing.Point(431, 79);
            this.direccion.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.direccion.Name = "direccion";
            this.direccion.Size = new System.Drawing.Size(243, 110);
            this.direccion.TabIndex = 56;
            this.direccion.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(445, 363);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Correo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(445, 292);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Teléfono";
            // 
            // codigo
            // 
            this.codigo.AutoSize = true;
            this.codigo.ForeColor = System.Drawing.Color.White;
            this.codigo.Location = new System.Drawing.Point(26, 8);
            this.codigo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.codigo.Name = "codigo";
            this.codigo.Size = new System.Drawing.Size(40, 13);
            this.codigo.TabIndex = 53;
            this.codigo.Text = "Código";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(562, 473);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 72);
            this.button1.TabIndex = 52;
            this.button1.Text = "Guardar cambios";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // correo
            // 
            this.correo.Location = new System.Drawing.Point(448, 379);
            this.correo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.correo.Name = "correo";
            this.correo.Size = new System.Drawing.Size(243, 20);
            this.correo.TabIndex = 51;
            // 
            // telefono
            // 
            this.telefono.Location = new System.Drawing.Point(448, 308);
            this.telefono.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.telefono.Name = "telefono";
            this.telefono.Size = new System.Drawing.Size(243, 20);
            this.telefono.TabIndex = 50;
            // 
            // cod
            // 
            this.cod.Location = new System.Drawing.Point(30, 24);
            this.cod.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cod.Name = "cod";
            this.cod.Size = new System.Drawing.Size(214, 20);
            this.cod.TabIndex = 49;
            this.cod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cod_KeyDown);
            this.cod.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cod_KeyUp);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 50);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(395, 558);
            this.dataGridView1.TabIndex = 48;
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            this.dataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(445, 220);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Nombre";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(448, 236);
            this.nombre.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(243, 20);
            this.nombre.TabIndex = 58;
            // 
            // vendmod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(94)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(725, 617);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.direccionn);
            this.Controls.Add(this.direccion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.codigo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.correo);
            this.Controls.Add(this.telefono);
            this.Controls.Add(this.cod);
            this.Controls.Add(this.dataGridView1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "vendmod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "vendmod";
            this.Load += new System.EventHandler(this.vendmod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label direccionn;
        private System.Windows.Forms.RichTextBox direccion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label codigo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox correo;
        private System.Windows.Forms.TextBox telefono;
        private System.Windows.Forms.TextBox cod;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nombre;
    }
}