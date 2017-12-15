using Connective.Abstract.Interface;
using Connective.Tables;
using Connective.TablesGateway;
using Connective.XMLGateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Factory
{
    public class SportFactory
    {
        public SportGatewayInterface<Sport> GetSport()
        {
            if (Configure.SPORTREAD == 0)
            {
                return SportGateway<Sport>.Instance;
            }
            else
            {
                return SportXMLGateway<Sport>.Instance;
                //return DiscountXMLGateway<DiscountCard>.Instance;
            }

        }
    }
}
