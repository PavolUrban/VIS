using Connective.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Abstract.Interface
{
    public interface ZapasyNaTiketeGatewayInterface<T> : Gateway<T>
        where T : ZapasyNaTikete
    {
    }
}
