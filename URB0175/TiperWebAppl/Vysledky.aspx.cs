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



            /*
            SportFactory sportF = new SportFactory();
            SportXMLGateway<Sport> sportg = (SportXMLGateway<Sport>)sportF.GetSport();

            Collection<Sport> sporty = sportg.s();
           
            if (!IsPostBack)
            {

                DropDownList1.DataSource = sporty;
                DropDownList1.DataBind();
            }
            */

            //select podla sportov z xml
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
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
    }
}