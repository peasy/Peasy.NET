using Orders.com.DataProxy;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.Http
{
    public class CustomersHttpServiceProxy : HttpServiceProxyBase<Customer, long>, ICustomerDataProxy
    {
        protected override string RequestUri
        {
            get { return "http://localhost:53534/api/customers"; }
        }
    }
}
