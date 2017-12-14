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
    public partial class Vysledky : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ZapasyFactory zF = new ZapasyFactory();
            ZapasyGateway<Zapasy> zg = (ZapasyGateway<Zapasy>)zF.GetZapasy();

            Collection<VysledkyZapasov> zapasy = zg.SelectVysledky(); //tu potrebujeme ukoncene

            VysledkyZapasov.DataSource = zapasy;
            VysledkyZapasov.DataBind();
        }
    }
}