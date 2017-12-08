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
    }
}
