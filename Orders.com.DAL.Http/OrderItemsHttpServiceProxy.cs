using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.com.DAL.Http
{
    public class OrderItemsHttpServiceProxy : OrdersDotComHttpProxyBase<OrderItem, long>, IOrderItemDataProxy
    {
        protected override string RequestUri
        {
            get { return $"{BaseAddress}/orderitems"; }
        }

        public IEnumerable<OrderItem> GetByOrder(long orderID)
        {
            return base.GET<IEnumerable<OrderItem>>($"{RequestUri}?orderid={orderID}");
        }

        public Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID)
        {
            return base.GETAsync<IEnumerable<OrderItem>>($"{RequestUri}?orderid={orderID}");
        }

        public OrderItem Ship(OrderItem orderItem)
        {
            return base.PUT<OrderItem, OrderItem>(orderItem, $"{RequestUri}/{orderItem.ID}/ship");
        }

        public Task<OrderItem> ShipAsync(OrderItem orderItem)
        {
            return base.PUTAsync<OrderItem, OrderItem>(orderItem, $"{RequestUri}/{orderItem.ID}/ship");
        }

        public OrderItem Submit(OrderItem orderItem)
        {
            return base.PUT<OrderItem, OrderItem>(orderItem, $"{RequestUri}/{orderItem.ID}/submit");
        }

        public Task<OrderItem> SubmitAsync(OrderItem orderItem)
        {
            return base.PUTAsync<OrderItem, OrderItem>(orderItem, $"{RequestUri}/{orderItem.ID}/submit");
        }
    }
}
