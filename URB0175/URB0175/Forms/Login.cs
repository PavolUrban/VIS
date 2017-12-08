using Connective;
using Connective.Tables;
using Connective.TablesGateway;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URB0175.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }




        
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime value = new DateTime(2017, 1, 18);
            Bookmaker boookmaker = new Bookmaker(0,"meno" , "priezvisko", value , textBox1.Text, value ,"pohlavie",textBox2.Text);
            Collection<Bookmaker> bookmakers = BookmakerGateway.Select();

            if (Functions.IsValidEmail(textBox1.Text))
            {
                if (textBox2.Text.Length > 4)
                {
                    if (BookmakerGateway.CheckPassword(boookmaker))
                    {
                        foreach (Bookmaker bm in bookmakers)
                        {
                            if (bm.email.IndexOf(textBox1.Text) > -1 && bm.heslo.IndexOf(textBox2.Text) > -1 && bm.email.Length==textBox1.Text.Length && bm.heslo.Length == textBox2.Text.Length)
                            {
                                this.Dispose();
                                Autorization.Instance.SetCurrentBookmaker(bm);
                            }
                        }   
                        
                    }
                    else
                    {
                        MessageBox.Show("You enetered wrong credentials, try again!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Password is too short!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            

            }

           
            else
            {
                MessageBox.Show("You entered invalid mail address!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
