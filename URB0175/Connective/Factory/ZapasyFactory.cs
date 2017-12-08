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
    public class ZapasyFactory
    {
        public ZapasyGatewayInterface<Zapasy> GetZapasy()
        {
            if (Configure.ZAPASYREAD == 0)
            {
                return ZapasyGateway<Zapasy>.Instance;
            }
            else
            {
                return ZapasyGateway<Zapasy>.Instance;
                //return DiscountXMLGateway<DiscountCard>.Instance;
            }

        }
    }
}
