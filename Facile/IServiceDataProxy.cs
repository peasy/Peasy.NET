using Facile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facile
{
    public interface IServiceDataProxy<T, TKey> : IDataProxy<T, TKey>, 
                                                  ITransactionSupportStatusProvider, 
                                                  ILatencyProneStatusProvider
    {
    }
}
