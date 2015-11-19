using Orders.com.DataProxy;
using Orders.com.Domain;

namespace Orders.com.DAL.Http
{
    public class CustomersHttpServiceProxy : OrdersDotComHttpProxyBase<Customer, long>, ICustomerDataProxy
    {
        protected override string RequestUri
        {
            get { return $"{BaseAddress}/customers"; }
        }
    }
}
