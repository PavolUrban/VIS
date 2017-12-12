using Connective.Factory;
using Connective.Tables;
using Connective.TablesGateway;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URB0175.Forms
{
    public partial class SetReesult : Form
    {
        int idZapasu=0;

        public SetReesult()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool OpenRecord(object primaryKey)
        {
            idZapasu = (int)primaryKey;
            label2.Text = idZapasu.ToString();   
            return true;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zap = (ZapasyGateway<Zapasy>)zF.GetZapasy();
            zap.UpdateResult(idZapasu, comboBox1.SelectedIndex );
            MessageBox.Show("Zápas vyhodnotený!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
        }
    }
}
