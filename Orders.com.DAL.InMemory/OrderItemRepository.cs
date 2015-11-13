using AutoMapper;
using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.com.DAL.InMemory
{
    public class OrderItemRepository : OrdersDotComMockBase<OrderItem>, IOrderItemDataProxy
    {
        public IEnumerable<OrderItem> GetByOrder(long orderID)
        {
            Debug.WriteLine("Executing EF OrderItem.GetByOrder");
            return Data.Values.Where(i => i.OrderID == orderID)
                              .Select(Mapper.Map<OrderItem, OrderItem>)
                              .ToArray();
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID)
        {
            return GetByOrder(orderID);
        }
    }
}
