namespace URB0175
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.zapasyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pridaťZápasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vyhodnotiťZápasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mojeZápasyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mojProfilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.odhlásiťToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zapasyToolStripMenuItem,
            this.mojProfilToolStripMenuItem,
            this.odhlásiťToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // zapasyToolStripMenuItem
            // 
            this.zapasyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pridaťZápasToolStripMenuItem,
            this.vyhodnotiťZápasToolStripMenuItem,
            this.mojeZápasyToolStripMenuItem});
            this.zapasyToolStripMenuItem.Name = "zapasyToolStripMenuItem";
            this.zapasyToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.zapasyToolStripMenuItem.Text = "Zápasy";
            this.zapasyToolStripMenuItem.Click += new System.EventHandler(this.zapasyToolStripMenuItem_Click);
            // 
            // pridaťZápasToolStripMenuItem
            // 
            this.pridaťZápasToolStripMenuItem.Name = "pridaťZápasToolStripMenuItem";
            this.pridaťZápasToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.pridaťZápasToolStripMenuItem.Text = "Pridať zápas";
            this.pridaťZápasToolStripMenuItem.Click += new System.EventHandler(this.pridaťZápasToolStripMenuItem_Click);
            // 
            // vyhodnotiťZápasToolStripMenuItem
            // 
            this.vyhodnotiťZápasToolStripMenuItem.Name = "vyhodnotiťZápasToolStripMenuItem";
            this.vyhodnotiťZápasToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.vyhodnotiťZápasToolStripMenuItem.Text = "Vyhodnotiť zápas";
            this.vyhodnotiťZápasToolStripMenuItem.Click += new System.EventHandler(this.vyhodnotiťZápasToolStripMenuItem_Click);
            // 
            // mojeZápasyToolStripMenuItem
            // 
            this.mojeZápasyToolStripMenuItem.Name = "mojeZápasyToolStripMenuItem";
            this.mojeZápasyToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.mojeZápasyToolStripMenuItem.Text = "Moje zápasy";
            this.mojeZápasyToolStripMenuItem.Click += new System.EventHandler(this.mojeZápasyToolStripMenuItem_Click);
            // 
            // mojProfilToolStripMenuItem
            // 
            this.mojProfilToolStripMenuItem.Name = "mojProfilToolStripMenuItem";
            this.mojProfilToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.mojProfilToolStripMenuItem.Text = "Moj profil";
            this.mojProfilToolStripMenuItem.Click += new System.EventHandler(this.mojProfilToolStripMenuItem_Click);
            // 
            // odhlásiťToolStripMenuItem
            // 
            this.odhlásiťToolStripMenuItem.Name = "odhlásiťToolStripMenuItem";
            this.odhlásiťToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.odhlásiťToolStripMenuItem.Text = "Odhlásiť";
            this.odhlásiťToolStripMenuItem.Click += new System.EventHandler(this.odhlásiťToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem zapasyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mojProfilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pridaťZápasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vyhodnotiťZápasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mojeZápasyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem odhlásiťToolStripMenuItem;
    }
}

