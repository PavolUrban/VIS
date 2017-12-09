using Connective;
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
    public partial class EditProfile : Form
    {
        ErrorProvider errorProvider = new ErrorProvider();
        Bookmaker b = Autorization.Instance.GetCurrentBookmaker();

        public EditProfile()
        {
            InitializeComponent();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)

        {
            if (GetData())
            {
                BookmakerGateway.Update(b);
                MessageBox.Show("Profil upravený!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }

            else
            {
                MessageBox.Show("Profil sa nepodarilo upraviť!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }





        private bool GetData()
        {
            bool ret = true;
           
            errorProvider.Clear();

            


            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                errorProvider.SetError(textBox1, "Nevyplnene pole meno.");
                ret = false;
            }
            else
            {
                b.meno = textBox1.Text;
            }


            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                errorProvider.SetError(textBox2, "Nevyplnene priezvisko.");
                ret = false;
            }
            else
            {
                b.priezvisko = textBox2.Text;
            }



            if (Functions.IsValidEmail(textBox3.Text))
            {
                b.email = textBox3.Text;
            }

            else
            {
                ret = false;
                MessageBox.Show("You entered invalid mail address!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }





            return ret;
        }







        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
