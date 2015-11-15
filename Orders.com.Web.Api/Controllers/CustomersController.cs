using Orders.com.BLL;
using Orders.com.DAL.InMemory;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.com.Web.Api.Controllers
{
    public class CustomersController : ApiControllerBase<Customer, long>
    {
        public CustomersController(ICustomerService customerService)
        {
            _businessService = customerService;
        }
    }
}