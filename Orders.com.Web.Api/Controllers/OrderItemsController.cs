using Orders.com.BLL;
using Orders.com.Domain;
using System.Collections.Generic;
using System.Linq;
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
        public IHttpActionResult Submit(long orderItemID)
        {
            var result = BusinessService.SubmitCommand(orderItemID).Execute();
            if (result.Success)
                return Ok(result.Value);

            return BadRequest(string.Join(",", result.Errors.Select(e => e.ErrorMessage)));
        }

        [HttpPut]
        /// PUT api/orderitems/123/ship
        [Route("api/orderitems/{orderitemid:long}/ship")]
        public IHttpActionResult Ship(long orderItemID)
        {
            var result = BusinessService.ShipCommand(orderItemID).Execute();
            if (result.Success)
                return Ok(result.Value);

            return BadRequest(string.Join(",", result.Errors.Select(e => e.ErrorMessage)));
        }
    }
}