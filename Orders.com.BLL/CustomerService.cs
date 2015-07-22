using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;

namespace Orders.com.BLL
{
    public class CustomerService : OrdersDotComServiceBase<Customer>
    {
        public CustomerService(ICustomerDataProxy dataProxy) : base(dataProxy)
        {
        }
    }
}
