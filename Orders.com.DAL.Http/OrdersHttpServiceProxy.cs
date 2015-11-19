using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.com.QueryData;

namespace Orders.com.DAL.Http
{
    public class OrdersHttpServiceProxy : OrdersDotComHttpProxyBase<Order, long>, IOrderDataProxy
    {
        protected override string RequestUri
        {
            get { return $"{BaseAddress}/orders"; }
        }

        public IEnumerable<OrderInfo> GetAll(int start, int pageSize)
        {
            return base.GET<IEnumerable<OrderInfo>>($"{RequestUri}?start={start}&pagesize={pageSize}");
        }

        public Task<IEnumerable<OrderInfo>> GetAllAsync(int start, int pageSize)
        {
            return base.GETAsync<IEnumerable<OrderInfo>>($"{RequestUri}?start={start}&pagesize={pageSize}");
        }

        public IEnumerable<Order> GetByCustomer(long customerID)
        {
            return base.GET<IEnumerable<Order>>($"{RequestUri}?customerid={customerID}");
        }

        public Task<IEnumerable<Order>> GetByCustomerAsync(long customerID)
        {
            return base.GETAsync<IEnumerable<Order>>($"{RequestUri}?customerid={customerID}");
        }

        public IEnumerable<Order> GetByProduct(long productID)
        {
            return base.GET<IEnumerable<Order>>($"{RequestUri}?productid={productID}");
        }

        public Task<IEnumerable<Order>> GetByProductAsync(long productID)
        {
            return base.GETAsync<IEnumerable<Order>>($"{RequestUri}?productid={productID}");
        }
    }
}
