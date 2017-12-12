using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using Connective.Tables;
using Connective.TablesGateway;
using System.Collections.ObjectModel;

namespace URB0175.Forms
{
    public partial class BookmakerProfil : Form
    {
   
        public BookmakerProfil()
        {
            InitializeComponent();
        }

        private void BookmakerProfil_Load(object sender, EventArgs e)
        {
            BindData();
        }



        private void BindData() {
            Bookmaker b = BookmakerGateway.Select2(Autorization.Instance.getId());
            Collection<Bookmaker> z = new Collection<Bookmaker>();
            z.Add(b);
            BindingList<Bookmaker> bindingList = new BindingList<Bookmaker>(z);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingList;
        }





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
            EditProfile ep = new EditProfile();
            ep.Show();
            this.Close();
            
        }


        private Bookmaker GetSelectedBookmaker()
        {
          
            if (dataGridView1.SelectedRows.Count == 1)
            {
                Bookmaker eb = dataGridView1.SelectedRows[0].DataBoundItem as Bookmaker;
                Bookmaker b = new Bookmaker();
                b.meno = eb.meno;
                b.priezvisko = eb.priezvisko;
                b.email = eb.email;
                return b;
            }
            else
            {
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


    class ExtendedBookmaker
    {
        public string Meno { get; set; }
        public string Priezvisko { get; set; }
        public string Email { get; set; }
    }
}
