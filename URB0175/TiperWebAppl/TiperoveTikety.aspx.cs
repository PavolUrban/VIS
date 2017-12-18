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

       



        public void ErrorTrap(string str)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert" + UniqueID,
               "alert('" + str + "');", true);
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            TiketyFactory tF = new TiketyFactory();
            TiketyGateway<Tikety> tg = (TiketyGateway<Tikety>)tF.GetTikety();

            TiperFactory tiperF = new TiperFactory();
            TiperGateway<Tiper> tiperg = (TiperGateway<Tiper>)tiperF.GetTiper();

            ZapasyNaTiketeFactory zntF = new ZapasyNaTiketeFactory();
            ZapasyNaTiketeGateway<ZapasyNaTikete> zntG = (ZapasyNaTiketeGateway<ZapasyNaTikete>)zntF.GetZapasyNaTikete();

            ZapasyFactory zapF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zapG = (ZapasyGateway<Zapasy>)zapF.GetZapasy();

            Collection<Tikety> tikety = tg.SelectTiperove(idTipera, "nevyhodnotene");
            if (tikety.Count == 0)
            {
                ErrorTrap("Nemáte žiadne tikety na vyhodnotenie");
            }


            else
            { 
            foreach (Tikety t in tikety)
            {
                int pocetZapasovNaTikete = 0;
                int uspesneZapasy = 0;
                int neuspesneZapasy = 0;
                int nevyhodnoteneZapasy = 0;



                int tiketID = t.RecordId;
                Collection<TiperoveZapasyNaTikete> tiperoveZapasy = zntG.SelectTiperoveZapasy(tiketID);
                pocetZapasovNaTikete = tiperoveZapasy.Count;




                System.Diagnostics.Debug.WriteLine("Toto je id tiketu: " + tiketID);
                foreach (TiperoveZapasyNaTikete moje in tiperoveZapasy)
                {
                    int tip = moje.tip;
                    int vysledokZapasu = zapG.VysledokZapasu(moje.idZapasu);

                    if (vysledokZapasu == 100)
                    {
                        ++nevyhodnoteneZapasy;
                        System.Diagnostics.Debug.WriteLine("Pocet nevyhodnotenych " + nevyhodnoteneZapasy);
                        System.Diagnostics.Debug.WriteLine(moje.idZapasu + " Zapas este nie je vyhodnoteny");
                    }

                    else
                    {
                        if (tip == vysledokZapasu)
                        {
                            ++uspesneZapasy;
                            System.Diagnostics.Debug.WriteLine("Pocet uspesnych " + uspesneZapasy);
                            System.Diagnostics.Debug.WriteLine(moje.idZapasu + " Tip uspesny");
                        }
                        else
                        {
                            ++neuspesneZapasy;
                            System.Diagnostics.Debug.WriteLine("Pocet neuspesnych " + neuspesneZapasy);
                            System.Diagnostics.Debug.WriteLine(moje.idZapasu + "Tip neuspesny");
                        }
                    }
                }


                if (pocetZapasovNaTikete == uspesneZapasy)
                {
                    ErrorTrap("Gratulujeme, tiket: " + t.RecordId + " je výherný!");
                    t.uspesnost_tiketu = true;
                    tg.Update(t);
                    double? vyhra = t.celkova_vyhra;
                    Tiper tipp = tiperg.Select(idTipera);
                    tipp.stav_uctu = tipp.stav_uctu + vyhra;
                    tiperg.Update(tipp);

                }

                else if (neuspesneZapasy > 0)
                {
                    ErrorTrap("Ľutujeme, tiket: " + t.RecordId + " je nevýherný.");
                    t.uspesnost_tiketu = false;
                    tg.Update(t);
                }

                else
                {
                    ErrorTrap("Tiket: " + t.RecordId + " zatiaľ nie je vyhodnotený.");
                }
            }
        }




        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
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

            else if (DropDownList1.Text == "nevyhodnotené")
            {
                Collection<Tikety> tikety = tg.SelectTiperove(idTipera, "nevyhodnotene");
                MojTikety.DataSource = tikety;
                MojTikety.DataBind();
            }

            //else vsetky
        }
    }
}