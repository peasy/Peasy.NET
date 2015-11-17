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

        [HttpPut]
        public OrderItem Submit(OrderItem item)
        {
            var orderItem = (_businessService as IOrderItemService).SubmitCommand(item.ID).Execute().Value;
            return orderItem;
        }

        [HttpPut]
        public OrderItem Ship(OrderItem item)
        {
            var orderItem = (_businessService as IOrderItemService).ShipCommand(item.ID).Execute().Value;
            return orderItem;
        }

        //[HttpPut]
        //public OrderItem Backorder(OrderItem item)
        //{
        //    var orderItem = (_businessService as IOrderItemService).back(item.ID).Execute().Value;
        //    return orderItem;
        //}
    }
}