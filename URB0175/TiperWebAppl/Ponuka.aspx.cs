using Connective.Factory;
using Connective.Tables;
using Connective.TablesGateway;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TiperWebAppl
{
    public partial class Ponuka : System.Web.UI.Page
    {

        

        protected void Page_Load(object sender, EventArgs e)
        {
            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zg = (ZapasyGateway<Zapasy>)zF.GetZapasy();

            Collection<Zapasy> zapasy = zg.Select();

            kurzovaPonuka.DataSource = zapasy;
            kurzovaPonuka.DataBind();
        }

        protected void kurzovaPonuka_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void KurzovaPonuka_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Id zápasu";
                e.Row.Cells[1].Text = "Domáci";
                e.Row.Cells[2].Text = "Hostia";
                e.Row.Cells[3].Text = "Kurz domáci";
                e.Row.Cells[4].Text = "Kurz remíza";
                e.Row.Cells[5].Text = "Kurz hostia";
                e.Row.Cells[6].Text = "Id sportu";
                e.Row.Cells[7].Text = "Id bookmakera";
                e.Row.Cells[8].Text = "Vysledok";
            }
        }

    }
}