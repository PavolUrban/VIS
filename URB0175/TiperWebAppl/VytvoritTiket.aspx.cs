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
    public partial class VytvoritTiket : System.Web.UI.Page
    {


        string kodTiketu = "";
        double celkovyKurz = 1.0;
        private static Random random = new Random();

        //generator kodu tiketu
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        string[] tipy = new string[] { "1", "0", "2" };




        protected void Page_Load(object sender, EventArgs e)
        {
            int idTipera = AutorizaciaTiper.Instance.getIdTiper();
            TiperFactory tiperF = new TiperFactory();
            TiperGateway<Tiper> tiperG = (TiperGateway<Tiper>)tiperF.GetTiper();
            Tiper prihlaseny = tiperG.Select(idTipera);

            Label5.Text = "Aktuálny stav účtu: " +prihlaseny.stav_uctu;

            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zg = (ZapasyGateway<Zapasy>)zF.GetZapasy();
            Collection<string> matches = new Collection<string>();

            Collection<Zapasy> zapasy = zg.SelectAkteri();

            foreach (Zapasy zapas in zapasy)
            {
                matches.Add(zapas.akteriZapasu);
                
            }



            if (!IsPostBack)
            {
               
                DropDownList1.DataSource = matches;
                DropDownList1.DataBind();

                DropDownList2.DataSource = tipy;
                DropDownList2.DataBind();


                DropDownList3.DataSource = matches;
                DropDownList3.DataBind();

                DropDownList4.DataSource = tipy;
                DropDownList4.DataBind();

                DropDownList5.DataSource = matches;
                DropDownList5.DataBind();

                DropDownList6.DataSource = tipy;
                DropDownList6.DataBind();
            }
            
        }

        protected void vytvorTiket(object sender, EventArgs e)
        {
            TiketyFactory tF = new TiketyFactory();
            TiketyGateway<Tikety> tiket = (TiketyGateway<Tikety>)tF.GetTikety();


            double cislo = 14.4;
            kodTiketu = RandomString(15);
            Tikety t = new Tikety(kodTiketu, AutorizaciaTiper.Instance.getIdTiper(), cislo, cislo, cislo, null);
           


            tiket.Insert(t);
            pridatZapas();
        }

        protected void pridatZapas()
        {

            int idTiketu = getIdTiketu();

            int tip1 = int.Parse(DropDownList2.Text);
            int tip2 = int.Parse(DropDownList4.Text);
            int tip3 = int.Parse(DropDownList6.Text);

            ZapasyNaTikete znt1 = new ZapasyNaTikete(idTiketu, getidZapasu(DropDownList1), tip1);
            ZapasyNaTikete znt2 = new ZapasyNaTikete(idTiketu, getidZapasu(DropDownList3), tip2);
            ZapasyNaTikete znt3 = new ZapasyNaTikete(idTiketu, getidZapasu(DropDownList5), tip3);

            ZapasyNaTiketeFactory zntF = new ZapasyNaTiketeFactory();
            ZapasyNaTiketeGateway<ZapasyNaTikete> zntG = (ZapasyNaTiketeGateway<ZapasyNaTikete>)zntF.GetZapasyNaTikete();

            zntG.Insert(znt1);
            zntG.Insert(znt2);
            zntG.Insert(znt3);

            
            celkovyKurz = celkovyKurz * getKurz(getidZapasu(DropDownList1),DropDownList2) * getKurz(getidZapasu(DropDownList3), DropDownList4)* getKurz(getidZapasu(DropDownList5), DropDownList6);
           
            setTiketCelkovyKurz();
            

           


        }




        protected int getidZapasu(DropDownList d)
        {
            string akteriZapasu = d.Text;
            string result = akteriZapasu.Substring(0, akteriZapasu.IndexOf("."));
            int id = int.Parse(result);
            return id;
        }


        protected int getIdTiketu()
        {
            TiketyFactory tF = new TiketyFactory();
            TiketyGateway<Tikety> tG = (TiketyGateway<Tikety>)tF.GetTikety();

            int idTiketu = 0;

            Collection<Tikety> tikety = tG.Select();

            foreach (Tikety t in tikety)
            {
                if (t.kod_tiketu.IndexOf(kodTiketu) > -1 && t.kod_tiketu.Length == kodTiketu.Length)
                {

                    idTiketu = t.RecordId;
                    System.Diagnostics.Debug.WriteLine("Toto je id tiketu" + idTiketu);
                }
            }

            return idTiketu;
        }



        protected void setTiketCelkovyKurz()
        {
            TiketyFactory tF = new TiketyFactory();
            TiketyGateway<Tikety> tG = (TiketyGateway<Tikety>)tF.GetTikety();

            int idTiketu = getIdTiketu();
            //select podla id
            Tikety myTiket = tG.Select(idTiketu);
            myTiket.celkovy_kurz = celkovyKurz;//tu dopis kurz
            System.Diagnostics.Debug.WriteLine("Toto je id tiketu" + myTiket.RecordId +" a toto jeho celkovy kurz "+myTiket.celkovy_kurz);
            //nastavovanie vkladu

            if (TextBox1.Text == "")
            {
                ErrorTrap("Zadajte prosim vklad.");
                myRollback();
            }

            else
            {
                myTiket.vklad = double.Parse(TextBox1.Text);
                myTiket.celkova_vyhra = myTiket.celkovy_kurz * myTiket.vklad;
                odcitatVklad(); //odcitanie vkladu tiperovi
                tG.Update(myTiket);
            }
            
           
        }


        protected void odcitatVklad()
        {


            int idTipera = AutorizaciaTiper.Instance.getIdTiper();
            TiperFactory tipF = new TiperFactory();
            TiperGateway<Tiper> tipG = (TiperGateway<Tiper>)tipF.GetTiper();


            Tiper t = tipG.Select(idTipera);
            double vklad = double.Parse(TextBox1.Text);
            if (t.stav_uctu >= vklad)
            {
                t.stav_uctu = t.stav_uctu - double.Parse(TextBox1.Text);
                tipG.Update(t);

                Label3.Text = "Celkový kurz: " + celkovyKurz.ToString();
                double vyhra = vklad * celkovyKurz;
                Label4.Text = "Možná výhra: " + vyhra.ToString();
                Label5.Text = "Aktuálny stav účtu: " + t.stav_uctu;
                ErrorTrap("Tiket bol úspešne vytvorený! Môžete pokračovať v tipovaní.");
            }

            else
            {
                ErrorTrap("Nemáte dostatok financí, prosím zmeňte vklad. Maximálne môžete vložiť "+ t.stav_uctu);
                myRollback(); //zmazanie predom vytvoreneho tiektu + zapasov na nom
            }
               

        }


        public void ErrorTrap(string str)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert" + UniqueID,
               "alert('" + str + "');", true);
        }


        //zmazanie v pripade neuspechu
        public void myRollback()
        {
            ZapasyNaTiketeFactory zantF = new ZapasyNaTiketeFactory();
            ZapasyNaTiketeGateway<ZapasyNaTikete> zantG = (ZapasyNaTiketeGateway<ZapasyNaTikete>)zantF.GetZapasyNaTikete();
            zantG.Delete(getIdTiketu());

            TiketyFactory tikF = new TiketyFactory();
            TiketyGateway<Tikety> tikG = (TiketyGateway<Tikety>)tikF.GetTikety();
            tikG.Delete(getIdTiketu());
        }


        protected void UpdateTiket()
        {
            int idTiketu = getIdTiketu();

            TiketyFactory tF = new TiketyFactory();
            TiketyGateway<Tikety> tG = (TiketyGateway<Tikety>)tF.GetTikety();
            //tG.Update()
            

        }


        protected double getKurz(int idZapasu, DropDownList d)
        {
            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zapG = (ZapasyGateway<Zapasy>)zF.GetZapasy();


            double myKurz = 0.0;

            int tip1 = int.Parse(d.Text);
          
            double mykurz = zapG.KurzZapasu(idZapasu, tip1);
          
            

            return mykurz;
        }

        

      

      
    }
}