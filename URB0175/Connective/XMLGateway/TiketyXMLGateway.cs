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
    public class TiketyXMLGateway<T> : TiketyGatewayInterface<T>
        where T : Tikety
    {

        private static TiketyXMLGateway<Tikety> instance;
        private TiketyXMLGateway() { }

        public static TiketyXMLGateway<Tikety> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TiketyXMLGateway<Tikety>();
                }
                return instance;
            }
        }



        public XElement Insert(Tikety tikety)
        {
            XElement result = new XElement("Tiket",
                new XAttribute("IdTiketu", tikety.RecordId),
                new XAttribute("KodTiketu", tikety.kod_tiketu),
                new XAttribute("IdTipera", tikety.id_tipera),
                new XAttribute("CelkovyKurz", (tikety.celkovy_kurz == null) ? "null" : tikety.celkovy_kurz.ToString()),
                new XAttribute("Vklad", tikety.vklad),
                new XAttribute("CelkovaVyhra", (tikety.celkova_vyhra == null) ? "null" : tikety.celkova_vyhra.ToString()),
                new XAttribute("UspesnostTiketu", (tikety.uspesnost_tiketu == null) ? "null" : tikety.uspesnost_tiketu.ToString()));


            return result;
        }




        Collection<T> Gateway<T>.Select()
        {
            XDocument xDoc = XDocument.Load(Paths.FilePath);
            Collection<T> tikety= new Collection<T>();

            List<XElement> elements = xDoc.Descendants("Tikety").Descendants("Tiket").ToList();
            foreach (var element in elements)
            {
                var kurzC = 0.0;
                var vyhraC = 0.0;
                var id1 = 0;
                var id2 = 0;
                var vklad = 0;
                bool uspesnost;
                Tikety tiket = new Tikety ()
                { 
                    RecordId = int.TryParse(element.Attribute("IdTiketu").Value, out id1) == true ? id1 : 0,
                    kod_tiketu = element.Attribute("KodTiketu").Value,
                    id_tipera = int.TryParse(element.Attribute("IdTipera").Value, out id2) == true ? id2 : 0,
                    celkovy_kurz = double.TryParse(element.Attribute("CelkovyKurz").Value, out kurzC) == true ? kurzC : 0.0,
                    vklad = int.TryParse(element.Attribute("Vklad").Value, out vklad) == true ? vklad : 0,
                    celkova_vyhra = double.TryParse(element.Attribute("CelkovaVyhra").Value, out vyhraC) == true ? vyhraC : 0.0,
                    uspesnost_tiketu = bool.TryParse(element.Attribute("Uspesnost").Value, out uspesnost) == true ? uspesnost : true

                };

                if (tiket.celkovy_kurz == 0.0)
                {
                    tiket.celkovy_kurz = null;
                }

                if (tiket.celkova_vyhra == 0.0)
                {
                    tiket.celkova_vyhra = null;
                }



                tikety.Add((T)tiket);
            }

            return tikety;
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
