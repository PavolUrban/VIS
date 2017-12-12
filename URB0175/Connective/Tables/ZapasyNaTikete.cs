﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Tables
{
    public class ZapasyNaTikete 
    {
        public int Tikety_id_tiketu { get; set; }
        public int Zapasy_id_zapasu { get; set; }
        public int tip { get; set; }


        public ZapasyNaTikete()
        {
        }

        public ZapasyNaTikete(int idTiketu, int idZapasu, int tip) {
            this.Tikety_id_tiketu = idTiketu;
            this.Zapasy_id_zapasu = idZapasu;
            this.tip = tip;
        }
    }
}
