using Orders.com.BLL;
using Orders.com.Domain;

namespace Orders.com.Web.Api.Controllers
{
    public class OrderItemsController : ApiControllerBase<OrderItem, long>
    {
        public OrderItemsController(IOrderItemService orderItemService)
        {
            _businessService = orderItemService;
        }
    }
}