using Connective.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Tables
{
    public class Zapasy : DBRecord
    {
        public override int RecordId { get; set; }
        public string domaci_tim { get; set; }
        public string hostujuci_tim { get; set; }
        public double kurz_domaci { get; set; }
        public double? kurz_remiza { get; set; }
        public double kurz_hostia { get; set; }
        public int id_sportu { get; set; }
        public int id_bookmakera { get; set; }
        public int? vysledok { get; set; }
        
        public string akteriZapasu { get { return this.RecordId + ". " + this.domaci_tim + " vs. " + this.hostujuci_tim; } }
        
    }
}
