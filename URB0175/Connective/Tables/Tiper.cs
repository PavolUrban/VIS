using Connective.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Tables
{
    public class Tiper : DBRecord
    {
        public override int RecordId { get; set; }
        public string meno { get; set; }
        public string priezvisko { get; set; }
        public DateTime datum_narodenia { get; set; }
        public string email { get; set; }
        public DateTime datum_registracie { get; set; }
        public string pohlavie { get; set; }
        public double? stav_uctu { get; set; }
        public string heslo { get; set; }


        public Tiper()
        {

        }


        public Tiper(int RecordId, string meno, string priezvisko, DateTime narod, string email, DateTime registracia, string pohlavie, double stavUctu, string heslo)
        {
            this.RecordId = RecordId;
            this.meno = meno;
            this.priezvisko = priezvisko;
            this.datum_narodenia = narod;
            this.email = email;
            this.datum_registracie = registracia;
            this.pohlavie = pohlavie;
            this.stav_uctu = stavUctu;
            this.heslo = heslo;

        }

    }
}
