using Connective;
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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("som na zaciaatku");
            if (AutorizaciaTiper.Instance.GetCurrentTiper() != null)
            {
                AutorizaciaTiper.Instance.SetCurrentTiper(null);
            }
        }

        protected void confirmButton_Click(object sender, EventArgs e)
        {

           

            TiperFactory tF = new TiperFactory();
            TiperGateway<Tiper> tg = (TiperGateway<Tiper>)tF.GetTiper();

            Collection<Tiper> tiperi = tg.Select();

            DateTime value = new DateTime(2017, 1, 18);
            string email = String.Format("{0}", Request.Form["loginInput"]); ;
            string heslo = String.Format("{0}", Request.Form["passwordInput"]);
            Tiper tiper = new Tiper(0,"meno","priezvisko",value, email,value, "pohlavie", 24.55,heslo);


            


            if (Functions.IsValidEmail(email))
            {
                if (heslo.Length > 4)
                {
                    if (tg.CheckPassword(tiper))
                    {
                        foreach (Tiper tp in tiperi)
                        {
                            if (tp.email.IndexOf(email) > -1 && tp.heslo.IndexOf(heslo) > -1 && tp.email.Length == email.Length && tp.heslo.Length == heslo.Length)
                            {
                              
                                AutorizaciaTiper.Instance.SetCurrentTiper(tp);
                                System.Diagnostics.Debug.WriteLine("moje id "+ AutorizaciaTiper.Instance.getIdTiper());
                                Response.Redirect("~/Home.aspx");
                            }
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No mail was entered');window.location ='Login.aspx';", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password too short.');window.location ='Login.aspx';", true);
                }



            }


            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid mail.);window.location ='Login.aspx';", true);
            }






        }
    }
}