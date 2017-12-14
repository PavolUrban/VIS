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
        string[] uspesnost = new string[] { "všetky","nevyhodnotené", "výherné", "nevýherné" };
        int idTipera = AutorizaciaTiper.Instance.getIdTiper();//prihlaseny tiper


        protected void Page_Load(object sender, EventArgs e)
        {

            TiketyFactory tF = new TiketyFactory();
            TiketyGateway<Tikety> tg = (TiketyGateway<Tikety>)tF.GetTikety();

            Collection<Tikety> tikety = tg.SelectTiperove(idTipera,"všetky");

            MojTikety.DataSource = tikety;
            MojTikety.DataBind();

            if (!IsPostBack)
            {

                DropDownList1.DataSource = uspesnost;
                DropDownList1.DataBind();
            }
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            TiketyFactory tF = new TiketyFactory();
            TiketyGateway<Tikety> tg = (TiketyGateway<Tikety>)tF.GetTikety();

            if (DropDownList1.Text == "výherné")
            {
                Collection<Tikety> tikety = tg.SelectTiperove(idTipera, "vyherne");
                MojTikety.DataSource = tikety;
                MojTikety.DataBind();
            }


            else if (DropDownList1.Text == "nevýherné")
            {
                Collection<Tikety> tikety = tg.SelectTiperove(idTipera, "nevyherne");
                MojTikety.DataSource = tikety;
                MojTikety.DataBind();
            }

            else if(DropDownList1.Text == "nevyhodnotené")
            {
                Collection<Tikety> tikety = tg.SelectTiperove(idTipera, "nevyhodnotene");
                MojTikety.DataSource = tikety;
                MojTikety.DataBind();
            }
        }
    }
}