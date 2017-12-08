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
    public class TiperFactory
    {
        public TiperGatewayInterface<Tiper> GetTiper()
        {
            if (Configure.TIPERREAD == 0)
            {
                return TiperGateway<Tiper>.Instance;
            }
            else
            {
                return TiperGateway<Tiper>.Instance;
                //return DiscountXMLGateway<DiscountCard>.Instance;
            }

        }
    }
}
