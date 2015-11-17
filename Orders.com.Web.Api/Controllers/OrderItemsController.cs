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

        private IOrderItemService BusinessService
        {
            get { return _businessService as IOrderItemService; }
        }

        [HttpGet]
        /// GET api/orderitems?orderid=123
        public IEnumerable<OrderItem> GetByOrder(long orderID)
        {
            var orderItems = BusinessService.GetByOrderCommand(orderID).Execute().Value;
            return orderItems;
        }

        [HttpPut]
        /// PUT api/orderitems/123/submit
        [Route("api/orderitems/{orderitemid:long}/submit")]
        public OrderItem Submit(long orderItemID)
        {
            var orderItem = BusinessService.SubmitCommand(orderItemID).Execute().Value;
            return orderItem;
        }

        [HttpPut]
        /// PUT api/orderitems/123/ship
        [Route("api/orderitems/{orderitemid:long}/ship")]
        public OrderItem Ship(long orderItemID)
        {
            var orderItem = BusinessService.ShipCommand(orderItemID).Execute().Value;
            return orderItem;
        }
    }
}