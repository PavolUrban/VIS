using Connective.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Tables
{
    public class Bookmaker : DBRecord
    {
        public override int RecordId { get; set; }
        public string meno { get; set; }
        public string priezvisko { get; set; }
        public DateTime datum_narodenia { get; set; }
        public string email { get; set; }
        public DateTime datum_nastupu_do_prace { get; set; }
        public string pohlavie { get; set; }
        public string heslo { get; set; }


        public Bookmaker()
        {

        }

        public Bookmaker(int RecordId, string email, string heslo)
        {
            this.email = email;
            this.heslo = heslo;
            this.RecordId = RecordId;
        }


        public Bookmaker(int RecordId, string meno, string priezvisko, DateTime narod, string email, DateTime nastup, string pohlavie, string heslo)
        {
            this.RecordId = RecordId;
            this.meno = meno;
            this.priezvisko = priezvisko;
            this.datum_narodenia = narod;
            this.email = email;
            this.datum_nastupu_do_prace = nastup;
            this.pohlavie = pohlavie;
            this.heslo = heslo;
            
        }
    }
}
