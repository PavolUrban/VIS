using Connective.Abstract.Interface;
using Connective.Tables;
using Connective.TablesGateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Factory
{
    public class TiketyFactory
    {
        public TiketyGatewayInterface<Tikety> GetTikety()
        {
            if (Configure.TIKETYREAD == 0)
            {
                return TiketyGateway<Tikety>.Instance;
            }
            else
            {
                return TiketyGateway<Tikety>.Instance;
                //return DiscountXMLGateway<DiscountCard>.Instance;
            }

        }
    }
}
