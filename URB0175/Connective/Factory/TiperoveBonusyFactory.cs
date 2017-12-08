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
    public class TiperoveBonusyFactory
    {
        public TiperoveBonusyGatewayInterface<TiperoveBonusy> GetTiperoveBonusy()
        {
            if (Configure.TIPEROVEBONUSYREAD == 0)
            {
                return TiperoveBonusyGateway<TiperoveBonusy>.Instance;
            }
            else
            {
                return TiperoveBonusyGateway<TiperoveBonusy>.Instance;
                //return DiscountXMLGateway<DiscountCard>.Instance;
            }

        }
    }
}
