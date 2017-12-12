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
    public class BookmakerXMLGateway<T> : BookmakerGatewayInterface<T>
        where T : Bookmaker
    {
        private static BookmakerXMLGateway<Bookmaker> instance;
        private BookmakerXMLGateway() { }

        public static BookmakerXMLGateway<Bookmaker> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookmakerXMLGateway<Bookmaker>();
                }
                return instance;
            }
        }





        public XElement Insert(Bookmaker bookmaker)
        {
            XElement result = new XElement("Bookmaker",
                new XAttribute("IdBookmakera", bookmaker.RecordId),
                new XAttribute("Meno", bookmaker.meno),
                new XAttribute("Priezvisko", bookmaker.priezvisko),
                new XAttribute("DatumNarodenia", bookmaker.datum_narodenia),
                new XAttribute("Email", bookmaker.email),
                new XAttribute("DatumNastupu", bookmaker.datum_nastupu_do_prace),
                new XAttribute("Pohlavie", bookmaker.pohlavie),
               new XAttribute("Heslo", bookmaker.heslo));


            return result;
        }


        Collection<T> Gateway<T>.Select()
        {
            XDocument xDoc = XDocument.Load(Paths.FilePath); //este nie je cesta
            Collection<T> bookmakeri = new Collection<T>();

            List<XElement> elements = xDoc.Descendants("Bookmakeri").Descendants("Bookmaker").ToList();
            foreach (var element in elements)
            {
                var id = 0;
                DateTime d = new DateTime(); 
                Bookmaker bookmaker = new Bookmaker()
                {
                    RecordId = int.TryParse(element.Attribute("IdBookmakera").Value, out id) == true ? id : 0,
                    meno = element.Attribute("Meno").Value,
                    priezvisko = element.Attribute("Priezvisko").Value,
                    datum_narodenia = DateTime.TryParse(element.Attribute("DatumNarodenia").Value, out d) == true ? d : new DateTime(1900, 1, 18),
                    email = element.Attribute("Email").Value,
                    datum_nastupu_do_prace = DateTime.TryParse(element.Attribute("DatumNastupu").Value, out d) == true ? d : new DateTime(1900, 1, 18),
                    pohlavie = element.Attribute("Pohlavie").Value,
                    heslo = element.Attribute("Heslo").Value
                };




                bookmakeri.Add((T)bookmaker);
            }

            return bookmakeri;
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
