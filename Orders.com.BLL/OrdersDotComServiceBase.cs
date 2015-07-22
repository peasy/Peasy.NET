using Facile;
using Facile.Core;
using Orders.com.Core.DataProxy;

namespace Orders.com.BLL
{
    public abstract class OrdersDotComServiceBase<T> : BusinessServiceBase<T, long> where T : IDomainObject<long>, new()
    {
        public OrdersDotComServiceBase(IOrdersDotComDataProxy<T> dataProxy) : base(dataProxy)
        {
        }
    }
}
