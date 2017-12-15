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
    public class SportXMLGateway<T> : SportGatewayInterface<T>
        where T : Sport
    {

        private static SportXMLGateway<Sport> instance;
        private SportXMLGateway() { }

        public static SportXMLGateway<Sport> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SportXMLGateway<Sport>();
                }
                return instance;
            }
        }

       

        public XElement Insert(Sport sport)
        {
            XElement result = new XElement("Sport",
                new XAttribute("IdSportu", sport.RecordId),
                new XAttribute("NazovSportu", sport.nazov_sportu));
               

            return result;
        }




        Collection<T> Gateway<T>.Select()
        {
            XDocument xDoc = XDocument.Load(Paths.FilePath); //este nie je cesta
            Collection<T> sporty = new Collection<T>();

            List<XElement> elements = xDoc.Descendants("Sporty").Descendants("Sport").ToList();
            foreach (var element in elements)
            {
                var id = 0;
                Sport sport = new Sport()
                { 
                    RecordId = int.TryParse(element.Attribute("IdSportu").Value, out id) == true ? id : 0,
                    nazov_sportu= element.Attribute("NazovSportu").Value,
                };

              

                sporty.Add((T)sport);
            }

            return sporty;
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
