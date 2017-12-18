using Connective.Factory;
using Connective.Tables;
using Connective.TablesGateway;
using Connective.XMLGateway;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TiperWebAppl
{
    public partial class Vysledky : System.Web.UI.Page
    {
        public object DataGridViewElementStates { get; private set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zg = (ZapasyGateway<Zapasy>)zF.GetZapasy();

            Collection<VysledkyZapasov> zapasy = zg.SelectVysledky(); //tu potrebujeme ukoncene

            VysledkyZapasov.DataSource = zapasy;
            VysledkyZapasov.DataBind();


           
            

            SportFactory sportF = new SportFactory();
            Collection<Sport> sporty = sportF.GetSport().Select();

            Console.WriteLine(sporty);
           
            if (!IsPostBack)
            {

                DropDownList1.DataSource = from i in sporty
                                           select new ListItem()
                                           {
                                               Text = i.nazov_sportu,
                                               Value = i.nazov_sportu
                                           }; 
                DropDownList1.DataBind();
            }
            

            //select podla sportov z xml
        }


        

        protected int GetIndexOfSport()
        {
            int a;

            if (DropDownList1.Text == "futbal")
            {
                a = 1;
                return a;
            }

            else if (DropDownList1.Text == "hokej")
            {
                a = 2;
                return a;
            }

            else if (DropDownList1.Text == "basketbal")
            {
                a = 3;
                return a;
            }

            else if (DropDownList1.Text == "hadzana")
            {
                a = 4;
                return a;
            }

            else if (DropDownList1.Text == "americky futbal")
            {
                a = 5;
                return a;
            }

            else if (DropDownList1.Text == "volejbal")
            {
                a = 6;
                return a;
            }

            else if (DropDownList1.Text == "rugby")
            {
                a = 7;
                return a;
            }

            else
            {
                a = 8;
                return a;
            }
        }

        protected void VysledkyZapasov_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void VysledkyZapasov_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Id zápasu";
                e.Row.Cells[1].Text = "Domáci tím";
                e.Row.Cells[2].Text = "Hosťujúci tím";
                e.Row.Cells[3].Text = "Kurz domáci";
                e.Row.Cells[4].Text = "Kurz remíza";
                e.Row.Cells[5].Text = "Kurz hostia";
                e.Row.Cells[6].Text = "Výsledok";

            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.Write("Som vo funkcii");
            ZapasyFactory zapF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zapG = (ZapasyGateway<Zapasy>)zapF.GetZapasy();

            Collection<VysledkyZapasov> zapasy = zapG.SelectVysledkyPodlaSportu(GetIndexOfSport());
            VysledkyZapasov.DataSource = zapasy;
            VysledkyZapasov.DataBind();

        }
    }
}