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
    public class TiperoveBonusyXMLGateway<T> : TiperoveBonusyGatewayInterface<T>
        where T : TiperoveBonusy
    {

        private static TiperoveBonusyXMLGateway<TiperoveBonusy> instance;
        private TiperoveBonusyXMLGateway() { }

        public static TiperoveBonusyXMLGateway<TiperoveBonusy> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TiperoveBonusyXMLGateway<TiperoveBonusy>();
                }
                return instance;
            }
        }




        public XElement Insert(TiperoveBonusy TiperBonus)
        {
            XElement result = new XElement("TiperBonus",
                new XAttribute("IdTipera", TiperBonus.Tiper_id_tipera),
                new XAttribute("IdBonusu", TiperBonus.Bonus_id_bonusu));


            return result;
        }



        Collection<T> Gateway<T>.Select()
        {
            XDocument xDoc = XDocument.Load(Paths.FilePath); //este nie je cesta
            Collection<T> tiperoveBonusy = new Collection<T>();

            List<XElement> elements = xDoc.Descendants("TiperoveBonusy").Descendants("TiperBonus").ToList();
            foreach (var element in elements)
            {
                var id = 0;
                TiperoveBonusy tb = new TiperoveBonusy()
                {
                    Tiper_id_tipera = int.TryParse(element.Attribute("IdTipera").Value, out id) == true ? id : 0,
                    Bonus_id_bonusu = int.TryParse(element.Attribute("IdBonusu").Value, out id) == true ? id : 0,
                };



                tiperoveBonusy.Add((T)tb);
            }

            return tiperoveBonusy;
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
