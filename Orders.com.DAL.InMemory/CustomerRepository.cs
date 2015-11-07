using AutoMapper;
using Orders.com.Core.DataProxy;
using Orders.com.Core.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.DAL.InMemory
{
    public class CustomerRepository : OrdersDotComMockBase<Customer>, ICustomerDataProxy
    {
        protected override IEnumerable<Customer> SeedDataProxy()
        {
            yield return new Customer() { CustomerID = 1, Name = "Jimi Hendrix" };
            yield return new Customer() { CustomerID = 2, Name = "David Gilmour" };
            yield return new Customer() { CustomerID = 3, Name = "James Page" };
        }
    }
}
