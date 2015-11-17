using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.com.DataProxy
{
    public interface IOrderItemDataProxy : IOrdersDotComDataProxy<OrderItem>
    {
        IEnumerable<OrderItem> GetByOrder(long orderID);
        Task<IEnumerable<OrderItem>> GetByOrderAsync(long orderID);
        OrderItem Submit(OrderItem orderItem);
        Task<OrderItem> SubmitAsync(OrderItem orderItem);
        OrderItem Ship(OrderItem orderItem);
        Task<OrderItem> ShipAsync(OrderItem orderItem);
    }
}
