using Orders.com.BLL;
using Orders.com.Domain;
using System.Collections.Generic;
using System.Web.Http;

namespace Orders.com.Web.Api.Controllers
{
    public class OrderItemsController : ApiControllerBase<OrderItem, long>
    {
        public OrderItemsController(IOrderItemService orderItemService)
        {
            _businessService = orderItemService;
        }

        [HttpGet]
        /// GET api/orderitems?orderid=123
        public IEnumerable<OrderItem> GetByOrder(long orderID)
        {
            var orderItems = (_businessService as IOrderItemService).GetByOrderCommand(orderID).Execute().Value;
            return orderItems;
        }
    }
}