using Orders.com.BLL;
using Orders.com.Domain;

namespace Orders.com.Web.Api.Controllers
{
    public class OrdersController : ApiControllerBase<Order, long>
    {
        public OrdersController(IOrderService orderService)
        {
            _businessService = orderService;
        }
    }
}
