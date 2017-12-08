using Connective.Conn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connective.Abstract.Interface
{
    public interface Gateway<T>
        where T : class
    {
        int Insert(T t);
        int Update(T t);
        Collection<T> Select();
        int Delete(int id);
    }
}
