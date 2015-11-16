using Orders.com.BLL;
using Orders.com.Domain;
using Orders.com.QueryData;
using System.Collections.Generic;
using System.Web.Http;

namespace Orders.com.Web.Api.Controllers
{
    public class OrdersController : ApiControllerBase<Order, long>
    {
        public OrdersController(IOrderService orderService)
        {
            _businessService = orderService;
        }

        [HttpGet]
        /// GET api/orders?start=1&pagesize=10
        public IEnumerable<OrderInfo> GetAll(int start, int pageSize)
        {
            var orderInfos = (_businessService as IOrderService).GetAllCommand(start, pageSize).Execute().Value;
            return orderInfos;
        }

        [HttpGet]
        /// GET api/orders?customerid=123
        public IEnumerable<Order> GetByCustomer(long customerID)
        {
            var orders = (_businessService as IOrderService).GetByCustomerCommand(customerID).Execute().Value;
            return orders;
        }

        [HttpGet]
        /// GET api/orders?productid=123
        public IEnumerable<Order> GetByProduct(long productID)
        {
            var orders = (_businessService as IOrderService).GetByProductCommand(productID).Execute().Value;
            return orders;
        }
    }
}
