using Connective.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Tables
{
    public class Tikety : DBRecord
    {
        public override int RecordId { get; set; }
        public string kod_tiketu { get; set; }
        public int id_tipera { get; set; }
        public double? celkovy_kurz { get; set; }
        public double vklad { get; set; }
        public double? celkova_vyhra { get; set; }
        public bool? uspesnost_tiketu { get; set; }


        public Tikety()
        {

        }

        public Tikety(string kodT, int id, double kurz, double vklad, double vyhra, bool? uspesnost)
        {
            this.kod_tiketu = kodT;
            this.id_tipera = id;
            this.celkovy_kurz = kurz;
            this.vklad = vklad;
            this.celkova_vyhra = vyhra;
            this.uspesnost_tiketu = uspesnost;
        }


    }

   
}
