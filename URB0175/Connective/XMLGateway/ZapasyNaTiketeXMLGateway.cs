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
    public class ZapasyNaTiketeXMLGateway<T> : ZapasyNaTiketeGatewayInterface<T>
        where T : ZapasyNaTikete
    {

        private static ZapasyNaTiketeXMLGateway<ZapasyNaTikete> instance;
        private ZapasyNaTiketeXMLGateway() { }

        public static ZapasyNaTiketeXMLGateway<ZapasyNaTikete> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ZapasyNaTiketeXMLGateway<ZapasyNaTikete>();
                }
                return instance;
            }
        }





        public XElement Insert(ZapasyNaTikete znt)
        {
            XElement result = new XElement("ZapasNaTikete",
                new XAttribute("IdTiketu", znt.Tikety_id_tiketu),
                new XAttribute("IdZapasu", znt.Zapasy_id_zapasu),
                new XAttribute("Tip", znt.tip));


            return result;
        }


        Collection<T> Gateway<T>.Select()
        {
            XDocument xDoc = XDocument.Load(Paths.FilePath); //este nie je cesta
            Collection<T> zapasynatikete = new Collection<T>();

            List<XElement> elements = xDoc.Descendants("ZapasyNaTikete").Descendants("ZapasNaTikete").ToList();
            foreach (var element in elements)
            {
                var id = 0;
                ZapasyNaTikete znt = new ZapasyNaTikete()
                {
                    Tikety_id_tiketu = int.TryParse(element.Attribute("IdTiketu").Value, out id) == true ? id : 0,
                    Zapasy_id_zapasu = int.TryParse(element.Attribute("IdZapasu").Value, out id) == true ? id : 0,
                    tip = int.TryParse(element.Attribute("Tip").Value, out id) == true ? id : 0
                };



                zapasynatikete.Add((T)znt);
            }

            return zapasynatikete;
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
