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
    }
}
