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
    public class ZapasyNaTiketeFactory
    {

        public ZapasyNaTiketeGatewayInterface<ZapasyNaTikete> GetZapasyNaTikete()
        {
            if (Configure.ZAPASYNATIKETEREAD == 0)
            {
                return ZapasyNaTiketeGateway<ZapasyNaTikete>.Instance;
            }
            else
            {
                return ZapasyNaTiketeGateway<ZapasyNaTikete>.Instance;
                //return DiscountXMLGateway<DiscountCard>.Instance;
            }

        }
    }
}
