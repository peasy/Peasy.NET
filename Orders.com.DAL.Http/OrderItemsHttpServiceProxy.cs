using Orders.com.DataProxy;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.com.DAL.Http
{
    public class OrderItemsHttpServiceProxy : HttpServiceProxyBase<OrderItem, long>, IOrderItemDataProxy
    {
        protected override string RequestUri
        {
            get { return "http://localhost:53534/api/orderitems"; }
        }

        public IEnumerable<OrderItem> GetByOrder(long orderID)
        {
            return base.GET<IEnumerable<OrderItem>>($"{RequestUri}?orderid={orderID}");
        }

        public Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID)
        {
            return base.GETAsync<IEnumerable<OrderItem>>($"{RequestUri}?orderid={orderID}");
        }
    }
}
