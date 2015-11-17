using AutoMapper;
using Orders.com.DataProxy;
using Orders.com.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Orders.com.DAL.InMemory
{
    public class OrderItemRepository : OrdersDotComMockBase<OrderItem>, IOrderItemDataProxy
    {
        public OrderItem Backorder(OrderItem orderItem)
        {
            return Update(orderItem);
        }

        public Task<OrderItem> BackorderAsync(OrderItem orderItem)
        {
            return UpdateAsync(orderItem);
        }

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

        public OrderItem Ship(OrderItem orderItem)
        {
            return Update(orderItem);
        }

        public Task<OrderItem> ShipAsync(OrderItem orderItem)
        {
            return UpdateAsync(orderItem);
        }

        public OrderItem Submit(OrderItem orderItem)
        {
            return Update(orderItem);
        }

        public Task<OrderItem> SubmitAsync(OrderItem orderItem)
        {
            return UpdateAsync(orderItem);
        }
    }
}
