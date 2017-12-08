using Connective.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using URB0175.Forms;

namespace URB0175
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Forms.Login form = new Forms.Login();
            form.ShowDialog();
        }

        private void zapasyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mojProfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.Write("sfafa: " + Autorization.Instance.GetCurrentBookmaker().meno);
            BookmakerProfil b = new BookmakerProfil();
            b.Show();
        }
    }
}
