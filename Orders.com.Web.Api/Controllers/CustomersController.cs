using Orders.com.BLL;
using Orders.com.Domain;

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