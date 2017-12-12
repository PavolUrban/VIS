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

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        string[] tipy = new string[] { "1", "0", "2" };

        protected void Page_Load(object sender, EventArgs e)
        {
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



            int tip = int.Parse(DropDownList2.Text);
            ZapasyNaTikete znt = new ZapasyNaTikete(idTiketu, 5, tip);

            ZapasyNaTiketeFactory zntF = new ZapasyNaTiketeFactory();
            ZapasyNaTiketeGateway<ZapasyNaTikete> zntG = (ZapasyNaTiketeGateway<ZapasyNaTikete>)zntF.GetZapasyNaTikete();

            zntG.Insert(znt);




        }

      
    }
}