using Connective.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URB0175
{
    public class Autorization
    {


        private static Autorization instance;
        private Autorization() { }
        private Bookmaker bookmaker = null;

        public static Autorization Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Autorization();
                }
                return instance;
            }
        }



        
        
        public Bookmaker GetCurrentBookmaker() {
            return bookmaker;
        }

        public int getId()
        {
            if (bookmaker != null)
            {
                return bookmaker.RecordId;
            }

            else
                return 0; //co tu??
            
        } 
        
        public void SetCurrentBookmaker(Bookmaker obj) {
            bookmaker = obj;
        }


        public void LogOut()
        {
            bookmaker = null;
        }
    }
}
