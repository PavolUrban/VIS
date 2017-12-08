using Connective.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Tables
{
    public class Sport : DBRecord
    {
        public override int RecordId { get; set; }
        public string nazov_sportu { get; set; }
    }
}
