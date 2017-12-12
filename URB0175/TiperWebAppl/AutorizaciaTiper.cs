using Connective.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiperWebAppl
{
    public class AutorizaciaTiper
    {



        private static AutorizaciaTiper instance;
        private AutorizaciaTiper() { }
        private Tiper tiper = null;

        public static AutorizaciaTiper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AutorizaciaTiper();
                }
                return instance;
            }
        }





        public Tiper GetCurrentTiper()
        {
            return tiper;
        }

        public int getIdTiper()
        {
            if (tiper != null)
            {
                return tiper.RecordId;
            }

            else
                return 0; //co tu??

        }

        public void SetCurrentTiper(Tiper obj)
        {
            tiper = obj;
        }


        public void LogOut()
        {
            tiper = null;
        }


    }
}