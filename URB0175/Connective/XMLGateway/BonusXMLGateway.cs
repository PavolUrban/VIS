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
    public class BonusXMLGateway<T> : BonusGatewayInterface<T>
        where T : Bonus
    {

        private static BonusXMLGateway<Bonus> instance;
        private BonusXMLGateway() { }

        public static BonusXMLGateway<Bonus> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BonusXMLGateway<Bonus>();
                }
                return instance;
            }
        }



        public XElement Insert(Bonus bonus)
        {
            XElement result = new XElement("Bonus",
                new XAttribute("IdBonusu", bonus.RecordId),
                new XAttribute("NazovBonusu", bonus.nazov_bonusu),
                new XAttribute("PopisBonusu", bonus.popis_bonusu));


            return result;
        }





        Collection<T> Gateway<T>.Select()
        {
            XDocument xDoc = XDocument.Load(Paths.FilePath); //este nie je cesta
            Collection<T> bonusy = new Collection<T>();

            List<XElement> elements = xDoc.Descendants("Bonusy").Descendants("Bonus").ToList();
            foreach (var element in elements)
            {
                var id = 0;
                Bonus bonus = new Bonus()
                {
                    RecordId = int.TryParse(element.Attribute("IdBonusu").Value, out id) == true ? id : 0,
                    nazov_bonusu = element.Attribute("NazovBonusu").Value,
                    popis_bonusu = element.Attribute("PopisBonusu").Value
                };



                bonusy.Add((T)bonus);
            }

            return bonusy;
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
