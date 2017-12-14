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
    public partial class TiperoveTikety : System.Web.UI.Page
    {
        public object DataGridViewElementStates { get; private set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {

            TiketyFactory tF = new TiketyFactory();
            TiketyGateway<Tikety> tg = (TiketyGateway<Tikety>)tF.GetTikety();

            int idTipera = AutorizaciaTiper.Instance.getIdTiper();

            Collection<Tikety> tikety = tg.SelectTiperove(idTipera);

            MojTikety.DataSource = tikety;
            MojTikety.DataBind();
        }

        protected void MojTikety_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void MojTikety_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Id tiketu";
                e.Row.Cells[1].Text = "Kód tiketu";
                e.Row.Cells[2].Text = "Id tipéra";
                e.Row.Cells[3].Text = "Celkový kurz";
                e.Row.Cells[4].Text = "Vklad";
                e.Row.Cells[5].Text = "Celková výhra";
                e.Row.Cells[6].Text = "Úspešnosť";

            }
        }
    }
}