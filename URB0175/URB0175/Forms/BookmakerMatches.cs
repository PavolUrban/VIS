using Connective.Factory;
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
    public partial class BookmakerMatches : Form
    {
        public BookmakerMatches()
        {
            InitializeComponent();
            UnfinishedMatches();
        }



       



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        private void BindData()
        {
            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zap = (ZapasyGateway<Zapasy>)zF.GetZapasy();
            Collection<PomocnyObjekt4> mojeZapasy = zap.SelectBookmakersMatches(Autorization.Instance.getId());
            BindingList<PomocnyObjekt4> bindingList = new BindingList<PomocnyObjekt4>(mojeZapasy);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingList;
        }




        private void UnfinishedMatches()
        {
            int numberUnfinished = 0;
            Console.Write( "Ty si prihlaseny " + Autorization.Instance.getId());
            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zap = (ZapasyGateway<Zapasy>)zF.GetZapasy();
            numberUnfinished = zap.UnfinishedBookmakersMatches(Autorization.Instance.getId());
            Console.Write("Tu chceme cislo " + "Ty si prihlaseny "+Autorization.Instance.getId());
            label2.Text = numberUnfinished.ToString();
        }

        private void BookmakerMatches_Load(object sender, EventArgs e)
        {
            BindData();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            AddResult ar = new AddResult();
            ar.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
