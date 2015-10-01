using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peasy.Core
{
    public interface IDataProxy<T, TKey> : ISupportCRUD<T, TKey>, 
                                           ISupportCRUDAsync<T, TKey> 
    {
    }
}
