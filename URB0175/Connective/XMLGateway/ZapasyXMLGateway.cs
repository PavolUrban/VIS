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
    public class ZapasyXMLGateway<T> : ZapasyGatewayInterface<T>
        where T : Zapasy
    {

        private static ZapasyXMLGateway<Zapasy> instance;
        private ZapasyXMLGateway() { }

        public static ZapasyXMLGateway<Zapasy> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ZapasyXMLGateway<Zapasy>();
                }
                return instance;
            }
        }





        public XElement Insert(Zapasy zapasy)
        {
            XElement result = new XElement("Zapas",
                new XAttribute("Id", zapasy.RecordId),
                new XAttribute("DomaciTim", zapasy.domaci_tim),
                new XAttribute("HostujuciTim", zapasy.hostujuci_tim),
                new XAttribute("KurzDomaci", zapasy.kurz_domaci),
                new XAttribute("KurzRemiza", (zapasy.kurz_remiza == null) ? "null" : zapasy.kurz_remiza.ToString()),
                new XAttribute("KurzHostia", zapasy.kurz_hostia),
                new XAttribute("IdSportu", zapasy.id_sportu),
                new XAttribute("IdBookmakera", zapasy.id_bookmakera),
                new XAttribute("Vysledok", (zapasy.vysledok == null) ? "null" : zapasy.vysledok.ToString()));

            return result;
        }


        Collection<T> Gateway<T>.Select()
        {
            XDocument xDoc = XDocument.Load(Paths.FilePath); 
            Collection<T> zapasy = new Collection<T>();

            List<XElement> elements = xDoc.Descendants("Zapasy").Descendants("Zapas").ToList();
            foreach (var element in elements)
            {
                var kurzD = 0.0;
                var kurzR = 0.0;
                var kurzH = 0.0;
                var id1 = 0;
                var id2 = 0;
                var vysledok1 = 0;
                Zapasy zapas = new Zapasy()
                { //spytat sa preco tu nemoze byt id
                    domaci_tim = element.Attribute("DomaciTim").Value,
                    hostujuci_tim = element.Attribute("HostujuciTim").Value,
                    kurz_domaci= double.TryParse(element.Attribute("KurzDomaci").Value, out kurzD) == true ? kurzD : 0.0,
                    kurz_remiza = double.TryParse(element.Attribute("KurzRemiza").Value, out kurzR) == true ? kurzR : 0.0, //spytat sa preco tu musi byt out
                    kurz_hostia = double.TryParse(element.Attribute("KurzHostia").Value, out kurzH) == true ? kurzH : 0.0,
                    id_sportu = int.TryParse(element.Attribute("IdSportu").Value, out id1) == true ? id1 : 0,
                    id_bookmakera = int.TryParse(element.Attribute("IdBookmakera").Value, out id2) == true ? id2 : 0,
                    vysledok = int.TryParse(element.Attribute("Vysledok").Value, out vysledok1) == true ? vysledok1 : 0
                   
                };

                if (zapas.kurz_remiza == 0.0)
                {
                    zapas.kurz_remiza = null;
                }

                if (zapas.vysledok == 0)
                {
                    zapas.vysledok = null;
                }

                zapasy.Add((T)zapas);
            }

            return zapasy;
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
