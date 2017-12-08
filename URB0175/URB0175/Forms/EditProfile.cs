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

       
        public EditProfile()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)

        {
            Bookmaker b = Autorization.Instance.GetCurrentBookmaker();
            b.meno = textBox1.Text;
            b.priezvisko = textBox2.Text;
            b.email = textBox3.Text;

            BookmakerGateway.Update(b);


        }





        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
