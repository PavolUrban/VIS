using Connective.Factory;
using Connective.Tables;
using Connective.TablesGateway;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URB0175.Forms
{
    public partial class AddMatch : Form
    {
        private Zapasy z =new Zapasy() ;
        ErrorProvider errorProvider = new ErrorProvider();
        public AddMatch()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            string sport = comboBox1.Text;
            Collection<PomocnyObjekt2> z = ZapasyTabulka.MojVypisZapasov2(sport);
            BindingList<PomocnyObjekt2> bindingList = new BindingList<PomocnyObjekt2>(z);
            VypisZapasov.AutoGenerateColumns = false;
            VypisZapasov.DataSource = bindingList;
            */
        }

        protected void NewRecord()
        {
        }



        private bool GetData()
        {
            bool ret = true;
            double j;

            errorProvider.Clear();



            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider.SetError(textBox1, "Nevyplnene pole domaci tim.");
                ret = false;
            }
            else
            {
                z.domaci_tim = textBox1.Text;
            }


            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider.SetError(textBox2, "Nevyplnene pole hostujuci tim.");
                ret = false;
            }
            else
            {
                z.hostujuci_tim = textBox2.Text;
            }





            if (double.TryParse(textBox3.Text, out j))
            {
                z.kurz_domaci = j;
            }
            else
            {
                ret = false;
                errorProvider.SetError(textBox3, "Chybne vyplnené pole kurz domáci.");
            }

            if (double.TryParse(textBox4.Text, out j))
            {
                z.kurz_remiza = j;
            }

            else
            {
                ret = false;
                errorProvider.SetError(textBox4, "Chybne vyplnené pole kurz remíza.");
            }

            if (double.TryParse(textBox5.Text, out j))
            {
                z.kurz_hostia= j;
            }

            else
            {
                ret = false;
                errorProvider.SetError(textBox5, "Chybne vyplnené pole kurz hostia.");
            }



            if (comboBox1.SelectedIndex == 0)
            {
                z.id_sportu = 1;
            }

            else if (comboBox1.SelectedIndex == 1)
            {
                z.id_sportu = 2;
            }

            else if (comboBox1.SelectedIndex == 2)
            {
                z.id_sportu = 3;
            }

            else
            {
                z.id_sportu = 4;
            }


            z.id_bookmakera = Autorization.Instance.getId();



            return ret;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GetData())
            {
                CreateMatch();
                Console.Write("domaci " + z.domaci_tim + " hostia " + z.hostujuci_tim + " " + z.kurz_domaci + " " + z.kurz_remiza + " " + z.kurz_hostia +" sport "+ z.id_sportu+" bookmaker "+z.id_bookmakera);
                MessageBox.Show("Zápas bol úspešne pridaný!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.Close();
            }
            else
                MessageBox.Show("Zápas sa nepodarilo pridať!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }

        private void CreateMatch()
        {
            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zap = (ZapasyGateway<Zapasy>)zF.GetZapasy();
            zap.Insert(z);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
