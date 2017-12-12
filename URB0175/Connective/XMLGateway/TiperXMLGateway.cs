using Connective.Abstract.Interface;
using Connective.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Connective.XMLGateway
{
    public class TiperXMLGateway<T> : TiperGatewayInterface<T>
        where T : Tiper
    {
        private static TiperXMLGateway<Tiper> instance;
        private TiperXMLGateway() { }

        public static TiperXMLGateway<Tiper> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TiperXMLGateway<Tiper>();
                }
                return instance;
            }
        }



        public XElement Insert(Tiper tiper)
        {
            XElement result = new XElement("Tiper",
                new XAttribute("IdTipera", tiper.RecordId),
                new XAttribute("Meno", tiper.meno),
                new XAttribute("Priezvisko", tiper.priezvisko),
                new XAttribute("DatumNarodenia", tiper.datum_narodenia),
                new XAttribute("Email", tiper.email),
                new XAttribute("DatumRegistracie", tiper.datum_registracie),
                new XAttribute("Pohlavie", tiper.pohlavie),
                new XAttribute("StavUctu", (tiper.stav_uctu == null) ? "null" : tiper.stav_uctu.ToString()),
                new XAttribute("Heslo", (tiper.heslo == null) ? "null" : tiper.stav_uctu.ToString()));



            return result;
        }


        Collection<T> Gateway<T>.Select()
        {
            XDocument xDoc = XDocument.Load(Paths.FilePath); //este nie je cesta
            Collection<T> tiperi = new Collection<T>();

            List<XElement> elements = xDoc.Descendants("Tiperi").Descendants("Tiper").ToList();
            foreach (var element in elements)
            {
                var id = 0;
                var ucet = 0.0;
                DateTime d = new DateTime();
                Tiper tiper = new Tiper()
                {
                    RecordId = int.TryParse(element.Attribute("IdTipera").Value, out id) == true ? id : 0,
                    meno = element.Attribute("Meno").Value,
                    priezvisko = element.Attribute("Priezvisko").Value,
                    datum_narodenia = DateTime.TryParse(element.Attribute("DatumNarodenia").Value, out d) == true ? d : new DateTime(1900, 1, 18),
                    email = element.Attribute("Email").Value,
                    datum_registracie = DateTime.TryParse(element.Attribute("DatumRegistracie").Value, out d) == true ? d : new DateTime(1900, 1, 18),
                    pohlavie = element.Attribute("Pohlavie").Value,
                    stav_uctu = double.TryParse(element.Attribute("StavUctu").Value, out ucet) == true ? ucet : 1.11111111,
                    heslo = element.Attribute("Heslo").Value
                };

                if (tiper.stav_uctu == 1.11111111)
                {
                    tiper.stav_uctu = null;
                }


                tiperi.Add((T)tiper);
            }

            return tiperi;
        }






        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(T t)
        {
            throw new NotImplementedException();
        }

      

        public int Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}
