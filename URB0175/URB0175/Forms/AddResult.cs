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
    public partial class AddResult : Form
    {
        Collection<PomocnyObjekt5> mojeZapasy;

        public AddResult()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BindData()
        {
            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zap = (ZapasyGateway<Zapasy>)zF.GetZapasy();
            mojeZapasy = zap.SelectBookmakersUnfinishedMatches(Autorization.Instance.getId());
            BindingList<PomocnyObjekt5> bindingList = new BindingList<PomocnyObjekt5>(mojeZapasy);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingList;
        }

        private void AddResult_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            SetReesult sr = new SetReesult();
            int idZapasu = GetSelected().ID_Zapasu;
            sr.OpenRecord(idZapasu);
            sr.ShowDialog();


            mojeZapasy.Remove(mojeZapasy.Where(x => x.ID_Zapasu == idZapasu).ToList().First());
            BindingList<PomocnyObjekt5> bindingList = new BindingList<PomocnyObjekt5>(mojeZapasy);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingList;
        }


       


        private PomocnyObjekt5 GetSelected()
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                PomocnyObjekt5 z = dataGridView1.SelectedRows[0].DataBoundItem as PomocnyObjekt5;
                return z;
            }

            else
            {
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
