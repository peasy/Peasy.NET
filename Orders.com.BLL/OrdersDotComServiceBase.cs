using Facile;
using Facile.Core;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.BLL
{
    public abstract class OrdersDotComServiceBase<T> : BusinessServiceBase<T, long> where T : IDomainObject<long>, new()
    {
        public OrdersDotComServiceBase(IOrdersDotComDataProxy<T> dataProxy) : base(dataProxy)
        {
        }
    }
}
