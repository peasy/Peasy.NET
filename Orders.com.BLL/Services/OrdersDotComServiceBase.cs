using Peasy;
using Peasy.Core;
using Orders.com.DataProxy;

namespace Orders.com.BLL
{
    public abstract class OrdersDotComServiceBase<T> : BusinessServiceBase<T, long> where T : IDomainObject<long>, new()
    {
        public OrdersDotComServiceBase(IOrdersDotComDataProxy<T> dataProxy) : base(dataProxy)
        {
        }
    }
}
