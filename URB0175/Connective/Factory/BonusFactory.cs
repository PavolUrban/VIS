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
    public class BonusFactory
    {
        public BonusGatewayInterface<Bonus> GetBonus()
        {
            if (Configure.BONUSREAD == 0)
            {
                return BonusGateway<Bonus>.Instance;
            }
            
            else
            {
                return BonusGateway<Bonus>.Instance;
                //return ClientXMLGateway<Client>.Instance;
            }
            
        }
    }
}
