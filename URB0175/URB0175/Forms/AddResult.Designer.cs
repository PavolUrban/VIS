namespace URB0175.Forms
{
    partial class AddResult
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
            this.button1 = new System.Windows.Forms.Button();
            this.id_zapasu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_zapasu,
            this.timD,
            this.timH});
            this.dataGridView1.Location = new System.Drawing.Point(68, 32);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(435, 159);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ukončiť";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // id_zapasu
            // 
            this.id_zapasu.DataPropertyName = "ID_Zapasu";
            this.id_zapasu.HeaderText = "ID Zápasu";
            this.id_zapasu.Name = "id_zapasu";
            this.id_zapasu.ReadOnly = true;
            // 
            // timD
            // 
            this.timD.DataPropertyName = "domaci";
            this.timD.HeaderText = "Domáci tím";
            this.timD.Name = "timD";
            this.timD.ReadOnly = true;
            // 
            // timH
            // 
            this.timH.DataPropertyName = "hostia";
            this.timH.HeaderText = "Hosťujúci tím";
            this.timH.Name = "timH";
            this.timH.ReadOnly = true;
            // 
            // AddResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AddResult";
            this.Text = "AddResult";
            this.Load += new System.EventHandler(this.AddResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_zapasu;
        private System.Windows.Forms.DataGridViewTextBoxColumn timD;
        private System.Windows.Forms.DataGridViewTextBoxColumn timH;
    }
}