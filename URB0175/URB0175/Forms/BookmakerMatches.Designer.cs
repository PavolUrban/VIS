namespace URB0175.Forms
{
    partial class BookmakerMatches
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.domaci = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hostia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kurzD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kurzR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kurzH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.domaci,
            this.hostia,
            this.kurzD,
            this.kurzR,
            this.kurzH});
            this.dataGridView1.Location = new System.Drawing.Point(0, 1);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(543, 219);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // domaci
            // 
            this.domaci.DataPropertyName = "domaciTim";
            this.domaci.HeaderText = "Domáci tím";
            this.domaci.Name = "domaci";
            // 
            // hostia
            // 
            this.hostia.DataPropertyName = "hostujuciTim";
            this.hostia.HeaderText = "Hosťujúci tím";
            this.hostia.Name = "hostia";
            // 
            // kurzD
            // 
            this.kurzD.DataPropertyName = "kurzDomaci";
            this.kurzD.HeaderText = "Kurz domáci";
            this.kurzD.Name = "kurzD";
            // 
            // kurzR
            // 
            this.kurzR.DataPropertyName = "kurzRemiza";
            this.kurzR.HeaderText = "Kurz remíza";
            this.kurzR.Name = "kurzR";
            // 
            // kurzH
            // 
            this.kurzH.DataPropertyName = "kurzHostia";
            this.kurzH.HeaderText = "Kurz hostia";
            this.kurzH.Name = "kurzH";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(351, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Zápasov na vyhodnotenie: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(494, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(442, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Vyhodnocovať";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(354, 272);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Ukončiť";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BookmakerMatches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 307);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "BookmakerMatches";
            this.Text = "BookmakerMatches";
            this.Load += new System.EventHandler(this.BookmakerMatches_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn domaci;
        private System.Windows.Forms.DataGridViewTextBoxColumn hostia;
        private System.Windows.Forms.DataGridViewTextBoxColumn kurzD;
        private System.Windows.Forms.DataGridViewTextBoxColumn kurzR;
        private System.Windows.Forms.DataGridViewTextBoxColumn kurzH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}